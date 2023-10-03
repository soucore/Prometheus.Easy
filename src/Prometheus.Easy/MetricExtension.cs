using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus.Easy.Core;

namespace Prometheus.Easy;

[ExcludeFromCodeCoverage]
public static class MetricExtension
{
    public static ITimer Time(this IHistogram histogram) 
        => histogram.NewTimer();
    public static ITimer Time(this IGauge histogram)
        => histogram.NewTimer();

    public static IServiceCollection AddPrometheusClient(this IServiceCollection services, Action<MetricsSettings, MetricLabel> action)
    {
        var settings = new MetricsSettings();
        var labels = new MetricLabel();
        action.Invoke(settings, labels);
        
        services.AddSingleton<Core.IMetricServer>(sp => new Core.MetricServer(settings, labels));
        return services;
    }
    
    public static IServiceCollection AddPrometheusClient(this IServiceCollection services)
    {
        services.AddPrometheusClient((_, _) => {});
        return services;
    }
    
    public static IHost UsePrometheusClient(this IHost host)
    {
        var service = host.Services.GetRequiredService<Core.IMetricServer>();
        service.Start();

        return host;
    }
}