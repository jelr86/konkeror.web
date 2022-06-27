using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Models
{
    public class CreateLicenseResultModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
