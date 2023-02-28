using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.DAL
{
    public class TimeTrackerDbContext : DbContext
    {
        public DbSet<Entities.ActivityEntity> Activities => Set<Entities.ActivityEntity>();
        public DbSet<Entities.ProjectEntity> Projects => Set<Entities.ProjectEntity>();
        public DbSet<Entities.UserEntity> Users => Set<Entities.UserEntity>();
    }
}
