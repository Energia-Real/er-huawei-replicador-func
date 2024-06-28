using ER.Huawei.Replicador.Core.Events;

namespace ER.Huawei.Replicador.Core.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler 
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler { }
}
