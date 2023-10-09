using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public sealed class RequestMetric : Metric
{
    private const string InputName = "app_request_count";
    private const string InputDescription = "Number of incoming requests";
    
    public readonly DynamicValues<Counter.Child> Count;

    public RequestMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var counter = Prometheus.Metrics.CreateCounter(InputName, InputDescription, "name");
        Count = new DynamicValues<Counter.Child>(counter);
    }

    public void Inc(string nameLabel, int i = 1)
    {
        Count[nameLabel]?.Inc(i);
    }   
}