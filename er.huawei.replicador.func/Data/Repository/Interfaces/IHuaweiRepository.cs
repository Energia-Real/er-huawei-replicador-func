using er.huawei.replicador.func.Application.Model;
using er.huawei.replicador.func.Application.Model.Dto;

namespace er.huawei.replicador.func.Data.Repository.Interfaces;

public interface IHuaweiRepository
{
    Task<ResponseModel<string>> GetRealTimeDeviceInfoAsync(FiveMinutesRequest request);
}
