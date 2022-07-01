using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace konkeror.data.Domain
{
    public class License
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        //[Column("clientid")]
        public string ClientId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public string ComputerCode { get; set; }
        //[ForeignKey("clientid")]
        //public virtual Client Client { get; set; }
    }
}
