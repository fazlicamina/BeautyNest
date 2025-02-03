using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BeautyNest.Models.Domain
{
    public class Recenzije
    {
        [Key]
        public int Id { get; set; }
        public string KlijentId { get; set; }
        public int SalonId { get; set; }
        [JsonIgnore]
        public Salon Salon { get; set; }
        public int RezervacijaId { get; set; }
        [JsonIgnore]
        public Rezervacija Rezervacija { get; set; }

        [Range(1, 5)]
        public int Ocjena { get; set; }

        public string Tekst { get; set; }

        public DateTime DatumRecenzije { get; set; } = DateTime.UtcNow;

        public string? Slike { get; set; }

    }
}

