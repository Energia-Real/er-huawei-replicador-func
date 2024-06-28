using ER.Huawei.Replicador.Core.Events;

namespace ER.Huawei.Replicador.Func.Domain.Events
{
    public  class DataCreatedEvent : Event
    {
        public int From { get; set; }
        public int To { get; set; }
        public decimal Amount { get; set; }

        public DataCreatedEvent(int from, int to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
