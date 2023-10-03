using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public class ExceptionMetric : StatisticHandler
{

    private const string ExceptionName = "soucore_exceptions";
    private const string ExceptionDescription = "Exception ocorridas no sistema";

    public readonly DynamicValues<Counter.Child> Error;

    public ExceptionMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var exceptions = Prometheus.Metrics.CreateCounter(ExceptionName, ExceptionDescription, "error");
        Error = new DynamicValues<Counter.Child>(exceptions);
    }
}