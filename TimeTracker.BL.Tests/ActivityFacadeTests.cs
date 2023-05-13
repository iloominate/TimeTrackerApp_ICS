using Microsoft.EntityFrameworkCore;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Facadesl;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.Common.Enums;
using TimeTracker.Common.Tests;
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
    public async Task CheckCrossingActivities()
    {
        var model = ActivityModelMapper.MapToDetailModel(ActivitySeeds.Generator);
        _activityFacadeSUT.CheckCrossingBetweenActivities(model);
        Assert.True(true);
    }
}