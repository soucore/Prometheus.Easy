using Prometheus.Easy.Metrics;

namespace Prometheus.Easy;

public static class Metric
{
    private static IDictionary<string, string> Labels { get; set; } = new Dictionary<string, string>();
    public static void SetMetricsToMemory(IDictionary<string, string> labels)
    {
        Labels = labels;
        Request = new RequestMetric(Labels);
        Integration = new IntegrationMetric(Labels);
        Exception = new ExceptionMetric(Labels);
        Process = new ProcessMetric(Labels);
    }

    private static ProcessMetric? _process;
    public static ProcessMetric Process
    {
        get
        {
            _process ??= new ProcessMetric(Labels);
            return _process;
        }
        private set => _process = value;
    }
    
    
    private static RequestMetric? _request;
    public static RequestMetric Request
    {
        get
        {
            _request ??= new RequestMetric(Labels);
            return _request;
        }
        private set => _request = value;
    }

    private static IntegrationMetric? _integration;
    public static IntegrationMetric Integration
    {
        get
        {
            _integration ??= new IntegrationMetric(Labels);
            return _integration;
        }
        private set => _integration = value;
    }

    private static ExceptionMetric? _exception;
    public static ExceptionMetric Exception
    {
        get
        {
            _exception ??= new ExceptionMetric(Labels);
            return _exception;
        }
        private set => _exception = value;
    }
}