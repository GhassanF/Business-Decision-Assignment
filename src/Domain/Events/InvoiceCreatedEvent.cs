namespace Business_Decision.Domain.Events;

public class InvoiceCreatedEvent : DomainEvent
{
    public Invoice Invoice { get; }

    public InvoiceCreatedEvent(Invoice invoice)
    {
        Invoice = invoice;
    }
}
