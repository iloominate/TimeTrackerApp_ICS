using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Seeds
{
    public static class ProjectAmountSeeds
    {

        public static readonly ProjectAmountEntity SchoolKris = new()
        {
            Id = Guid.Parse("5f690985-18d7-484a-824b-147e1c71cbc4"),
            ProjectId = ProjectSeeds.SchoolProject.Id,
            UserId = UserSeeds.KrisWithProject.Id,
            Project = ProjectSeeds.SchoolProject,
            User = UserSeeds.KrisWithProject
        };

        public static readonly ProjectAmountEntity SchoolAdam = new()
        {
            Id = Guid.Parse("db7afb18-71a5-45bd-86c5-6bf76ecedaec"),
            ProjectId = ProjectSeeds.SchoolProject.Id,
            UserId = UserSeeds.AdamUser.Id,
            Project = ProjectSeeds.SchoolProject,
            User = UserSeeds.AdamUser
        };

        public static readonly ProjectAmountEntity GameJamKris = new()
        {
            Id = Guid.Parse("18d3339f-7f40-4d1c-a49d-ef913081bcb9"),
            ProjectId = ProjectSeeds.GameJam.Id,
            UserId = UserSeeds.KrisWithProject.Id,
            Project = ProjectSeeds.GameJam,
            User = UserSeeds.KrisWithProject
        };

        public static readonly ProjectAmountEntity GameJamJohn = new()
        {
            Id = Guid.Parse("18d3339f-7f40-4d1c-a49d-ef913081bcb9"),
            ProjectId = ProjectSeeds.GameJam.Id,
            UserId = UserSeeds.JonhUser.Id,
            Project = ProjectSeeds.GameJam,
            User = UserSeeds.JonhUser
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<ProjectAmountEntity>().HasData(
                SchoolKris,
                SchoolAdam,
                GameJamJohn,
                GameJamKris
                );
    }
}
