using er.huawei.replicador.func.Application.Model;
using er.huawei.replicador.func.Application.Model.Dto;

namespace er.huawei.replicador.func.Domain.Interfaces;

public interface IBrand
{
    Task<ResponseModel<string>> GetRealTimeDeviceInfo(FiveMinutesRequest request);
}
