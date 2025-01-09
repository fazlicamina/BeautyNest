namespace BeautyNest.Models.DTO
{
    public class RezervacijaDto
    {
        
            public int Id { get; set; }
            public int SalonId { get; set; }
            public DateTime DatumRezervacije { get; set; }
            public TimeSpan VrijemePocetka { get; set; }
            public TimeSpan VrijemeZavrsetka { get; set; }
            public bool Status { get; set; }
            public string Poruka { get; set; }
        

    }
}
