﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Seeds;

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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UserEntity>()
                .HasMany<ProjectAmountEntity>(i => i.Projects)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<ProjectEntity>()
                .HasMany<ProjectAmountEntity>(i => i.Users)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ActivityEntity>()
                .HasOne<ProjectEntity>(i => i.Project)
                .WithMany(i => i.Activities)
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ActivityEntity>()
                .HasOne(i => i.User)
                .WithMany(i => i.Activities)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            if (!_seedDemoData) return;
            
            /*UserSeeds.Seed(modelBuilder);   
            ProjectSeeds.Seed(modelBuilder);
            ProjectAmountSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);*/
        }

    }
}
