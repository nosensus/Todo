using System;
using Microsoft.AspNetCore.Components.Web;
using Todo.WebApi.Models;

namespace Todo.WebApi.Data {
	public class FakeDb {
		public static List<TodoItem> todoItems = new() {
			new TodoItem {
				Id = Guid.NewGuid(),
				Title = "New Title",
				Description = "New description",
				Category = Categories.Work,
				DueDate = DateTime.UtcNow
			},
			new TodoItem {
				Id = Guid.NewGuid(),
				Title = "New Title 2",
				Description = "New description 2",
				Category = Categories.Home,
				DueDate = DateTime.UtcNow
			},
			new TodoItem {
				Id = Guid.NewGuid(),
				Title = "New Title 3",
				Description = "New description 3",
				Category = Categories.None,
				DueDate = DateTime.UtcNow
			},
		};
	}
}
