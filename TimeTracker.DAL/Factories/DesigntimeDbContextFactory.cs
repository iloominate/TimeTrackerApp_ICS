using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL.Factories
{
    public class DesigntimeDbContextFactory : IDesignTimeDbContextFactory<TimeTrackerDbContext>
    {
        private readonly DbContextSqLiteFactory _dbContextSqLiteFactory;
        private const string ConnectionString = $"Data Source=TimeTracker;Cache=Shared";

        public DesigntimeDbContextFactory() => _dbContextSqLiteFactory = new DbContextSqLiteFactory(ConnectionString);

        public TimeTrackerDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
    }
}
