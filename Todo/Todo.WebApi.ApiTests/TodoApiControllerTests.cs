using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApi.Controllers;
using Todo.WebApi.Models;

namespace Todo.WebApi.ApiTests;

public class TodoApiControllerTests {
	private readonly AddTodoResponse controllerTodoItemResponse = new() {
		Title = "Static Title",
		Description = "Static Description",
		Category = Categories.None
	};

	private TodoApiController _controller;

	[Test]
	public void Create_WhenValidRequest_ReturnsTodoItemModel() {
		// Arrange
		AddTodoResponse todoItemResponse = new() {
			Title = "New Title",
			Description = "New Description",
			Category = Categories.Car
		};
		_controller = new TodoApiController();

		// Act
		var actionResult = _controller.Create(todoItemResponse);
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as TodoItem;

		// Assertion
		todoItem.Should().NotBeNull();
		todoItem!.Title.Should().Be(todoItemResponse.Title);
	}

	[Test]
	public void ListItems_WhenItemsAddedToDatabase_ReturnsItemsFromDatabase() {
		// Arrange
		_controller = new TodoApiController();
		_controller.Create(controllerTodoItemResponse);

		// Act
		var actionResult = _controller.ListItems();
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as List<TodoItem>;

		// Assertion
		todoItem!.Count.Should().NotBe(null);
	}

	[Test]
	public void GetItem_WhenItemIsAddedToDatabaseWithTitle_ReturnsItemWithTheSameTitle() {
		// Arrange
		_controller = new TodoApiController();
		var controllerActionResult = _controller.Create(controllerTodoItemResponse);
		var controllerResult = controllerActionResult.Result as OkObjectResult;
		TodoItem? controllerTodoItem = controllerResult?.Value as TodoItem;

		// Act
		var actionResult = _controller.GetItem(controllerTodoItem!.Id);
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as TodoItem;

		// Assertion
		todoItem.Should().NotBeNull();
		todoItem!.Title.Should().BeEquivalentTo("Static Title");
	}

	[Test]
	public void GetTodoItem_WhenRecordIsNotInCollection_ReturnsStatusCode404() {
		// Arrange
		_controller = new TodoApiController();

		// Act
		var actionResult = _controller.GetItem(Guid.Empty);
		var result = actionResult.Result as NotFoundResult;

		// Assertion
		result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
	}

	[Test]
	public void Update_TodoItemTitle_ChangedTitleMatch() {
		// Arrange
		const string updateTitle = "Update Title";
		_controller = new TodoApiController();
		var controllerActionResult = _controller.Create(controllerTodoItemResponse);
		var controllerResult = controllerActionResult.Result as OkObjectResult;
		TodoItem? controllerTodoItem = controllerResult?.Value as TodoItem;

		var actionResult = _controller.GetItem(controllerTodoItem!.Id);
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as TodoItem;
		controllerTodoItem.Title = updateTitle;

		// Act
		var updateActionResult = _controller.UpdateItem(controllerTodoItem, todoItem!.Id);
		var newResult = updateActionResult.Result as OkObjectResult;
		var returnedTodoItem = newResult?.Value as TodoItem;

		// Assert
		returnedTodoItem!.Title.Should().Be(updateTitle);
	}

	[Test]
	public void Update_WhenIdInModelAndUrlIsNotEqual_ReturnsBadRequestMessage() {
		// Arrange
		_controller = new TodoApiController();
		var controllerActionResult = _controller.Create(controllerTodoItemResponse);
		var controllerResult = controllerActionResult.Result as OkObjectResult;
		TodoItem? controllerTodoItem = controllerResult?.Value as TodoItem;

		// Act
		var updateActionResult = _controller.UpdateItem(controllerTodoItem!, new Guid());
		var result = updateActionResult.Result as BadRequestObjectResult;

		// Assert
		result!.Value.Should().Be("id from request Body is not equal Id from URL");
	}

	[Test]
	public void Update_WhenRecordIsNotInCollection_ReturnsNotFound() {
		// Arange
		var todoItem = new TodoItem {
			Id = Guid.Empty,
			Title = string.Empty,
			Description = string.Empty
		};
		_controller = new TodoApiController();
		_controller.Create(controllerTodoItemResponse);

		// Act
		var updateActionResult = _controller.UpdateItem(todoItem, todoItem.Id);
		var result = updateActionResult.Result as NotFoundResult;

		// Assertion
		result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
	}

	[Test]
	public void Delete_CollectionTodoItem_ReturnsStatus200() {
		// Arrange
		_controller = new TodoApiController();
		var controllerActionResult = _controller.Create(controllerTodoItemResponse);
		var controllerResult = controllerActionResult.Result as OkObjectResult;
		TodoItem? controllerTodoItem = controllerResult?.Value as TodoItem;

		// Act
		var actionResult = _controller.DeleteItem(controllerTodoItem!.Id);
		var result = actionResult.Result as OkResult;

		// Assert
		result.StatusCode.Should().Be(StatusCodes.Status200OK);
	}

	[Test]
	public void Delete_RecordIsNotInCollection_ReturnsNotFound() {
		// Arrange
		_controller = new TodoApiController();

		// Act
		var updateActionResult = _controller.DeleteItem(new Guid());
		var result = updateActionResult.Result as NotFoundResult;

		// Assertion
		result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
	}
}
