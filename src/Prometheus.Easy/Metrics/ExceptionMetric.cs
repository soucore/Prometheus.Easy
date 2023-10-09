using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public sealed class ExceptionMetric : Metric
{

    private const string ExceptionName = "app_exceptions";
    private const string ExceptionDescription = "Exception ocorridas no sistema";

    public readonly DynamicValues<Counter.Child> Error;

    public ExceptionMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var exceptions = Prometheus.Metrics.CreateCounter(ExceptionName, ExceptionDescription, "name");
        Error = new DynamicValues<Counter.Child>(exceptions);
    }

    public void Inc(string name)
    {
        Error[name]?.Inc();
    }
    
    public void Inc(Exception ex)
    {
        var alias = ex.GetType().Name;
        Error[alias]?.Inc();
    }
}