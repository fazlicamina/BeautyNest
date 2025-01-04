using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.Domain
{
    public class SalonSlika
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; } = string.Empty;

        public int SalonId { get; set; }
        public Salon Salon { get; set; }
    }
}
