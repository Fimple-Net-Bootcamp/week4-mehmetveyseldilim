using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.DTOs.Response
{
    public class PetFoodHistoryDTO
    {
        public int Id {get; set;}

        public int PetId {get; set;}

        public int FoodId {get; set;}
        public required string FoodName {get; set;}

        public DateTime FeedTime {get; set;}
    }
}