using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;
using VirtualPetCare.Data.Entities;
using VirtualPetCare.Data.Exceptions;

namespace VirtualPetCare.Data.Repositories
{
    public class HealthRepository : IHealthRepository
    {
        private readonly IMapper _mapper;

        private readonly VirtualPetCareDbContext _context;

        private readonly ILogger<HealthRepository> _logger;

        public HealthRepository(IMapper mapper, VirtualPetCareDbContext context, 
        ILogger<HealthRepository> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<ReadHealthRecordDTO> GetHealthRecordForSpesificPetAsync(int petId)
        {
            _logger.LogDebug("In Health Repository {@GetHealthRecordForSpesificPetAsync} method",nameof(GetHealthRecordForSpesificPetAsync));
            _logger.LogDebug("Pet id: {@petId}", petId);

            var healthRecord = await _context.HealthRecords
                                .Where(record => record.PetId == petId)
                                .Include(record => record.Pet)
                                .SingleOrDefaultAsync();

            if(healthRecord == null)
            {
                throw new HealthRecordNotFound(petId);
            }
            
            var mappedHealthRecord = _mapper.Map<ReadHealthRecordDTO>(healthRecord);

            return mappedHealthRecord;

        }

        public async Task<ReadHealthRecordDTO> UpdateHealthRecordForSpesificPetAsync(int petId, UpdateHealthRecordDTO updateHealthRecordDTO)
        {
            _logger.LogDebug("In Health Repository {@UpdateHealthRecordForSpesificPetAsync} method",nameof(UpdateHealthRecordForSpesificPetAsync));
            _logger.LogDebug("Pet id: {@petId}", petId);
            _logger.LogDebug("Update Health Record DTO: {@updateHealthRecordDTO}", updateHealthRecordDTO);

            var healthRecordToBeUpdate = await _context.HealthRecords
                                .Where(record => record.PetId == petId)
                                .SingleOrDefaultAsync();

            if(healthRecordToBeUpdate == null)
            {
                throw new HealthRecordNotFound(petId);
            }

            _logger.LogDebug("Health Record before the mapping is : {@healthRecordToBeUpdate}", healthRecordToBeUpdate);

            _mapper.Map(updateHealthRecordDTO, healthRecordToBeUpdate);

            _logger.LogDebug("Health Record after the mapping is : {@healthRecordToBeUpdate}", healthRecordToBeUpdate);


            await _context.SaveChangesAsync();
            
            var mappedHealthRecord = _mapper.Map<ReadHealthRecordDTO>(healthRecordToBeUpdate);

            return mappedHealthRecord;

        }

    }
}