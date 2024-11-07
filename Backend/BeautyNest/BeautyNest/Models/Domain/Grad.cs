using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.Domain
{
    public class Grad
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }=string.Empty;
    }
}
