namespace BeautyNest.Models.DTO
{
    public class RecenzijaResponseDto
    {
        public int Id { get; set; }
        public string KlijentId { get; set; }
        public int SalonId { get; set; }
        public int RezervacijaId { get; set; }
        public int Ocjena { get; set; }
        public string Tekst { get; set; }
        public List<UslugaDto> Usluge { get; set; } = new List<UslugaDto>();
        public DateTime DatumRecenzije { get; set; }
    }
}
