namespace OpenTodo.Models;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "New Project";
    public string? Description { get; set; } = null;

    public List<TaskList> TaskLists { get; set; } =
    [
        new()
        {
            Id = Guid.NewGuid(),
            Title = "Default List"
        }
    ];
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}