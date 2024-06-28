using er.huawei.replicador.func.Application.Model.Dto;
using er.huawei.replicador.func.Application.Model.Huawei;

namespace er.huawei.replicador.func.Data.Repository.Interfaces;

public interface IMongoRepository
{
    Task<List<Device>> GetDeviceDataAsync();
    Task InsertDeviceDataAsync(PlantDeviceResult device);
}
