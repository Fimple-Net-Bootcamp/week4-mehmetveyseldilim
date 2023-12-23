using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.Entities;
using VirtualPetCare.Data.Exceptions;


namespace VirtualPetCare.Data.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        
        private readonly IMapper _mapper;

        private readonly VirtualPetCareDbContext _context;

        public TypeRepository(IMapper mapper, VirtualPetCareDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public Task<bool> DoesTypeExists(string typeName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<int>> GetTypeIntegerIdsAsync(ICollection<string> petTypeNames)
        {
            List<int> ids = new List<int>();


            var petTypeList = await _context.PetTypes.ToListAsync();

            foreach(var petType in petTypeList) 
            {
                if (petTypeNames.Any(type => string.Equals(type, petType.PetTypeName.ToString(), StringComparison.OrdinalIgnoreCase)))
                {
                    ids.Add(petType.Id);
                }
            }

            return ids;

        }

        public async Task<PetType> GetTypeIntegerIdsAsync(string petTypeName)
        {

            var petTypeList = await _context.PetTypes.ToListAsync();

            

            foreach(var petType in petTypeList) 
            {
                string petTypeEntityPetTypeName = petType.PetTypeName.ToString();

                if (string.Equals(petTypeName, petTypeEntityPetTypeName, StringComparison.OrdinalIgnoreCase))
                {
                   return petType;
                }
            }

            throw new NoPetTypeFound(petTypeName);

        }
    }
}