﻿using konkeror.app.Models;
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
    }
}
