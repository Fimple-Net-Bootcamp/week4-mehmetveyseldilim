using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VirtualPetCare.Data;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.API.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void Seed(this IApplicationBuilder applicationBuilder, IHostEnvironment environment) 
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                using(var context = scope.ServiceProvider.GetService<VirtualPetCareDbContext>()) 
                {

                    if(context is null) 
                    {
                        Console.WriteLine($"{nameof(context)} is null. Seeding cannot be instantiated");
                    }

                    context!.Database.Migrate();

                    if(!environment.IsProduction()) 
                    {
                        // var repositoryManager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();

                        context.SeedDatabase<CustomIdentityRole>("Roles.json");

                        // context.SeedUser(repositoryManager ,"Users.json");
                        // context.SeedDatabase<IdentityUserRole<int>>("UserRoles.json");

                    }

                    context.SeedDatabase<PetType>("PetTypes.json");
                    context.SeedDatabase<Food>("Foods.json");

                    // await context.SaveChangesAsync();
                    context.SeedDatabase<PetTypeFood>("PetTypeFoods.json");

                    context.SaveChanges();

                }
            }
        }

    }
}