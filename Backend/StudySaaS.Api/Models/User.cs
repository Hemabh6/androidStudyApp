namespace StudySaaS.Api.Models;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid InstituteId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}
