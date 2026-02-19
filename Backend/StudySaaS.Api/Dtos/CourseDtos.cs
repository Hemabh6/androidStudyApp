namespace StudySaaS.Api.Dtos;

public record CreateCourseRequest(string Title, string Description);
public record CreateMaterialRequest(Guid CourseId, string Title, string Type, string Url);
public record SubmitTestRequest(Guid TestId, Dictionary<Guid, int> Answers);
