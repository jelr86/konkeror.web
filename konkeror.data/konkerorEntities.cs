using konkeror.data.Domain;
using System;
using System.Data.Entity;

namespace konkeror.data
{
    public class konkerorEntities : DbContext
    {
        public konkerorEntities()
            :base("name=konkerordb")
        {
        }


        public virtual DbSet<Licencia> Licencias { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
    }
}
