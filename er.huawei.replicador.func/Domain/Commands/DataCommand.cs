using ER.Huawei.Replicador.Core.Commands;

namespace MicroRabbit.Banking.Domain.Commands
{
    public abstract class DataCommand : Command
    {
        public bool Success { get; protected set; }
    }
}
