namespace Prometheus.Easy.Core;

public sealed class DynamicValues<TChild> where TChild : ChildBase
{
    private readonly Collector<TChild> _collector;
    private readonly Dictionary<string, TChild> _dic = new();

    public DynamicValues(Collector<TChild> collector)
    {
        _collector = collector;
    }

    public TChild? this[params string[] values]
    {
        get
        {
            var dictKey = string.Join(':', values);

            if (string.IsNullOrWhiteSpace(dictKey))
                return default;

            if (_dic.TryGetValue(dictKey, out var value))
                return value;

            _dic[dictKey] = _collector.WithLabels(values);
            return _dic[dictKey];
        }
    }
}