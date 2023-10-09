using Prometheus.Easy.Metrics;

namespace Prometheus.Easy.Core;

public class MetricBuilder : IMetricBuilder
{
    public readonly IList<Type> Types = new List<Type>();
    public readonly Dictionary<string, string> Labels = new();
    
    public void AddLabel(string key, string value)
        => Labels.Add(key, value);
    
    public void AddMetricCenter<T>() where T : DefaultMetricCenter
        => Types.Add(typeof(T));
}