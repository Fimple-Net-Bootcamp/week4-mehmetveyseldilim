
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.Data.Contracts
{
    public interface IFoodRepository
    {
        Task<IEnumerable<ReadFoodDTO>> GetAllFoodsAsync();

        Task<ReadPetDTO> FeedThePet(int id, int foodId); 
    }
}