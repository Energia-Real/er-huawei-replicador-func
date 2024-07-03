using er.huawei.replicador.func.Domain.Commands;
using ER.Huawei.Replicador.Core.Bus;
using MediatR;


namespace MicroRabbit.Banking.Domain.CommandHandlers;

public class RealTimeDataCommandHandler : IRequestHandler<RealTimeDataCommand, bool>
{
    private readonly IEventBus _bus;

    public RealTimeDataCommandHandler(IEventBus bus)
    {
        _bus = bus;
    }

    public Task<bool> Handle(RealTimeDataCommand request, CancellationToken cancellationToken)
    {
        //logica para publicar el mensaje dentro del event bus rabbitmq

        _bus.Publish(request.PlantDeviceResult);

        return Task.FromResult(true);
    }
}
