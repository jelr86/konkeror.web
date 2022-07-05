using konkeror.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface ITransactionService
    {
        ServiceResult<RegisterTransactionResult> Register(TransactionModel transaction);
        void Report(string transactionId);
    }
}
