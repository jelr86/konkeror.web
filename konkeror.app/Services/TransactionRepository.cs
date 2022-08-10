using konkeror.app.Services.Interface;
using konkeror.data;
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
        private konkerorEntities _konkerorDb { get; }
        //private ILogger _logger { get; set; }
        public TransactionRepository(konkerorEntities entities)
        {
            _konkerorDb = entities;
            //_logger = logger;
        }
        public void Create(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid().ToString();
            transaction.CreatedDate = DateTime.UtcNow;
            _konkerorDb.Transactions.Add(transaction);
            _konkerorDb.SaveChanges();
        }

        public Transaction Get(string transactionId)
        {
            var tr = _konkerorDb.Transactions
                .Where(c => c.Id.Equals(transactionId))
                .FirstOrDefault();

            return tr;
        }

        public Transaction GetLatestByDevise(string deviseId)
        {
            var tr = _konkerorDb.Transactions
                .Where(c => c.DeviseId.Equals(deviseId)
                    && c.Closed.Equals(false))
                .OrderByDescending(c=> c.CreatedDate)
                .FirstOrDefault();

            return tr;
        }

        public void CloseTransaction(string transactionId)
        {
            var tr = Get(transactionId);
            tr.Closed = true;
            tr.ModifiedDate = DateTime.UtcNow;
            _konkerorDb.SaveChanges();
        }

        public void CloseAllOpenTransactionForDevise(string deviseId)
        {
            var trs = _konkerorDb.Transactions
                .Where(c => c.DeviseId.Equals(deviseId)
                 && c.Closed.Equals(false)).ToList();
            trs.ForEach(t => t.Closed = true);
            _konkerorDb.SaveChanges();
        }
    }
}
