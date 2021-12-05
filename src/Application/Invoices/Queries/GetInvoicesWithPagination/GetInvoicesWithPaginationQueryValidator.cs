using FluentValidation;

namespace Business_Decision.Application.Invoices.Queries.GetInvoicesWithPagination;

public class GetInvoicesWithPaginationQueryValidator : AbstractValidator<GetInvoicesWithPaginationQuery>
{
    public GetInvoicesWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber should be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should be greater than or equal to 1.");
    }
}
