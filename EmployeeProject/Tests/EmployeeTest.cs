using NUnit.Framework;
using EmployeeProject.Models;
using System.Collections.Generic;
using System.Linq;
namespace EmployeeProject.Tests
{
    [TestFixture]
    public class EmployeeRepositoryTests
    {
        private IEmployeeRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new EmployeeRepository();
        }

        [Test]
        public void GetAll_ReturnsEmployees()
        {
            // Arrange
var expected = new List<Employee>
{
    new Employee { EmployeeID = 1, Name = "John", Email = "john@example.com", Department = "IT" },
    new Employee { EmployeeID = 2, Name = "Jane", Email = "jane@example.com", Department = "HR" },
    new Employee { EmployeeID = 3, Name = "Bob", Email = "bob@example.com", Department = "Marketing" }
};

// Act
var result = _repository.GetAll();

// Assert
Assert.That(result, Is.Not.Empty);

        }

        [Test]
        public void Add_AddsEmployeeToDatabase()
        {
            // Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 1,
                Name = "John Smith",
                Email = "john.smith@example.com",
                Department = "Sales"
            };

            // Act
            _repository.Add(employee);

            // Assert
            var result = _repository.GetAll();
            Assert.That(result, Is.Not.Empty);

        }
    }
}
