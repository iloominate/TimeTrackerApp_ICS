using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Common.Enums;
using Xunit;
using Xunit.Abstractions;
using TimeTracker.Common.Tests;
using TimeTracker.DAL.Seeds;

namespace TimeTracker.DAL.Tests;

public class DbContextActivityTests : DbContextTestsBase
{
    public DbContextActivityTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNewActivityTest()
    {
        var creatorGuid = Guid.NewGuid();
        var newActivity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Name = "Test activity",
            Start = new DateTime(2020, 1, 1),
            End = new DateTime(2020, 2, 2),
            Type = ActivityType.Studying,
            Description = "Coding some stuff",
            UserId = Guid.NewGuid(),
            User = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "Betsy",
                Surname = "Bennett",
                PhotoUrl = "https://this-person-does-not-exist.com/img/avatar-111a0449eae48acbe95b576e3c51b779.jpg"
            },
            ProjectId = Guid.NewGuid(),
            Project = new ProjectEntity
            {
                Id = Guid.NewGuid(),
                Name = "Project 1",
                Creator = new UserEntity
                {
                    Id = creatorGuid,
                    Name = "Chad",
                    Surname = "Watts",
                    PhotoUrl = "https://www.google.com/"
                },
                CreatorId = creatorGuid,
            }
        };
        
        TimeTrackerDbContextSUT.Activities.Add(newActivity);
        await TimeTrackerDbContextSUT.SaveChangesAsync();
        
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Activities
            .Include(activity => activity.Project)
            .ThenInclude(project => project.Creator)
            .Include(activity => activity.User)
            .SingleAsync(i => i.Id == newActivity.Id);
        DeepAssert.Equal(newActivity, actualEntities);
        
    }

    [Fact]
    public async Task DeleteActivityById()
    {
        var activity = ActivitySeeds.MovementLogic;

        //TimeTrackerDbContextSUT.Activities.Remove(activity);
        TimeTrackerDbContextSUT.Activities.Remove(
            TimeTrackerDbContextSUT.Activities.Single(i => i.Id == activity.Id));
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        Assert.False(await TimeTrackerDbContextSUT.Activities.AnyAsync(i => i.Id == activity.Id));
    }
}