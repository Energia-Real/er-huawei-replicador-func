using er.huawei.replicador.func.Application.Model.Dto;
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

    public async Task<bool> Handle(RealTimeDataCommand request, CancellationToken cancellationToken)
    {
        //logica para publicar el mensaje dentro del event bus rabbitmq
        var plantDeviceResult = request.PlantDeviceResult;

        if ((plantDeviceResult.invertersList == null || !plantDeviceResult.invertersList.Any()) &&
        (plantDeviceResult.metterList == null || !plantDeviceResult.metterList.Any()))
        {
            plantDeviceResult.invertersList = new List<DeviceDataResponse<DeviceInverterDataItem>>();
            plantDeviceResult.metterList = new List<DeviceDataResponse<DeviceMetterDataItem>>();
            _bus.Publish(plantDeviceResult);
            return await Task.FromResult(true);
        }
        bool hasValidInverterData = plantDeviceResult.invertersList != null &&
                               plantDeviceResult.invertersList.Any(i => i.dataItemMap != null && i.dataItemMap.total_cap != null);

        bool hasValidMetterData = plantDeviceResult.metterList != null &&
                                  plantDeviceResult.metterList.Any(m => m.dataItemMap != null && m.dataItemMap.reverse_active_cap != null);

        if (!hasValidInverterData)
        {
            plantDeviceResult.invertersList = new List<DeviceDataResponse<DeviceInverterDataItem>>();
        }
        if (!hasValidMetterData)
        {
            plantDeviceResult.metterList = new List<DeviceDataResponse<DeviceMetterDataItem>>();
        }
        else
        _bus.Publish(plantDeviceResult);
        return await Task.FromResult(true);
    }
}
