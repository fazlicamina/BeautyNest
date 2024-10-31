namespace BeautyNest.Models.DTO
{
    public class KategorijaUslugeDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;

        public List<UslugaDto> Usluge { get; set; } = new List<UslugaDto>();

        public int SalonId { get; set; }
    }
}
