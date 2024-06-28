using er.huawei.replicador.func.Model;
using er.huawei.replicador.func.Model.Dto;
using er.huawei.replicador.func.Model.Huawei;

namespace er.huawei.replicador.func.Data.Repository.Interfaces
{
    public interface IMongoRepository
    {
        Task<List<Device>> GetDeviceDataAsyncByCode(string stationCode);
        Task<List<Device>> GetDeviceDataAsync();
        Task<PlantDeviceResult> GetRepliedDataAsync(RequestModel request);
        Task<List<PlantDto>> GetPlantListAsync();

        Task<List<MonthProjectResume>> GetMonthProjectResumesAsync(RequestModel? requestModel);

        Task<HealtCheckModel> GetHealtCheackAsync(RequestModel request);

        Task InsertDeviceDataAsync(PlantDeviceResult device);
        Task InsertMonthResumeDataAsync(MonthProjectResume resume);
        Task InsertHourResumeDataAsync(HourProjectResume resume);

        Task InsertDayResumeDataAsync(DayProjectResume resume);
        Task InsertHealtCheck(List<HealtCheckModel> model);

        Task DeleteManyFromCollection(string collectionName);
        Task DeleteManyFromCollectionByDate(string collectionName, DateTime date);
    }
}
