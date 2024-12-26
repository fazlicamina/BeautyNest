using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BeautyNest.Models.Domain
{
    public class Rezervacija
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Salon))]
        public int SalonId { get; set; }
        [JsonIgnore]
        public Salon Salon { get; set; }

        [ForeignKey(nameof(Usluga))]
        public int UslugaId { get; set; }
        public Usluga Usluga { get; set; }

        public DateTime DatumRezervacije { get; set; }

        public TimeSpan VrijemePocetka { get; set; }

        public TimeSpan VrijemeZavrsetka { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        public bool Status { get; set; } = false;
    }
}
