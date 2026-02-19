namespace StudySaaS.Api.Models;

public class StudyMaterial
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid InstituteId { get; set; }
    public Guid CourseId { get; set; }
    public required string Title { get; set; }
    public required string Type { get; set; }
    public required string Url { get; set; }
}
