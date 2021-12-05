namespace Business_Decision.Domain.Events;

public class InvoiceDeletedEvent : DomainEvent
{
    public Invoice Invoice { get; }
    
    public InvoiceDeletedEvent(Invoice invoice)
    {
        Invoice = invoice;
    }
}
