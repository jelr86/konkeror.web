using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Models
{
    public class TransactionModel
    {
        public string LicenseId { get; set; }
        public string Devise { get; set; }
        public int Minutes { get; set; }
    }
}
