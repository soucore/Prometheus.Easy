using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public sealed class ValidationMetric : Metric
{
    private const string Name = "app_validation_error";
    private const string Description = "App validation error count";
    
    public DynamicValues<Counter.Child> Error { get; set; }
    public ValidationMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var validation = Prometheus.Metrics.CreateCounter(Name, Description, "name");
        Error = new DynamicValues<Counter.Child>(validation);
    }

    public void IncError(string name, int i = 1) => Error[name]?.Inc(i);
}