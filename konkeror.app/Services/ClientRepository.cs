using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.app.Services.Interfaces;
using konkeror.data;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services
{
    public class ClientRepository : IClientRepository
    {
        private konkerorEntities _konkerorDb { get; }
        //private ILogger _logger { get; set; }
        private IMapper _mapper { get; }
        public ClientRepository(konkerorEntities entities, IMapper mapper)
        {
            _konkerorDb = entities;
            //_logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<Client> Get(int page, int take)
        {
            return _konkerorDb.Clients
                //.Skip(page*take)
                .Take(take).ToList();
        }

        public Client Get(string id)
        {
            var client = _konkerorDb.Clients
                .Where(c => c.Id.Equals(id))
                .FirstOrDefault();

            return client;
        }


        public void Create(Client client)
        {
            client.Id = Guid.NewGuid().ToString();
            client.Active = true;
            _konkerorDb.Clients.Add(client);
            _konkerorDb.SaveChanges();
        }

        public void Update(string id, Client client)
        {
            var cli = _konkerorDb.Clients
                .Where(c => c.Id.Equals(id))
                .FirstOrDefault();
            cli.Name = client.Name;
            cli.Nit = client.Nit;
            cli.UserName = client.UserName;
            cli.Password = client.Password;
            cli.Active = client.Active;
            _konkerorDb.SaveChanges();
        }

        public void Delete(string id)
        {
            var cli = _konkerorDb.Clients
            .Where(c => c.Id.Equals(id))
            .FirstOrDefault();
            _konkerorDb.Clients.Remove(cli);
            _konkerorDb.SaveChanges();
        }
    }
}
