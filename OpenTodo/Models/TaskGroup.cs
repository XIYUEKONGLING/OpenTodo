namespace OpenTodo.Models;

public class TaskGroup
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "New Group";
    public string? Description { get; set; } = null;
    
    public List<TaskItem> Tasks { get; set; } = new();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}