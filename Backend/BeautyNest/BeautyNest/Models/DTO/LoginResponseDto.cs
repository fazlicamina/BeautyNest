namespace BeautyNest.Models.DTO
{
    public class LoginResponseDto
    { 
        public string Username { get; set; }
        public string Token { get; set; }
        public List<string> Roles {  get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
