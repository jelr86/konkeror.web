using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.data.Domain
{
    public class Devise
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LicenseId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
