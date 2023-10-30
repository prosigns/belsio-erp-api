using Belsio.Erp.Shared.Events;

namespace Belsio.Erp.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}