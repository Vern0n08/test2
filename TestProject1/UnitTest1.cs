using Moq;
using System;
using System.Collections.Generic;
using WcfService;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
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
    }
}
