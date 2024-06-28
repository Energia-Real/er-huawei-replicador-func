using MongoDB.Bson;

namespace er.huawei.replicador.func.Model.Dto
{
    public class DayProjectResume
    {
        public ObjectId _id { get; set; }
        public string brandName { get; set; } = "brand";
        public string stationCode { get; set; }
        public DateTime repliedDateTime { get; set; }

        public List<DayResumeResponse> DayResume { get; set; }
    }
}
