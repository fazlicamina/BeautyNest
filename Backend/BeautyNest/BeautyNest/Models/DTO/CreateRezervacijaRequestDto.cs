namespace BeautyNest.Models.DTO
{
    public class CreateRezervacijaRequestDto
    {
        public int SalonId { get; set; }
        public int UslugaId { get; set; }
        public string UserId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public TimeSpan VrijemePocetka { get; set; }
    }
}
