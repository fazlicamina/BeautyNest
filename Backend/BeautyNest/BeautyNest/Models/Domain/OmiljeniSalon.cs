using System.ComponentModel.DataAnnotations;

namespace BeautyNest.Models.Domain
{
    public class OmiljeniSalon
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int SalonId { get; set; }
    }
}
