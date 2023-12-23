using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VirtualPetCare.Data.Entities
{
    public class HealthRecord
    {
        [Key]
        public int Id {get; set;}

        public HealthStatus GeneralHealth { get; set; }

        public HealthStatus GeneralHealthStringValue {get; set;}


        public DateTime? LastVaccinationDate { get; set; }

        public double? Weight {get; set;}


        //. Navigation Properties

        [Required]
        [ForeignKey(nameof(Pet))]

        public int PetId {get; set;}

        public required Pet Pet {get; set;}

    }

    public enum HealthStatus
    {
        Excellent,
        Good,
        Fair,
        Poor
    }

}