namespace BeautyNest.Models.DTO
{
    public class RezervacijaDto
    {
        public int Id { get; set; }
        public int SalonId { get; set; }
        public List<UslugaDto> Usluge { get; set; }
        public string UserId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public TimeSpan VrijemePocetka { get; set; }
        public TimeSpan VrijemeZavrsetka { get; set; }
        public bool Status { get; set; }
    }
}
