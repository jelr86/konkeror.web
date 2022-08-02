using konkeror.app.Models;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface IDeviseRepository
    {
        Devise GetDevise(string name, string licenseId);
        void Create(Devise devise);
        IEnumerable<Devise> Get(int page, int take);
    }
}
