using Prometheus.Easy.Metrics;

namespace SampleFunction;

public class MetricCenter : DefaultMetricCenter
{
    public CustomMetric Custom { get; set; }
}