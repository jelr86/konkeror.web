using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Models
{
    public class LicenseModel
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
