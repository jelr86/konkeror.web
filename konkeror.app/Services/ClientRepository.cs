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

        public IEnumerable<ClientModel> Get(int page, int take)
        {
            return _konkerorDb.Clients
                //.Skip(page*take)
                .Take(take).ToList()
                .Select(c => _mapper.Map<ClientModel>(c));
        }

        public ClientModel Get(string id)
        {
            var client = _konkerorDb.Clients
                .Where(c => c.Id.Equals(new Guid(id)))
                .FirstOrDefault();

            return _mapper.Map<ClientModel>(client);
        }


        public void Create(CreateClientModel client)
        {
            var c = _mapper.Map<Client>(client);
            c.Id = Guid.NewGuid().ToString();
            c.Active = true;
            _konkerorDb.Clients.Add(c);
            _konkerorDb.SaveChanges();
        }

        public void Update(string id, UpdateClientModel client)
        {
            var cli = _konkerorDb.Clients
                .Where(c => c.Id.Equals(new Guid(id)))
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
            .Where(c => c.Id.Equals(new Guid(id)))
            .FirstOrDefault();
            _konkerorDb.Clients.Remove(cli);
            _konkerorDb.SaveChanges();
        }
    }
}
