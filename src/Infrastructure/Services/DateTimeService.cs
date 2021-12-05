using Business_Decision.Application.Common.Interfaces;

namespace Business_Decision.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
