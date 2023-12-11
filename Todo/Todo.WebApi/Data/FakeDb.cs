using System;
using Todo.WebApi.Models;

namespace Todo.WebApi.Data {
	public class FakeDb {
		public static List<TodoItem> todoItems = new List<TodoItem>();
	}
}
