namespace TimeTracker.DAL.Entities;

public class ActivityEntity : IEntity
{
    public Guid ID { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string ActivityType { get; set; }
    public string Description { get; set; }
    public int UserID { get; set; }
    public UserEntity User { get; set; }
    public int ProjectID { get; set; }
    public ProjectEntity Project { get; set; }
}