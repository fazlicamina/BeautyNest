using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.DTO
{
    public class RecenzijaRequestDto
    {
        [Required]
        public int RezervacijaId { get; set; }

        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int Ocjena { get; set; }


        [Required(ErrorMessage = "Tekst recenzije je obavezan.")]
        public string Tekst { get; set; }

        public List<IFormFile>? Slike { get; set; }
    }
}
