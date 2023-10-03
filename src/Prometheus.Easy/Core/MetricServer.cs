using Prometheus;

namespace Prometheus.Easy.Core;

public class MetricServer : IMetricServer
{
    private readonly MetricLabel _labels;
    private readonly KestrelMetricServer _server;

    public MetricServer()
    {
        _labels = new MetricLabel();
        var settings = new MetricsSettings();
        _server = new KestrelMetricServer(port: settings.Port);
    }
    
    public MetricServer(MetricsSettings settings, MetricLabel labels)
    {
        _labels = labels;
        _server = new KestrelMetricServer(port: settings.Port);
    }

    public bool IsRunning { get; private set; }
    public void Start()
    {
        _server.Start();
        Metric.SetMetricsToMemory(_labels.Data);
        IsRunning = true;
    }

    public void Stop()
    {
        if(IsRunning)
            _server.Stop();
        IsRunning = false;
    }
}