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
        using var _ = _metric.Process.Time(nameof(MyHttpTrigger));
        _metric.Request.Inc(nameof(MyHttpTrigger));
        _metric.Custom.LogCountInc();
        _metric.Integration.Result("rightnow", 200);
        var r = new Random();
        var rInt = r.Next(0, 5);
        if(rInt == 3)
            _metric.Integration.Result("rightnow", 500);
        
        _metric.Validation.IncError("Contract");
        
        var logger = executionContext.GetLogger("MyHttpTrigger");
        logger.LogInformation("C# HTTP trigger function processed a request.");
        _metric.Custom.LogCountInc();

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        _metric.Exception.Inc(new NullReferenceException("Null Variable"));

        using (var timer = _metric.Integration.Time("rightnow"))
        {
            Thread.Sleep(500);
        }
        
        return Task.FromResult(response);
    }
}