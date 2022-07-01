using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.data.Domain
{
    public class Client
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Nit { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<License> Licenses { get; set; }
    }
}
