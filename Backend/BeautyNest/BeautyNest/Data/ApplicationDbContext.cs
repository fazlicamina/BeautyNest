using BeautyNest.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static System.Net.WebRequestMethods;

namespace BeautyNest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Salon> Saloni { get; set; }
        public DbSet<Kategorija> Kategorije{ get; set; }

        public DbSet<KategorijaUsluge> KategorijeUsluga { get; set; }
        public DbSet<Usluga> Usluge { get; set; }

        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }

        public static void Initialize(IServiceProvider serviceProvider, bool isDevelopment)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Kategorije.Any())
            {
                return;
            }
            dbContext.SaveChanges();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<User>()
                .ToTable("User");

            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.Salon)
                .WithMany(s => s.Rezervacije)
                .HasForeignKey(r => r.SalonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.Usluga)
                .WithMany(u => u.Rezervacije)
                .HasForeignKey(r => r.UslugaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rezervacije)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usluga>()
                .HasOne(u => u.KategorijaUsluge)
                .WithMany(k => k.Usluge)
                .HasForeignKey(u => u.KategorijaUslugeId)
                .OnDelete(DeleteBehavior.SetNull);
        }



    }
}
