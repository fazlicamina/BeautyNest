using Microsoft.AspNetCore.Identity;

namespace BeautyNest.Models.Domain
{
    public class User : IdentityUser
    {

        // svi
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[]? ProfilePicture { get; set; }

        //uposlenik
        public int? SalonId { get; set; }

    }
}
