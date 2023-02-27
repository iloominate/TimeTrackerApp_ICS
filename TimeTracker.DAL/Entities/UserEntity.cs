using System.Diagnostics;

namespace TimeTracker.DAL.Entities
{
    public class UserEntity : IEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<ActivityEntity> Activities { get; set; }
        public ICollection<ProjectEntity> Projects { get; set; }
    }
}