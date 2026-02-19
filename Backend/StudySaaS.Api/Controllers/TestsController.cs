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
public class TestsController(InMemoryDataStore store, CurrentTenant tenant) : ControllerBase
{
    [HttpGet("course/{courseId:guid}")]
    public ActionResult<IEnumerable<Test>> GetByCourse(Guid courseId)
    {
        if (tenant.InstituteId is null)
        {
            return BadRequest("Tenant not resolved.");
        }

        var tests = store.Tests.Where(t => t.InstituteId == tenant.InstituteId && t.CourseId == courseId);
        return Ok(tests);
    }

    [HttpPost("submit")]
    [Authorize(Roles = "Student")]
    public ActionResult<object> Submit(SubmitTestRequest request)
    {
        if (tenant.InstituteId is null)
        {
            return BadRequest("Tenant not resolved.");
        }

        var test = store.Tests.FirstOrDefault(t => t.InstituteId == tenant.InstituteId && t.Id == request.TestId);
        if (test is null)
        {
            return NotFound("Test not found.");
        }

        var total = test.Questions.Count;
        var score = test.Questions.Count(q => request.Answers.TryGetValue(q.Id, out var selected) && selected == q.CorrectIndex);

        return Ok(new
        {
            testId = test.Id,
            total,
            score,
            percentage = total == 0 ? 0 : Math.Round((double)score / total * 100, 2)
        });
    }
}
