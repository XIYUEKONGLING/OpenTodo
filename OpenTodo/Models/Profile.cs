namespace OpenTodo.Models;

public class Profile
{
    // public long ProtocolVersion { get; set; }

    public UserInformation UserInfo { get; set; } = new();
    public List<Project> Projects { get; set; } = new();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}