using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace konkeror.app.Services
{
    public class LicenseService : BaseService, ILicenseService
    {
        private ILicenseRepository _licenseRepo { get; }
        private IMapper _mapper { get; }
        public LicenseService(ILicenseRepository licenseRepo, IMapper mapper)
        {
            _licenseRepo = licenseRepo;
            _mapper = mapper;
        }

        public ServiceResult<string> RegisterLicense(string clientId, string licenseCode)
        {
            var result = new ServiceResult<string>();
            
            var validationMessages = ValidateRegisterLicenseRequest(clientId, licenseCode, out var license);
            if (validationMessages.Count() > 0 || license == null)
            {
                result.ValidationMessages = validationMessages.ToList();
                return result;
            }

            var computerCode = Guid.NewGuid();
            _licenseRepo.UpdateComputerCode(license, computerCode);

            result.Result = computerCode.ToString();
            return result;
        }

        public ServiceResult<IEnumerable<LicenseModel>> GetByClient(string clientId, int take)
        {
            var result = new ServiceResult<IEnumerable<LicenseModel>>();
            
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(clientId, "clientId", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }

            result.Result = _licenseRepo.GetByClient(clientId, take)
                .Select(c => _mapper.Map<LicenseModel>(c));
            
            return result;
        }

        public ServiceResult<LicenseModel> Get(string Id)
        {
            var result = new ServiceResult<LicenseModel>();

            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(Id, "Id", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            result.Result = _mapper.Map<LicenseModel>(_licenseRepo.Get(Id));
            return result;
        }

        public ServiceResult<CreateLicenseResultModel> Create(CreateLicenseModel license)
        {
            var result = new ServiceResult<CreateLicenseResultModel>();
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(license.ClientId, "license.ClientId", validationMessages);
            ValidateStringValue(license.Name, "license.Name", validationMessages);
            ValidateDateTimeValue(license.ExpirationDate, "license.ExpirationDate", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            var l = _mapper.Map<License>(license);
            _licenseRepo.Create(l);
            
            result.Result = _mapper.Map<CreateLicenseResultModel>(l);
            return result;
        }

        public ServiceResult<bool> Update(string id, UpdateLicenseModel license)
        {
            var result = new ServiceResult<bool>();
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(id, "id", validationMessages);
            ValidateStringValue(license.Name, "license.Name", validationMessages);
            ValidateDateTimeValue(license.ExpirationDate, "license.ExpirationDate", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            var l = _mapper.Map<License>(license);
            _licenseRepo.Update(id, l);

            result.Result = true;
            return result;
        }

        public ServiceResult<bool> Delete(string id)
        {
            var result = new ServiceResult<bool>();
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(id, "id", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            try
            {
                _licenseRepo.Delete(id);
                result.Result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }

        private IEnumerable<ValidationMessage> ValidateRegisterLicenseRequest(string clientId, string licenseCode, out License license)
        {
            license = null;
            var validationMessages = new List<ValidationMessage>();

            ValidateGuidValue(clientId, "clientId", validationMessages);
            ValidateGuidValue(licenseCode, "licenseCode", validationMessages);
            if (validationMessages.Count() > 0) return validationMessages;
            
            var clientLicenses = _licenseRepo.GetByClient(clientId);
            if (clientLicenses == null || clientLicenses.Count() == 0)
                AddValidationMessage(validationMessages,
                    $"No licenses found for clientid {clientId}",
                    "Ensure there are licenses associated to the provided clientid");
            else
            {
                license = clientLicenses.Where(l => l.Code.Equals(licenseCode)).FirstOrDefault();
                if (license == null)
                    AddValidationMessage(validationMessages, $"No license found with provided code");
            }
            return validationMessages;
        }

        public bool ValidateLicense(string computerCode, string licenseCode)
        {
            var validationMessages = new List<ValidationMessage>();

            ValidateGuidValue(computerCode, "computerCode", validationMessages);
            ValidateGuidValue(licenseCode, "licenseCode", validationMessages);
            if (validationMessages.Count() > 0) return false;

            License license = _licenseRepo.GetByLicenseCode(licenseCode);
            return LicenseIsValid(license) &&
                license.ComputerCode.Equals(computerCode);
                
        }

        public bool LicenseIsValid(License license)
        {

            return license != null &&
                license.Active == true &&
                license.ExpirationDate > DateTime.UtcNow &&
                !string.IsNullOrEmpty(license.ComputerCode) &&
                !license.ComputerCode.Equals(Guid.Empty.ToString());

        }



        public ServiceResult<string> ResetLicense(string clientId, string licenseCode)
        {
            var result = new ServiceResult<string>();

            var validationMessages = ValidateRegisterLicenseRequest(clientId, licenseCode, out var license);
            if (validationMessages.Count() > 0 || license == null)
            {
                result.ValidationMessages = validationMessages.ToList();
                return result;
            }

            var computerCode = Guid.Empty;
            _licenseRepo.UpdateComputerCode(license, computerCode);

            return result;
        }
    }
}
