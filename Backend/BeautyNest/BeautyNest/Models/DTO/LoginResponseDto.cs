﻿namespace BeautyNest.Models.DTO
{
    public class LoginResponseDto
    { 
        public string Username { get; set; }
        public string Token { get; set; }
        public List<string> Roles {  get; set; }
    }
}
