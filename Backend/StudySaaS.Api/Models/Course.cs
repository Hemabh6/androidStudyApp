namespace StudySaaS.Api.Models;

public class Course
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid InstituteId { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid TeacherId { get; set; }
}
