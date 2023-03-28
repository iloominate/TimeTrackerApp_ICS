﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.DAL.Seeds
{
    public static class ActivitySeeds
    {

        /*
        
        
        
        
        f0550f74-a7b8-4e76-bd4d-996b2506e50e
        */
        public static readonly ActivityEntity MovementLogic = new()
        {
            Id = Guid.Parse("6f8edb5a-7f72-45e0-8845-c28e69329753"),
            Start = DateTime.Parse("01/01/2023 10:20:00"),
            End = DateTime.Parse("01/01/2023 23:00:00"),
            Type = ActivityType.Work,
            Description = "Working Movement Logic for Game",
            User = UserSeeds.KrisWithProject,
            Project = ProjectSeeds.GameJam,
            UserId = UserSeeds.KrisWithProject.Id,
            ProjectId = ProjectSeeds.GameJam.Id
        };

        public static readonly ActivityEntity LevelDesign = new()
        {
            Id = Guid.Parse("ab6f9735-39f8-4517-a5f2-677552588f41"),
            Start = DateTime.Parse("01/01/2023 12:30:00"),
            End = DateTime.Parse("01/01/2023 16:00:00"),
            Type = ActivityType.Work,
            Description = "Level Design for Game",
            User = UserSeeds.JonhUser,
            Project = ProjectSeeds.GameJam,
            UserId = UserSeeds.JonhUser.Id,
            ProjectId = ProjectSeeds.GameJam.Id
        };

        public static readonly ActivityEntity Syntax = new()
        {
            Id = Guid.Parse("42be7be1-2339-44ca-b484-6dda12452bca"),
            Start = DateTime.Parse("01/03/2023 10:00:00"),
            End = DateTime.Parse("01/03/2023 13:00:00"),
            Type = ActivityType.Studying,
            Description = "ITS HELL!!!",
            User = UserSeeds.KrisWithProject,
            Project = ProjectSeeds.SchoolProject,
            UserId = UserSeeds.KrisWithProject.Id,
            ProjectId = ProjectSeeds.SchoolProject.Id
        };

        public static readonly ActivityEntity Generator = new()
        {
            Id = Guid.Parse("1848eb4c-9e4a-4aef-840c-7037236c1cc7"),
            Start = DateTime.Parse("01/03/2023 11:00:00"),
            End = DateTime.Parse("01/03/2023 15:00:00"),
            Type = ActivityType.Studying,

            User = UserSeeds.AdamUser,
            Project = ProjectSeeds.SchoolProject,
            UserId = UserSeeds.AdamUser.Id,
            ProjectId = ProjectSeeds.SchoolProject.Id
        };

        public static void Seed(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<ActivityEntity>().HasData(
                Generator,
                Syntax,
                LevelDesign,
                MovementLogic
                );
    }
}
