using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public sealed class IntegrationMetric : Metric
{
    private const string TimeoutName = "app_integration_timeout";
    private const string TimeoutDescription = "Integrator processing time";

    private const string ResultName = "app_integration_result";
    private const string ResultDescription = "Integrator result";
    
    private readonly DynamicValues<Gauge.Child> _timeout;
    private readonly DynamicValues<Counter.Child> _result;

    public IntegrationMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var timeoutGauge = Prometheus.Metrics.CreateGauge(TimeoutName, TimeoutDescription, "name");
        var result = Prometheus.Metrics.CreateCounter(ResultName, ResultDescription, "name", "code");
        _timeout = new DynamicValues<Gauge.Child>(timeoutGauge);
        _result = new DynamicValues<Counter.Child>(result);
    }

    public IDisposable Time(string name) => _timeout[name]?.Time()!;

    public void Result(string name, int statusCode) => _result[name, statusCode.ToString()]?.Inc();
}