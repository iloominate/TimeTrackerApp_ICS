using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests
{
    public class DbContextUserTests
    {
        private readonly TimeTrackerDbContext _dbContextSUT;

        public DbContextUserTests()
        {
            _dbContextSUT = new TimeTrackerDbContext();
        }

        [Fact]
        public void AddNewUserTest()
        {
            var newUser = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Surname = "Doe",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            _dbContextSUT.Users.Add(newUser);
            _dbContextSUT.SaveChanges();
        }

        [Fact]
        public void RemoveUserTest()
        {
            var newUser = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Solomon",
                Surname = "Grundy",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            _dbContextSUT.Users.Add(newUser);
            _dbContextSUT.SaveChanges();

            _dbContextSUT.Users.Remove(newUser);
            _dbContextSUT.SaveChanges();

        }
    }
}