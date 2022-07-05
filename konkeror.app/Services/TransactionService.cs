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
        private IDeviseRepository DeviseRepository { get; }
        private IMapper Mapper { get; }
        public TransactionService(ITransactionRepository transactionRepo, IDeviseRepository deviseRepo, IMapper mapper)
        {
            TransactionRepository = transactionRepo;
            DeviseRepository = deviseRepo;
            Mapper = mapper;
        }

        public ServiceResult<RegisterTransactionResult> Register(TransactionModel transaction)
        {
            //validate transaction request

            var serviceRes = new ServiceResult<RegisterTransactionResult>();
            //verify devise exists
            var devise = DeviseRepository.GetDevise(transaction.Devise, transaction.LicenseId);
            if (devise == null)
            {
                devise = DeviseRepository.Create(Mapper.Map<Devise>(transaction));
            }

            //verify a current transaction
            var tr = TransactionRepository.GetLatestByDevise(devise.Id);
            if (tr != null && IsTransactionCurrent(tr))
            {
                tr.StartDate = DateTime.UtcNow.AddMinutes(AvailableMins(tr) * -1);
                tr.ModifiedDate = DateTime.UtcNow;
                TransactionRepository.UpdateTime(tr);
            }
            else
                tr = TransactionRepository.Create(Mapper.Map<Transaction>(transaction));
                
            serviceRes.Result = new RegisterTransactionResult()
            {
                Id = tr.Id,
                Minutes = AvailableMins(tr)
            };
            return serviceRes;
        }

        public void Report(string transactionId)
        {
            Transaction tr = TransactionRepository.Get(transactionId);
            if (tr != null)
            {
                if (tr.StartDate.Equals(DateTime.MinValue)) 
                    tr.StartDate = DateTime.UtcNow;
                tr.ModifiedDate = DateTime.UtcNow;
                TransactionRepository.UpdateTime(tr); 
            }
        }

        protected bool IsTransactionCurrent(Transaction tr)
        {
            if (tr.StartDate.Equals(DateTime.MinValue) || 
                tr.ModifiedDate.Equals(DateTime.MinValue)) return true;
            
            var isCurrent = tr.StartDate.AddMinutes(tr.Minutes) > DateTime.UtcNow;
            if (isCurrent)
            {
                return AvailableMins(tr) >= 2;
            }
            return false;
        }

        protected int AvailableMins(Transaction tr)
        {
            if (tr.StartDate.Equals(DateTime.MinValue) ||
                tr.ModifiedDate.Equals(DateTime.MinValue)) return tr.Minutes;
            var elapsedMins = (int)(tr.ModifiedDate - tr.StartDate).TotalMinutes;
            
            return elapsedMins > tr.Minutes ? 0 :
                tr.Minutes - elapsedMins;
        }
    }
}
