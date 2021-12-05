using Business_Decision.Application.Common.Mappings;
using Business_Decision.Domain.Entities;

namespace Business_Decision.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
