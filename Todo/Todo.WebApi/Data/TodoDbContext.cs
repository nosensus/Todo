using Microsoft.EntityFrameworkCore;
using Todo.WebApi.Models;

namespace Todo.WebApi.Data;

public class TodoDbContext : DbContext {
	public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {}
	
	public required DbSet<TodoItem> TodoList { get; set; }
}
