using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business_Decision.Application.Common.Interfaces;
using Business_Decision.Application.Common.Mappings;
using Business_Decision.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business_Decision.Application.Invoices.Queries.GetInvoicesWithPagination;

public class GetInvoicesWithPaginationQuery : IRequest<PaginatedList<InvoiceDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetInvoiceWithPaginationQueryHandler : IRequestHandler<GetInvoicesWithPaginationQuery, PaginatedList<InvoiceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetInvoiceWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InvoiceDto>> Handle(GetInvoicesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Invoices
            .AsNoTracking()
            .OrderBy(x => x.InvoiceNumber)
            .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
