using Prometheus;
using Prometheus.Easy.Core;

namespace SampleFunction;

public class CustomMetric : Metric
{
    public Counter LogCount { get; set; }
    
    public CustomMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        LogCount = Metrics.CreateCounter("app_logcount", "Log Count");
    }

    public void LogCountInc(int i  = 1)
    {
        LogCount.Inc(i);
    }
}