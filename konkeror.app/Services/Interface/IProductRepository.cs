using konkeror.app.Models;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get(int page, int take);
        Product Get(string id);
        void Create(Product product);
        void Update(string id, Product client);
        void Delete(string id);
    }
}
