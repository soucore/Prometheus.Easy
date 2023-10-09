using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public sealed class ProcessMetric : Metric
{
    private const string InputName = "app_process_duration";
    private const string InputDescription = "Process execution time per request";
    
    private readonly DynamicValues<Gauge.Child> _timeout;
    
    public ProcessMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var timeoutGauge = Prometheus.Metrics.CreateGauge(InputName, InputDescription, "name");
        _timeout = new DynamicValues<Gauge.Child>(timeoutGauge);
    }

    public IDisposable Time(string name) => _timeout[name]?.Time()!;
}