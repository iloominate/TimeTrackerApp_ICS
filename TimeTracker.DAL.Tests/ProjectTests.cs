using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Tests;

public class ProjectTests
{
    private readonly TimeTrackerDbContext _dbContextSUT;

    public ProjectTests()
    {
        _dbContextSUT = new TimeTrackerDbContext();
    }

    [Fact]
    public void NewProject_Add_Added()
    {
        var project = new ProjectEntity()
        {
            ID = Guid.NewGuid(),
            Name = "TimeTracker"
        };

        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.SaveChanges();
    }
}