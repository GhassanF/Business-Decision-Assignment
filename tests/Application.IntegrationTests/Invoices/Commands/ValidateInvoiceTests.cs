using Business_Decision.Application.Invoices.Commands.CreateInvoice;
using Business_Decision.Application.Invoices.Commands.ValidateInvoice;
using NUnit.Framework;

namespace Business_Decision.Application.IntegrationTests.Invoices.Commands;

using static Testing;

public class ValidateInvoiceTests : TestBase
{
    [Test]
    public async Task ShouldUpdateAllValidInvoices()
    {
        var userId = await RunAsDefaultUserAsync();

        var invoice1 = await SendAsync(new CreateInvoiceCommand
        {
            Id = 1,
            InvoiceNumber = 123,
            Amount = 33,
            Department = "IT"
        });

        var invoice2 = await SendAsync(new CreateInvoiceCommand
        {
            Id = 2,
            InvoiceNumber = 12,
            Amount = 3,
            Department = "HR"
        });

        var validationResult = await SendAsync(new ValidateInvoiceCommand() );
        Assert.IsTrue(validationResult);
    }

    [Test]
    public async Task ShouldNotUpdateAllValidInvoices()
    {
        var userId = await RunAsDefaultUserAsync();

        var invoice1 = await SendAsync(new CreateInvoiceCommand
        {
            Id = 1,
            InvoiceNumber = 123,
            Amount = 733,
            Department = "IT"
        });

        var invoice2 = await SendAsync(new CreateInvoiceCommand
        {
            Id = 2,
            InvoiceNumber = 12,
            Amount = 3,
            Department = "HR"
        });

        var validationResult = await SendAsync(new ValidateInvoiceCommand());
        Assert.IsFalse(validationResult);
    }
}
