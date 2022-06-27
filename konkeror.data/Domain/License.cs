using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace konkeror.data.Domain
{
    public class License
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid ClientId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid Code { get; set; }
        public bool Active { get; set; }
        public Guid ComputerCode { get; set; }
        public virtual Client Client { get; set; }
    }
}
