using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.DTOs.Response;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.Data.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //. source -> destination
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, UserForRead>();

            CreateMap<Activity, ReadActivityDTO>();
            CreateMap<Food, ReadFoodDTO>();

            // CreateMap<Food, PetFoodDTO>()
            //     .ForMember(dest => dest.FeedTime, opt => opt.MapFrom(src => DateTime.UtcNow));

            


            CreateMap<CustomIdentityRole, ReadRoleDTO>();
            CreateMap<ApplicationUserRole, ReadUserRoleDTO>();
            CreateMap<User, ReadUserDTO>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles));

            CreateMap<Pet, ReadHealthRecordDTO>();
            CreateMap<CreatePetDTO, HealthRecord>()
                .ForMember(dest => dest.GeneralHealth, 
                    opt => opt.MapFrom(src => Enum.Parse<HealthStatus>(src.GeneralHealth, true)));

            CreateMap<HealthRecord, ReadHealthRecordDTO>();


            CreateMap<UpdateHealthRecordDTO, HealthRecord>()
                .ForMember(dest => dest.LastVaccinationDate, opt => opt.MapFrom(src => src.LastVaccinationDate))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.GeneralHealthStringValue, opt => opt.MapFrom(src => src.GeneralHealth))
                .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));



            CreateMap<PetFoodHistory, PetFoodHistoryDTO>()
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food!.FoodName))
                .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));;

            CreateMap<CreatePetDTO, Pet>();
            CreateMap<Pet, ReadPetDTO>()
                .ForMember(dest => dest.FeedHistory, opt => opt.MapFrom(src => src.FeedHistory));
                // .ForMember(dest => dest.HealthRecord?.GeneralHealth, opt => opt.MapFrom(src => src.HealthRecord?.GeneralHealth.ToString());
            
            CreateMap<UpdatePetDTO, Pet>();

            CreateMap<JsonPatchDocument<UpdateHealthRecordDTO>, JsonPatchDocument<HealthRecord>>();
                CreateMap<Operation<UpdateHealthRecordDTO>, Operation<HealthRecord>>();


        }
    }
}