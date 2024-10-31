using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.Domain
{
    public class KategorijaUsluge
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }=string.Empty;
        public ICollection<Usluga> Usluge { get; set; }
    }
}
