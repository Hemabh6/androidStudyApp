namespace StudySaaS.Api.Dtos;

public record RegisterInstituteRequest(string Name, string Code, string Subdomain, string AdminName, string AdminEmail, string AdminPassword);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token, Guid UserId, Guid InstituteId, string Role, string FullName);
