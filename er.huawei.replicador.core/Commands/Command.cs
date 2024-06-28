using ER.Huawei.Replicador.Core.Events;

namespace ER.Huawei.Replicador.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
