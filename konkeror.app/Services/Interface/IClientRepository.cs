using konkeror.app.Models;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface IClientRepository
    {
        IEnumerable<Client> Get(int page, int take);
        Client Get(string id);
        void Create(Client client);
        void Update(string id, Client client);
        void Delete(string id);
    }
}
