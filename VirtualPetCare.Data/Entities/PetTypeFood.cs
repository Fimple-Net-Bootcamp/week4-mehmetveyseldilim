using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VirtualPetCare.Data.Entities
{
    public class PetTypeFood
    {   
        [Column(Order = 0)]
        public int FoodId {get; set;}

        [Column(Order = 1)]
        public int PetTypeId {get; set;}

        public  Food? Food {get; set;}

        public  PetType? PetType {get; set;}

    }
}