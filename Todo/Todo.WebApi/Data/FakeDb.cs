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
				DueDate = DateTime.Now
			}
		};
	}
}
