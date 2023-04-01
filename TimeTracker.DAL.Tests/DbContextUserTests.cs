using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using TimeTracker.Common.Tests;

namespace TimeTracker.DAL.Tests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNewUserTest()
    {
        var newUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Surname = "Doe",
            PhotoUrl = "http://example.com/photo.jpg",
            Activities = new List<ActivityEntity>(),
            Projects = new List<ProjectAmountEntity>(),
            CreatedProjects = new List<ProjectEntity>()
        };

        TimeTrackerDbContextSUT.Users.Add(newUser);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Users.SingleAsync(i => i.Id == newUser.Id);
        DeepAssert.Equal(newUser, actualEntities);
    }
}