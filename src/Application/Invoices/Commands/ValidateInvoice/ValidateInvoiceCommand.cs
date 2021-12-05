using Business_Decision.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Business_Decision.Application.Invoices.Commands.ValidateInvoice;

public class ValidateInvoiceCommand : IRequest<bool>
{
}

public class ValidateInvoiceCommandHandler : IRequestHandler<ValidateInvoiceCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public List<string> validationErrors { get; set; } = new List<string>();

    public ValidateInvoiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ValidateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invalidInvoices = _context.Invoices.Where(x => x.Warning == true).Count();
       
        if(invalidInvoices > 0)
        {
            return false;
        }

        var validInvoices = _context.Invoices.Where(x=>x.Validated != true).ToList();

        validInvoices.ForEach(x => x.Validated = true);

        _context.Invoices.UpdateRange(validInvoices);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
