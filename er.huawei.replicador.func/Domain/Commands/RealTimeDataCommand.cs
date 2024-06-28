using MicroRabbit.Banking.Domain.Commands;

namespace er.huawei.replicador.func.Domain.Commands
{
    public class RealTimeDataCommand : DataCommand
    {
        private bool v;

        public RealTimeDataCommand(bool v)
        {
            this.v = v;
        }
    }
}
