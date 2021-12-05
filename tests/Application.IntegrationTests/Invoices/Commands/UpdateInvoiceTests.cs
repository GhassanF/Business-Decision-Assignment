using Business_Decision.Application.Common.Exceptions;
using Business_Decision.Application.Invoices.Commands.CreateInvoice;
using Business_Decision.Application.Invoices.Commands.UpdateInvoice;
using FluentAssertions;
using NUnit.Framework;

namespace Business_Decision.Application.IntegrationTests.Invoices.Commands;

using static Testing;

public class UpdateInvoiceTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var invoice = new UpdateInvoiceCommand();

        await FluentActions.Invoking(() => SendAsync(invoice)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireValidInvoiceId()
    {
        var invoice = new UpdateInvoiceCommand()
        {
            Id = 99999,
            Amount = 55,
            Department = "HR",
            InvoiceNumber = 33
        };

        await FluentActions.Invoking(() => SendAsync(invoice)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateInvoice()
    {
        var userId = await RunAsDefaultUserAsync();

        var newInvoice = await SendAsync(new CreateInvoiceCommand
        {
            InvoiceNumber = 123,
            Amount = 33,
            Department = "IT"
        });

        var updatedInvoice = new UpdateInvoiceCommand()
        {
            Id= newInvoice.Id,
            InvoiceNumber = 567,
            Amount = 100,
            Department = "HR"
        };

        updatedInvoice.Should().NotBeNull();
        updatedInvoice.Id.Should().Be(newInvoice.Id);
        updatedInvoice.Amount.Should().Be(updatedInvoice.Amount);
        updatedInvoice.InvoiceNumber.Should().Be(updatedInvoice.InvoiceNumber);
        updatedInvoice.Department.Should().Be(updatedInvoice.Department);
    }
}
