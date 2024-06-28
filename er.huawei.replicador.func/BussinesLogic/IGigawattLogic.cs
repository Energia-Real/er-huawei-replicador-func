using er.huawei.replicador.func.Model.Gigawatt;
using er.huawei.replicador.func.Model;
using er.huawei.replicador.func.Model.Huawei;
using er.huawei.replicador.func.Model.Dto;

namespace er.huawei.replicador.func.BussinesLogic;

public interface IGigawattLogic
{
    Task<bool> ReplicateToMongoDb();
}
