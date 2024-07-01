using er.huawei.replicador.func.Application.Model.Dto;
using ER.Huawei.Replicador.Core.Events;

namespace ER.Huawei.Replicador.Func.Domain.Events;

public  class DataCreatedEvent : Event
{
    private PlantDeviceResult _plantDeviceResult { get; set; }
    public bool _success { get; set; }
    

    public DataCreatedEvent(bool success, PlantDeviceResult plantDeviceResult)
    {
        _success = success;
        _plantDeviceResult = plantDeviceResult;
    }
}
