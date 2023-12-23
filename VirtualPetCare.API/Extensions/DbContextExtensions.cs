using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VirtualPetCare.API.Helper;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.API.Extensions
{
    public static class DbContextExtensions
    {
        public static void SeedDatabase<T>(this DbContext context, string filePath) where T : class
        {
            if(!context.Set<T>().Any()) 
            {
                var objects = SeedHelper.SeedData<T>(filePath);
                context.Set<T>().AddRange(objects);
            }
        }
    }
}