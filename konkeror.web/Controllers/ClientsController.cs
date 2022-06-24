using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;

namespace konkeror.web.Controllers
{
    public class ClientsController : ApiController
    {
        private IClientRepository _clientRepo { get; }
        public ClientsController(IClientRepository repo)
        {
            _clientRepo = repo;
        }

        // GET api/values
        public IEnumerable<ClientModel> Get(int page, int take)
        {
            return _clientRepo.Get(page, take) ;
        }

        // GET api/values/5
        public ClientModel Get(string id)
        {
            
            return _clientRepo.Get(id);
        }

        // POST api/values
        public void Post([FromBody] CreateClientModel client)
        {
            try
            {
                _clientRepo.Create(client);
            }
            catch (Exception)
            {

            }
        }

        // PUT api/values/5
        public void Put(string id, [FromBody] UpdateClientModel value)
        {
            try
            {
                _clientRepo.Update(id, value);
            }
            catch (Exception)
            {

            }
        }

        // DELETE api/values/5
        public void Delete(string id)
        {
            try
            {
                _clientRepo.Delete(id);
            }
            catch (Exception)
            {

            }
        }
    }
}
