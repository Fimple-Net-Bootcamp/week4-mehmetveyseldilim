using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualPetCare.Data.Entities
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string PetTypeName {get; set;}


        [Required]
        public required string Name { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId {get; set;}
        public required User User {get; set;}


        public HealthRecord? HealthRecord {get; set;}


        [Required]
        [ForeignKey(nameof(PetType))]
        public int PetTypeId {get; set;}

        public required PetType PetType {get; set;}

        public ICollection<PetFoodHistory>? FeedHistory {get; set;}


    }
}