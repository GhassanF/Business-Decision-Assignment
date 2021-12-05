using FluentValidation;

namespace Business_Decision.Application.Invoices.Commands.UpdateInvoice;

public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
{
    public UpdateInvoiceCommandValidator()
    {
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.InvoiceNumber).NotEmpty();
        RuleFor(x => x.Department).NotEmpty();
    }
}
