using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private StoreDbContext _dbContext;

        public ProductRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
        }
        public void Update(Product newProduct)
        {
            var productInDb = _dbContext.Products.SingleOrDefault(x => x.Id == newProduct.Id);
            productInDb.Id = newProduct.Id;
            productInDb.Name = newProduct.Name;
            productInDb.Brand = newProduct.Brand;
            productInDb.Stocks = newProduct.Stocks;
        }
        public void Delete(int id)
        {
            var productInDb = _dbContext.Products.SingleOrDefault(x => x.Id == id);
            _dbContext.Products.Remove(productInDb);        
        }
        public List<Product> GetData()
        {
            return _dbContext.Products.ToList();
        }
        public List<Product> Search(string value)
        {
            return _dbContext.Products.Where(x => x.Brand.Contains(value)
                                            || x.Name.Contains(value)
                                            || x.Id.ToString().Contains(value)
                                            || x.Stocks.ToString().Contains(value)
                                            ).ToList();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

