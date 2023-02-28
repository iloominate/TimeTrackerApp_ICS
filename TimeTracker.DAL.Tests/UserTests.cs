using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Tests
{
    public class UserTests
    {
        private readonly TimeTrackerDbContext _dbContextSUT;

        public UserTests()
        {
            _dbContextSUT = new TimeTrackerDbContext();
        }

        [Fact]
        public void NewUser_Add_Added()
        {
            var user = new UserEntity()
            {
                ID = Guid.NewGuid(),
                Name = "John",
                Surname = "Doe",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            _dbContextSUT.Users.Add(user);
            _dbContextSUT.SaveChanges();
        }
    }
}