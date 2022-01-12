using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Commands;
using WpfApp.Model;
using WpfApp.ServiceReference;

namespace WpfApp.ViewModel
{
    public class MainViewModel : ViewModel
    {
        private ProductService _productService;
        public MainViewModel()
        {
            AddCommand = new DelegateCommand<object>(AddExecute);
            DeleteCommand = new DelegateCommand<object>(DeleteExecute);
            UpdateCommand = new DelegateCommand<object>(UpdateExecute);
            SearchCommand = new DelegateCommand<object>(SearchExecute);
            _productService = new ProductService();
            Load();
        }
      
        private void AddExecute(object sender)
        {
            if (String.IsNullOrEmpty(Brand) || String.IsNullOrEmpty(Product) || String.IsNullOrEmpty(Stocks.ToString()))
            {
                MessageBox.Show("Invalid Inputs!", "Error");
            }
            else
            {
                _productService.Add(new Product
                {
                    Name = Product,
                    Brand = Brand,
                    Stocks = Convert.ToInt32(Stocks)
                });
                Load();
                Clear();
            }
        }
        private void DeleteExecute(object sender)
        {
            if (MySelectedItem != null)
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _productService.Delete(Convert.ToInt32(Id));
                    Load();
                    Clear();
                }
            }
        }
        private void UpdateExecute(object sender)
        {
            var newProduct = new Product()
            {
                Id = Convert.ToInt32(Id),
                Name = Product,
                Brand = Brand,
                Stocks = Convert.ToInt32(Stocks)
            };
            _productService.Update(newProduct);
            Load();
            Clear();
        }
        private void SearchExecute(object sender)
        {
            var search = Search;
            var filteredList = _productService.Search(search);
            
            if(filteredList.Count < 1)
            {
                MessageBox.Show("No Records found!", "Warning");              
            }
            else
            {
                List = filteredList;
            }            
        }
        private void Load()
        {
            List = _productService.GetData();
        }
        private void Clear()
        {
            Id = string.Empty;
            Product = string.Empty;
            Brand = string.Empty;
            Stocks = string.Empty;
        }
        public DelegateCommand<object> AddCommand { get; set; }
        public DelegateCommand<object> DeleteCommand { get; set; }
        public DelegateCommand<object> UpdateCommand { get; set; }
        public DelegateCommand<object> SearchCommand { get; set; }

        private ObservableCollection<Product> _list;
        public ObservableCollection<Product> List
        {
            get { return _list; }
            set
            {
                _list = value;
                NotifyProperyChanged("List");
            }
        }

        private Product _selectedItem;
        public Product MySelectedItem 
        { 
            get 
            {               
                return _selectedItem; 
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    if(value != null)
                    {
                        Brand = value.Brand;
                        Product = value.Name;
                        Stocks = value.Stocks.ToString();
                        Id = value.Id.ToString();
                    }
                    
                }
            }
        }
        private string _product;
        public string Product
        {
            get { return _product; }
            set
            {
                if (value != _product)
                {
                    _product = value;
                    NotifyProperyChanged("Product");
                }
            }
        }

        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set
            {
                if (value != _brand)
                {
                    _brand = value;
                    NotifyProperyChanged("Brand");
                }
            }
        }

        private string _stocks;
        public string Stocks
        {
            get { return _stocks; }
            set
            {
                if (value != _stocks)
                {
                    _stocks = value;
                    NotifyProperyChanged("Stocks");
                }
            }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyProperyChanged("Id");
                }
            }
        }

        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                if (value != _search)
                {
                    _search = value;
                    NotifyProperyChanged("Search");
                }
            }
        }      
    }
}
