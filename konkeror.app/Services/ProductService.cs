using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace konkeror.app.Services
{
    public class ProductService : BaseService, IProductService
    {
        private IProductRepository ProductRepo { get; }
        private IMapper Mapper { get; }
        public ProductService(IProductRepository productRepo, IMapper mapper)
        {
            ProductRepo = productRepo;
            Mapper = mapper;
        }


        public ServiceResult<IEnumerable<ProductModel>> Get(int take)
        {
            var result = new ServiceResult<IEnumerable<ProductModel>>();

            result.Result = ProductRepo.Get(1, take)
                .Select(c => Mapper.Map<ProductModel>(c));
            
            return result;
        }

        public ServiceResult<ProductModel> Get(string Id)
        {
            var result = new ServiceResult<ProductModel>();

            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(Id, "Id", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            result.Result = Mapper.Map<ProductModel>(ProductRepo.Get(Id));
            return result;
        }

        public ServiceResult<ProductModel> Create(CreateProductModel prod)
        {
            var result = new ServiceResult<ProductModel>();
            var validationMessages = new List<ValidationMessage>();
            
            ValidateStringValue(prod.Name, "product.Name", validationMessages);
            ValidateIntValue(prod.Minutes, "product.Minutes", validationMessages);
            ValidateIntValue(prod.PlayersQty, "product.PlayersQty", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            var p = Mapper.Map<Product>(prod);
            ProductRepo.Create(p);

            result.Result = Mapper.Map<ProductModel>(p);
            return result;
        }

        public ServiceResult<bool> Update(string id, CreateProductModel prod)
        {
            var result = new ServiceResult<bool>();
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(id, "id", validationMessages);
            ValidateStringValue(prod.Name, "product.Name", validationMessages);
            ValidateIntValue(prod.Minutes, "product.Minutes", validationMessages);
            ValidateIntValue(prod.PlayersQty, "product.PlayersQty", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            var l = Mapper.Map<Product>(prod);
            ProductRepo.Update(id, l);

            result.Result = true;
            return result;
        }

        public ServiceResult<bool> Delete(string id)
        {
            var result = new ServiceResult<bool>();
            var validationMessages = new List<ValidationMessage>();
            ValidateGuidValue(id, "id", validationMessages);
            if (validationMessages.Count() > 0)
            {
                result.ValidationMessages = validationMessages;
                return result;
            }
            try
            {
                ProductRepo.Delete(id);
                result.Result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }

    }
}
