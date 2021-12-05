﻿using Business_Decision.Application.Common.Mappings;
using Business_Decision.Domain.Entities;

namespace Business_Decision.Application.Invoices.Models;

public class InvoiceContract:IMapFrom<Invoice>
{
    public int Id { get; set; }
    public int InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    public string Department { get; set; }
    public bool Warning { get; set; } = false;
    public bool Validated { get; set; } = false;
}
