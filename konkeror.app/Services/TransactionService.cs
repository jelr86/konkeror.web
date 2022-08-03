using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace konkeror.app.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        private ITransactionRepository TransactionRepository { get; }
        private IProductRepository ProductRepository { get; }
        private IDeviseRepository DeviseRepository { get; }
        private ILicenseRepository LicenseRepository { get; }
        private IMapper Mapper { get; }

        private Product Product { get; set; }
        private License License { get; set; }
        public TransactionService(ITransactionRepository transactionRepo, IDeviseRepository deviseRepo,
            IProductRepository productRepo, ILicenseRepository licenseRepository, IMapper mapper)
        {
            TransactionRepository = transactionRepo;
            DeviseRepository = deviseRepo;
            Mapper = mapper;
            ProductRepository = productRepo;
            LicenseRepository = licenseRepository;
        }

        public ServiceResult<RegisterTransactionResult> Register(TransactionModel transaction)
        {
            Product = null;
            var serviceRes = new ServiceResult<RegisterTransactionResult>();
            var validationMessages = new List<ValidationMessage>();
            ValidateRequest(transaction, validationMessages);
            if (validationMessages.Count() > 0 || Product == null)
            {
                serviceRes.ValidationMessages = validationMessages;
                return serviceRes;
            }

            //verify devise exists
            var devise = DeviseRepository.GetDevise(transaction.Devise, transaction.LicenseId);
            if (devise == null)
            {
                devise = new Devise() { 
                    Name = transaction.Devise,
                    LicenseId = transaction.LicenseId
                };
                DeviseRepository.Create(devise);
            }
            
            var tr = Mapper.Map<Transaction>(transaction);
            tr.DeviseId = devise.Id;
            tr.ProductId = Product?.Id;
            TransactionRepository.Create(tr);
                
            serviceRes.Result = new RegisterTransactionResult()
            {
                Id = tr.Id
            };
            return serviceRes;
        }
        
        private void ValidateRequest(TransactionModel tr, IList<ValidationMessage> validationMessages)
        {
            GetLicense(tr.LicenseId, validationMessages);
            GetProduct(tr.ProductId, validationMessages);
            ValidateStringValue(tr.Devise, "Devise", validationMessages);
        }

        private void GetProduct(string id, IList<ValidationMessage> validationMessages)
        {
            ValidateGuidValue(id, "ProductId", validationMessages);
            if (validationMessages.Count() > 0)
            {
                return;
            }
            var pr = ProductRepository.Get(id);
            if (pr == null)
            {
                AddValidationMessage(validationMessages, "Invalid ProductId");
            }
            Product = pr;
        }

        private void GetLicense(string id, IList<ValidationMessage> validationMessages)
        {
            ValidateGuidValue(id, "LicenseId", validationMessages);
            if (validationMessages.Count() > 0)
            {
                return;
            }
            var license = LicenseRepository.Get(id);
            if (license == null)
            {
                AddValidationMessage(validationMessages, "Invalid LicenseId");
            }

            if (!license.Active || license.ExpirationDate < DateTime.UtcNow)
            {
                AddValidationMessage(validationMessages, "License inactive or expired");
            }
            License = license;
        }


        //public void Report(string transactionId)
        //{
        //    Transaction tr = TransactionRepository.Get(transactionId);
        //    if (tr != null)
        //    {
        //        if (tr.StartDate.Equals(DateTime.MinValue)) 
        //            tr.StartDate = DateTime.UtcNow;
        //        tr.ModifiedDate = DateTime.UtcNow;
        //        TransactionRepository.UpdateTime(tr); 
        //    }
        //}

        //protected bool IsTransactionCurrent(Transaction tr)
        //{
        //    if (tr.StartDate.Equals(DateTime.MinValue) || 
        //        tr.ModifiedDate.Equals(DateTime.MinValue)) return true;
            
        //    var isCurrent = tr.StartDate.AddMinutes(tr.Minutes) > DateTime.UtcNow;
        //    if (isCurrent)
        //    {
        //        return AvailableMins(tr) >= 2;
        //    }
        //    return false;
        //}

        //protected int AvailableMins(Transaction tr)
        //{
        //    if (tr.StartDate.Equals(DateTime.MinValue) ||
        //        tr.ModifiedDate.Equals(DateTime.MinValue)) return tr.Minutes;
        //    var elapsedMins = (int)(tr.ModifiedDate - tr.StartDate).TotalMinutes;
            
        //    return elapsedMins > tr.Minutes ? 0 :
        //        tr.Minutes - elapsedMins;
        //}
    }
}
