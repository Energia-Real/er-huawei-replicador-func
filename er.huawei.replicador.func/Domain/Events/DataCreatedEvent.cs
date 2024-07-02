using er.huawei.replicador.func.Application.Model.Dto;
using ER.Huawei.Replicador.Core.Events;

namespace ER.Huawei.Replicador.Func.Domain.Events;

public  class DataCreatedEvent : Event
{
    public PlantDeviceResult PlantDeviceResult { get; set; }
    public bool Success { get; set; }
    

    public DataCreatedEvent(bool success, PlantDeviceResult plantDeviceResult)
    {
        Success = success;
        PlantDeviceResult = plantDeviceResult;
    }
}
