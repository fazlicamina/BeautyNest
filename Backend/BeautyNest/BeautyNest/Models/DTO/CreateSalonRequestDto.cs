namespace BeautyNest.Models.DTO
{
    public class CreateSalonRequestDto
    {
        public string Naziv { get; set; } = string.Empty;

        public string Adresa { get; set; } = string.Empty;

        public string KontaktTelefon { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Opis { get; set; } = string.Empty;

        public TimeSpan RadnoVrijemeOd { get; set; }

        public TimeSpan RadnoVrijemeDo { get; set; }

        public bool SubotaRadna { get; set; } = false;

        public string NaslovnaFotografija { get; set; } = string.Empty;

        public List<int> Kategorije { get; set; } = new List<int>();

        public List<CreateKategorijaUslugeRequestDto> KategorijeUsluga { get; set; } = new List<CreateKategorijaUslugeRequestDto>();
    }
}
