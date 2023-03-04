using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests;

public class DbContextProjectTests : DbContextTestsBase
{
    public DbContextProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNewProjectTest()
    {
        var newProject = new ProjectEntity
        {
            Id = Guid.NewGuid(),
            Name = "Project 1",
            CreatorId = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "Chad",
                Surname = "Watts",
                PhotoUrl = "https://www.google.com/"
            }
        };
            
        TimeTrackerDbContextSUT.Projects.Add(newProject);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Projects.SingleAsync(i => i.Id == newProject.Id);
        Assert.Equal(newProject, actualEntities);
    }
}