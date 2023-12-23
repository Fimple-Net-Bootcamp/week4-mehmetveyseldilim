
namespace VirtualPetCare.Data.DTOs.Response
{
    public class ReadPetDTO
    {
        public int Id {get; set;}
        public required string PetTypeName {get; set;}

        public required string Name {get; set;}

        public int UserId {get; set;}

        public ReadUserDTO? User {get; set;}

        public ReadHealthRecordDTO? HealthRecord {get; set;}

        public ICollection<PetFoodHistoryDTO>? FeedHistory {get; set;}

    }
}