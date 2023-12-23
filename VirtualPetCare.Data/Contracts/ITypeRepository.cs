using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.Data.Contracts
{
    public interface ITypeRepository
    {
        Task<bool> DoesTypeExists(string typeName);

        Task<IEnumerable<int>> GetTypeIntegerIdsAsync(ICollection<string> petTypeNames);

        Task<PetType> GetTypeIntegerIdsAsync(string petTypeName);
    }
}