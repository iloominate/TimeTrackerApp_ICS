using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Seeds
{
    public static class UserSeeds
    {
        public static readonly UserEntity JonhUser = new()
        {
            Id = Guid.Parse("facd3e3b-42b0-423c-851c-8d81032b7c3e"),
            Name = "John",
            Surname = "Smeet",
            PhotoUrl = @"https://twizz.ru/wp-content/uploads/2019/02/bez-nazvaniya-7.jpg"
        };

        public static readonly UserEntity AdamUser = new()
        {
            Id = Guid.Parse("70f386a7-a8af-42ba-9d45-816570af3645"),
            Name = "Adam",
            Surname = "Silvester",
            PhotoUrl = @"https://cdn.vectorstock.com/i/1000x1000/77/50/man-person-thinking-icon-vector-10457750.webp"
        };

        public static readonly UserEntity Kris = new()
        {
            Id = Guid.Parse("9cd38b1e-e53a-45b2-bd7c-60ae5ffbd358"),
            Name = "Kris",
            Surname = "Kortes",
            PhotoUrl = "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
        };

        static UserSeeds()
        {
            JonhUser.Projects.Add(ProjectAmountSeeds.GameJamJohn);
            JonhUser.Activities.Add(ActivitySeeds.LevelDesign);

            AdamUser.Projects.Add(ProjectAmountSeeds.SchoolAdam);
            AdamUser.Activities.Add(ActivitySeeds.Generator);

            Kris.Projects.Add(ProjectAmountSeeds.GameJamKris);
            Kris.Projects.Add(ProjectAmountSeeds.SchoolKris);
            Kris.Activities.Add(ActivitySeeds.Syntax);
            Kris.Activities.Add(ActivitySeeds.MovementLogic);
        }

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<UserEntity>().HasData(
                JonhUser with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<ProjectAmountEntity>(), CreatedProjects = Array.Empty<ProjectEntity>() },
                AdamUser with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<ProjectAmountEntity>(), CreatedProjects = Array.Empty<ProjectEntity>() },
                Kris with { Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<ProjectAmountEntity>(), CreatedProjects = Array.Empty<ProjectEntity>() },
            );
    }
}
