using ER.Huawei.Replicador.Core.Commands;
using ER.Huawei.Replicador.Core.Events;

namespace ER.Huawei.Replicador.Core.Bus
{
    public interface  IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;

    }
}
