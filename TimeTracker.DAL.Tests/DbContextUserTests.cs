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
        var newUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Surname = "Doe",
            PhotoUrl = "http://example.com/photo.jpg",
        };

        TimeTrackerDbContextSUT.Users.Add(newUser);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Users.SingleAsync(i => i.Id == newUser.Id);
        DeepAssert.Equal(newUser, actualEntities);
    }

    [Fact]
    //its not work. You are can delete this or fix
    public async Task GetAll_Users_ContainsSeededKris()
    {
        //Act
        var entities = await TimeTrackerDbContextSUT.Users
            //.Where(i => i.Id == UserSeeds.KrisWithProject.Id)
            .ToArrayAsync();

        //Assert
        Assert.Contains(
            UserSeeds.Kris with
            { 
                Activities = Array.Empty<ActivityEntity>(),
                CreatedProjects = Array.Empty<ProjectEntity>(), 
                Projects = Array.Empty<ProjectAmountEntity>()
            },
            entities);
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
            };

        //Act
        dbx.Users.Update(UpdatedEntity);
        await TimeTrackerDbContextSUT.SaveChangesAsync();

        //Assert
        
        var actualEntity = await dbx.Users.SingleAsync(i => i.Id == UpdatedEntity.Id);
        DeepAssert.Equal(UpdatedEntity, actualEntity);
    }
}