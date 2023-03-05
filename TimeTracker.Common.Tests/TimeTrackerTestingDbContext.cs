using TimeTracker.DAL;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Common.Tests;

public class TimeTrackerTestingDbContext : TimeTrackerDbContext
{
    private readonly bool _seedTestingData;

    public TimeTrackerTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
        : base(contextOptions, seedDemoData: false)
    {
        _seedTestingData = seedTestingData;
    }
}