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

        public string KlijentId { get; set; }
        public string SalonNaziv { get; set; }
        public string SalonAdresa { get; set; }
        public List<string> Usluge { get; set; }
        public int Trajanje { get; set; }

    }
}
