using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.DAL;
using WcfService.BLL;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ProductService : IProductService
    {
        private ProductLogic productLogic;
        public ProductService()
        {
            productLogic = new ProductLogic(new StoreDbContext());
        }
        public void AddProduct(Product product)
        {          
            productLogic.AddProduct(product);
        }

        public void DeleteProduct(int id)
        {
            productLogic.DeleteProduct(id);
        }

        public List<Product> GetProduct()
        {
            return productLogic.GetProduct();
        }

        public void UpdateProduct(Product product)
        {
            productLogic.UpdateProduct(product);
        }

        public List<Product> SearchProduct(string value)
        {
           return productLogic.SearchProduct(value);
        }
    }
}
