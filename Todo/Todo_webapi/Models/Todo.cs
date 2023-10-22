using System.ComponentModel.DataAnnotations;

namespace Todo_webapi.Models;

public class Todo {
	public int Id { get; set; }
	[Required]
	public string Title { get; set; }
	[Required]
	public string Description { get; set; }
	public string Category { get; set; }
	public DateTime DateCreated { get; set; }
	public DateTime DueDate { get; set; }
	public string CardColor { get; set; }
	public bool IsImportant { get; set; }
	public bool IsComplete { get; set; }
}
