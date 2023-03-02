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
    public void AddNewProjectTest()
    {
        var project = new ProjectEntity()
        {
            Id = Guid.NewGuid(),
            Name = "Project 1",
        };

        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.SaveChanges();
    }
    
    [Fact]
    public void RemoveProjectTest()
    {
        var project = new ProjectEntity()
        {
            Id = Guid.NewGuid(),
            Name = "Project 2",
        };

        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.SaveChanges();
        
        _dbContextSUT.Projects.Remove(project);
        _dbContextSUT.SaveChanges();
    }
}