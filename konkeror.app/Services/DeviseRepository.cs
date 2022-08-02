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
    public class DeviseRepository : IDeviseRepository
    {
        private konkerorEntities _konkerorDb { get; }
        //private ILogger _logger { get; set; }
        public DeviseRepository(konkerorEntities entities)
        {
            _konkerorDb = entities;
            //_logger = logger;
        }

        public IEnumerable<Devise> Get(int page, int take)
        {
            return _konkerorDb.Devises
                //.Skip(page*take)
                .Take(take).ToList();
        }


        public Devise GetDevise(string name, string licenseId)
        {
            var dev = _konkerorDb.Devises
                .Where(c => c.Name.Equals(name) && c.LicenseId.Equals(licenseId))
                .FirstOrDefault();

            return dev;
        }

        public void Create(Devise devise)
        {
            devise.Id = Guid.NewGuid().ToString();
            devise.CreatedDate = DateTime.UtcNow;
            _konkerorDb.Devises.Add(devise);
            _konkerorDb.SaveChanges();
        }
    }
}
