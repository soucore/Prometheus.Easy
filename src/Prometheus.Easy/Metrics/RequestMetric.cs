using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public class RequestMetric : StatisticHandler
{
    private const string InputName = "soucore_request_count";
    private const string InputDescription = "Number of incoming requests";
    
    public readonly Counter Count;

    public RequestMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        Count = Prometheus.Metrics.CreateCounter(InputName, InputDescription);
    }
}