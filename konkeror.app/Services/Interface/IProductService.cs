using konkeror.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services.Interface
{
    public interface IProductService
    {
        ServiceResult<IEnumerable<ProductModel>> Get(int take);
        ServiceResult<ProductModel> Get(string Id);
        ServiceResult<ProductModel> Create(CreateProductModel license);
        ServiceResult<bool> Update(string id, CreateProductModel license);
        ServiceResult<bool> Delete(string id);
    }
}
