namespace OpenTodo.Models;

public class TaskList
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "New List";
    public string? Description { get; set; } = null;
    
    public List<TaskItem> UngroupedTasks { get; set; } = new();
    public List<TaskGroup> TaskGroups { get; set; } = new();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}