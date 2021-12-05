using Business_Decision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Business_Decision.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(x=>x.Amount).IsRequired();
        builder.Property(x=>x.InvoiceNumber).IsRequired();
        builder.Property(x=>x.Department).IsRequired();
    }
}
