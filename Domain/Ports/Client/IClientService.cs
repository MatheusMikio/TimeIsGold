using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Client;
using Domain.DTOs.Client;
using Domain.Entities;
using Domain.Ports.Base;


namespace Domain.Ports.Client
{
    public interface IClientService : IBaseService
    {
        public bool Create(ClientDTO clientDTO, out List<ErrorMessage> messages);
        public void Update(ClientDTOUpdate entity, out List<ErrorMessage> mensagens);
    }
}
