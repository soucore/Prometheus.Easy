using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public sealed class IntegrationMetric : Metric
{
    private const string TimeoutName = "app_integration_timeout";
    private const string TimeoutDescription = "Integrator processing time";

    private const string ResultName = "app_integration_result";
    private const string ResultDescription = "Integrator result";
    
    private const string RetryName = "app_integration_retry";
    private const string RetryDescription = "Integrator count retry";
    
    private const string CircuitBreakerName = "app_integration_circuit_breaker";
    private const string CircuitBreakerDescription = "Integrator count Circuit Breaker";
    
    private readonly DynamicValues<Gauge.Child> _timeout;
    private readonly DynamicValues<Counter.Child> _result;
    private readonly DynamicValues<Counter.Child> _retry;
    private readonly DynamicValues<Counter.Child> _circuitBreaker;

    public IntegrationMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var timeoutGauge = Prometheus.Metrics.CreateGauge(TimeoutName, TimeoutDescription, "name");
        var result = Prometheus.Metrics.CreateCounter(ResultName, ResultDescription, "name", "code");
        var retry = Prometheus.Metrics.CreateCounter(RetryName, RetryDescription, "name");
        var circuitBreaker = Prometheus.Metrics.CreateCounter(CircuitBreakerName, CircuitBreakerDescription, "name");
        _timeout = new DynamicValues<Gauge.Child>(timeoutGauge);
        _result = new DynamicValues<Counter.Child>(result);
        _retry = new DynamicValues<Counter.Child>(retry);
        _circuitBreaker = new DynamicValues<Counter.Child>(circuitBreaker);
    }

    public IDisposable Time(string name) => _timeout[name]?.Time();
    public void RetryInc(string name, int i = 1) => _retry[name]?.Inc(i);
    public void CircuitBreakerInc(string name, int i = 1) => _circuitBreaker[name]?.Inc(i);
    public void Result(string name, int statusCode) => _result[name, statusCode.ToString()]?.Inc();
}