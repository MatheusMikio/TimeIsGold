using System.ComponentModel.DataAnnotations;
using Application.DTOs.Client;
using AutoMapper;
using Domain.DTOs.Client;
using Domain.Entities;
using Domain.Ports.Client;

namespace Application.Services
{
    public class ClientService : BaseService<ClientDTO, Client, IClientRepository>, IClientService
    {

        public ClientService(IClientRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public bool Create(ClientDTO clientDTO, out List<ErrorMessage> messages)
        {
            bool valid = Validate(clientDTO, out messages, _repository);

            if (valid)
            {
                try
                {
                    Client clientEntity = _mapper.Map<Client>(clientDTO);
                    _repository.Create(clientEntity);
                    return true;
                }
                catch (Exception ex)
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o cliente"));
                    return false;
                }
            }
            return false;
        }

        public void Update(ClientDTOUpdate entity, out List<ErrorMessage> messages)
        {
            bool valid = ValidateUpdate(entity, out messages, _repository);

            if (valid)
            {
                Client? clientEntity = _repository.GetById<Client>(entity.Id);
                //_mapper.Map<Client>(clientDTO);
                if (clientEntity == null)
                {
                    messages.Add(new ErrorMessage("Cliente", "Cliente não encontrado"));
                    return;
                }

                try
                {
                    clientEntity.Name = entity.Name;
                    clientEntity.Email = entity.Email;
                    clientEntity.Cpf = entity.Cpf;
                    _repository.Update(entity);
                }
                catch (Exception ex)
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o cliente"));
                }
            }
        }

        public static bool Validate(ClientDTO client, out List<ErrorMessage> messages, IClientRepository repository)
        {
            ValidationContext context = new(client);
            List<ValidationResult> results = new();
            bool validation = Validator.TryValidateObject(client, context, results, true);

            messages = results.Select(e => new ErrorMessage(e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToList();

            if (repository.EmailExists(client.Email))
            {
                messages.Add(new ErrorMessage("Email", "Email já cadastrado"));
                validation = false;
            }

            return validation;
        }

        public static bool ValidateUpdate(ClientDTOUpdate client, out List<ErrorMessage> messages, IClientRepository repository)
        {
            ValidationContext context = new(client);
            List<ValidationResult> results = new();
            bool validation = Validator.TryValidateObject(client, context, results, true);

            messages = results.Select(e => new ErrorMessage(e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToList();

            Client? existingClient = repository.GetById<Client>(client.Id);
            if (existingClient == null)
            {
                messages.Add(new ErrorMessage("Cliente", "Cliente não encontrado."));
                validation = false;
            }

            return validation;
        }
    }
}
