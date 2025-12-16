namespace OpenTodo.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "New Task";
    public string? Description { get; set; }
    
    // public List<TaskItem> SubTasks { get; set; } = new();
    public int Progress { get; set; }
    
    public DateOnly? StartDate { get; set; }
    public DateOnly? DueDate { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? DueTime { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    public bool IsCompleted { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}