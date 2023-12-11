using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApi.Controllers;
using Todo.WebApi.Models;

namespace Todo.WebApi.ApiTests;

public class TodoApiControllerTests {
	private readonly AddTodoResponse _todoItemResponse = new() {
		Title = "Static Title",
		Description = "Static Description",
		Category = Categories.None
	};

	private TodoApiController _controller;
	private TodoItem? _todoItem;

	[SetUp]
	public void Setup() {
		_controller = new TodoApiController();
		var actionResult = _controller.Create(_todoItemResponse);
		var result = actionResult.Result as OkObjectResult;
		_todoItem = result?.Value as TodoItem;
	}

	[Test]
	public void ReturnsTodoItemModelAfterCallsCreate() {
		// Arrange
		AddTodoResponse todoItemResponse = new() {
			Title = "New Title",
			Description = "New Description",
			Category = Categories.Car
		};

		// Act
		var actionResult = _controller.Create(todoItemResponse);
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as TodoItem;

		// Assertion
		todoItem.Should().NotBeNull();
		todoItem!.Title.Should().Be(todoItemResponse.Title);
	}

	[Test]
	public void ReturnTodoItemsCount() {
		// Act
		var actionResult = _controller.ListItems();
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as List<TodoItem>;

		// Assertion
		todoItem!.Count.Should().Be(1);
	}

	[Test]
	public void ReturnsTodoItemTitleThatWasSetInSetup() {
		// Act
		var actionResult = _controller.GetItem(_todoItem!.Id);
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as TodoItem;

		// Assertion
		todoItem.Should().NotBeNull();
		todoItem!.Title.Should().BeEquivalentTo("Static Title");
	}

	[Test]
	public void ReturnsStatusCode404WhenRecordIsNotInCollection() {
		// Act
		var actionResult = _controller.GetItem(Guid.Empty);
		var result = actionResult.Result as NotFoundResult;

		// Assertion
		result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
	}

	[Test]
	public void UpdateTodoItemTitleGetById() {
		// Arrange
		const string updateTitle = "Update Title";
		var actionResult = _controller.GetItem(_todoItem!.Id);
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as TodoItem;
		_todoItem.Title = updateTitle;

		// Act
		var updateActionResult = _controller.UpdateItem(_todoItem, todoItem!.Id);
		var newResult = updateActionResult.Result as OkObjectResult;
		var returnedTodoItem = newResult?.Value as TodoItem;

		// Assert
		returnedTodoItem!.Title.Should().Be(updateTitle);
	}

	[Test]
	public void UpdateTodoItemReturnsBadRequestMessageWhenTodoItemIdIsWrong() {
		// Act
		var updateActionResult = _controller.UpdateItem(_todoItem!, new Guid());
		var result = updateActionResult.Result as BadRequestObjectResult;

		// Assert
		result!.Value.Should().Be("id from request Body is not equal Id from URL");
	}

	[Test]
	public void UpdateTodoItemReturnsNotFoundWhenRecordIsNotInCollection() {
		// Arange
		var todoItem = new TodoItem {
			Id = Guid.Empty,
			Title = string.Empty,
			Description = string.Empty
		};

		// Act
		var updateActionResult = _controller.UpdateItem(todoItem, todoItem.Id);
		var result = updateActionResult.Result as NotFoundResult;

		// Assertion
		result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
	}

	[Test]
	public void CollectionTodoItemShouldBeNullAfterDeletingRecord() {
		// Act
		var actionResult = _controller.DeleteItem(_todoItem!.Id);
		var result = actionResult.Result as OkObjectResult;
		var todoItem = result?.Value as TodoItem;

		// Assert
		todoItem.Should().BeNull();
	}

	[Test]
	public void ReturnsNotFoundWhenDeletingRecordIsNotInCollection() {
		// Act
		var updateActionResult = _controller.DeleteItem(new Guid());
		var result = updateActionResult.Result as NotFoundResult;

		// Assertion
		result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
	}
}
