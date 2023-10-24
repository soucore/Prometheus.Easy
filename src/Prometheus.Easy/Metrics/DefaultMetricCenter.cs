namespace Prometheus.Easy.Metrics;

public class DefaultMetricCenter
{
    public RequestMetric Request { get; set; }
    public ExceptionMetric Exception { get; set; }
    public IntegrationMetric Integration { get; set; }
    public ProcessMetric Process { get; set; }
    public ValidationMetric Validation { get; set; }
    public MessageMetric Message { get; set; }
}