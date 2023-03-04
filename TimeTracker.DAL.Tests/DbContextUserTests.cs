using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

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
            PhotoUrl = "http://example.com/photo.jpg"
        };

        TimeTrackerDbContextSUT.Users.Add(newUser);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Users.SingleAsync(i => i.Id == newUser.Id);
        Assert.Equal(newUser, actualEntities);
    }
}