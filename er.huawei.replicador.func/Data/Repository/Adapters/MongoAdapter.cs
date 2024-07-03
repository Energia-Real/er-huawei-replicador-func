using er.huawei.replicador.func.Application.Model.Dto;
using er.huawei.replicador.func.Application.Model.Huawei;
using er.huawei.replicador.func.Data.Repository.Interfaces;
using er.huawei.replicador.func.Domain.Commands;
using ER.Huawei.Replicador.Core.Bus;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace er.huawei.replicador.func.Data.Repository.Adapters;

public class MongoAdapter : IMongoRepository
{

    private readonly MongoClient _MongoClient;
    private IMongoDatabase _database;
    private readonly IEventBus _bus;

    public bool Success { get; private set; }
    public PlantDeviceResultEvent Result { get; private set; }

    public MongoAdapter(IConfiguration configuration, IEventBus bus)
    {
        _MongoClient = new MongoClient("mongodb+srv://cloudservices:PtbsrhOZbWW6qlCT@er-closter.yhbgqe4.mongodb.net/");
        _database = _MongoClient.GetDatabase("er-gigawatt-develop");
        _bus = bus;
    }

    public async Task<List<Device>> GetDeviceDataAsync()
    {
        try
        {
            var collection = _database.GetCollection<Device>("Devices");

            // Crear índice en stationCode si no existe (opcional)
            var indexKeysDefinition = Builders<Device>.IndexKeys.Ascending(x => x.stationCode);
            var indexModel = new CreateIndexModel<Device>(indexKeysDefinition);
            await collection.Indexes.CreateOneAsync(indexModel);

            // Definir los códigos de estación que quieres filtrar
            var stationCodes = new List<string>
            {
            "NE=33761005",
            "NE=33754356",
            "NE=33778453",
            "NE=33723147",
            "NE=33691316"
            };

            // Crear el filtro
            var filter = Builders<Device>.Filter.In(x => x.stationCode, stationCodes);

            // Obtener los registros que cumplen con el filtro
            var resultado = await collection.Find(filter).ToListAsync();

            return resultado;
        }
        catch (Exception ex)
        {
            // Manejar la excepción y devolver un resultado vacío o lanzarla nuevamente según sea necesario
            Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            return new List<Device>();
        }
    }

    public async Task InsertDeviceDataAsync(PlantDeviceResultEvent device)
    {
        try
        {
            var collection = _database.GetCollection<PlantDeviceResultEvent>("RepliRealtimeData");
            device.repliedDateTime = DateTime.Now;

            // Insertar el dispositivo en la colección
            // await collection.InsertOneAsync(device);

            var realTimeDataCommand = new RealTimeDataCommand(
                Success = true,
                Result = device
                );

            _bus.SendCommand(realTimeDataCommand);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar en la base de datos: {ex.Message}");
        }
    }

}
