using System.ComponentModel.DataAnnotations;


namespace VirtualPetCare.Data.Entities
{
    public class Activity
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public required string ActivityName {get; set;}

        public ICollection<PetTypeActivity>? PetTypeActivities {get; set;}
    }
}