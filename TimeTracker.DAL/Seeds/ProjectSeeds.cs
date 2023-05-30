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
            Creator = UserSeeds.Kris,
            CreatorId = UserSeeds.Kris.Id
        };

        public static readonly ProjectEntity GameJam = new()
        {
            Id = Guid.Parse("e6790563-6d01-4032-8b3e-3466b4ba43a8"),
            Name = "GAME JAM 2077",
            Creator = UserSeeds.JonhUser,
            CreatorId = UserSeeds.JonhUser.Id
        };

        public static readonly ProjectEntity Startup = new()
        {
            Id = Guid.Parse("3a1db597-dc1e-424c-ae03-32f7a5cbd889"),
            Name = "NEW STARTUP!!!",
            Creator = UserSeeds.AdamUser,
            CreatorId = UserSeeds.AdamUser.Id
        };

        public static readonly ProjectEntity GrillDay = new()
        {
            Id = Guid.Parse("72e6029a-2294-4e74-96a1-6b57bf648fd5"),
            Name = "Grill day organazation (TeamBuilding)",
            Creator = UserSeeds.UserWithCreatedProject,
            CreatorId = UserSeeds.UserWithCreatedProject.Id
        };

        public static readonly ProjectEntity HouseBuilding = new()
        {
            Id = Guid.Parse("0f2dd3e8-5045-4aba-b71e-910d3cac8934"),
            Name = "Build house for director)",
            Creator = UserSeeds.Kris,
            CreatorId = UserSeeds.Kris.Id
        };

        static ProjectSeeds()
        {
            SchoolProject.Users.Add(ProjectAmountSeeds.SchoolKris);
            SchoolProject.Users.Add(ProjectAmountSeeds.SchoolAdam);
            SchoolProject.Activities.Add(ActivitySeeds.Generator);
            SchoolProject.Activities.Add(ActivitySeeds.Syntax);

            GameJam.Users.Add(ProjectAmountSeeds.GameJamKris);
            GameJam.Users.Add(ProjectAmountSeeds.GameJamJohn);
            GameJam.Activities.Add(ActivitySeeds.LevelDesign);
            GameJam.Activities.Add(ActivitySeeds.MovementLogic);

            HouseBuilding.Users.Add(ProjectAmountSeeds.HouseKris);

            Startup.Activities.Add(ActivitySeeds.WoodChopping);
        }

        public static void Seed(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<ProjectEntity>().HasData(
                HouseBuilding with { Creator = null, Users = Array.Empty<ProjectAmountEntity>(), Activities = Array.Empty<ActivityEntity>() },
                GrillDay with { Creator = null, Users = Array.Empty<ProjectAmountEntity>(), Activities = Array.Empty<ActivityEntity>() },
                Startup with { Creator = null, Users = Array.Empty<ProjectAmountEntity>(), Activities = Array.Empty<ActivityEntity>() },
                SchoolProject with {Creator = null, Users = Array.Empty<ProjectAmountEntity>(), Activities = Array.Empty<ActivityEntity>() },
                GameJam with { Creator = null, Users = Array.Empty<ProjectAmountEntity>(), Activities = Array.Empty<ActivityEntity>() }
                
                );
    }
}
