using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.Domain
{
    public class Kategorija
    {
 
            [Key]
            public int Id { get; set; }
            public string Naziv { get; set; } = string.Empty;
            public string Slika { get; set; }=string.Empty;

            public ICollection<Salon> Saloni { get; set; }

    }
}
