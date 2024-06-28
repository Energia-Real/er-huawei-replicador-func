﻿namespace er.huawei.replicador.func.Model.Dto
{
    using MongoDB.Bson;
    public class PlantDeviceResult
    {
        public ObjectId _id { get; set; }
        public string brandName { get; set; } = "brand";
        public string stationCode { get; set; }
        public DateTime repliedDateTime { get; set; }
        public List<DeviceDataResponse<DeviceInverterDataItem>> invertersList { get; set; }
        public List<DeviceDataResponse<DeviceMetterDataItem>> metterList { get; set; }
    }
}
