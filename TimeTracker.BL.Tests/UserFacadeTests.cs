using Microsoft.EntityFrameworkCore;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.Common.Tests;
using TimeTracker.DAL.Seeds;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests;

public sealed class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;
    
    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var user = await _userFacadeSUT.GetAsync(Guid.Parse("607c5e57-a7a9-4303-beab-9f6726af6b26"));
        Assert.Null(user);
    }
    
    [Fact]
    public async Task GetAll_Single_SeededUser()
    {
        var users = await _userFacadeSUT.GetAsync();
        var user = users.Single(i => i.Id == UserSeeds.JonhUser.Id);
        
        DeepAssert.Equal(UserModelMapper.MapToListModel(UserSeeds.JonhUser), user);
    }

    [Fact]
    public async Task SeededUserWithoutProject_DeleteById_DoesNotThrow()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.JonhUser.Id);
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == UserSeeds.JonhUser.Id));
    }
    
    [Fact]
    public async Task SeededUser_Delete_DeletesAllProjectsHeIsAssignedTo()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.JonhUser.Id);
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Projects.AnyAsync(i => i.Id == ProjectSeeds.SchoolProject.Id));
    }

}