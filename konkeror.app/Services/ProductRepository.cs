using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.app.Services.Interfaces;
using konkeror.data;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services
{
    public class ProductRepository : IProductRepository
    {
        private konkerorEntities _konkerorDb { get; }
        //private ILogger _logger { get; set; }
        public ProductRepository(konkerorEntities entities)
        {
            _konkerorDb = entities;
            //_logger = logger;
        }

        public IEnumerable<Product> Get(int page, int take)
        {
            return _konkerorDb.Products
                //.Skip(page*take)
                .Take(take).ToList();
        }

        public Product Get(string id)
        {
            var prod = _konkerorDb.Products
                .Where(c => c.Id.Equals(id))
                .FirstOrDefault();

            return prod;
        }

        public void Create(Product product)
        {
            
            product.Id = Guid.NewGuid().ToString();
            _konkerorDb.Products.Add(product);
            _konkerorDb.SaveChanges();
        }

        public void Update(string id, Product product)
        {
            var prod = _konkerorDb.Products
                .Where(c => c.Id.Equals(id))
                .FirstOrDefault();
            prod.Name = product.Name;
            prod.Minutes = product.Minutes;
            prod.PlayersQty = product.PlayersQty;
            _konkerorDb.SaveChanges();
        }

        public void Delete(string id)
        {
            var prod = _konkerorDb.Products
            .Where(c => c.Id.Equals(id))
            .FirstOrDefault();
            _konkerorDb.Products.Remove(prod);
            _konkerorDb.SaveChanges();
        }
    }
}
