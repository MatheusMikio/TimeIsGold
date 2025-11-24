using Application.DTOs.Client;
using Application.DTOs.Enterprise;
using Application.DTOs.Plan;
using Application.DTOs.Professional;
using Application.DTOs.Scheduling;
using Application.DTOs.SchedulingType;
using AutoMapper;
using Domain.DTOs.Plan;
using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class EntityToDTOMapping : Profile
    {
        public EntityToDTOMapping()
        {
            //Mapeamento de Entity para DTO Output
            CreateMap<Client, ClientDTOOutput>()
                .ForMember(dest => dest.Schedulings, opt => opt.Ignore());
            
            CreateMap<Enterprise, EnterpriseDTOOutput>()
                .ForMember(dest => dest.SchedulingType, opt => opt.MapFrom(src => src.SchedulingType))
                .ForMember(dest => dest.Professionals, opt => opt.MapFrom(src => src.Professionals));
            
            CreateMap<Plan, PlanDTOOutput>()
               .ForMember(dest => dest.Enterprises, opt => opt.Ignore());
            
            CreateMap<Professional, ProfessionalDTOOutput>()
                .ForMember(dest => dest.EnterpriseId, opt => opt.MapFrom(src => src.EnterpriseId))
                .ForMember(dest => dest.Schedulings, opt => opt.Ignore());
            
            CreateMap<Scheduling, SchedulingDTOOutput>()
                .ForMember(dest => dest.Professional, opt => opt.MapFrom(src => src.Professional))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client));
            
            CreateMap<SchedulingType, SchedulingTypeDTOOutput>();

            //Mapeamento de DTO para entidade
            CreateMap<ClientDTO, Client>();
            CreateMap<EnterpriseDTO, Enterprise>();
            CreateMap<PlanDTO, Plan>();
            CreateMap<ProfessionalDTO, Professional>();
            CreateMap<SchedulingDTO, Scheduling>();
            CreateMap<SchedulingTypeDTO, SchedulingType>();

            //Mapeamento dos enums
            CreateMap<int, PlanLevel>().ConvertUsing(src => (PlanLevel)src);
            CreateMap<PlanLevel, int>().ConvertUsing(src => (int)src);

            CreateMap<int, ProfessionalType>().ConvertUsing(src => (ProfessionalType)src);
            CreateMap<ProfessionalType, int>().ConvertUsing(src => (int)src);
        }
    }
}
