using System.ComponentModel.DataAnnotations;

namespace konkeror.data.Domain
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int PlayersQty { get; set; }
        
    }
}
