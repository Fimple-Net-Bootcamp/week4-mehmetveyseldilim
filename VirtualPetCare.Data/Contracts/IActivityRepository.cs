using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;

namespace VirtualPetCare.Data.Contracts
{
    public interface IActivityRepository
    {
        Task<ReadActivityDTO> CreateActivityAsync(CreateActivityDTO createActivityDTO);

        Task<IEnumerable<ReadActivityDTO>> GetActivitiesForSpesificPetAsync(int petId);
    }
}