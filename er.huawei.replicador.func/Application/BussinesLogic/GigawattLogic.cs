using er.huawei.replicador.func.Application.Model.Dto;
using er.huawei.replicador.func.Application.Model.Huawei;
using er.huawei.replicador.func.Data.Repository.Interfaces;
using er.huawei.replicador.func.Domain.Interfaces;
using MoreLinq;
using System.Data;

namespace er.huawei.replicador.func.Application.BussinesLogic;

public class GigawattLogic(IMongoRepository repository, IBrandFactory inverterFactory) : IGigawattLogic
{
    private const double factorEnergia = .438;
    private IMongoRepository _repository = repository;
    private readonly IBrandFactory _inverterFactory = inverterFactory;

    public async Task<bool> ReplicateToMongoDb()
    {
        // obtiene de mongo todas las plantas y sus dispositivos
        var proyects = await _repository.GetDeviceDataAsync();
        if (proyects is null || !proyects.Any())
        {
            return false;
        }

        // replica todos los datos de todos los dispositivos
        var devices = await ReplicateAlldeviceData(proyects);

        // agrupa los dispositivos por proyecto
        foreach (var proyect in proyects.GroupBy(a => a.stationCode))
        {
            var insertIntoMongo = new PlantDeviceResultEvent();
            insertIntoMongo.invertersList = new List<DeviceDataResponse<DeviceInverterDataItem>>();
            insertIntoMongo.metterList = new List<DeviceDataResponse<DeviceMetterDataItem>>();

            // por cada proyecto filtra los dispositivos por tipo para agregarlos a la BD
            foreach (var device in proyect)
            {
                var tosave = devices.invertersList.FirstOrDefault(a => a.devId == device.deviceId);

                insertIntoMongo.brandName = "huawei";
                insertIntoMongo.stationCode = device.stationCode;
                if (tosave is not null)
                {
                    insertIntoMongo.invertersList.Add(tosave);
                }
                else
                {
                    var metter = devices.metterList.FirstOrDefault(a => a.devId == device.deviceId);

                    if (metter is not null)
                    {
                        insertIntoMongo.metterList.Add(metter);
                    }
                }
            }

            await _repository.InsertDeviceDataAsync(insertIntoMongo);
        }

        return true;
    }

    private async Task<PlantDeviceResultEvent> ReplicateAlldeviceData(List<Device> devices)
    {
        // genera la instancia de la marca correspondiente
        var inverterBrand = _inverterFactory.Create("huawei");

        // separa en lista segun el tipo de dispositivo
        var inverters = devices.Where(a => a.devTypeId == 1).ToList();
        var metters = devices.Where(a => a.devTypeId == 17).ToList();

        // lo separa en grupos de 100 ya que el endpoint solo acepta un maximo de 100
        var gruposDe100 = inverters.Batch(100).ToList();

        // instancias de respuesta
        var responseAlldevices = new PlantDeviceResultEvent();
        responseAlldevices.invertersList = new List<DeviceDataResponse<DeviceInverterDataItem>>();
        responseAlldevices.metterList = new List<DeviceDataResponse<DeviceMetterDataItem>>();

        var inverterlist = new List<DeviceDataResponse<DeviceInverterDataItem>>();
        var metterlist = new List<DeviceDataResponse<DeviceMetterDataItem>>();

        // genera el request para replicar los inversores
        foreach (var group in gruposDe100)
        {
            var devIdsList = new List<string>();
            foreach (var item in group)
            {
                devIdsList.Add(item.deviceId.ToString());
            }

            var devIds = string.Join(",", devIdsList);
            var realtimeRequest = new FiveMinutesRequest
            {
                devIds = devIds,
                devTypeId = 1
            };

            // manda el request al brand del dispositivo
            var devicesRealTimeInfo = await inverterBrand.GetRealTimeDeviceInfo(realtimeRequest);
            try
            {
                var inverter = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceFiveMinutesResponse<DeviceInverterDataItem>>(devicesRealTimeInfo.Data);

                inverterlist.AddRange(inverter.data);
            }
            catch (Exception ex)
            {
                Thread.Sleep(300000);
                continue;
            }
        }

        responseAlldevices.invertersList = inverterlist;

        // genera el request para replicar los medidores
        foreach (var device in metters.Batch(100).ToList())
        {
            var devIdsList = new List<string>();
            foreach (var item in device)
            {
                devIdsList.Add(item.deviceId.ToString());
            }

            var devIds = string.Join(",", devIdsList);
            var realtimeRequest = new FiveMinutesRequest
            {
                devIds = devIds,
                devTypeId = 17
            };

            // manda el request al brand del dispositivo
            var devicesRealTimeInfo = await inverterBrand.GetRealTimeDeviceInfo(realtimeRequest);
            try
            {
                var metter = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceFiveMinutesResponse<DeviceMetterDataItem>>(devicesRealTimeInfo.Data);

                metterlist.AddRange(metter.data);
            }
            catch (Exception ex)
            {
                Thread.Sleep(300000);
                continue;
            }
        }

        responseAlldevices.metterList = metterlist;

        return responseAlldevices;
    }

}