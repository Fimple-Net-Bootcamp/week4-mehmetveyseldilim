using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualPetCare.Data.Entities
{
    public class PetFoodHistory
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public DateTime FeedTime {get; set;}

        [Column(Order = 0)]

        public int PetId {get; set;}

        public  Pet? Pet {get; set;}

        [Column(Order = 1)]

        public int FoodId {get; set;}

        public  Food? Food {get; set;}






    }
}