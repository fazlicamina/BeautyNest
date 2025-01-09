using Microsoft.AspNetCore.Identity;

namespace BeautyNest.Models.Domain
{
    public class User : IdentityUser
    {

        // svi
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[]? ProfilePicture { get; set; }

        //ispod atributi se odnose na uposlenika, to je ID salona u kojem radi
        public int? SalonId { get; set; }

    }
}
