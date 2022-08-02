using konkeror.app.Models;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface ITransactionRepository
    {
        Transaction GetLatestByDevise(string deviseId);
        void Create(Transaction transaction);
        Transaction Get(string transactionId);
        void UpdateTime(Transaction tr);
    }
}
