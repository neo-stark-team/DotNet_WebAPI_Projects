using System.Threading.Tasks;
using dotnetapp.Controllers;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;

namespace dotnetapp.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> productService;
        ProductController productController;

         public ProductControllerTests()
         {
             productService = new Mock<IProductService>();
             productController =new ProductController(productService.Object);
         }
         [Test]
        public void TestCase7_Product()
        {
            //Arrange
            var list =GetProductsData();
            var productsList =list.AsQueryable();
            productService.Setup(x => x.GetProductList()).Returns(productsList);
            var productController =new ProductController(productService.Object);
            //Act
            var productResult=productController.GetAll();
            //Assert
            Assert.NotNull(productResult);
            Assert.AreEqual(GetProductsData().Count(),productResult.Count());
            Assert.AreEqual(GetProductsData().ToString(), productResult.ToString());
            Assert.IsTrue(productsList.Equals(productResult));
        }

        [Test]
        public void TestCase8_Product()
        {
            //Arrange
            var productsList =GetProductsData();
            productService.Setup(x => x.AddProduct(productsList[1])).Returns(true);
            //Act
            var result=productController.AddProduct(productsList[1]);
            //Assert
            Assert.NotNull(result);
            Assert.IsTrue(result);
        }
        [Test]
        public void TestCase9_Product()
        {
            //Arrange
            var productsList =GetProductsData();
            productService.Setup(x => x.DeleteProduct(productsList[1].Id)).Returns(true);
            //Act
            var result = productController.DeleteProduct(productsList[1].Id);
            //Assert
            Assert.NotNull(result);
            Assert.IsTrue(result);
        }
        
        private List<Product> GetProductsData()
        {
           List<Product> productsData = new List<Product>
           {
               new Product
               {
                   Id =1,
                   Name="Book1",
                   Price=280.00M
                  
               },
               new Product
               {
                   Id =2,
                   Name="Book2",
                   Price=180.00M
               },
               new Product
               {
                   Id =3,
                   Name="Book3",
                   Price=395.00M
               }
           };
           return productsData;
        }
    }
}
