using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.DAL;

namespace WcfService.BLL
{
    public class ProductLogic
    {
        private StoreDbContext _context;
        public ProductLogic(StoreDbContext context)
        {
            _context = context;
        }
        public void AddProduct(Product product)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                unitOfWork.Products.Add(product);
                unitOfWork.Save();
            }
        }
        public void DeleteProduct(int id)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                unitOfWork.Products.Delete(id);
                unitOfWork.Save();
            }
        }
        public List<Product> GetProduct()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                return unitOfWork.Products.GetData();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                unitOfWork.Products.Update(product);
                unitOfWork.Save();
            }
        }
        public List<Product> SearchProduct(string value)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                return unitOfWork.Products.Search(value);
            }
        }
    }
}