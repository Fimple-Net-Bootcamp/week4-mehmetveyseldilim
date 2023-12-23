using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;

namespace VirtualPetCare.Data.Contracts
{
    public interface IPetRepository
    {
        Task<ReadPetDTO> CreatePetAsync(CreatePetDTO createPetDTO, int userId);

        Task<IEnumerable<ReadPetDTO>> GetAllPetsAsync();

        Task<ReadPetDTO> GetPetByIdAsync(int petId);

        Task<IEnumerable<ReadPetDTO>> GetPetsByUserIdAsync(int userId);

        Task<ReadPetDTO> UpdatePetAsync(int userId, int petId, UpdatePetDTO updatePetDTO);

    }
}