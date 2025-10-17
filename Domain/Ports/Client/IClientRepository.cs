using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Client;
using Domain.DTOs.Plan;
using Domain.Ports.Base;

namespace Domain.Ports.Client
{
    public interface IClientRepository : IBaseRepository
    {
        bool EmailExists(string email);
        public bool IsUnique(ClientDTOUpdate client);
    }
}
