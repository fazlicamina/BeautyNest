using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.DTO
{
    public class RecenzijaRequestDto
    {
        [Required]
        public int RezervacijaId { get; set; }

        [Range(1, 5)]
        public int Ocjena { get; set; }

        public string Tekst { get; set; }

        public List<byte[]> Slike { get; set; } = new List<byte[]>();
    }
}
