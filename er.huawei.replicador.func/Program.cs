using er.huawei.replicador.func.Application.BussinesLogic;
using er.huawei.replicador.func.Application.Services;
using er.huawei.replicador.func.Data.Repository.Adapters;
using er.huawei.replicador.func.Data.Repository.Interfaces;
using er.huawei.replicador.func.Domain.Commands;
using er.huawei.replicador.func.Domain.Interfaces;
using MediatR;
using MicroRabbit.Banking.Domain.CommandHandlers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ER.Huawei.Replicador.IoC;

var builder = new HostBuilder();

var host = builder
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {

        services.RegisterServices();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<IHuaweiRepository, HuaweiAdapter>();

        services.AddSingleton<IMongoRepository, MongoAdapter>();

        // Register IBrandFactory in the service container
        services.AddSingleton<IBrandFactory, BrandFactory>();
        services.AddSingleton<IGigawattLogic, GigawattLogic>();

        services.AddHttpClient();

        services.AddTransient<IRequestHandler<RealTimeDataCommand, bool>, RealTimeDataCommandHandler>();
    })
    .Build();

host.Run();
