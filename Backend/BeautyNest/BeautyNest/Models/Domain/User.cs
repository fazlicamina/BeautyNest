using Microsoft.AspNetCore.Identity;

namespace BeautyNest.Models.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[]? ProfilePicture { get; set; }
        public ICollection<Rezervacija> Rezervacije { get; set; } = new List<Rezervacija>();

    }
}
