using Microsoft.AspNetCore.Http.HttpResults;
using Todo.WebApi.Models;

namespace Todo.WebApi.Repository;

public interface ITodoRepository {
	void Create(TodoItem todoItem);
	IEnumerable<TodoItem> ListItems();
	TodoItem? GetItem(Guid id);
	void UpdateItem(TodoItem todoItem);
	void DeleteItem(Guid id);
}
