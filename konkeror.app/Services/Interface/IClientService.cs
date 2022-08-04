using konkeror.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface IClientService
    {
        ServiceResult<IEnumerable<ClientModel>> Get(int take);
        ServiceResult<ClientModel> Get(string Id);
        ServiceResult<ClientModel> Create(CreateClientModel client);
        ServiceResult<bool> Update(string id, UpdateClientModel client);
        ServiceResult<bool> Delete(string id);
    }
}
