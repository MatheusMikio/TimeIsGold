using Application.DTOs.Plan;
using AutoMapper;
using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PlanService : BaseService<PlanDTO, Plan, IPlanRepository>, IPlanService
    {
        public PlanService(IPlanRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
