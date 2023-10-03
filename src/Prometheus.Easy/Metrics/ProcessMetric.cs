using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public class ProcessMetric : StatisticHandler
{
    private const string InputName = "soucore_process_duration";
    private const string InputDescription = "Process execution time per request";
    
    private readonly DynamicValues<Gauge.Child> _timeout;
    
    public ProcessMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var timeoutGauge = Prometheus.Metrics.CreateGauge(InputName, InputDescription, "alias");
        _timeout = new DynamicValues<Gauge.Child>(timeoutGauge);
    }

    public Gauge.Child? Time(string labelAlias) => _timeout[labelAlias];
}