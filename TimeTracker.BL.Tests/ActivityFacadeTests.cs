using Microsoft.EntityFrameworkCore;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Facadesl;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.Common.Enums;
using TimeTracker.Common.Tests;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Seeds;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests;

public sealed  class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;
    
    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var activity = await _activityFacadeSUT.GetAsync(Guid.Parse("607c5e57-a7a9-4303-beab-9f6726af6b26"));
        Assert.Null(activity);
    }


    [Fact]
    public async Task AddNewActivity_Crossing()
    {
        var model = ActivityModelMapper.MapToDetailModel(ActivitySeeds.Generator);

        _ = Assert.ThrowsAsync<DbUpdateException>(() => _activityFacadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task AddNewActivity_NotCrossing()
    {
        var model = ActivitySeeds.LevelDesign with
        {
            Id = Guid.Parse("836b3e93-fa20-4c93-a7ea-1ab4f59a32a8"),
            Start = DateTime.Parse("05/05/2023 10:20:00"),
            End = DateTime.Parse("05/05/2023 11:00:00"),
        };

        await _activityFacadeSUT.SaveAsync(ActivityModelMapper.MapToDetailModel(model));
        Assert.True(true);
    }
}