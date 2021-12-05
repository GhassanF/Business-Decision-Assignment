using AutoMapper;
using Business_Decision.Application.Common.Exceptions;
using Business_Decision.Application.Common.Interfaces;
using Business_Decision.Application.Invoices.Models;
using Business_Decision.Domain.Entities;
using MediatR;

namespace Business_Decision.Application.Invoices.Commands.UpdateInvoice;

public class UpdateInvoiceCommand : IRequest<InvoiceContract>
{
    public int Id { get; set; }
    public int InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    public bool Warning { get; set; } = false;
    public bool Validated { get; set; } = false;
    public string Department { get; set; }
}

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, InvoiceContract>
{
    public readonly IApplicationDbContext _context;
    public readonly IMapper _mapper;

    private const decimal warningThreadshold = 700;
    public UpdateInvoiceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InvoiceContract> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Invoices.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Invoice), request.Id);
        }

        entity.Id = request.Id;
        entity.Amount = request.Amount;
        entity.Warning = request.Amount > warningThreadshold;
        entity.Validated = false;
        entity.Department = request.Department;

        _context.Invoices.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var invoiceContract = _mapper.Map<InvoiceContract>(entity);

        return invoiceContract;
    }
}