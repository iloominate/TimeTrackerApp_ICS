using TimeTracker.BL.Facades;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.Common.Tests;
using TimeTracker.DAL.Seeds;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests;

public sealed class ProjectFacadeTests : FacadeTestsBase
{
    private readonly IProjectFacade _projectFacadeSUT;

    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        // Arrange
        var project = await _projectFacadeSUT.GetAsync(Guid.Parse("607c5e57-a7a9-4303-beab-9f6726af6b26"));
        
        // Assert
        Assert.Null(project);
    }
    [Fact]
    public async Task GetById_Existent()
    {
        var projects = await _projectFacadeSUT.GetAsync();
        var project = projects.Single(i => i.Id == ProjectSeeds.GameJam.Id);

        DeepAssert.Equal(ProjectModelMapper.MapToListModel(ProjectSeeds.GameJam),project);
    }

    [Fact]
    public async Task DeleteById_WithoutUsersAndActivities()
    {
        //var project = await _projectFacadeSUT.GetAsync(ProjectSeeds.GrillDay.Id);
        //Assert.Null(project);

        await _projectFacadeSUT.DeleteAsync(ProjectSeeds.GrillDay.Id);

        Assert.True(await _projectFacadeSUT.GetAsync(ProjectSeeds.GrillDay.Id) is null);
    }

    [Fact]
    public async Task DeleteById_WithUsersAndActivities()
    {
        await _projectFacadeSUT.DeleteAsync(ProjectSeeds.GameJam.Id);

        Assert.True(await _projectFacadeSUT.GetAsync(ProjectSeeds.GameJam.Id) is null);
    }



}