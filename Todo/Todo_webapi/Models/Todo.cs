using System.ComponentModel.DataAnnotations;

namespace Todo_webapi.Models;

public class Todo {
	[Required]
	public Guid Id { get; set; }
	[Required]
	[MaxLength(30)]
	public string Title { get; set; }
	public string Description { get; set; }
	public Categories Category { get; set; } = Categories.None;
	public DateTime DueDate { get; set; }
	public CardColors CardColor { get; set; } = CardColors.White;
	public bool IsImportant { get; set; }
	public bool IsComplete { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}


public class AddTodoResponse {
	[Required]
	[MaxLength(30)]
	public string Title { get; set; }
	public string Description { get; set; }
	public Categories Category { get; set; }
}
