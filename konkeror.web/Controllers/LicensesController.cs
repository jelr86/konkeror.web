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
    public class LicensesController : ApiController
    {
        private ILicenseRepository _licenseRepo { get; }
        public LicensesController(ILicenseRepository repo)
        {
            _licenseRepo = repo;
        }

        public IEnumerable<LicenseModel> Get(string clientId, int take)
        {
            return _licenseRepo.GetByClient(clientId, take) ;
        }

        public LicenseModel Get(string id)
        {
            
            return _licenseRepo.Get(id);
        }

        public CreateLicenseResultModel Post([FromBody] CreateLicenseModel license)
        {
            try
            {
                return _licenseRepo.Create(license);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Put(string id, [FromBody] UpdateLicenseModel value)
        {
            try
            {
                _licenseRepo.Update(id, value);
            }
            catch (Exception)
            {

            }
        }

        public void Delete(string id)
        {
            try
            {
                _licenseRepo.Delete(id);
            }
            catch (Exception)
            {

            }
        }
    }
}
