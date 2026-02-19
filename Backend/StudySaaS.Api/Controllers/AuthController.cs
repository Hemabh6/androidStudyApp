using Microsoft.AspNetCore.Mvc;
using StudySaaS.Api.Data;
using StudySaaS.Api.Dtos;
using StudySaaS.Api.Models;
using StudySaaS.Api.Services;

namespace StudySaaS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(InMemoryDataStore store, TokenService tokenService, CurrentTenant tenant) : ControllerBase
{
    [HttpPost("register-institute")]
    public ActionResult<AuthResponse> RegisterInstitute(RegisterInstituteRequest request)
    {
        if (store.Institutes.Any(i => i.Code == request.Code || i.Subdomain == request.Subdomain))
        {
            return Conflict("Institute code/subdomain already exists.");
        }

        var institute = new Institute
        {
            Name = request.Name,
            Code = request.Code,
            Subdomain = request.Subdomain
        };

        var admin = new User
        {
            InstituteId = institute.Id,
            FullName = request.AdminName,
            Email = request.AdminEmail,
            Password = request.AdminPassword,
            Role = "InstituteAdmin"
        };

        store.Institutes.Add(institute);
        store.Users.Add(admin);

        var token = tokenService.Generate(admin);
        return Ok(new AuthResponse(token, admin.Id, admin.InstituteId, admin.Role, admin.FullName));
    }

    [HttpPost("login")]
    public ActionResult<AuthResponse> Login(LoginRequest request)
    {
        if (tenant.InstituteId is null)
        {
            return BadRequest("Tenant not resolved. Pass X-Tenant header.");
        }

        var user = store.Users.FirstOrDefault(u =>
            u.InstituteId == tenant.InstituteId &&
            u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase) &&
            u.Password == request.Password);

        if (user is null)
        {
            return Unauthorized("Invalid credentials.");
        }

        var token = tokenService.Generate(user);
        return Ok(new AuthResponse(token, user.Id, user.InstituteId, user.Role, user.FullName));
    }
}
