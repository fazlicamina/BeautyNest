namespace BeautyNest.Models.DTO
{
    public class UslugaDto
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public decimal Cijena { get; set; }

        public TimeSpan Trajanje { get; set; }
        public int KategorijaUslugeId { get; set; }

        public string KategorijaNaziv { get; set; } = string.Empty;
    }
}
