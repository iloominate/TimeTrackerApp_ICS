using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimeTracker.DAL.Entities;
namespace TimeTracker.Common.Tests.Seeds
{
    public static class UserSeeds
    {
        /*
        d97769d8-3db4-43b8-bc3c-5aef580d8d6a
        80d176fc-358d-4c83-84a8-47af9b051512
        d91f5a92-aee7-49cb-8a1a-2a7c06396c54
        22cebcd6-f458-4cfb-b509-ca7579422119
        89cf18b6-2c18-4a65-8556-1705e2238896
        079cd181-96a9-4abc-8661-e4960277a8c1
        b3a231e6-2166-4286-946f-2e0520a953ae
        2526210c-9fea-4027-9cc2-082e86bdc2f3
        582ea6b6-6d61-42e6-82a6-b35b5255bc84
        647d5042-67b2-43ed-a4eb-077df21bea4b
        75ae42c9-81f4-4da8-bafd-263a5430f208
        48902492-63b4-415e-abb5-330acfc40bb9
        cc714ea4-6cf1-4058-b964-d4443a824b87
        71abe31a-7311-4e61-8e78-010d2d8d9493
        9c113245-090e-4bf0-9195-97341d92f39a
        396a9ee7-96a2-47a4-bc3e-280826d2ff3c
        b0f2c10c-ed1b-407b-892e-502cbd329dd6
        b4290f3e-9f01-4afd-9b8b-962eedd0bdf4
         * */
        public static readonly UserEntity EmptyUserEntity = new()
        {
            Id = default,
            Name = default!,
            Surname = default!,
            PhotoUrl = default!,
        };
        public static readonly UserEntity Kris = new()
        {
            Id = Guid.Parse("53525bb6-2647-417c-aaa8-545cec5c3b1c"),
            Name = "Kris",
            Surname = "Stratulat",
            PhotoUrl = "https://twizz.ru/wp-content/uploads/2019/02/bez-nazvaniya-7.jpg",
        };
        //public static readonly UserEntity KrisUpdate = 
        public static readonly UserEntity UserEntity1 = new()
        {
            Id = Guid.Parse("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
            Name = "Seeded Name User 1",
            Surname = "Seeded Surname User 1",
            PhotoUrl = "https://ps.w.org/user-avatar-reloaded/assets/icon-128x128.png?rev=2540745"
        };
        public static readonly UserEntity UserEntity2 = new()
        {
            Id = Guid.Parse("5147a000-6246-4dc3-8819-769b6db484b1"),
            Name = "Seeded Name User 2",
            Surname = "Seeded Surname User 2",
            PhotoUrl = "https://ps.w.org/user-avatar-reloaded/assets/icon-128x128.png?rev=2540745"
        };
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                UserEntity1,
                UserEntity2,
                Kris
                );
        }
    }
}
