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
        private IMapper Mapper { get; }
        public TransactionService(ITransactionRepository transactionRepo, IDeviseRepository deviseRepo,
            IProductRepository productRepo, IMapper mapper)
        {
            TransactionRepository = transactionRepo;
            DeviseRepository = deviseRepo;
            Mapper = mapper;
            ProductRepository = productRepo;
        }

        public ServiceResult<RegisterTransactionResult> Register(TransactionModel transaction)
        {
            var serviceRes = new ServiceResult<RegisterTransactionResult>();
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
            var validationMessages = new List<ValidationMessage>();
            var product = GetProduct(transaction.ProductId, validationMessages);
            if (validationMessages.Count() > 0 || product == null)
            {
                serviceRes.ValidationMessages = validationMessages;
                return serviceRes;
            }

            var tr = Mapper.Map<Transaction>(transaction);
            tr.DeviseId = devise.Id;
            tr.ProductId = product?.Id;
            TransactionRepository.Create(tr);
                
            serviceRes.Result = new RegisterTransactionResult()
            {
                Id = tr.Id
            };
            return serviceRes;
        }

        private Product GetProduct(string id, IList<ValidationMessage> validationMessages)
        {
            ValidateGuidValue(id, "ProductId", validationMessages);
            if (validationMessages.Count() > 0)
            {
                return null;
            }
            var pr = ProductRepository.Get(id);
            if (pr == null)
            {
                AddValidationMessage(validationMessages, "Invalid ProductId");
            }
            return pr;
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
