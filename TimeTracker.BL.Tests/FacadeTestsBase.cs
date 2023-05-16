global using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TimeTracker.BL.Mappers;
using TimeTracker.Common.Tests;
using TimeTracker.Common.Tests.Factories;
using TimeTracker.DAL;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);
        
        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        
        ActivityEntityMapper = new ActivityEntityMapper();
        ProjectAmountEntityMapper = new ProjectAmountEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        UserEntityMapper = new UserEntityMapper();

        ActivityModelMapper = new ActivityModelMapper();
        ProjectAmountModelMapper = new ProjectAmountModelMapper();
        ProjectModelMapper = new ProjectModelMapper(ActivityModelMapper,ProjectAmountModelMapper);
        UserModelMapper = new UserModelMapper(ActivityModelMapper,ProjectModelMapper,ProjectAmountModelMapper);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }
    
    protected IDbContextFactory<TimeTrackerDbContext> DbContextFactory { get; }
    
    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected ProjectAmountEntityMapper ProjectAmountEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected UserEntityMapper UserEntityMapper { get; }
    
    protected IActivityModelMapper ActivityModelMapper { get; }
    protected IProjectAmountModelMapper ProjectAmountModelMapper { get; }
    protected IProjectModelMapper ProjectModelMapper { get; }
    protected IUserModelMapper UserModelMapper { get; }
    
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }
    
    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }
    
    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
    
}
