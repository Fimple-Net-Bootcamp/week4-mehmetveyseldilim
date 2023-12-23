using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.DTOs
{
    public class UpdateHealthRecordDTO
    {
        public string? GeneralHealth {get; set;}

        public DateTime? LastVaccinationDate {get; set;}

        public double? Weight {get; set;}
    }
}