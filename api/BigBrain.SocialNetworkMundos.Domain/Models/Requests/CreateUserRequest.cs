﻿using System.ComponentModel.DataAnnotations;

namespace BigBrain.SocialNetworkMundos.Domain.Models.Requests
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
    
    
