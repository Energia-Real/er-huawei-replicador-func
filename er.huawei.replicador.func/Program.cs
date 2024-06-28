using er.huawei.replicador.func.BussinesLogic;
using er.huawei.replicador.func.Services.Interfaces;
using er.huawei.replicador.func.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = new HostBuilder();

var host = builder
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<IBrandFactory, BrandFactory>();
        services.AddSingleton<IGigawattLogic, GigawattLogic>();
    })
    .Build();

host.Run();
