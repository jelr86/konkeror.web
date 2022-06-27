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
    public class LicenseRepository : ILicenseRepository
    {
        private konkerorEntities _konkerorDb { get; }
        //private ILogger _logger { get; set; }
        private IMapper _mapper { get; }
        public LicenseRepository(konkerorEntities entities, IMapper mapper)
        {
            _konkerorDb = entities;
            //_logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<LicenseModel> GetByClient(string clientId, int take = 10)
        {
            return _konkerorDb.Licenses
                .Where(l => l.ClientId.Equals(new Guid(clientId)))
                .Take(take)
                .ToList()
                .Select(c => _mapper.Map<LicenseModel>(c));
        }

        public LicenseModel Get(string id)
        {
            var client = _konkerorDb.Licenses
                .Where(c => c.ID.Equals(new Guid(id)))
                .FirstOrDefault();

            return _mapper.Map<LicenseModel>(client);
        }


        public CreateLicenseResultModel Create(CreateLicenseModel license)
        {
            //Todo: validate not emtpy name, valid clientid, valid expirationdate
            var l = new License
            {
                ID = Guid.NewGuid(),
                Name = license.Name,
                ClientId = new Guid(license.ClientId),
                ExpirationDate = license.ExpirationDate,
                Active = true,
                CreatedDate = DateTime.UtcNow,
                Code = Guid.NewGuid(),
                ComputerCode = Guid.Empty
            };
            _konkerorDb.Licenses.Add(l);
            _konkerorDb.SaveChanges();
            return _mapper.Map<CreateLicenseResultModel>(l);
        }

        public void Update(string id, UpdateLicenseModel license)
        {
            var lic = _konkerorDb.Licenses
                .Where(c => c.ID.Equals(new Guid(id)))
                .FirstOrDefault();
            lic.Name = license.Name;
            lic.Active = license.Active;
            lic.ExpirationDate = license.ExpirationDate;
            lic.ModifiedDate = DateTime.Now;
            _konkerorDb.SaveChanges();
        }

        public void Delete(string id)
        {
            var lic = _konkerorDb.Licenses
            .Where(c => c.ID.Equals(new Guid(id)))
            .FirstOrDefault();
            _konkerorDb.Licenses.Remove(lic);
            _konkerorDb.SaveChanges();
        }
    }
}
