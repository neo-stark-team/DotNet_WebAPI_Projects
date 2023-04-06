using NUnit.Framework;
using CarProject.Controllers;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TestCar
{
    [TestFixture]
    public class CarControllerTests
    {
        private CarController _controller;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _controller = new CarController();
        }

        [Test]
        public void Index_Returns_ViewResult_With_Cars()
        {
            // Act
            IActionResult result = _controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsInstanceOf<List<Car>>(viewResult.Model);
        }

        [Test]
        public void Create_Returns_Redirect_To_Index_Action()
        {
            // Arrange
            var car = new Car
            {
                Car_Brand = "TestBrand",
                Type = "TestType",
                Color = "TestColor",
                Mileage = "TestMileage",
                Price = "TestPrice"
            };

            // Act
            var result = _controller.Create(car) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
        [Test]
        public void Create_Returns_ViewResult()
        {
            // Act
            IActionResult result = _controller.Create();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
