using Application.DTOs.Client;
using Domain.DTOs.Client;
using Domain.Entities;
using Domain.Ports.Base;


namespace Domain.Ports.Client
{
    public interface IClientService : IBaseService
    {
        public bool Create(ClientDTO clientDTO, out List<ErrorMessage> messages);
        public void Update(ClientDTOUpdate entity, out List<ErrorMessage> messagens);
        public ClientDTOOutput Login(string email, string password, out List<ErrorMessage> messagens);
    }
}
