using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Models
{
    public class CreateLicenseModel
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
