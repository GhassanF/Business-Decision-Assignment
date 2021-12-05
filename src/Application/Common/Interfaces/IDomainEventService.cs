using Business_Decision.Domain.Common;

namespace Business_Decision.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
