using Business_Decision.Application.Common.Models;
using Business_Decision.Application.Invoices.Commands.CreateInvoice;
using Business_Decision.Application.Invoices.Commands.DeleteInvoice;
using Business_Decision.Application.Invoices.Commands.UpdateInvoice;
using Business_Decision.Application.Invoices.Commands.ValidateInvoice;
using Business_Decision.Application.Invoices.Models;
using Business_Decision.Application.Invoices.Queries.GetInvoicesWithPagination;
using Business_Decision.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Business_Decision.WebUI.Controllers;

[Authorize]
public class InvoicesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<InvoiceDto>>> GetTodoItemsWithPagination([FromQuery] GetInvoicesWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("validation")]
    public async Task<ActionResult<bool>> GatValidationErrors()
    {
        return await Mediator.Send(new ValidateInvoiceCommand());
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceContract>> Create(CreateInvoiceCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InvoiceContract>> Update(int id, UpdateInvoiceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteInvoiceCommand { Id = id });

        return NoContent();
    }

}
