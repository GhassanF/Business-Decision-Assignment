
using Business_Decision.Application.Common.Exceptions;
using Business_Decision.Application.Invoices.Commands.CreateInvoice;
using Business_Decision.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Business_Decision.Application.IntegrationTests.Invoices.Commands;

using static Testing;

public class CreateInvoiceTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var invoice = new CreateInvoiceCommand();

        await FluentActions.Invoking(() => SendAsync(invoice)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateInvoice()
    {
        var userId = await RunAsDefaultUserAsync();

        var newInvoice = await SendAsync(new CreateInvoiceCommand
        {
            Id = 1,
            InvoiceNumber = 123,
            Amount = 33,
            Department = "IT"
        });

        var createdInvoice = await FindAsync<Invoice>(newInvoice.Id);

        createdInvoice.Should().NotBeNull();
        createdInvoice!.Id.Should().Be(newInvoice.Id);
        createdInvoice.InvoiceNumber.Should().Be(newInvoice.InvoiceNumber);
        createdInvoice.Amount.Should().Be(newInvoice.Amount);
        createdInvoice.Department.Should().Be(newInvoice.Department);
        createdInvoice.CreatedBy.Should().Be(userId);
        createdInvoice.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        createdInvoice.LastModifiedBy.Should().BeNull();
        createdInvoice.LastModified.Should().BeNull();
    }
}
