using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public sealed class IntegrationMetric : Metric
{
    private const string TimeoutName = "app_integration_timeout";
    private const string TimeoutDescription = "Integrator processing time";

    private readonly DynamicValues<Gauge.Child> _timeout;

    public IntegrationMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var timeoutGauge = Prometheus.Metrics.CreateGauge(TimeoutName, TimeoutDescription, "name");
        _timeout = new DynamicValues<Gauge.Child>(timeoutGauge);
    }

    public IDisposable Time(string name) => _timeout[name]?.Time()!;
}