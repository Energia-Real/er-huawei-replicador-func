using er.huawei.replicador.func.Application.Model;
using er.huawei.replicador.func.Application.Model.Dto;
using er.huawei.replicador.func.Data.Repository.Interfaces;
using er.huawei.replicador.func.Domain.Interfaces;

namespace er.huawei.replicador.func.Application.Services;

public class HuaweiService : IBrand
{
    private readonly IHuaweiRepository _repository;

    public HuaweiService(IHuaweiRepository huaweiRepository)
    {
        _repository = huaweiRepository;
    }

    public async Task<ResponseModel<string>> GetRealTimeDeviceInfo(FiveMinutesRequest request)
    {
        var response = await _repository.GetRealTimeDeviceInfoAsync(request);

        return new ResponseModel<string> { ErrorMessage = response.ErrorMessage, Success = response.Success, Data = response.Data };
    }
}