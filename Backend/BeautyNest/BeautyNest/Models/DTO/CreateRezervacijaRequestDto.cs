namespace BeautyNest.Models.DTO
{
    public class CreateRezervacijaRequestDto
    {
        public int SalonId { get; set; }
        public List<int> UslugaIds { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public TimeSpan VrijemePocetka { get; set; }
    }
}
