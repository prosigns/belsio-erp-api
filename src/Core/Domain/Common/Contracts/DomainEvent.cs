using Belsio.Erp.Shared.Events;

namespace Belsio.Erp.Domain.Common.Contracts;

public abstract class DomainEvent : IEvent
{
    public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}