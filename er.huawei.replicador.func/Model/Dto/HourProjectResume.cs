using MongoDB.Bson;

namespace er.huawei.replicador.func.Model.Dto
{
    public class HourProjectResume
    {
        public ObjectId _id { get; set; }
        public string brandName { get; set; } = "brand";
        public string stationCode { get; set; }
        public DateTime repliedDateTime { get; set; }

        public List<HourResumeResponse> HourResume { get; set; }
    }
}
