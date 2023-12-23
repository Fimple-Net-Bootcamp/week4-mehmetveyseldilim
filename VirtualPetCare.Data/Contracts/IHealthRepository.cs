using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;

namespace VirtualPetCare.Data.Contracts
{
    public interface IHealthRepository
    {
        Task<ReadHealthRecordDTO> GetHealthRecordForSpesificPetAsync(int petId);

        Task<ReadHealthRecordDTO> UpdateHealthRecordForSpesificPetAsync(int petId, UpdateHealthRecordDTO updateHealthRecordDTO);
    }
}