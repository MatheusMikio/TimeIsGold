using Application.DTOs.Client;
using Application.DTOs.Enterprise;
using Application.DTOs.Plan;
using Application.DTOs.Professional;
using Application.DTOs.Scheduling;
using Application.DTOs.SchedulingType;
using AutoMapper;
using Domain.Entities;
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
        }
    }
}
