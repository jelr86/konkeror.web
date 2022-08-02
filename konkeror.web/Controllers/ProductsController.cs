using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.web.Common;

namespace konkeror.web.Controllers
{
    public class ProductsController : ApiController
    {
        private IProductService ProductService { get; }
        public ProductsController(IProductService service)
        {
            ProductService = service;
        }

        
        public IHttpActionResult Get(int page, int take)
        {
            try
            {
                var prods = ProductService.Get(take);
                if (prods.ValidationMessages?.Count > 0)
                    return new ErrorResult(prods.ValidationMessages, Request);
                if (prods.Result == null || prods.Result.Count() == 0)
                    return NotFound();
                return Ok(prods.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Get(string id)
        {
            try
            {
                var prod = ProductService.Get(id);
                if (prod.ValidationMessages?.Count > 0)
                    return new ErrorResult(prod.ValidationMessages, Request);
                if (prod.Result == null)
                    return NotFound();
                return Ok(prod.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Post([FromBody] CreateProductModel prod)
        {
            try
            {
                var r = ProductService.Create(prod);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Put(string id, [FromBody] CreateProductModel value)
        {
            try
            {
                var r = ProductService.Update(id, value);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                var r = ProductService.Delete(id);
                if (r.ValidationMessages?.Count > 0)
                    return new ErrorResult(r.ValidationMessages, Request);
                return Ok(r.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
