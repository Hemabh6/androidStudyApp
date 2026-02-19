using StudySaaS.Api.Models;

namespace StudySaaS.Api.Data;

public class InMemoryDataStore
{
    public List<Institute> Institutes { get; } = [];
    public List<User> Users { get; } = [];
    public List<Course> Courses { get; } = [];
    public List<StudyMaterial> Materials { get; } = [];
    public List<Test> Tests { get; } = [];

    public InMemoryDataStore()
    {
        var institute = new Institute { Name = "Demo Academy", Code = "DEMO", Subdomain = "demo" };
        Institutes.Add(institute);

        var teacher = new User
        {
            InstituteId = institute.Id,
            FullName = "Demo Teacher",
            Email = "teacher@demo.com",
            Password = "Pass@123",
            Role = "Teacher"
        };
        Users.Add(teacher);

        var student = new User
        {
            InstituteId = institute.Id,
            FullName = "Demo Student",
            Email = "student@demo.com",
            Password = "Pass@123",
            Role = "Student"
        };
        Users.Add(student);

        var course = new Course
        {
            InstituteId = institute.Id,
            Title = "UPSC Foundations",
            Description = "Indian Polity and History basics",
            TeacherId = teacher.Id
        };
        Courses.Add(course);

        Materials.Add(new StudyMaterial
        {
            InstituteId = institute.Id,
            CourseId = course.Id,
            Title = "Laxmikanth Quick Revision",
            Type = "PDF",
            Url = "https://example.com/materials/laxmikanth.pdf"
        });

        Tests.Add(new Test
        {
            InstituteId = institute.Id,
            CourseId = course.Id,
            Title = "Polity Mock 1",
            Questions =
            [
                new Question
                {
                    Prompt = "Who is the head of the state in India?",
                    Options = ["Prime Minister", "President", "Chief Justice", "Speaker"],
                    CorrectIndex = 1
                }
            ]
        });
    }
}
