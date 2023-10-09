using Prometheus.Easy.Metrics;

namespace Prometheus.Easy.Core;

public interface IMetricBuilder
{
    /// <summary>
    /// Adds default fixed labels. The "app" key is reserved for the app name, and can be rewritten.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void AddLabel(string key, string value);
    
    /// <summary>
    /// Add metrics centralizing class to be injected into the container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    void AddMetricCenter<T>() where T : DefaultMetricCenter;
}