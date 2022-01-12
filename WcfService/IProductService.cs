using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {

        [OperationContract]
        List<Product> GetProduct();

        [OperationContract]
        void AddProduct(Product product);

        [OperationContract]
        void UpdateProduct(Product product);

        [OperationContract]
        void DeleteProduct(int id);

        [OperationContract]
        List<Product> SearchProduct(string value);



        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Stocks { get; set; }
    }
}
