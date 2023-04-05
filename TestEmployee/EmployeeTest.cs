using NUnit.Framework;
using EmployeeProject.Controllers;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestEmployee
{
    public class Tests
    {
        private EmployeeController _controller;

    [SetUp]
    public void Setup()
    {
        _controller = new EmployeeController();
    }

    [Test]
    public void Index_Returns_ViewResult()
    {
        // Act
        ActionResult result = _controller.Index();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }

    [Test]
    public void Create_Returns_ViewResult()
    {
        // Act
        ActionResult result = _controller.Create();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }

    [Test]
    public void Create_Post_Returns_RedirectToActionResult()
    {
        // Arrange
        Employee employee = new Employee { Name = "John Smith", Email = "john@example.com", Department = "IT" };

        // Act
        ActionResult result = _controller.Create(employee);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
    }
    }
}