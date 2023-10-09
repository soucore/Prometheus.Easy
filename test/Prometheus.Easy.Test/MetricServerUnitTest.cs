using Microsoft.Extensions.DependencyInjection;

namespace Prometheus.Easy.Test;

public class MetricServerUnitTest
{
    private readonly HttpClient _client = new();
    private const int SleepMilliSeconds = 1000;
    private const int Seconds = SleepMilliSeconds / 1000;


    public MetricServerUnitTest()
    {
        _client.BaseAddress = new Uri("http://localhost:9096/");
    }

    [Test, Order(1)]
    public async Task MustReturnMetricNameWhenStandardMetricRegistered()
    {
        //Arrange
        const string nameApp = "name-app";

        //Act
        var services = new ServiceCollection();
        services.AddPrometheusClient();
        services.ConfigureMetric(builder =>
        {
            builder.AddLabel("app", nameApp);
        });
        
        var builder = services.BuildServiceProvider();
        builder.GetRequiredService<Bootstrap>().Build(builder);
        var server = builder.GetRequiredService<Core.IMetricServer>();
        server.Start();

        var response = await _client.GetAsync("/metrics");
        var result = await response.Content.ReadAsStringAsync();

        //assert
        Assert.IsTrue(result.Contains(nameApp));
    }

    [Test, Order(2)]
    public void ShouldReturnTimeAsPrometheusWhenDurationOfHistogramIsExecuted()
    {
        //arrange

        var histogram = global::Prometheus.Metrics.CreateHistogram("name_histogram", "Name description");

        //act
        using var timer = histogram.Time();
        Thread.Sleep(SleepMilliSeconds);
        var seconds = timer.ObserveDuration().TotalSeconds;

        //assert
        Assert.IsTrue(seconds > Seconds);
    }

    [Test, Order(3)]
    public void ShouldReturnTimeAsPrometheusWhenDurationGaugeIsExecuted()
    {
        //arrange
        var gauge = global::Prometheus.Metrics.CreateGauge("name_gauge", "Name description");

        //act
        using var timer = gauge.Time();
        Thread.Sleep(SleepMilliSeconds);
        var seconds = timer.ObserveDuration().TotalSeconds;

        //assert
        Assert.IsTrue(seconds > Seconds);
    }
}