using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.Domain
{
    public class Salon
    {
        [Key]
        public int Id { get; set; }

        public string Naziv { get; set; }=string.Empty;

        public string Adresa { get; set; } = string.Empty;

        public string KontaktTelefon { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public double ProsjecnaOcjena { get; set; }

        public string Opis { get; set; } = string.Empty;

        public TimeSpan RadnoVrijemeOd { get; set; }

        public TimeSpan RadnoVrijemeDo { get; set; }

        public bool SubotaRadna { get; set; }

        public string NaslovnaFotografija { get; set; } = string.Empty;
        public ICollection<Kategorija> Kategorije { get; set; }

    }
}
