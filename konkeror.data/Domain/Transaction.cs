using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.data.Domain
{
    public class Transaction
    {
        [Key]
        public string Id { get; set; }
        public string DeviseId { get; set; }
        public int Minutes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ModifiedDate { get; set; }



    }
}
