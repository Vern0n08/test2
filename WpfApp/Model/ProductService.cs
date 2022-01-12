using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.ViewModel;
using WpfApp.ServiceReference;

namespace WpfApp.Model
{
    public class ProductService
    {
        private ProductServiceClient _productServiceClient;

        public ProductService()
        {
            _productServiceClient = new ProductServiceClient();
        }
        public void Add(Product product)
        {
            _productServiceClient.AddProduct(product);
        }
        public void Update(Product newProduct)
        {
            _productServiceClient.UpdateProduct(newProduct);
        }
        public void Delete(int id)
        {
            _productServiceClient.DeleteProduct(id);        
        }
        public ObservableCollection<Product> GetData()
        {
            return _productServiceClient.GetProduct();
        }
        public ObservableCollection<Product> Search(string value)
        {
            return _productServiceClient.SearchProduct(value);
        }
    }
}

