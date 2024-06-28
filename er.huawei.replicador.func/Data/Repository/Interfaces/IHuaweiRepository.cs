namespace er.huawei.replicador.func.Data.Repository.Interfaces
{
    using er.huawei.replicador.func.Model;
    using er.huawei.replicador.func.Model.Dto;
    using er.huawei.replicador.func.Model.Huawei;

    public interface IHuaweiRepository
    {
        Task<DeviceData> GetDevListMethodAsync(string stationCode);

        Task<ResponseModel<string>> GetRealTimeDeviceInfoAsync(FiveMinutesRequest request);

        Task<ResponseModel<List<HealtCheckModel>>> GetStationHealtCheck(string stationCodes);
        Task<ResponseModel<string>> GetMonthProjectResumeAsync(StationAndCollectTimeRequest request);
        Task<ResponseModel<string>> GetDailyProjectResumeAsync(StationAndCollectTimeRequest request);
        Task<ResponseModel<string>> GetHourlyProjectResumeAsync(StationAndCollectTimeRequest request);
    }
}
