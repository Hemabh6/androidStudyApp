namespace StudySaaS.Api.Models;

public class Institute
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Subdomain { get; set; }
}
