using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.DTOs
{
    public record TokenDto(string AccessToken, string RefreshToken);
}