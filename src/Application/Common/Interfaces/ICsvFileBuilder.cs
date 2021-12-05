using Business_Decision.Application.TodoLists.Queries.ExportTodos;

namespace Business_Decision.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
