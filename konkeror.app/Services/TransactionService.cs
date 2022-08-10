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
        private ILicenseService LicenseService { get; }
        private IMapper Mapper { get; }
        private Product Product { get; set; }
        public TransactionService(ITransactionRepository transactionRepo, IDeviseRepository deviseRepo,
            IProductRepository productRepo, ILicenseService licenseService, IMapper mapper)
        {
            TransactionRepository = transactionRepo;
            DeviseRepository = deviseRepo;
            ProductRepository = productRepo;
            LicenseService = licenseService;
            Mapper = mapper;
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
            if (transaction.ForceNew)
            {
                TransactionRepository.CloseAllOpenTransactionForDevise(devise.Id);
            }
            else
            {
                var latestTr = TransactionRepository.GetLatestByDevise(devise.Id);
                if (latestTr != null && !latestTr.Closed)
                {
                    serviceRes.Result = new RegisterTransactionResult()
                    {
                        Id = latestTr.Id,
                        IsNew = false
                    };
                    return serviceRes;
                }
            }
            
            var tr = Mapper.Map<Transaction>(transaction);
            tr.DeviseId = devise.Id;
            tr.ProductId = Product.Id;
            TransactionRepository.Create(tr);
                
            serviceRes.Result = new RegisterTransactionResult()
            {
                Id = tr.Id,
                IsNew = true
            };
            return serviceRes;
        }

        public ServiceResult<bool> CloseTransaction(string transactionId)
        {
            Product = null;
            var serviceRes = new ServiceResult<bool>();
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(transactionId, "transactionId", validationMessages);
            if (validationMessages.Count() > 0)
            {
                serviceRes.ValidationMessages = validationMessages;
                return serviceRes;
            }

            TransactionRepository.CloseTransaction(transactionId);
            serviceRes.Result = true;
            return serviceRes;
        }

        private void ValidateRequest(TransactionModel tr, IList<ValidationMessage> validationMessages)
        {
            ValidateLicense(tr.LicenseId, validationMessages);
            ValidateProduct(tr.ProductId, validationMessages);
            ValidateStringValue(tr.Devise, "Devise", validationMessages);
        }

        private void ValidateProduct(string id, IList<ValidationMessage> validationMessages)
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

        private void ValidateLicense(string id, IList<ValidationMessage> validationMessages)
        {
            ValidateGuidValue(id, "LicenseId", validationMessages);
            if (validationMessages.Count() > 0)
            {
                return;
            }

            
            var license = LicenseService.Get(id).Result;
            if (license == null)
            {
                AddValidationMessage(validationMessages, "Invalid LicenseId");
            }

            if (!LicenseService.LicenseIsValid(license.Id))
            {
                AddValidationMessage(validationMessages, "License inactive or expired");
            }
        }
    }
}
