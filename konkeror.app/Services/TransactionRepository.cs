using konkeror.app.Services.Interface;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        public Transaction Create(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Transaction Get(string transactionId)
        {
            throw new NotImplementedException();
        }

        public Transaction GetLatestByDevise(string deviseId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTime(Transaction tr)
        {
            throw new NotImplementedException();
        }
    }
}
