namespace Prometheus.Easy.Core;

public class MetricServer : IMetricServer
{
    private readonly KestrelMetricServer _server;
    
    public MetricServer(MetricOptions settings)
    {
        _server = new KestrelMetricServer(port: settings.Port);
    }

    public bool IsRunning { get; private set; }
    public void Start()
    {
        _server.Start();
        IsRunning = true;
    }

    public void Stop()
    {
        if(IsRunning)
            _server.Stop();
        IsRunning = false;
    }
}