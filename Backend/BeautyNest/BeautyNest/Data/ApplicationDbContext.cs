using BeautyNest.Models.Domain;
using Microsoft.EntityFrameworkCore;
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

        public static void Initialize(IServiceProvider serviceProvider, bool isDevelopment)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Kategorije.Any())
            {
                return;
            }

            // Save Changes to DB
            dbContext.SaveChanges();
        }


    }
}
