using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrain.SocialNetworkMundos.Domain.Interfaces
{
    public interface IImageValidationService
    {
        bool IsValidImage(IFormFile file, out string validationError);

    }
}
