namespace StudySaaS.Api.Models;

public class Test
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid InstituteId { get; set; }
    public Guid CourseId { get; set; }
    public required string Title { get; set; }
    public List<Question> Questions { get; set; } = [];
}

public class Question
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Prompt { get; set; }
    public List<string> Options { get; set; } = [];
    public int CorrectIndex { get; set; }
}
