using FluentValidation;

namespace Business_Decision.Application.Invoices.Commands.CreateInvoice;

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.InvoiceNumber).NotEmpty();
        RuleFor(x => x.Department).NotEmpty();
    }
}
