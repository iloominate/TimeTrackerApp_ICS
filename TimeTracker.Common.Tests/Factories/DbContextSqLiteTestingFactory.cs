using TimeTracker.DAL;
using Microsoft.EntityFrameworkCore;

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

        return new TimeTrackerTestingDbContext(builder.Options, _seedTestingData);
    }
}