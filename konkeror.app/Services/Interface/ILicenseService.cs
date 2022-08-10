using konkeror.app.Models;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface ILicenseService
    {
        ServiceResult<IEnumerable<LicenseModel>> GetByClient(string clientId, int take);
        ServiceResult<string> RegisterLicense(string clientId, string licenseCode);
        ServiceResult<LicenseModel> Get(string Id);
        ServiceResult<CreateLicenseResultModel> Create(CreateLicenseModel license);
        ServiceResult<bool> Update(string id, UpdateLicenseModel license);
        ServiceResult<bool> Delete(string id);
        bool ValidateLicense(string computerCode, string licenseCode);
        bool LicenseIsValid(License license);
        bool LicenseIsValid(string licenseId);
        ServiceResult<string> ResetLicense(string clientId, string licenseCode);
    }
}
