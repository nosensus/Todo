using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.WebApi.Data;
using Todo.WebApi.Models;

namespace Todo.WebApi.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoApiController : ControllerBase {
	private TodoDbContext _db;

	public TodoApiController(TodoDbContext db) {
		_db = db;
	}
	
	/// <summary>
	/// Add new item
	/// </summary>
	[HttpPost(Name = "Add new item")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<TodoItem>> Create([FromBody] AddTodoResponse addTodoResponse)
	{
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}
		
		DateTime dateNow = DateTime.UtcNow;
		TodoItem newTodo = new() {
			Id = Guid.NewGuid(),
			Title = addTodoResponse.Title,
			Description = addTodoResponse.Description,
			Category = addTodoResponse.Category,
			DueDate =  addTodoResponse.DueDate,
			CardColor = addTodoResponse.CardColor,
			IsImportant = addTodoResponse.IsImportant,
			IsCompleted = addTodoResponse.IsCompleted,
			CreatedDate = dateNow,
			UpdatedDate = dateNow
		};
		
		_db.TodoList.Add(newTodo);
		await _db.SaveChangesAsync();

		return Ok(newTodo);
	}

	/// <summary>
	/// Get-TodoItems
	/// </summary>
	[HttpGet(Name = "Get todo items")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<IEnumerable<TodoItem>>> ListItems() {
		return await _db.TodoList.ToListAsync();
	}

	/// <summary>
	/// Find item by ID
	/// </summary>
	[HttpGet("{id:Guid}", Name = "Find item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<TodoItem>> GetItem(Guid id) {
		Guid Id;
		if (!Guid.TryParse(id.ToString(), out Id)) {
			return BadRequest("Id is empty or not correct");
		}

		TodoItem item = await _db.TodoList.FirstOrDefaultAsync(item => item.Id == Id);
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
	public async Task<ActionResult<TodoItem>> UpdateItem(
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

		TodoItem item = await _db.TodoList.FirstOrDefaultAsync(item => item.Id == Id);
		if (item is null) {
			return NotFound();
		}

		item.Title = todo.Title;
		item.Description = todo.Description;
		item.Category = todo.Category;
		item.UpdatedDate = DateTime.UtcNow;
		item.DueDate = todo.DueDate;
		item.CardColor = todo.CardColor;
		item.IsImportant = todo.IsImportant;
		item.IsCompleted = todo.IsCompleted;
		await _db.SaveChangesAsync();
		
		return Ok(item);
	}

	/// <summary>
	/// Delete item by ID
	/// </summary>
	[HttpDelete("{id:Guid}", Name = "Delete item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<TodoItem>> DeleteItem(Guid id) {
		Guid Id;
		if (!Guid.TryParse(id.ToString(), out Id)) {
			return BadRequest();
		}

		TodoItem item = await _db.TodoList.FirstOrDefaultAsync(item => item.Id == Id);
		if (item is null) {
			return NotFound();
		}

		_db.TodoList.Remove(item);
		await _db.SaveChangesAsync();
		
		return Ok();
	}
}
