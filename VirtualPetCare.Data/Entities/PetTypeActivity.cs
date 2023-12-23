using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VirtualPetCare.Data.Entities
{
    public class PetTypeActivity
    {
        [Column(Order = 0)]

        public int ActivityId {get; set;}
        [Column(Order = 1)]

        public int PetTypeId {get; set;}

        public  PetType? PetType {get; set;}

        public  Activity? Activity {get; set;}
    }
}