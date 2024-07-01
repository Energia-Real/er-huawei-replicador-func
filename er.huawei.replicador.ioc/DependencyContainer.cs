using ER.Huawei.Replicador.Bus;
using ER.Huawei.Replicador.Core.Bus;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ER.Huawei.Replicador.IoC;

public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddSingleton<IEventBus, RabbitMQBus>(sp => { 
            var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory );
        });


        return services;
    }

}