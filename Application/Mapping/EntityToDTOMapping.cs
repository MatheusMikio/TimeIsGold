﻿using Application.DTOs.Client;
using Application.DTOs.Enterprise;
using Application.DTOs.Plan;
using Application.DTOs.Professional;
using Application.DTOs.Scheduling;
using Application.DTOs.SchedulingType;
using AutoMapper;
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
            CreateMap<Client, ClientDTOOutput>();
            CreateMap<Enterprise, EnterpriseDTOOutput>();
            CreateMap<Plan, PlanDTOOutput>();
            CreateMap<Professional, ProfessionalDTOOutput>();
            CreateMap<Scheduling, SchedulingDTOOutput>();
            CreateMap<SchedulingType, SchedulingTypeDTOOutput>();

            //Mapeamento de Entity para DTO Input
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

            ////Evitar ciclos de referência
            //CreateMap<Plan, PlanDTOOutput>()
            //    .ForMember(dest => dest.Enterprises, opt => opt.Ignore()); 

            //CreateMap<Enterprise, EnterpriseDTOOutput>()
            //    .ForMember(dest => dest.Plan, opt => opt.Ignore()); 

            //CreateMap<Professional, ProfessionalDTOOutput>()
            //    .ForMember(dest => dest.Enterprise, opt => opt.Ignore()); 

            //CreateMap<SchedulingType, SchedulingTypeDTOOutput>()
            //    .ForMember(dest => dest.Enterprise, opt => opt.Ignore());

            //CreateMap<Client, ClientDTOOutput>()
            //    .ForMember(dest => dest.Enterprises, opt => opt.Ignore());
        }
    }
}
