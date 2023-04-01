using TimeTracker.DAL;
using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Seeds;

namespace TimeTracker.Common.Tests.Factories;

public class DbContextSqLiteTestingFactory : IDbContextFactory<TimeTrackerDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSqLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public TimeTrackerDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TimeTrackerDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        return new TimeTrackerTestingDbContext(builder.Options, _seedTestingData);
    }
}