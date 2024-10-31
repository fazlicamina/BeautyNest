using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.Domain
{
    public class Usluga
    {
        [Key]
        public int Id { get; set; }

        public string Naziv { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cijena { get; set; }

        public TimeSpan Trajanje { get; set; }

        public int KategorijaUslugeId { get; set; }

        [ForeignKey("KategorijaUslugeId")]
        public KategorijaUsluge KategorijaUsluge { get; set; }

    }
}
