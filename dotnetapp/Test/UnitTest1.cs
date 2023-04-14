using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Controllers;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Tests
{
    public class TransactionControllerTests
    {
        private TransactionDbContext _context;
        private TransactionController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TransactionDbContext>()
                .UseInMemoryDatabase(databaseName: "TransactionTest")
                .Options;

            _context = new TransactionDbContext(options);

            _controller = new TransactionController(_context);

            _context.Transactions.AddRange(new List<Transaction>
            {
                new Transaction { Id = 1, TransactionDate = new DateTime(2023, 4, 20), ProductName = "watch", Quantity = 5, TotalPrice = 700 },
                new Transaction { Id = 2, TransactionDate = new DateTime(2023, 5, 1), ProductName = "Shirt", Quantity = 10, TotalPrice = 1000 },
                new Transaction { Id = 3, TransactionDate = new DateTime(2023, 5, 1), ProductName = "Mobile", Quantity = 7, TotalPrice = 500 }
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetTransactions_ReturnsAllEvents()
        {
            var result = await _controller.GetTransactions();

            Assert.IsInstanceOf(typeof(List<Transaction>), result.Value);
            Assert.AreEqual(3, result.Value.Count());
        }
        [Test]
        public async Task GetTransaction_InvalidId_ReturnsNotFound()
        {
            int eventId = 100;

            var result = await _controller.GetTransaction(eventId);

            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
            
        }

        [Test]
        public async Task GetTransaction_ReturnsSingleEvent()
        {
            int eventId = 2;

            var result = await _controller.GetTransaction(eventId);

            Assert.IsInstanceOf(typeof(Transaction), result.Value);
            Assert.AreEqual(eventId, result.Value.Id);
        }

        [Test]
        public async Task DeleteTransaction_DeletesExistingEvent()
        {
            int eventId = 1;

            var result = await _controller.DeleteTransaction(eventId);

            Assert.IsInstanceOf(typeof(NoContentResult), result);
            var eventFromDb = _context.Transactions.Find(eventId);
            Assert.IsNull(eventFromDb);
        }

         [Test]
        public async Task PostTransaction_WithValidData_ReturnsCreatedAtActionResult()
        {
            var testTransaction = new Transaction {Id = 4 ,TransactionDate = new DateTime(2023, 7, 1), ProductName = "Laptop", Quantity = 1, TotalPrice = 75000 };

            var result = await _controller.PostTransaction(testTransaction);

            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        }

        [Test]
        public async Task PostTransaction_WithValidData_ReturnsCreatedTransaction()
        {
            var testTransaction = new Transaction { Id = 5 ,TransactionDate = new DateTime(2023, 7, 1), ProductName = "Charger", Quantity = 7, TotalPrice = 10000 };

            var result = await _controller.PostTransaction(testTransaction);

            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);

            var createdTransaction = createdResult.Value as Transaction;
            Assert.IsNotNull(createdTransaction);
            Assert.AreEqual(testTransaction.TransactionDate, createdTransaction.TransactionDate);
            Assert.AreEqual(testTransaction.ProductName, createdTransaction.ProductName);
            Assert.AreEqual(testTransaction.Quantity, createdTransaction.Quantity);
            Assert.AreEqual(testTransaction.TotalPrice, createdTransaction.TotalPrice);
        }  
        
        [Test]
        public async Task PutTransaction_Should_Update_Transaction()
        {
            // Arrange
            var controller = new TransactionController(_context);
            var Transaction = new Transaction { 
                        Id = 10,
                        TransactionDate = new DateTime(2023, 04, 13),
                        ProductName = "Bat",
                        Quantity = 1,
                        TotalPrice = 5000
                    };
            await controller.PostTransaction(Transaction);

            Transaction.Quantity = 5;
            Transaction.ProductName = "Shoes";

            var result = await controller.PutTransaction(Transaction.Id, Transaction);

            Assert.That(result, Is.InstanceOf<NoContentResult>());

            var updatedTransaction = await _context.Transactions.FindAsync(Transaction.Id);
            Assert.That(updatedTransaction, Is.Not.Null);
            Assert.That(updatedTransaction.Quantity, Is.EqualTo(Transaction.Quantity));
            Assert.That(updatedTransaction.ProductName, Is.EqualTo(Transaction.ProductName));
        }

        
        [Test]
        public async Task PutTransaction_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var Transaction = new Transaction
            {
                Id = 10,
                TransactionDate = new DateTime(2023, 04, 13),
                ProductName = "Shirt",
                Quantity = 8,
                TotalPrice = 1000
            };
            _context.Transactions.Add(Transaction);
            await _context.SaveChangesAsync();

            var result = await _controller.PutTransaction(2, Transaction);

            Assert.IsInstanceOf(typeof(BadRequestResult), result);
        }
    }
}
