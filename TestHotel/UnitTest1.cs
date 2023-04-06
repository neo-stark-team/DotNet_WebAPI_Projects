using NUnit.Framework;
using BookProject.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace TestHotel
{
    [TestFixture]
    public class HotelControllerTests
    {
        private HotelController _controller;
        

        [SetUp]
        public void Setup()
        {
           
            _controller = new HotelController();
        }

        [Test]
        public void Index_Returns_ViewResult_With_Hotels()
        {
             // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Hotel>>(result.Model);
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
            var hotel = new Hotel
            {
                Hotel_Name = "Test Hotel",
                City = "Test City",
                No_of_Rooms = "100",
                Rating = "4"
            };
            // Act
            var result = _controller.Create(hotel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        
    }
}
