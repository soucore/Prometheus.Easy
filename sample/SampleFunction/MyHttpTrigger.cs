using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace SampleFunction;

public class MyHttpTrigger
{
    private readonly MetricCenter _metric;

    public MyHttpTrigger(MetricCenter metric)
    {
        _metric = metric;
    }
    
    [Function("MyHttpTrigger")]
    public Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        using var _ = _metric.Process.Time("rightnow");
        
        _metric.Request.Inc(nameof(MyHttpTrigger));
        var logger = executionContext.GetLogger("MyHttpTrigger");
        logger.LogInformation("C# HTTP trigger function processed a request.");
        _metric.Custom.LogCountInc();

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        _metric.Exception.Inc(new NullReferenceException("Null Variable"));

        Thread.Sleep(1000);
        
        return Task.FromResult(response);
    }
}