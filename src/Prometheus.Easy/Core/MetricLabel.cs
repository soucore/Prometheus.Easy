namespace Prometheus.Easy.Core;

public class MetricLabel
{
    public MetricLabel()
        => Data = new Dictionary<string, string>();
    public Dictionary<string, string> Data { get; private set; }

    public void Add(string key, string value)
        => Data.Add(key, value);
}