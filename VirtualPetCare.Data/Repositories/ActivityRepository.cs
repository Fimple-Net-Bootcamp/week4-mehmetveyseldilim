using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;
using VirtualPetCare.Data.Entities;
using VirtualPetCare.Data.Exceptions;

namespace VirtualPetCare.Data.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMapper _mapper;

        private readonly VirtualPetCareDbContext _context;

        private readonly ITypeRepository _typeRepository;

        public ActivityRepository(IMapper mapper, VirtualPetCareDbContext context, ITypeRepository typeRepository)
        {
            _mapper = mapper;
            _context = context;
            _typeRepository = typeRepository;
        }

        public async Task<ReadActivityDTO> CreateActivityAsync(CreateActivityDTO createActivityDTO)
        {
            var ids = await GetPetTypeIdsForListOfTypeNames(createActivityDTO.PetTypes);

            var activity = new Activity()
            {
                ActivityName = createActivityDTO.ActivityName
            };

            using(var transaction  = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Activities.Add(activity);
                    await _context.SaveChangesAsync();

                    var petTypeActivitiesList = new List<PetTypeActivity>();

                    foreach(int id in ids) 
                    {
                        var petTypeActivity = new PetTypeActivity()
                        {
                                ActivityId = activity.Id,
                                PetTypeId = id
                        };

                        petTypeActivitiesList.Add(petTypeActivity);

                    }

                    await _context.PetTypeActivities.AddRangeAsync(petTypeActivitiesList);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }

               
            }


            var mappedReadActivity = _mapper.Map<ReadActivityDTO>(activity);

            return mappedReadActivity;

        }

        public async Task<IEnumerable<ReadActivityDTO>> GetActivitiesForSpesificPetAsync(int petId)
        {
            var pet = await GetPetTypeIdForAGivenPetId(petId);

            var activities = await _context.PetTypeActivities
            .Where(pta => pta.PetTypeId == pet.PetTypeId)
            .Select(ap => ap.Activity)
            .ToListAsync();

            var mappedActivities = _mapper.Map<IEnumerable<ReadActivityDTO>>(activities);

            return mappedActivities;
        }

        private async Task<IEnumerable<int>> GetPetTypeIdsForListOfTypeNames(ICollection<string> petTypeName)
        {
            var ids = await _typeRepository.GetTypeIntegerIdsAsync(petTypeName);

            if(ids == null || ids.Count() == 0) 
            {
                string petTypeNameParameters = string.Join(",", petTypeName);
                throw new NoPetTypeFound(petTypeNameParameters);
            }

            return ids;
        }

        private async Task<Pet> GetPetTypeIdForAGivenPetId(int petId) 
        {
            var pet = await _context.Pets.Where(pet => pet.Id == petId).SingleOrDefaultAsync();

            if(pet == null) 
            {
                throw new PetNotFound(petId);
            }

            return pet;
        }
        
    }
}