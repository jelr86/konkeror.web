using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.web.Common;

namespace konkeror.web.Controllers
{
    public class ClientsController : ApiController
    {
        private IClientService ClientService { get; }
        public ClientsController(IClientService service)
        {
            ClientService = service;
        }

        // GET api/values
        public IHttpActionResult Get(int page, int take)
        {
            try
            {
                var clis = ClientService.Get(take);
                if (clis.ValidationMessages?.Count > 0)
                    return new ErrorResult(clis.ValidationMessages, Request);
                if (clis.Result == null || clis.Result.Count() == 0)
                    return NotFound();
                return Ok(clis.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/values/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                var cli = ClientService.Get(id);
                if (cli.ValidationMessages?.Count > 0)
                    return new ErrorResult(cli.ValidationMessages, Request);
                if (cli.Result == null)
                    return NotFound();
                return Ok(cli.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] CreateClientModel client)
        {
            try
            {
                var r = ClientService.Create(client);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // PUT api/values/5
        public IHttpActionResult Put(string id, [FromBody] UpdateClientModel value)
        {
            try
            {
                var r = ClientService.Update(id, value);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var r = ClientService.Delete(id);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
