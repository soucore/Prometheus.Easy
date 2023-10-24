using Prometheus.Easy.Core;

namespace Prometheus.Easy.Metrics;

public class MessageMetric : Metric
{
    private const string InputName = "app_message_input";
    private const string InputDescription = "Number of message received";
    
    private const string OutputName = "app_message_output";
    private const string OutputDescription = "Number of message sent";
    
    private readonly DynamicValues<Counter.Child> _input;
    private readonly DynamicValues<Counter.Child> _output;

    public MessageMetric(IDictionary<string, string> defaultLabels) : base(defaultLabels)
    {
        var input = Prometheus.Metrics.CreateCounter(InputName, InputDescription, "name");
        var output = Prometheus.Metrics.CreateCounter(OutputName, OutputDescription, "name");
        _input = new DynamicValues<Counter.Child>(input);
        _output = new DynamicValues<Counter.Child>(output);
    }

    public void InputInc(string nameLabel, int i = 1) => _input[nameLabel]?.Inc(i);
    public void OutputInc(string nameLabel, int i = 1) => _output[nameLabel]?.Inc(i);
}