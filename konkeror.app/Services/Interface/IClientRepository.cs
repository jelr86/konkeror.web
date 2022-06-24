using konkeror.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface IClientRepository
    {
        IEnumerable<ClientModel> Get(int page, int take);
        ClientModel Get(string id);
        void Create(CreateClientModel client);
        void Update(string id, UpdateClientModel client);
        void Delete(string id);
    }
}
