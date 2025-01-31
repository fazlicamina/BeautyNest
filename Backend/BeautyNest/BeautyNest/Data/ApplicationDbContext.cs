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

        public DbSet<OmiljeniSalon> OmiljeniSaloni { get; set; }

        public DbSet<Salon> Saloni { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }

        public DbSet<KategorijaUsluge> KategorijeUsluga { get; set; }
        public DbSet<Usluga> Usluge { get; set; }

        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<SalonSlika> SalonSlike { get; set; }

        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<UslugaRezervacija> UslugeRezervacije { get; set; }

        public DbSet<Recenzije> Recenzije { get; set; }

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

            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.Salon)
                .WithMany(s => s.Rezervacije)
                .HasForeignKey(r => r.SalonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usluga>()
                .HasOne(u => u.KategorijaUsluge)
                .WithMany(k => k.Usluge)
                .HasForeignKey(u => u.KategorijaUslugeId)
                .OnDelete(DeleteBehavior.SetNull);

            //usluga rezervacija medjutabela

            modelBuilder.Entity<UslugaRezervacija>()
        .HasKey(ur => new { ur.UslugaId, ur.RezervacijaId });

        
            modelBuilder.Entity<UslugaRezervacija>()
                .HasOne(ur => ur.Usluga)
                .WithMany(u => u.UslugeRezervacija)
                .HasForeignKey(ur => ur.UslugaId)
                .OnDelete(DeleteBehavior.NoAction);  

            modelBuilder.Entity<UslugaRezervacija>()
                .HasOne(ur => ur.Rezervacija)
                .WithMany(r => r.UslugeRezervacija)
                .HasForeignKey(ur => ur.RezervacijaId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
    }
