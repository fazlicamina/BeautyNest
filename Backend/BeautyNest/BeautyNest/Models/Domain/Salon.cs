using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        //slike
        [JsonIgnore]
        public ICollection<SalonSlika> GalerijaSlika { get; set; } = new List<SalonSlika>();


        public TimeSpan RadnoVrijemeOd { get; set; }

        public TimeSpan RadnoVrijemeDo { get; set; }

        public bool SubotaRadna { get; set; }

        public string NaslovnaFotografija { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Kategorija> Kategorije { get; set; }

        [JsonIgnore]
        public ICollection<KategorijaUsluge> KategorijeUsluga { get; set; } = new List<KategorijaUsluge>();

     
        public int? GradId { get; set; }
        public Grad Grad { get; set; }

        [JsonIgnore]
        public ICollection<Rezervacija> Rezervacije { get; set; } = new List<Rezervacija>();


    }
}
