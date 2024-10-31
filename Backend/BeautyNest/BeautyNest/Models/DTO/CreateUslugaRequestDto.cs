using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.DTO
{
    public class CreateUslugaRequestDto
    {
        public string Naziv { get; set; }

        public decimal Cijena { get; set; }

        public TimeSpan Trajanje { get; set; }

        public int KategorijaUslugeId { get; set; }
    }
}
