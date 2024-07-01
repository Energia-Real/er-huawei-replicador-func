using er.huawei.replicador.func.Application.Model.Dto;
using MicroRabbit.Banking.Domain.Commands;

namespace er.huawei.replicador.func.Domain.Commands;

public class RealTimeDataCommand : DataCommand
{
    

    public RealTimeDataCommand(bool success, PlantDeviceResult plantDeviceResult)
    {
        Success = success;
        PlantDeviceResult = plantDeviceResult;
    }

    public bool Success { get; }
    public PlantDeviceResult PlantDeviceResult { get; }
}
