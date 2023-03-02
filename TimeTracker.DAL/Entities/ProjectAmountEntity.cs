using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL.Entities
{
    public class ProjectAmountEntity : IEntity
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }

        public UserEntity? User { get; set; }
        public ProjectEntity? Project { get; set; }

    }
}
