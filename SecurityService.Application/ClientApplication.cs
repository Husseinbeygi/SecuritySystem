﻿using _0_Framework.Application;
using AutoMapper;
using SecurityService.Application.Service.Dtos.Client;
using SecuritySystem.Domain.Client;

namespace SecuritySystem.Application
{
    public class ClientApplication : IClientApplication
    {
        private readonly IClientRepository _repository;
        private readonly IPasswordHasher _passwordhasher;
        public ClientApplication(IClientRepository repository, IPasswordHasher passwordhasher)
        {
            _repository = repository;
            _passwordhasher = passwordhasher;
        }

        public void Create(CreateClient command)
        {
            if (_repository.Exists(x => x.ClientId == command.ClientId))
            {
                return;
            }
            var _create = new Client(command.ClientId, command.UserName, command.Password);
            _repository.Create(_create);
            _repository.SaveChanges();
        }

        public bool IsClientValidate(string clientId, string username, string password)
        {
           ClientValidation _client = _repository.GetClientCredentials(clientId);
            if (_client == null)
                return false;

            if (_client.UserName == username)
            {
                if (_client.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public List<ClientViewModel> Search(ClientSearchModel command)
        {
            return _repository.Search(command);
        }
    }
}
