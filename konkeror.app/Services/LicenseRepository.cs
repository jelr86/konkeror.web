using AutoMapper;
using konkeror.app.Services.Interface;
using konkeror.data;
using konkeror.data.Domain;
using System;
using System.Linq;

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

        public IQueryable<License> GetByClient(string clientId, int take = -1)
        {
            var licenses = _konkerorDb.Licenses
                .Where(l => l.ClientId.Equals(new Guid(clientId)));

            if (take > 0) licenses.Take(take);

            return licenses;
        }

        public License Get(string id)
        {
            return _konkerorDb.Licenses
                .Where(c => c.ID.Equals(new Guid(id)))
                .FirstOrDefault();
        }


        public void Create(License license)
        {
            var l = new License
            {
                ID = Guid.NewGuid(),
                Name = license.Name,
                ClientId = license.ClientId,
                ExpirationDate = license.ExpirationDate,
                Active = true,
                CreatedDate = DateTime.UtcNow,
                Code = Guid.NewGuid(),
                ComputerCode = Guid.Empty
            };
            _konkerorDb.Licenses.Add(l);
            _konkerorDb.SaveChanges();
        }

        public void Update(string id, License license)
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

        public void UpdateComputerCode(License lic, Guid computerCode)
        {
            lic.ComputerCode = computerCode;
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
