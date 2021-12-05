using Business_Decision.Application.Common.Exceptions;
using Business_Decision.Application.Common.Interfaces;
using Business_Decision.Domain.Entities;
using MediatR;

namespace Business_Decision.Application.Invoices.Commands.DeleteInvoice;

public class DeleteInvoiceCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand>
{
    public readonly IApplicationDbContext _context;

    public DeleteInvoiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Invoices.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Invoice), request.Id);
        }

        _context.Invoices.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
