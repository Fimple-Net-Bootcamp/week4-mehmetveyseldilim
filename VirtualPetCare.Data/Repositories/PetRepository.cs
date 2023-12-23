using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;
using VirtualPetCare.Data.Entities;
using VirtualPetCare.Data.Exceptions;

namespace VirtualPetCare.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly IMapper _mapper;

        private readonly VirtualPetCareDbContext _context;

        private readonly ITypeRepository _typeRepository;

        private readonly ILogger<PetRepository> _logger;

        private readonly UserManager<User> _userManager;

        private readonly IHealthRepository _healthRepository;

        public PetRepository(IMapper mapper,
        VirtualPetCareDbContext context,
        ITypeRepository typeRepository,
        ILogger<PetRepository> logger,
        UserManager<User> userManager,
        IHealthRepository healthRepository)
        {
            _mapper = mapper;
            _context = context;
            _typeRepository = typeRepository;
            _logger = logger;
            _userManager = userManager;
            _healthRepository = healthRepository;
        }


        public async Task<ReadPetDTO> CreatePetAsync(CreatePetDTO createPetDTO, int userId)
        {
            string petTypeName = createPetDTO.PetTypeName;
            PetType petType = await _typeRepository.GetTypeIntegerIdsAsync(petTypeName);
            var healthRecord = _mapper.Map<HealthRecord>(createPetDTO);



            var pet = _mapper.Map<Pet>(createPetDTO);
            pet.UserId = userId;
            pet.PetTypeId = petType!.Id;
            pet.PetTypeName = petType.PetTypeName.ToString();

            using var transaction = _context.Database.BeginTransaction();
            {
                _context.Pets.Add(pet);
                await _context.SaveChangesAsync();

                healthRecord.PetId = pet.Id;
                healthRecord.GeneralHealthStringValue = healthRecord.GeneralHealth;

                _context.HealthRecords.Add(healthRecord);
                await _context.SaveChangesAsync();

                transaction.Commit();
                
            }

            var mappedHealthRecord = _mapper.Map<ReadHealthRecordDTO>(healthRecord);

            var mappedPet = _mapper.Map<ReadPetDTO>(pet);
            return mappedPet;
        }

        public async Task<IEnumerable<ReadPetDTO>> GetAllPetsAsync()
        {
            var pets = await _context.Pets.ToListAsync();

            var mappedPets = _mapper.Map<IEnumerable<ReadPetDTO>>(pets);

            return mappedPets;
        }

        public async Task<ReadPetDTO> GetPetByIdAsync(int petId)
        {
            // var pet = await GetPetByIdOrExceptionAsync(petId);

            // var user = await _userManager.FindByIdAsync(pet.UserId.ToString());
            // var healthRecord = await _healthRepository.GetHealthRecordForSpesificPetAsync(petId);

            var pet = await _context.Pets.Where(p => p.Id == petId)
                        .Include(p => p.User)
                            .ThenInclude(user => user.UserRoles)
                                .ThenInclude(role => role.Role)
                        .Include(p => p.HealthRecord)
                        .Include(pet => pet.FeedHistory)
                        .SingleOrDefaultAsync();

            if(pet == null) 
            {
                throw new PetNotFound(petId);
            }

            
            var mappedPet = _mapper.Map<ReadPetDTO>(pet);
            

            return mappedPet;
        }

        public async Task<IEnumerable<ReadPetDTO>> GetPetsByUserIdAsync(int userId)
        {
            var user = await _context.Users.Where(user => user.Id == userId).SingleOrDefaultAsync();

            if(user == null)
            {
                throw new UserNotFound(userId);
            }

            var pets = await _context.Pets
                                .Where(pet => pet.UserId == userId)
                                .Include(pet => pet.User)
                                    .ThenInclude(user => user.UserRoles)
                                        .ThenInclude(userrole => userrole.Role)
                                .Include(pet => pet.FeedHistory)
                                .Include(pet => pet.HealthRecord)
                                .ToListAsync();

                                   
            IEnumerable<ReadPetDTO> mappedPets = _mapper.Map<IEnumerable<ReadPetDTO>>(pets);

            return mappedPets;
        }

        public async Task<ReadPetDTO> UpdatePetAsync(int userId, int petId, UpdatePetDTO updatePetDTO)
        {
            var pet = await _context.Pets.Where(pet => pet.Id == petId)
                        .Include(pet => pet.User)
                        .SingleOrDefaultAsync();

            if(pet == null) 
            {
                throw new PetNotFound(petId);
            }
            if(pet.User == null) 
            {
                throw new UserNotFound(userId);
            }

            if(pet.UserId != userId) 
            {
                throw new UserPetDoesNotMatch(userId, petId);
            }

            _mapper.Map<Pet>(updatePetDTO);

            await _context.SaveChangesAsync();

            var mappedPet = _mapper.Map<ReadPetDTO>(pet);

            return mappedPet;



        }

        private async Task<Pet> GetPetByIdOrExceptionAsync(int petId) 
        {
            var pet = await _context.Pets.Where(pet => pet.Id == petId).FirstOrDefaultAsync();

            if (pet == null) 
            {
                throw new PetNotFound(petId);
            }

            return pet;
        }

    }
}