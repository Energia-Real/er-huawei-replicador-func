namespace er.huawei.replicador.func.Application.BussinesLogic;

public interface IGigawattLogic
{
    Task<bool> ReplicateToMongoDb();
}
