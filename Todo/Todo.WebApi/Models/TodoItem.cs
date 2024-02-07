using System.ComponentModel.DataAnnotations;

namespace Todo.WebApi.Models;

public class TodoItem {
	[Required]
	public Guid Id { get; set; }
	[Required]
	[MaxLength(30)]
	public required string Title { get; set; }
	public required string Description { get; set; }
	public Categories Category { get; set; } = Categories.None;
	public DateTime DueDate { get; set; }
	public CardColors CardColor { get; set; } = CardColors.White;
	public bool IsImportant { get; set; }
	public bool IsCompleted { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}


public class AddTodoResponse {
	[Required]
	[MaxLength(30)]
	public required string Title { get; set; }
	public required string Description { get; set; }
	public Categories Category { get; set; }
	public DateTime DueDate { get; set; }
}
