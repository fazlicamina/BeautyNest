﻿namespace BeautyNest.Models.DTO
{
    public class RegisterRequestDto
    {
        public string UserName {  get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty; 
        public string Password { get; set; } =string.Empty;
        
    }
}