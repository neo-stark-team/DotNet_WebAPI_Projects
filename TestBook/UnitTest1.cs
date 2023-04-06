using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestBook
{
    [TestFixture]
    public class BookControllerTests
    {
        private BookController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new BookController();
        }

        [Test]
        public void Index_Returns_ViewResult_With_Books()
        {
            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Book>>(result.Model);
        }

        [Test]
        public void Create_Returns_ViewResult()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_Post_RedirectsTo_Index_On_Successful_Insert()
        {
            // Arrange
            var book = new Book
            {
                Book_Name = "Test Book",
                Author = "Test Author",
                No_of_pages = "100",
                Price = "10.00"
            };

            // Act
            var result = _controller.Create(book) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        
    }
}
