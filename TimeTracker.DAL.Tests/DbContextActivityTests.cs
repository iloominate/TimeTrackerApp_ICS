using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests
{
    public class DbContextActivityTests
    {
        private readonly TimeTrackerDbContext _dbContextSUT;

        public DbContextActivityTests()
        {
            _dbContextSUT = new TimeTrackerDbContext();
        }

        [Fact]
        public void AddNewActivityTest()
        {
            var activity = new ActivityEntity()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                ActivityType = "Coding",
                Description = "Coding some stuff",
                UserId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
            };

            _dbContextSUT.Activities.Add(activity);
            _dbContextSUT.SaveChanges();
        }
        
        [Fact]
        public void RemoveActivityTest()
        {
            var activity = new ActivityEntity()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                ActivityType = "Cleaning",
                Description = "Cleaning my room",
                UserId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
            };

            _dbContextSUT.Activities.Add(activity);
            _dbContextSUT.SaveChanges();
            
            _dbContextSUT.Activities.Remove(activity);
            _dbContextSUT.SaveChanges();
        }
    }
}