namespace BeautyNest.Models.DTO
{
    public class SalonDto
    {
        public int Id { get; set; }

        public string Naziv { get; set; } = string.Empty;

        public string Adresa { get; set; } = string.Empty;

        public string KontaktTelefon { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public double ProsjecnaOcjena { get; set; }

        public string Opis { get; set; } = string.Empty;

        public TimeSpan RadnoVrijemeOd { get; set; }

        public TimeSpan RadnoVrijemeDo { get; set; }

        public bool SubotaRadna { get; set; }

        public string NaslovnaFotografija { get; set; } = string.Empty;

        public List<KategorijaDto> Kategorije { get; set; } = new List<KategorijaDto>();
    }
}
