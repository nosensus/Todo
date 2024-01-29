using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApi.Data;
using Todo.WebApi.Models;

namespace Todo.WebApi.Controllers;

[ApiController]
[Route("todo")]
public class TodoApiController : ControllerBase {
	/// <summary>
	/// Add new item
	/// </summary>
	[HttpPost(Name = "Add new item")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public ActionResult<TodoItem> Create([FromBody] AddTodoResponse addTodoResponse) {
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		DateTime dateNow = DateTime.Now;
		TodoItem newTodo = new() {
			Id = Guid.NewGuid(),
			Title = addTodoResponse.Title,
			Description = addTodoResponse.Description,
			Category = addTodoResponse.Category,
			CreatedDate = dateNow,
			UpdatedDate = dateNow
		};
		FakeDb.todoItems.Add(newTodo);
		return Ok(newTodo);
	}

	/// <summary>
	/// Get-TodoItems
	/// </summary>
	[HttpGet(Name = "Get todo items")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public ActionResult<IEnumerable<TodoItem>> ListItems() {
		return Ok(FakeDb.todoItems);
	}

	/// <summary>
	/// Find item by ID
	/// </summary>
	[HttpGet("{id:Guid}", Name = "Find item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public ActionResult<TodoItem> GetItem(Guid id) {
		Guid Id;
		if (!Guid.TryParse(id.ToString(), out Id)) {
			return BadRequest("Id is empty or not correct");
		}

		TodoItem item = FakeDb.todoItems.Find(item => item.Id == Id);
		if (item == null) {
			return NotFound();
		}

		return Ok(item);
	}

	/// <summary>
	/// Update Item by ID
	/// </summary>
	[HttpPut("{id:Guid}", Name = "Update Item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult<TodoItem> UpdateItem(
		[FromBody] TodoItem todo,
		[FromQuery, Required] Guid id) {
		if (!ModelState.IsValid) {
			return BadRequest();
		}

		Guid Id;
		if (!Guid.TryParse(id.ToString(), out Id)) {
			return BadRequest();
		}

		if (todo.Id != Id) {
			return BadRequest("id from request Body is not equal Id from URL");
		}

		TodoItem item = FakeDb.todoItems.Find(item => item.Id == Id);
		if (item is null) {
			return NotFound();
		}

		item.Title = todo.Title;
		item.Description = todo.Description;
		item.Category = todo.Category;
		item.UpdatedDate = DateTime.Now;
		item.DueDate = todo.DueDate;
		item.CardColor = todo.CardColor;
		item.IsImportant = todo.IsImportant;
		item.IsCompleted = todo.IsCompleted;
		return Ok(item);
	}

	/// <summary>
	/// Delete item by ID
	/// </summary>
	[HttpDelete("{id:Guid}", Name = "Delete item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult<TodoItem> DeleteItem(Guid id) {
		Guid Id;
		if (!Guid.TryParse(id.ToString(), out Id)) {
			return BadRequest();
		}

		TodoItem item = FakeDb.todoItems.Find(item => item.Id == Id);
		if (item is null) {
			return NotFound();
		}

		FakeDb.todoItems.Remove(item);
		return Ok();
	}
}
