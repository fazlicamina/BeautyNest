using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.Domain
{
    public class UslugaRezervacija
    {

        public int UslugaId { get; set; }
        public Usluga Usluga { get; set; }

        public int RezervacijaId { get; set; }
        public Rezervacija Rezervacija { get; set; }

    }
}
