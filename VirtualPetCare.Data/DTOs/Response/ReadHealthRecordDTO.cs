

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace VirtualPetCare.Data.DTOs.Response
{
    public class ReadHealthRecordDTO
    {
        public int Id {get; set;}

        public required string GeneralHealth { get; set; }

        public DateTime? LastVaccinationDate { get; set; }

        public double? Weight {get; set;}

    }
}