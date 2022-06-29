using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services;
using konkeror.app.Services.Interface;
using konkeror.web.Common;

namespace konkeror.web.Controllers
{
    public class LicensesController : ApiController
    {
        private ILicenseService _licenseService { get; }
        public LicensesController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        public IHttpActionResult Get(string clientId, int take)
        {
            try
            {
                var lic = _licenseService.GetByClient(clientId, take);
                if (lic.ValidationMessages.Count > 0)
                    return new ErrorResult(lic.ValidationMessages, Request);
                if (lic.Result == null || lic.Result.Count() == 0)
                    return NotFound();
                return Ok(lic.Result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(string id)
        {
            try
            {
                var lic = _licenseService.Get(id);
                if (lic.ValidationMessages.Count > 0)
                    return new ErrorResult(lic.ValidationMessages, Request);
                if (lic.Result == null)
                    return NotFound();
                return Ok(lic.Result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Post([FromBody] CreateLicenseModel license)
        {
            try
            {
                var r = _licenseService.Create(license);
                if (r.ValidationMessages.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(string id, [FromBody] UpdateLicenseModel value)
        {
            try
            {
                var r = _licenseService.Update(id, value);
                if (r.ValidationMessages.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                var r = _licenseService.Delete(id);
                if (r.ValidationMessages.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
