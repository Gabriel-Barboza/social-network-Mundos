﻿namespace BigBrain.SocialNetworkMundos.Domain.Entities
{
    public class JwtSettings
    {

        public string SecretKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int ExpirationMinutes { get; set; }
    }

}

