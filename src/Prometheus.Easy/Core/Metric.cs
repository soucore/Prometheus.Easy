using System.Diagnostics;

namespace Prometheus.Easy.Core;

public abstract class Metric
{
    private readonly IDictionary<string, string> _defaultLabels;
    private static bool StaticLabel { get; set; }

    protected Metric(IDictionary<string, string> defaultLabels)
    {
        _defaultLabels = defaultLabels;
        BuildLabelApp();
        if (StaticLabel) return;
            
        global::Prometheus.Metrics.DefaultRegistry.SetStaticLabels(new Dictionary<string, string>(_defaultLabels));
        StaticLabel = true;
    }

    private void BuildLabelApp()
    {
        if (_defaultLabels.ContainsKey("app"))
            return;
        var app = Process.GetCurrentProcess().ProcessName;
        _defaultLabels.Add("app", app);
    }
}