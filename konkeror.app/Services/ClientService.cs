using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace konkeror.app.Services
{
    public class ClientService : BaseService, IClientService
    {
        private IClientRepository ClientRepo { get; }
        private IMapper Mapper { get; }
        public ClientService(IClientRepository clientRepo, IMapper mapper)
        {
            ClientRepo = clientRepo;
            Mapper = mapper;
        }


        public ServiceResult<IEnumerable<ClientModel>> Get(int take)
        {
            var result = new ServiceResult<IEnumerable<ClientModel>>();

            result.Result = ClientRepo.Get(1, take)
                .Select(c => Mapper.Map<ClientModel>(c));

            return result;
        }

        public ServiceResult<ClientModel> Get(string Id)
        {
            var result = new ServiceResult<ClientModel>();

            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(Id, "Id", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            result.Result = Mapper.Map<ClientModel>(ClientRepo.Get(Id));
            return result;
        }

        public ServiceResult<ClientModel> Create(CreateClientModel client)
        {
            var result = new ServiceResult<ClientModel>();
            var validationMessages = new List<ValidationMessage>();

            ValidateStringValue(client.Name, "Name", validationMessages);
            ValidateStringValue(client.Nit, "Nit", validationMessages);
            ValidateStringValue(client.UserName, "UserName", validationMessages);
            ValidateStringValue(client.Password, "Password", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            var c = Mapper.Map<Client>(client);
            ClientRepo.Create(c);

            result.Result = Mapper.Map<ClientModel>(c);
            return result;
        }

        public ServiceResult<bool> Update(string id, UpdateClientModel client)
        {
            var result = new ServiceResult<bool>();
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(id, "id", validationMessages);
            ValidateStringValue(client.Name, "Name", validationMessages);
            ValidateStringValue(client.Nit, "Nit", validationMessages);
            ValidateStringValue(client.UserName, "UserName", validationMessages);
            ValidateStringValue(client.Password, "Password", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            var c = Mapper.Map<Client>(client);
            ClientRepo.Update(id, c);

            result.Result = true;
            return result;
        }

        public ServiceResult<bool> Delete(string id)
        {
            var result = new ServiceResult<bool>();
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(id, "id", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            try
            {
                ClientRepo.Delete(id);
                result.Result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
