using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace konkeror.app.Models
{
    public class ProductModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int PlayersQty { get; set; }
    }
}