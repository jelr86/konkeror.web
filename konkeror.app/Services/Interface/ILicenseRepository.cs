using konkeror.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface ILicenseRepository
    {
        IEnumerable<LicenseModel> GetByClient(string clientId, int take);
        LicenseModel Get(string id);
        CreateLicenseResultModel Create(CreateLicenseModel client);
        void Update(string id, UpdateLicenseModel client);
        void Delete(string id);
    }
}
