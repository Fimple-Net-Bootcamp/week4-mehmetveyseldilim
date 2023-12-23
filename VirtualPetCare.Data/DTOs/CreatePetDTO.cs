using VirtualPetCare.Data.BaseValidationModel;

namespace VirtualPetCare.Data.DTOs
{
    public class CreatePetDTO : BaseValidationModel<CreatePetDTO>
    {
        public required string PetTypeName {get; set;}

        public required string Name {get; set;}

        public required string GeneralHealth {get; set;}

        public DateTime? LastVaccinationDate {get; set;}

        public double? Weight {get; set;}

    }
}