using konkeror.app.Models;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface ILicenseRepository
    {
        IEnumerable<License> GetByClient(string clientId, int take = -1);
        License Get(string id);
        void Create(License license);
        void Update(string id, License client);
        void UpdateComputerCode(License lic, Guid computerCode);
        void Delete(string id);
        License GetByLicenseCode(string licenseCode);
    }
}
