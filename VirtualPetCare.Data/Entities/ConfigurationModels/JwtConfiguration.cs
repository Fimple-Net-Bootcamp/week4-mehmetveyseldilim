using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Entities.ConfigurationModels
{
    public class JwtConfiguration
    {
        public string Section { get; set; } = "JwtSettings";
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public string? Expires { get; set; }
        [Range(10, 30, ErrorMessage = "liveLoading must be between 10 and 30")]
        public string? liveLoading { get; set; }
    }
}