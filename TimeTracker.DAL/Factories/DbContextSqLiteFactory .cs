using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL.Factories
{
    public class DbContextSqLiteFactory : IDbContextFactory<TimeTrackerDbContext>
    {
        private readonly bool _seedTestingData;
        private readonly DbContextOptionsBuilder<TimeTrackerDbContext> _contextOptionsBuilder = new();
        public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
        {
            _seedTestingData = seedTestingData;

            _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
        }

        public TimeTrackerDbContext CreateDbContext() =>  new(_contextOptionsBuilder.Options, _seedTestingData);
    }
}
