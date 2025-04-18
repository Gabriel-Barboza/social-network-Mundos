using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BigBrain.SocialNetworkMundos.Application.Services
{
        public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var userIdClaim = user?.FindFirst(JwtRegisteredClaimNames.Sub) ??
                              user?.FindFirst(ClaimTypes.NameIdentifier);

            return Guid.TryParse(userIdClaim?.Value, out var userId) ? userId : null;
        }
    
    }
}
