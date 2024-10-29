namespace BeautyNest.Models.DTO
{
    public class KategorijaDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Slika { get; set; }=string.Empty;

        public List<SalonDto> Saloni { get; set; } = new List<SalonDto>();
    }
}
