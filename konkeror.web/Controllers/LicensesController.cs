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
        private ILicenseService LicenseService { get; }
        public LicensesController(ILicenseService licenseService)
        {
            LicenseService = licenseService;
        }

        public IHttpActionResult Get(string clientId, int take)
        {
            try
            {
                var lic = LicenseService.GetByClient(clientId, take);
                if (lic.ValidationMessages?.Count > 0)
                    return new ErrorResult(lic.ValidationMessages, Request);
                if (lic.Result == null || lic.Result.Count() == 0)
                    return NotFound();
                return Ok(lic.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Get(string id)
        {
            try
            {
                var lic = LicenseService.Get(id);
                if (lic.ValidationMessages?.Count > 0)
                    return new ErrorResult(lic.ValidationMessages, Request);
                if (lic.Result == null)
                    return NotFound();
                return Ok(lic.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Post([FromBody] CreateLicenseModel license)
        {
            try
            {
                var r = LicenseService.Create(license);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Put(string id, [FromBody] UpdateLicenseModel value)
        {
            try
            {
                var r = LicenseService.Update(id, value);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                var r = LicenseService.Delete(id);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut()]
        [Route("api/licenses/register")]
        public IHttpActionResult RegisterLicense(string clientId, string licenseCode)
        {
            try
            {
                var r = LicenseService.RegisterLicense(clientId, licenseCode);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet()]
        [Route("api/licenses/validate")]
        public IHttpActionResult ValidateLicense(string computerCode, string licenseCode)
        {
            try
            {
                if (!LicenseService.ValidateLicense(computerCode, licenseCode))
                    return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut()]
        [Route("api/licenses/reset")]
        public IHttpActionResult ResetLicense(string clientId, string licenseCode)
        {
            try
            {
                var r = LicenseService.ResetLicense(clientId, licenseCode);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
