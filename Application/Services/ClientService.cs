using System.ComponentModel.DataAnnotations;
using Application.DTOs.Client;
using AutoMapper;
using Domain.DTOs.Client;
using Domain.Entities;
using Domain.Ports.Client;
using System.Text.RegularExpressions;

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
                    clientEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(clientDTO.Password);
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
                    clientEntity.Phone = entity.Phone;
                    clientEntity.PasswordHash = entity.Password;
                    _repository.Update(clientEntity);
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

            if (!ValidatePhone(client.Phone, messages)) validation = false;

            if (repository.EmailExists(client.Email))
            {
                messages.Add(new ErrorMessage("Email", "Email já cadastrado"));
                validation = false;
            }
            if (!ValidateEmail(client.Email))
            {
                messages.Add(new ErrorMessage("Email", "Email inválido."));
                validation = false;
            }
            if (!ValidateCpf(client.Cpf))
            {
                messages.Add(new ErrorMessage("CPF", "CPF inválido."));
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

            if (!ValidateName(client.Name, messages)) validation = false;
            if (!ValidatePhone(client.Phone, messages)) validation = false;

            // Senha só se o usuário quiser trocar
            if (!string.IsNullOrWhiteSpace(client.Password))
            {
                if (!ValidatePassword(client.Password, messages)) validation = false;
            }


            if (!ValidateEmail(client.Email))
            {
                messages.Add(new ErrorMessage("Email", "Email inválido."));
                validation = false;
            }

            if (!ValidateCpf(client.Cpf))
            {
                messages.Add(new ErrorMessage("CPF", "CPF inválido."));
                validation = false;
            }

            return validation;
        }

        public bool VerifyPassword(Client clientEntity, string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, clientEntity.PasswordHash);
        }

        public void ChangePassword(long clientId, string newPassword)
        {
            var client = _repository.GetById<Client>(clientId);
            if (client == null) throw new Exception("Cliente não encontrado");

            client.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _repository.Update(client);
        }

        public static bool ValidateCpf(string cpf)
        {
            ReadOnlySpan<char> cpfSpan = cpf;
            Span<int> digits = stackalloc int[11];
            int count = 0;

            foreach (char cpfNum in cpfSpan)
            {
                if (char.IsDigit(cpfNum))
                {
                    if (count >= 11) return false;
                    digits[count++] = cpfNum - '0';
                }
            }

            if (count != 11) return false;

            bool allSame = true;
            for (int i = 1; i < 11; i++)
            {
                if (digits[i] != digits[0])
                {
                    allSame = false;
                    break;
                }
            }
            if (allSame) return false;

            static int CalculateCheckDigit(ReadOnlySpan<int> digits)
            {
                int sum = 0;
                int multiplication = digits.Length + 1;
                for (int i = 0; i < digits.Length; i++)
                {
                    sum += digits[i] * (multiplication - i);
                }
                int rest = sum % 11;
                return (rest < 2) ? 0 : 11 - rest;
            }

            int digitCheck1 = CalculateCheckDigit(digits.Slice(0, 9));

            if (digitCheck1 != digits[9]) return false;

            int digitCheck2 = CalculateCheckDigit(digits.Slice(0, 10));
            return digits[10] == digitCheck2;
        }

        //private static string RemoveCpfMask(string cpf)
        //{
        //    return new string(cpf.Where(char.IsDigit).ToArray());
        //}

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            email = email.Trim();

            int atIndex = email.IndexOf('@');
            if (atIndex <= 0 || atIndex != email.LastIndexOf('@')) return false;

            string local = email.Substring(0, atIndex);
            string domain = email.Substring(atIndex + 1);

            if (local.Length == 0 || domain.Length == 0) return false;

            int dotIndex = domain.IndexOf('.');
            if (dotIndex <= 0 || dotIndex == domain.Length - 1) return false;

            foreach (char character in domain)
            {
                if (char.IsWhiteSpace(character))
                    return false;
            }

            foreach (char character in local)
            {
                if (!(char.IsLetterOrDigit(character) || character == '.' || character == '_' || character == '-'))
                    return false;
            }

            foreach (char character in domain)
            {
                if (!(char.IsLetterOrDigit(character) || character == '.' || character == '-'))
                    return false;
            }

            return true;
        }

        private static bool ValidateName(string name, List<ErrorMessage> messages)
        {
            if (string.IsNullOrEmpty(name))
            {
                messages.Add(new ErrorMessage("Name", "O nome é obrigatório."));
                return false;
            }
            if (name.Length < 3 || name.Length > 100)
            {
                messages.Add(new ErrorMessage("Name", "O nome do profissional deve ter entre 3 e 100 caracteres."));
                return false;
            }
            if (!Regex.IsMatch(name, @"^[a-zA-ZÀ-ÿ\s]+$"))
            {
                messages.Add(new ErrorMessage("Name", "O nome deve conter apenas letras e espaços."));
                return false;
            }
            return true;
        }

        private static bool ValidatePhone(string phone, List<ErrorMessage> messages)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                messages.Add(new ErrorMessage("Phone", "O telefone é obrigatório."));
                return false;
            }
            if (!Regex.IsMatch(phone, @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$"))
            {
                messages.Add(new ErrorMessage("Phone", "O telefone fornecido é inválido. Use o formato XX XXXXX-XXXX."));
                return false;
            }
            return true;
        }

        private static bool ValidatePassword(string password, List<ErrorMessage> messages)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                messages.Add(new ErrorMessage("Password", "A senha é obrigatória."));
                return false;
            }
            if (password.Length < 8)
            {
                messages.Add(new ErrorMessage("Password", "A senha deve ter pelo menos 8 caracteres."));
                return false;
            }
            if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).*$"))
            {
                messages.Add(new ErrorMessage("Password", "A senha deve conter pelo menos uma letra maiúscula, minúscula e número."));
                return false;
            }
            return true;
        }
    }
}