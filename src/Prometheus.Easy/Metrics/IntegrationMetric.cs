using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public class IntegrationMetric : StatisticHandler
{
    private const string TimeoutName = "soucore_integration_timeout";
    private const string TimeoutDescription = "Integrator processing time";

    private readonly DynamicValues<Gauge.Child> _timeout;

    public IntegrationMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var timeoutGauge = Prometheus.Metrics.CreateGauge(TimeoutName, TimeoutDescription, "integrator");
        _timeout = new DynamicValues<Gauge.Child>(timeoutGauge);
    }
    
    public Gauge.Child? Time(string integratorName) => _timeout[integratorName];
}