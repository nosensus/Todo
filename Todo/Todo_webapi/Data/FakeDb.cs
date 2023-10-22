using System;
using Todo_webapi.Models;

namespace Todo_webapi.Data {
	public class FakeDb {
		public static List<Todo> TodoItems = new() {
			new Todo {
				Id = 1,
				Title = "Buy some brad",
				Description = "Go to shop market and buy white and brown brad",
				Category = "Home",
				DateCreated = new DateTime(),
				DueDate = new DateTime(),
				CardColor = "White",
				IsImportant = false,
				IsComplete = false
			}
		};
	}
}

