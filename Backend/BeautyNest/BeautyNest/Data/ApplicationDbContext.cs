using BeautyNest.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeautyNest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Salon> Saloni { get; set; }
        public DbSet<Kategorija> Kategorije{ get; set; }

        public DbSet<KategorijaUsluge> KategorijeUsluga { get; set; }
        public DbSet<Usluga> Usluge { get; set; }

        public DbSet<Grad> Gradovi { get; set; }    
   
    }
}
