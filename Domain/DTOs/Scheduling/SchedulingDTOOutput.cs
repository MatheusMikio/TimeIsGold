using Application.DTOs.Client;
using Application.DTOs.Professional;
using Application.DTOs.SchedulingType;
using Domain.Entities.BaseEntities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Scheduling
{
    public class SchedulingDTOOutput : BaseEntityDTO
    {
        public ProfessionalDTOOutput Professional { get; set; }
        public ClientDTOOutput Client { get; set; }
        public SchedulingTypeDTOOutput SchedulingType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Status Status { get; set; }
    }
}
