using ER.Huawei.Replicador.Core.Commands;

namespace MicroRabbit.Banking.Domain.Commands
{
    public abstract class DataCommand : Command
    {
        public int From { get; protected set; }
        public int To { get; protected set; }
        public decimal Amount { get; protected set; }
    }
}
