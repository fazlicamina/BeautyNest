using BeautyNest.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var klijentRoleId = "70e860a1-ef1a-4c6c-aaad-ce85f38de238";
            var uposlenikRoleId = "cbf297f5-0f16-4294-95cb-bd94ba401f0a";
            var vlasnikRoleId = "fff530b1-5200-4ccf-84e1-8840b57e1ab1";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id=klijentRoleId,
                    Name="Klijent",
                    NormalizedName="Klijent".ToUpper(),
                    ConcurrencyStamp=klijentRoleId
                },
                new IdentityRole()
                {
                    Id=uposlenikRoleId,
                    Name="Uposlenik",
                    NormalizedName="Uposlenik".ToUpper(),
                    ConcurrencyStamp=uposlenikRoleId
                },
                new IdentityRole()
                {
                    Id=vlasnikRoleId,
                    Name="Vlasnik",
                    NormalizedName="Vlasnik".ToUpper(),
                    ConcurrencyStamp=vlasnikRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


            //seedanje klijenta

            var klijentUserId = "41451580-acf7-422e-81b1-3bab4d8213a6";

            var klijent = new User()
            {
                Id = klijentUserId,
                UserName = "fazlicamina02",
                Email = "fazlicamina02@gmail.com",
                NormalizedEmail = "fazlicamina02@gmail.com".ToUpper(),
                NormalizedUserName = "fazlicamina02".ToUpper(),
                FirstName = "Amina",
                LastName = "Fazlić"
            };

            klijent.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(klijent, "Amina123!");

            builder.Entity<User>().HasData(klijent);

            var klijentRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId=klijentUserId, RoleId=klijentRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(klijentRoles);

            //seeding 3 uposlenika

            var uposlenikAdna = "b3b5c3e0-2f78-4f55-badb-d19d328c3240";
            var uposlenikAnida = "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03";
            var uposlenikAldin = "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529";

            var uposlenici = new List<User>
    {
        new User
        {
            Id = uposlenikAdna,
            UserName = "adnah",
            Email = "adnah@gmail.com",
            NormalizedEmail = "ADNAH@GMAIL.COM",
            NormalizedUserName = "ADNAH",
            FirstName = "Adna",
            LastName = "Halilović",
            SalonId=1
        },
        new User
        {
            Id = uposlenikAnida,
            UserName = "anidasabic",
            Email = "anidasabic@gmail.com",
            NormalizedEmail = "ANIDASABIC@GMAIL.COM",
            NormalizedUserName = "ANIDASABIC",
            FirstName = "Anida",
            LastName = "Šabić",
            SalonId=1
        },
        new User
        {
            Id = uposlenikAldin,
            UserName = "aldinh",
            Email = "aldinh@gmail.com",
            NormalizedEmail = "ALDINH@GMAIL.COM",
            NormalizedUserName = "ALDINH",
            FirstName = "Aldin",
            LastName = "Hodžić",
            SalonId=1
        }
    };

            foreach (var uposlenik in uposlenici)
            {
                uposlenik.PasswordHash = new PasswordHasher<User>().HashPassword(uposlenik, "Uposlenik123!");
            }

            builder.Entity<User>().HasData(uposlenici);

            var uposlenikRoles = new List<IdentityUserRole<string>>
    {
        new() { UserId = uposlenikAdna, RoleId = uposlenikRoleId },
        new() { UserId = uposlenikAnida, RoleId = uposlenikRoleId },
        new() { UserId = uposlenikAldin, RoleId = uposlenikRoleId }
    };

            builder.Entity<IdentityUserRole<string>>().HasData(uposlenikRoles);


        }
    }
}
