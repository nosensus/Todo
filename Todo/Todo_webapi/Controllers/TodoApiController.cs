using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Todo_webapi.Data;
using Todo_webapi.Models;

namespace Todo_webapi.Controllers;

[ApiController]
[Route("todo")]
public class TodoApiController : ControllerBase {
	/// <summary>
	/// Add new item
	/// </summary>
	[HttpPost(Name = "Add new item")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public ActionResult<Todo> Create([FromBody] AddTodoResponse addTodoResponse) {
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		DateTime dateNow = DateTime.Now;
		Todo newTodo = new() {
			Id = Guid.NewGuid(),
			Title = addTodoResponse.Title,
			Description = addTodoResponse.Description,
			CreatedDate = dateNow,
			UpdatedDate = dateNow
		};
		FakeDb.todoItems.Add(newTodo);
		return Ok(newTodo);
	}

	/// <summary>
	/// Get todo items
	/// </summary>
	[HttpGet(Name = "Get todo items")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public ActionResult<IEnumerable<Todo>> ListItems() {
		return Ok(FakeDb.todoItems);
	}

	/// <summary>
	/// Find item by ID
	/// </summary>
	[HttpGet("{id:Guid}", Name = "Find item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public ActionResult<Todo> GetItem(Guid id) {
		Guid Id;
		if (!Guid.TryParse(id.ToString(), out Id)) {
			return BadRequest("Id is empty or not correct");
		}

		Todo item = FakeDb.todoItems.FirstOrDefault(item => item.Id == Id);
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
	public ActionResult<Todo> UpdateItem(
		[FromBody] Todo todo,
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

		Todo item = FakeDb.todoItems.FirstOrDefault(item => item.Id == Id);
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
		item.IsComplete = todo.IsComplete;
		return Ok(item);
	}

	/// <summary>
	/// Delete item by ID
	/// </summary>
	[HttpDelete("{id:Guid}", Name = "Delete item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult<Todo> DeleteItem(Guid id) {
		Guid Id;
		if (!Guid.TryParse(id.ToString(), out Id)) {
			return BadRequest();
		}

		Todo item = FakeDb.todoItems.FirstOrDefault(item => item.Id == Id);
		if (item is null) {
			return NotFound();
		}

		FakeDb.todoItems.Remove(item);
		return Ok(item);
	}
}
