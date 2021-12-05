using Business_Decision.Application.Common.Exceptions;
using Business_Decision.Application.Invoices.Commands.CreateInvoice;
using Business_Decision.Application.Invoices.Commands.DeleteInvoice;
using Business_Decision.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Business_Decision.Application.IntegrationTests.Invoices.Commands;

using static Testing;
public class DeleteInvoiceTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidInvoiceId()
    {
        var command = new DeleteInvoiceCommand { Id = 9999 };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteInvoice()
    {
        var newInvoice = await SendAsync(new CreateInvoiceCommand
        {
            Id = 1,
            InvoiceNumber = 123,
            Amount = 33,
            Department = "IT"
        });

        await SendAsync(new DeleteInvoiceCommand
        {
            Id = newInvoice.Id
        });

        var deletedInvoice = await FindAsync<Invoice>(newInvoice.Id);
        deletedInvoice.Should().BeNull();
    }
}
