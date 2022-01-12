using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WcfService;
using WcfService.Model;
using WcfService.DAL;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void GetProduct_UserLoadProduct_ReturnAllProducts()
        {
            //Arrange
            var productService = new ProductService();
            var list = new List<Product>();
            var product = new List<Product>
            {
                new Product { Name = "test", Brand = "test", Stocks = 2 }              
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(product.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(product.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(product.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(product.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
            //Act
            using (var unitOfWork = new UnitOfWork(mockContext.Object))
            {
                list = unitOfWork.Products.GetData();
                unitOfWork.Save();
            }
            //Assert
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("test", list[0].Name);
        }
        [Test]
        public void AddProduct_UserAddProduct_ProductSaveInDB()
        {
            //Arrange
            var list = new List<Product>();
            var product = new Product()
            {
                Name = "Cellphone",
                Brand = "Asus",
                Stocks = 5
            };
            var products = new List<Product>().AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
            //Act
            using (var unitOfWork = new UnitOfWork(mockContext.Object))
            {
                unitOfWork.Products.Add(product);
                unitOfWork.Save();
            };

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());


        }
    }
}
