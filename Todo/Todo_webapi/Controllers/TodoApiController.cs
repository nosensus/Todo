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
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult<Todo> Create(
		[FromBody] Todo todo,
		[FromQuery, Required] string title,
		[FromQuery, Required] string description) {
		if (todo is null) {
			return BadRequest();
		}

		todo.Id = FakeDb.TodoItems.OrderByDescending(item => item.Id).FirstOrDefault().Id + 1;
		FakeDb.TodoItems.Add(todo);

		return Ok(todo);
	}

	/// <summary>
	/// Get todo items
	/// </summary>
	[HttpGet(Name = "Get todo items")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult<IEnumerable<Todo>> ListItems() {
		return Ok(FakeDb.TodoItems);
	}

	/// <summary>
	/// Find item by ID
	/// </summary>
	[HttpGet("{id:int}", Name = "Find item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult<Todo> GetItem(int id) {
		if (id == 0) {
			return BadRequest();
		}

		var item = FakeDb.TodoItems.FirstOrDefault(item => item.Id == id);
		if (item == null) {
			return NotFound();
		}

		return Ok(item);
	}

	/// <summary>
	/// Find item by Title
	/// </summary>
	[HttpGet("{title}", Name = "Find item by Title")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult<Todo> GetItem(string title) {
		if (title == "") {
			return BadRequest();
		}

		var item = FakeDb.TodoItems.FirstOrDefault(item => item.Title == title);
		if (item == null) {
			return NotFound();
		}

		return Ok(item);
	}

	/// <summary>
	/// Update Item by ID
	/// </summary>
	[HttpPut("{id:int}", Name = "Update Item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult<Todo> UpdateItem(
		int id,
		Todo? todoItem,
		[FromQuery, Required] string title,
		[FromQuery, Required] string description) {
		if (todoItem == null && id != todoItem.Id) {
			return BadRequest(todoItem);
		}

		var item = FakeDb.TodoItems.FirstOrDefault(item => item.Id == id);
		item.Title = todoItem.Title;
		item.Description = todoItem.Description;

		return NoContent();
	}

	/// <summary>
	/// Update Item by Title
	/// </summary>
	[HttpPut("{title}", Name = "Update Item by Title")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult<Todo> UpdateItem(
		string title,
		Todo? todoItem,
		[FromQuery, Required] string description) {
		if (todoItem == null && title != todoItem.Title) {
			return BadRequest(todoItem);
		}

		var item = FakeDb.TodoItems.FirstOrDefault(item => item.Title == title);
		item.Title = todoItem.Title;
		item.Description = todoItem.Description;

		return NoContent();
	}

	/// <summary>
	/// Delete item by ID
	/// </summary>
	[HttpDelete("{id:int}", Name = "Delete item by ID")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult<Todo> DeleteItem(int id) {
		if (id == 0) {
			return BadRequest();
		}

		var item = FakeDb.TodoItems.FirstOrDefault(item => item.Id == id);
		if (item == null) {
			return NotFound();
		}
		FakeDb.TodoItems.Remove(item);

		return NoContent();
	}

	/// <summary>
	/// Delete item by Title
	/// </summary>
	[HttpDelete("{title}", Name = "Delete item by Title")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public ActionResult<Todo> DeleteItem(string title) {
		if (title == "") {
			return BadRequest();
		}

		var item = FakeDb.TodoItems.FirstOrDefault(item => item.Title == title);
		if (item == null) {
			return NotFound();
		}
		FakeDb.TodoItems.Remove(item);

		return NoContent();
	}
}
