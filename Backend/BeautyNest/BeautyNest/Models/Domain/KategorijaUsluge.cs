using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautyNest.Models.Domain
{
    public class KategorijaUsluge
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }=string.Empty;

        [ForeignKey("Salon")]
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
        public ICollection<Usluga> Usluge { get; set; }
    }
}
