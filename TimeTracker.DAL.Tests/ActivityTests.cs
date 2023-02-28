using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Tests;

public class ActivityTests
{
    private readonly TimeTrackerDbContext _dbContextSUT;

    public ActivityTests()
    {
        _dbContextSUT = new TimeTrackerDbContext();
    }

    [Fact]
    public void NewActivity_Add_Added()
    {
        var activity = new ActivityEntity()
        {
            ID = Guid.NewGuid(),
            Start = DateTime.Now,
            End = DateTime.Now,
            ActivityType = "Coding",
            Description = "Coding some stuff",
            UserID = 1,
            ProjectID = 1
        };

        _dbContextSUT.Activities.Add(activity);
        _dbContextSUT.SaveChanges();
    }
}