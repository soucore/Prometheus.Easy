namespace Prometheus.Easy.Core;

public interface IMetricServer
{
    /// <summary>
    ///     Server is Running?
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    ///     Start server
    /// </summary>
    void Start();

    /// <summary>
    ///     Stop server
    /// </summary>
    void Stop();
}