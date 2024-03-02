using Todo.WebApi.Data;
using Todo.WebApi.Models;

namespace Todo.WebApi.Repository;

public class TodoRepository(TodoDbContext context) : ITodoRepository, IDisposable, IAsyncDisposable {
	private TodoDbContext Context = context;
	
	public void Create(TodoItem todoItem) {
		Context.Add(todoItem);
		Context.SaveChanges();
	}

	public IEnumerable<TodoItem> ListItems() {
		return Context.TodoList;
	}

	public TodoItem? GetItem(Guid id) {
		throw new NotImplementedException();
	}

	public void UpdateItem(TodoItem todoItem) {
		var item = ThrowExceptionIfEntityIsNotFound(todoItem.Id);
		
		item.Title = todoItem.Title;
		item.Description = todoItem.Description;
		item.Category = todoItem.Category;
		item.UpdatedDate = DateTime.UtcNow;
		item.DueDate = todoItem.DueDate;
		item.CardColor = todoItem.CardColor;
		item.IsImportant = todoItem.IsImportant;
		item.IsCompleted = todoItem.IsCompleted;

		Context.SaveChanges();
	}
	
	private TodoItem ThrowExceptionIfEntityIsNotFound(Guid id) {
		var entity = Context.TodoList.FirstOrDefault(x => x.Id == id);
		if (entity is null) {
			throw new Exception("Item not found");
		}

		return entity;
	}

	public void DeleteItem(Guid id) {
		throw new NotImplementedException();
	}

	public void Dispose() {
		throw new NotImplementedException();
	}

	public ValueTask DisposeAsync() {
		throw new NotImplementedException();
	}
}
