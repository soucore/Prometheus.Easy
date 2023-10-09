using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Prometheus.Easy.Core;
using Prometheus.Easy.Metrics;

namespace Prometheus.Easy;

public sealed class Bootstrap
{
    private readonly IServiceCollection _services;
    private readonly List<Type> _types = new();
    private IDictionary<string, string> _labels = new Dictionary<string, string>();
    
    public Bootstrap(IServiceCollection services)
    {
        _services = services;
        AddMetricCenter(new List<Type>(1) {typeof(DefaultMetricCenter)});
    }

    public void AddLabel(IDictionary<string, string> labels)
        => _labels = labels;

    public void AddMetricCenter(IList<Type> types)
    {
        _types.AddRange(types);
        foreach (var type in types)
            _services.TryAddSingleton(type);
    }
    
    public void Build(IServiceProvider provider) 
    {
        foreach (var type in _types)
        {
            var metricCenter = provider.GetRequiredService(type);
            var properties = metricCenter.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                if (propertyType.IsSubclassOf(typeof(Metric)))
                {
                    var instance = ActivatorUtilities.CreateInstance(provider, propertyType, _labels);
                    if(instance is null) 
                        throw new MetricBuilderException($"Unable to create instance of {nameof(propertyType)}");
            
                    property.SetValue(metricCenter, instance);
                }
                else
                {
                    throw new MetricBuilderException($"Class {nameof(type)} does not inherit {nameof(Core.Metric)}");
                }
            }
        }
    }
}
