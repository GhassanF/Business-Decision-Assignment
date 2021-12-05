using Business_Decision.Application.Common.Behaviours;
using Business_Decision.Application.Common.Interfaces;
using Business_Decision.Application.Invoices.Commands.CreateInvoice;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Business_Decision.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateInvoiceCommand>> _logger = null!;
    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateInvoiceCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateInvoiceCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateInvoiceCommand { Amount = 50, Department = "IT", InvoiceNumber = 1 }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateInvoiceCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateInvoiceCommand { Amount = 50, Department = "IT", InvoiceNumber = 1 }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
