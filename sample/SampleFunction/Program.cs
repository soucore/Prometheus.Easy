using Microsoft.Extensions.Hosting;
using Prometheus.Easy;
using SampleFunction;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddPrometheusClient()
        .ConfigureMetric(builder =>
        {
            builder.AddLabel("app", "app-example");
            builder.AddMetricCenter<MetricCenter>();
        });
    })
    .Build();

host.UsePrometheusClient();
host.Run();