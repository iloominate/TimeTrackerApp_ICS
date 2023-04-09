using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using TimeTracker.Common.Tests;
using TimeTracker.DAL.Seeds;

namespace TimeTracker.DAL.Tests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNewUserTest()
    {
        // Arrange
        var newUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Surname = "Doe",
            PhotoUrl = "http://example.com/photo.jpg",
        };

        // Act
        TimeTrackerDbContextSUT.Users.Add(newUser);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Users.SingleAsync(i => i.Id == newUser.Id);

        DeepAssert.Equal(newUser, actualEntities);
    }

    [Fact]
    public async Task GetAll_Users_ContainsSeededKris()
    {
        //Act
        var entities = await TimeTrackerDbContextSUT.Users
            .Where(i => i.Id == UserSeeds.Kris.Id)
            .ToArrayAsync();

        var actual = entities.First();
        var expected = UserSeeds.Kris with
        {
            Activities = new List<ActivityEntity>(),
            CreatedProjects = new List<ProjectEntity>(),
            Projects = new List<ProjectAmountEntity>()
        };

        //Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Surname, actual.Surname);
        Assert.Equal(expected.PhotoUrl, actual.PhotoUrl);
        Assert.True(expected.Projects.SequenceEqual(actual.Projects));
        Assert.True(expected.Activities.SequenceEqual(actual.Activities));
        Assert.True(expected.CreatedProjects.SequenceEqual(actual.CreatedProjects));
    }

    [Fact]
    public async Task Update_UserInformation()
    {
        //Arrange
        var baseEntity = UserSeeds.KrisUpdate;
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var entity = await TimeTrackerDbContextSUT.Users.SingleAsync(i => i.Id == baseEntity.Id);

        var UpdatedEntity =
            entity with
            {
                Name = entity.Name + " Updated",
                Surname = entity.Surname + " Updated",
                PhotoUrl = entity.PhotoUrl + " Updated",
            };

        //Act
        dbx.Users.Update(UpdatedEntity);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        //Assert
        var actualEntity = await dbx.Users.SingleAsync(i => i.Id == UpdatedEntity.Id);
        DeepAssert.Equal(UpdatedEntity, actualEntity);
    }

    [Fact]
    public async Task DeleteById_User()
    {
        //Arrange
        var baseEntity = UserSeeds.KrisDelete;

        //Act
        TimeTrackerDbContextSUT.Users.Remove(
            TimeTrackerDbContextSUT.Users.Single(i => i.Id == baseEntity.Id));
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TimeTrackerDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
    }
}