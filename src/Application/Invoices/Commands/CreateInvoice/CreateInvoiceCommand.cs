using AutoMapper;
using Business_Decision.Application.Common.Interfaces;
using Business_Decision.Application.Invoices.Models;
using Business_Decision.Domain.Entities;
using MediatR;

namespace Business_Decision.Application.Invoices.Commands.CreateInvoice;

public class CreateInvoiceCommand : IRequest<InvoiceContract>
{
    public int Id { get; set; }
    public int InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    public bool Warning { get; set; }
    public bool Validated { get; set; }
    public string Department { get; set; }
}

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceContract>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    private const decimal warningThreadshold = 700;


    public CreateInvoiceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InvoiceContract> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var entity = new InvoiceContract
        {
            Amount = request.Amount,
            Department = request.Department,
            InvoiceNumber = request.InvoiceNumber,
            Warning = request.Amount > warningThreadshold
        };

        var dbEntity = _mapper.Map<Invoice>(entity);

        _context.Invoices.Add(dbEntity);

        await _context.SaveChangesAsync(cancellationToken);

        entity = _mapper.Map<InvoiceContract>(dbEntity);

        return entity;
    }
}
