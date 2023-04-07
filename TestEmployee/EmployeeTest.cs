using System.Collections.Generic;
using System.Linq;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

[TestFixture]
public class EmployeeControllerTests
{
    private Mock<IEmployeeRepository> _mockRepository;
    private EmployeeController _controller;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _controller = new EmployeeController(_mockRepository.Object);
    }

    [Test]
    public void Index_ReturnsViewWithEmployees()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new Employee { EmployeeID = 1, Name = "John Doe", Email = "john.doe@example.com", Department = "Sales" },
            new Employee { EmployeeID = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Department = "Marketing" }
        };
        _mockRepository.Setup(r => r.GetAll()).Returns(employees);

        // Act
        var result = _controller.Index() as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Model);
        var model = result.Model as IEnumerable<Employee>;
        Assert.AreEqual(2, model.Count());
    }
    

    [Test]
    public void Create_Post_AddsEmployeeToRepositoryAndRedirectsToIndex()
    {
        // Arrange
        var employee = new Employee { Name = "John Doe", Email = "john.doe@example.com", Department = "Sales" };

        // Act
        var result = _controller.Create(employee) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        _mockRepository.Verify(r => r.Add(employee), Times.Once);
    }
    [Test]
public void Index_ReturnsViewWithNoEmployees()
{
    // Arrange
    _mockRepository.Setup(r => r.GetAll()).Returns(new List<Employee>());

    // Act
    var result = _controller.Index() as ViewResult;

    // Assert
    Assert.IsNotNull(result);
    Assert.IsNotNull(result.Model);
    var model = result.Model as IEnumerable<Employee>;
    Assert.AreEqual(0, model.Count());
}


}
