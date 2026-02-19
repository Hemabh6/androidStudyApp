using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudySaaS.Api.Data;
using StudySaaS.Api.Dtos;
using StudySaaS.Api.Models;
using StudySaaS.Api.Services;

namespace StudySaaS.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CoursesController(InMemoryDataStore store, CurrentTenant tenant) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAll()
    {
        if (tenant.InstituteId is null)
        {
            return BadRequest("Tenant not resolved.");
        }

        return Ok(store.Courses.Where(c => c.InstituteId == tenant.InstituteId));
    }

    [HttpPost]
    [Authorize(Roles = "Teacher,InstituteAdmin")]
    public ActionResult<Course> Create(CreateCourseRequest request)
    {
        if (tenant.InstituteId is null)
        {
            return BadRequest("Tenant not resolved.");
        }

        var course = new Course
        {
            InstituteId = tenant.InstituteId.Value,
            Title = request.Title,
            Description = request.Description,
            TeacherId = Guid.Parse(User.FindFirst("sub")!.Value)
        };

        store.Courses.Add(course);
        return CreatedAtAction(nameof(GetAll), new { id = course.Id }, course);
    }

    [HttpGet("{courseId:guid}/materials")]
    public ActionResult<IEnumerable<StudyMaterial>> GetMaterials(Guid courseId)
    {
        if (tenant.InstituteId is null)
        {
            return BadRequest("Tenant not resolved.");
        }

        var materials = store.Materials.Where(m => m.InstituteId == tenant.InstituteId && m.CourseId == courseId);
        return Ok(materials);
    }

    [HttpPost("materials")]
    [Authorize(Roles = "Teacher,InstituteAdmin")]
    public ActionResult<StudyMaterial> AddMaterial(CreateMaterialRequest request)
    {
        if (tenant.InstituteId is null)
        {
            return BadRequest("Tenant not resolved.");
        }

        var material = new StudyMaterial
        {
            InstituteId = tenant.InstituteId.Value,
            CourseId = request.CourseId,
            Title = request.Title,
            Type = request.Type,
            Url = request.Url
        };

        store.Materials.Add(material);
        return Ok(material);
    }
}
