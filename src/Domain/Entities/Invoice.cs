namespace Business_Decision.Domain.Entities;

public class Invoice : AuditableEntity
{
    public int Id { get; set; }
    public int InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    public string Department { get; set; } = "All";
    public bool Warning { get; set; } = false;
    public bool Validated { get; set; } = false;
}
