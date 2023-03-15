using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Common.Enums;
using Xunit;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests;

public class DbContextActivityTests : DbContextTestsBase
{
    public DbContextActivityTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNewActivityTest()
    {
        var newActivity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Start = DateTime.Now,
            End = DateTime.Now,
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
                    Id = Guid.NewGuid(),
                    Name = "Chad",
                    Surname = "Watts",
                    PhotoUrl = "https://www.google.com/"
                }
            }
        };
        
        TimeTrackerDbContextSUT.Activities.Add(newActivity);
        await TimeTrackerDbContextSUT.SaveChangesAsync();
        
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Activities.SingleAsync(i => i.Id == newActivity.Id);
        Assert.Equal(newActivity, actualEntities);
        
    }
}