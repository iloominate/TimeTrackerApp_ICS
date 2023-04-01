using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Seeds
{
    static public class ProjectSeeds
    {
        
        public static readonly ProjectEntity SchoolProject = new()
        {
            Id = Guid.Parse("cb67db40-55f1-4d2f-9569-3bf5694ad802"),
            Name = "IFJ",
            Creator = UserSeeds.KrisWithProject,
            CreatorId = UserSeeds.KrisWithProject.Id
        };

        public static readonly ProjectEntity GameJam = new()
        {
            Id = Guid.Parse("e6790563-6d01-4032-8b3e-3466b4ba43a8"),
            Name = "GAME JAM 2077",
            Creator = UserSeeds.AdamUser,
            CreatorId = UserSeeds.AdamUser.Id
        };

        static ProjectSeeds()
        {
            SchoolProject.Users.Add(ProjectAmountSeeds.SchoolKris);
            SchoolProject.Users.Add(ProjectAmountSeeds.SchoolAdam);

            GameJam.Users.Add(ProjectAmountSeeds.GameJamKris);
            GameJam.Users.Add(ProjectAmountSeeds.GameJamJohn);
        }

        public static void Seed(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<ProjectEntity>().HasData(
                SchoolProject,
                GameJam
                );
    }
}
