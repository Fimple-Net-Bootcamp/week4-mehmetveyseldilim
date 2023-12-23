using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;
using VirtualPetCare.Data.Entities;
using VirtualPetCare.Data.Exceptions;

namespace VirtualPetCare.Data.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly IMapper _mapper;
        private readonly VirtualPetCareDbContext _context;


        public FoodRepository(IMapper mapper, VirtualPetCareDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ReadFoodDTO>> GetAllFoodsAsync()
        {
            var foods = await _context.Foods.ToListAsync();

            var mappedFoods = _mapper.Map<IEnumerable<ReadFoodDTO>>(foods);

            return mappedFoods;
        }

        public async Task<ReadPetDTO> FeedThePet(int id, int foodId)
        {
            var pet = await GetPetById(id);

            var food = await GetFoodById(foodId);

            var feedPet = await FeedThePet(pet, food);

            var mappedPet = _mapper.Map<ReadPetDTO>(feedPet);

            return mappedPet;
        }

        private async Task<Pet> GetPetById(int id) 
        {
            var pet = await _context.Pets
                      .Where(pet => pet.Id == id)
                      .Include(pet => pet.FeedHistory)
                      .FirstOrDefaultAsync();

            if(pet == null) 
            {
                throw new PetNotFound(id);
            }

            return pet;
        }

        private async Task<Food> GetFoodById(int id) 
        {
            var food = await _context.Foods.Where(food => food.Id == id).FirstOrDefaultAsync();

            if(food == null) 
            {
                throw new FoodNotFound(id);
            }

            return food;
        }



        private async Task<Pet> FeedThePet(Pet pet, Food food) 
        {
            int petTypeId = pet.PetTypeId;
            int foodId = food.Id;

            var doesPetConsumeThisFood = await _context
                                        .PetTypeFoods.
                                        AnyAsync(petTypeFood => 
                                        petTypeFood.PetTypeId == petTypeId 
                                        && petTypeFood.FoodId == foodId);


            if(doesPetConsumeThisFood)
            {
                var petFoodHistory = new PetFoodHistory
                {
                    FeedTime = DateTime.UtcNow,
                    PetId = pet.Id,
                    FoodId = food.Id,
                    Pet = pet,
                    Food = food

                };

                _context.PetFoodHistories.Add(petFoodHistory);
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new PetFoodDoesNotMatch(pet.Id, foodId);
            }

            
            return pet;

        }
    }
}