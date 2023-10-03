namespace Prometheus.Easy.Core;
public record MetricsSettings
{
    public bool Enabled { get; set; } = true;
    public int Port { get; set; } = 9096;
    public string? ProcessName { get; set; }
}