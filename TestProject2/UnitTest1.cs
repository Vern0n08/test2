using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WcfService;
using WcfService.DAL;
using WcfService.BLL;
using Xunit;

namespace TestProject2
{
    public class UnitTest1
    {      
        [Fact]
        public void GetProduct_UserLoadProduct_ReturnAllProducts()
        {
            //Arrange
            var list = new List<Product>();
            var mockSet = MockDbSet(new List<Product> {
                new Product() { Id = 99, Name = "Keyboard", Brand = "Keychron", Stocks = 9 }
            });            

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            //Act         
            var productLogic = new ProductLogic(mockContext.Object);
            list = productLogic.GetProduct();

            var count = list.Count;
            //Assert
            Assert.Equal(1, count);
            Assert.Equal(99, list[0].Id);
            Assert.Equal("Keyboard", list[0].Name);
            Assert.Equal("Keychron", list[0].Brand);
            Assert.Equal(9, list[0].Stocks);
        }

        [Fact]
        public void AddProduct_UserAddProduct_ProductSaved()
        {
            //Arrange
            var mockSet = MockDbSet(new List<Product> { });           
            var products = new Product() { Id = 99, Name = "Test", Brand = "Test", Stocks = 9 };

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
            var productLogic = new ProductLogic(mockContext.Object);
            //Act
            productLogic.AddProduct(products);

            //Assert
            Assert.Equal(1, (from a in mockContext.Object.Products select a).Count());
            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteProduct_UserDeleteProduct_ProductDeleted()
        {
            //Arrange
            var mockSet = MockDbSet(new List<Product>{
                new Product() { Id = 99, Name = "Test", Brand = "Test", Stocks = 9 }
            });            

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);   
            var productLogic = new ProductLogic(mockContext.Object);

            //Act
            productLogic.DeleteProduct(99);

            //Assert
            Assert.Equal(0, (from a in mockContext.Object.Products select a).Count());
            mockSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void UpdateProduct_UserUpdateProduct_ProductUpdated()
        {
            //Arrange
            var list = new List<Product>();
            var mockSet = MockDbSet(new List<Product> {
                new Product() { Id = 1, Name = "Keyboard", Brand = "Keychron", Stocks = 9 }
            });
            var productNewUpdate = new Product() { Id = 1, Name = "Mouse", Brand = "SteelSeries", Stocks = 2 };

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var productLogic = new ProductLogic(mockContext.Object);

            //Act
            productLogic.UpdateProduct(productNewUpdate);
            list = productLogic.GetProduct();

            //Assert
            Assert.Equal(1, list[0].Id);
            Assert.Equal("Mouse", list[0].Name);
            Assert.Equal("SteelSeries", list[0].Brand);
            Assert.Equal(2, list[0].Stocks);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SearchProduct_UserSearchProduct_GetFilteredProduct()
        {
            //Arrange
            var list = new List<Product>();
            var mockSet = MockDbSet(new List<Product> {
                new Product() { Id = 1, Name = "Keyboard", Brand = "Keychron", Stocks = 9 },
                new Product() { Id = 2, Name = "Monitor", Brand = "Acer", Stocks = 10 }
            });

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var productLogic = new ProductLogic(mockContext.Object);

            //Act          
            list = productLogic.SearchProduct("Keyboard");

            //Assert
            Assert.Equal(1, list[0].Id);
            Assert.Equal("Keyboard", list[0].Name);
            Assert.Equal("Keychron", list[0].Brand);
            Assert.Equal(9, list[0].Stocks);
        }
        
        public static Mock<DbSet<T>> MockDbSet<T>(List<T> inputDbSetContent) where T : class
        {
            var DbSetContent = inputDbSetContent.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();

            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(DbSetContent.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(DbSetContent.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(DbSetContent.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => inputDbSetContent.GetEnumerator());
            dbSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>((s) => inputDbSetContent.Add(s));
            dbSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>((s) => inputDbSetContent.Remove(s));
            return dbSet;
        }
    }
}
