using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsPro.Controllers;
using SportsPro.Data;
using SportsPro.Models;
using System;
using Xunit;

namespace XUnitTestProject2.Tests
{
   public class CustomerControllerTests
    {
        [Fact]
        public void ListActionMethod_ReturnsAViewResult()
        {
            //arrange
            var unit = new Mock<IUnitOfWork>();
            var products = new Mock<IGenericRepository<Product>>();
            var customers = new Mock<IGenericRepository<Customer>>();
            unit.Setup(r => r.ProductRepository).Returns(products.Object);
            unit.Setup(r => r.CustomerRepository).Returns(customers.Object);

            var http = new Mock<IHttpContextAccessor>();

            var controller = new ProductController(unit.Object);

            //act
            var result = controller.List();

            //assert
            Assert.IsType<ViewResult>(result);


        }
        [Fact]
        public void AddActionMethod_ReturnsAViewResult()
        {
            //arrange
            var unit = new Mock<IUnitOfWork>();
            var products = new Mock<IGenericRepository<Product>>();
            var customers = new Mock<IGenericRepository<Customer>>();
            unit.Setup(r => r.ProductRepository).Returns(products.Object);
            unit.Setup(r => r.CustomerRepository).Returns(customers.Object);

            var http = new Mock<IHttpContextAccessor>();

            var controller = new ProductController(unit.Object);

            //act
            var result = controller.Add();

            //assert
            Assert.IsType<ViewResult>(result);


        }
    }
}
