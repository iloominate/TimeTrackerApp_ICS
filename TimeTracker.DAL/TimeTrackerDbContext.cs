﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.DAL
{
    

    public class TimeTrackerDbContext : DbContext
    {
        private readonly bool _seedDemoData;

        public TimeTrackerDbContext(DbContextOptions contextOptions, bool seedDemoData = false) 
            : base(contextOptions) => 
            _seedDemoData = seedDemoData;

        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProjectAmountEntity>  ProjectAmounts => Set<ProjectAmountEntity>();
    }
}
