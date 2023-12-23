using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualPetCare.Data.Entities
{
    public class PetType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PetTypeEnum PetTypeName {get; set;}

        public PetTypeEnum PetTypeNameStringValue {get; set;}

        public ICollection<Pet>? Pets {get; set;}

        public  ICollection<PetTypeFood>? PetFoods {get; set;}

        public  ICollection<PetTypeActivity>? PetTypeActivities {get; set;}
        
    }

    public enum PetTypeEnum
    {
        Dog = 1,
        Cat = 2,
        Fish = 3,
        Hamster = 4
    }


}