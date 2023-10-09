using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Prometheus.Easy.Core;

namespace Prometheus.Easy;

public static class MetricExtension
{
    public static ITimer Time(this IHistogram histogram) 
        => histogram.NewTimer();
    public static ITimer Time(this IGauge histogram)
        => histogram.NewTimer();

    public static IServiceCollection AddPrometheusClient(this IServiceCollection services, Action<MetricOptions> action)
    {
        var options = new MetricOptions();
        action.Invoke(options);
        services.TryAddSingleton<Core.IMetricServer>(new Core.MetricServer(options));
        services.TryAddSingleton(new Bootstrap(services));
        return services;
    }
    
    public static IServiceCollection AddPrometheusClient(this IServiceCollection services)
    {
        services.AddPrometheusClient(_ => {});
        return services;
    }
    
    public static IServiceCollection ConfigureMetric(this IServiceCollection services, Action<IMetricBuilder> action)
    {
        var configurationMetric = new MetricBuilder();
        action.Invoke(configurationMetric);

        if (services.First(c => c.ServiceType == typeof(Bootstrap)).ImplementationInstance is not Bootstrap metric)
            throw new MetricBuilderException($"{nameof(Bootstrap) } cannot be null");
        
        metric.AddMetricCenter(configurationMetric.Types);
        metric.AddLabel(configurationMetric.Labels);
        return services;
    }
    
    public static IHost UsePrometheusClient(this IHost host)
    {
        host.Services.GetRequiredService<Bootstrap>().Build(host.Services);
        host.Services.GetRequiredService<Core.IMetricServer>().Start();

        return host;
    }
}