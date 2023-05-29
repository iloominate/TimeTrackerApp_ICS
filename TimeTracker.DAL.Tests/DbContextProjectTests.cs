using TimeTracker.DAL.Entities;
using TimeTracker.Common.Tests;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using TimeTracker.DAL.Seeds;

namespace TimeTracker.DAL.Tests;

public class DbContextProjectTests : DbContextTestsBase
{
    public DbContextProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNewProjectTest()
    {
        var userGuid = Guid.NewGuid();
        var newProject = new ProjectEntity
        {
            Id = Guid.NewGuid(),
            Name = "Project 1",
            Creator = new UserEntity
            {
                Id = userGuid,
                Name = "Chad",
                Surname = "Watts",
                PhotoUrl = "https://www.google.com/"
            },
            CreatorId = userGuid,
            
        };
        
            
        TimeTrackerDbContextSUT.Projects.Add(newProject);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Projects
            .Include(i => i.Creator)
            .SingleAsync(i => i.Id == newProject.Id);
        DeepAssert.Equal(newProject, actualEntities);
    }

    [Fact]
    public async Task GetById_WithUsers()
    {
        var entity = await TimeTrackerDbContextSUT.Projects
                            .Include(i => i.Users)
                            .ThenInclude(i => i.User)
                            .SingleAsync(i => i.Id == ProjectSeeds.GameJam.Id);

        

        //Assert
        DeepAssert.Equal(ProjectSeeds.GameJam, entity);
    }

    [Fact]
    public async Task GetById_Project()
    {
        var project = await TimeTrackerDbContextSUT.Projects
            .SingleAsync(i => i.Id == ProjectSeeds.GrillDay.Id);

        DeepAssert.Equal(ProjectSeeds.GrillDay with { Creator = null}, project);
    }

    [Fact]
    public async Task DeleteExistProject_WithoutActivities()
    {
        var project = ProjectSeeds.GrillDay with { Creator = null};
        Assert.NotNull(project);

        TimeTrackerDbContextSUT.Projects.Remove(project);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        Assert.False(await TimeTrackerDbContextSUT.Projects.AnyAsync(p => p.Id == project.Id));
    }

    [Fact]
    public async Task DeleteProject_WithUsers()
    {
        var project = TimeTrackerDbContextSUT.Projects
            .Include(i => i.Users)
            .ThenInclude(i => i.User)
            .Include(i => i.Creator)
            .Single(i => i.Id == ProjectSeeds.HouseBuilding.Id);
            
        
        Assert.NotNull(project);

        //project.Users.Clear();
        //project.Users.Add(ProjectAmountSeeds.GameJamKris);
        //project.Users.Add(ProjectAmountSeeds.GameJamJohn);

        TimeTrackerDbContextSUT.Remove(project);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        Assert.False(await TimeTrackerDbContextSUT.Projects.AnyAsync(i => i.Id == project.Id));
        Assert.False(await TimeTrackerDbContextSUT.ProjectAmounts
            .AnyAsync(i => project.Users.Select(projAmount => projAmount.Id).Contains(i.Id)));
    }

    [Fact]
    public async Task DeleteProject_WithActivity()
    {
        var project = TimeTrackerDbContextSUT.Projects
            .Include(i => i.Users)
            .ThenInclude(i => i.User)
            .Include(i => i.Creator)
            .Single(i => i.Id == ProjectSeeds.Startup.Id);


        Assert.NotNull(project);

        //project.Users.Clear();
        //project.Users.Add(ProjectAmountSeeds.GameJamKris);
        //project.Users.Add(ProjectAmountSeeds.GameJamJohn);

        TimeTrackerDbContextSUT.Remove(project);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        Assert.False(await TimeTrackerDbContextSUT.Projects.AnyAsync(i => i.Id == project.Id));
        Assert.False(await TimeTrackerDbContextSUT.ProjectAmounts
            .AnyAsync(i => project.Users.Select(projAmount => projAmount.Id).Contains(i.Id)));
    }
}