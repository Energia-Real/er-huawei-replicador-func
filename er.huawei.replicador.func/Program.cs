using er.huawei.replicador.func.Application.BussinesLogic;
using er.huawei.replicador.func.Application.Services;
using er.huawei.replicador.func.Domain.Commands;
using er.huawei.replicador.func.Domain.Interfaces;
using MediatR;
using MicroRabbit.Banking.Domain.CommandHandlers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder();

var host = builder
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<IBrandFactory, BrandFactory>();
        services.AddSingleton<IGigawattLogic, GigawattLogic>();
        services.AddTransient<IRequestHandler<RealTimeDataCommand, bool>, RealTimeDataCommandHandler>();
    })
    .Build();

host.Run();
