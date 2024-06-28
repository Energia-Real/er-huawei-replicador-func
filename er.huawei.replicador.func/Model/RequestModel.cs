﻿namespace er.huawei.replicador.func.Model
{
    public class RequestModel
    {
        public string Brand { get; set; } = string.Empty;
        public string PlantCode { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
