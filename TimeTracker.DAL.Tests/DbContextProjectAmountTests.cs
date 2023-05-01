using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Common.Enums;
using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using TimeTracker.Common.Tests;
using TimeTracker.DAL.Seeds;

namespace TimeTracker.DAL.Tests;

public class DbContextProjectAmountTests : DbContextTestsBase
{
    public DbContextProjectAmountTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetAll_ProjectAmounts_ForActivity()
    {
        var creatorId = Guid.NewGuid();
        var newActivity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
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
                    Id = creatorId,
                    Name = "Chad",
                    Surname = "Watts",
                    PhotoUrl = "https://www.google.com/"
                },
                CreatorId = creatorId
            }
        };

        TimeTrackerDbContextSUT.Activities.Add(newActivity);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Activities
            .Include(activity => activity.Project)
            .ThenInclude(project => project.Creator)
            .Include(activity => activity.User)
            .ToListAsync();

        //Assert.Single(actualEntities);
        Assert.Equal(newActivity.Id, actualEntities.First().Id);
        Assert.Equal(newActivity.Start, actualEntities.First().Start);
        Assert.Equal(newActivity.End, actualEntities.First().End);
        Assert.Equal(newActivity.Type, actualEntities.First().Type);
        Assert.Equal(newActivity.Description, actualEntities.First().Description);
        Assert.Equal(newActivity.UserId, actualEntities.First().UserId);
        Assert.Equal(newActivity.ProjectId, actualEntities.First().ProjectId);
        Assert.Equal(newActivity.User.Id, actualEntities.First().User.Id);
        Assert.Equal(newActivity.User.Name, actualEntities.First().User.Name);
        Assert.Equal(newActivity.User.Surname, actualEntities.First().User.Surname);
        Assert.Equal(newActivity.User.PhotoUrl, actualEntities.First().User.PhotoUrl);
        Assert.Equal(newActivity.Project.Id, actualEntities.First().Project.Id);
        Assert.Equal(newActivity.Project.Name, actualEntities.First().Project.Name);
        Assert.Equal(newActivity.Project.Creator.Id, actualEntities.First().Project.Creator.Id);
        Assert.Equal(newActivity.Project.Creator.Name, actualEntities.First().Project.Creator.Name);
        Assert.Equal(newActivity.Project.Creator.Surname, actualEntities.First().Project.Creator.Surname);
        Assert.Equal(newActivity.Project.Creator.PhotoUrl, actualEntities.First().Project.Creator.PhotoUrl);
    }
}