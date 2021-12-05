using Business_Decision.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business_Decision.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Invoice> Invoices { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
