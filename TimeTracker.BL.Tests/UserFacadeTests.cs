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
    public async Task SeededUser_DeleteById_DoesNotThrow_WithoutRemoveFromProject()
    {

        await _userFacadeSUT.DeleteAsync(UserSeeds.JonhUser.Id);
        
        //await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.True(await _userFacadeSUT.GetAsync(UserSeeds.JonhUser.Id) is null);
    }

    [Fact]
    public async Task SeededUser_NewUser()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.NewUser.Id);

        Assert.True(await _userFacadeSUT.GetAsync(UserSeeds.NewUser.Id) is null);
    }

    [Fact]
    public async Task SeededUser_WithCreatedProject()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserWithCreatedProject.Id);

        Assert.True(await _userFacadeSUT.GetAsync(UserSeeds.UserWithCreatedProject.Id) is null);
    }

    

}