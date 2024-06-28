using er.huawei.replicador.func.Model;
using er.huawei.replicador.func.Model.Dto;
using er.huawei.replicador.func.Model.Gigawatt;
using er.huawei.replicador.func.Model.Huawei;

namespace er.huawei.replicador.func.Services.Interfaces
{
    public interface IBrand
    {
        Task<DeviceData> GetDevicesAsync(string stationCode);
        Task<ResponseModel<SiteResume>> GetSiteDetailByPlantsAsync(string stationCode);
        Task<ResponseModel<string>> GetRealTimeDeviceInfo(FiveMinutesRequest request);

        Task<ResponseModel<List<HealtCheckModel>>> GetStationHealtCheck(string request);

        Task<ResponseModel<string>> GetMonthProjectResume(StationAndCollectTimeRequest request);
        Task<ResponseModel<string>> GetDailyProjectResume(StationAndCollectTimeRequest request);
        Task<ResponseModel<string>> GetHourlyProjectResume(StationAndCollectTimeRequest request);
    }
}
