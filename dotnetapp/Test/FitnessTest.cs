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

namespace dotnetapp.Test
{
    public class FitnessTrackerControllerTests
    {
        private FitnessTrackerDbContext _context;
        private FitnessTrackerController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FitnessTrackerDbContext>()
                .UseInMemoryDatabase(databaseName: "FitnessTrackerTest")
                .Options;

            _context = new FitnessTrackerDbContext(options);

            _controller = new FitnessTrackerController(_context);

            _context.FitnessTrackers.AddRange(new List<FitnessTracker>
            {
                new FitnessTracker { Id = 1, Workout_Date = new DateTime(2023, 4, 20), Steps = 100, Distance_km = 5, CaloriesBurned = 700, HeartRate = 87, SleepDuration = 8.5 },
                new FitnessTracker { Id = 2, Workout_Date = new DateTime(2023, 5, 1), Steps = 500, Distance_km = 10, CaloriesBurned = 1000, HeartRate = 77,SleepDuration = 8.5 },
                new FitnessTracker { Id = 3, Workout_Date = new DateTime(2023, 5, 1), Steps = 10000, Distance_km = 7, CaloriesBurned = 500, HeartRate = 75, SleepDuration = 8.5 }
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetFitnessTrackers_ReturnsAllEvents()
        {
            var result = await _controller.GetFitnessTrackers();

            Assert.IsInstanceOf(typeof(List<FitnessTracker>), result.Value);
            Assert.AreEqual(3, result.Value.Count());
        }

    
        [Test]
        public async Task GetFitnessTracker_InvalidId_ReturnsNotFound()
        {
            int eventId = 100;

            var result = await _controller.GetFitnessTracker(eventId);

            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
            
        }

        [Test]
        public async Task GetFitnessTracker_ReturnsSingleEvent()
        {
            int eventId = 2;

            var result = await _controller.GetFitnessTracker(eventId);

            Assert.IsInstanceOf(typeof(FitnessTracker), result.Value);
            Assert.AreEqual(eventId, result.Value.Id);
        }

        [Test]
        public async Task DeleteFitnessTracker_DeletesExistingEvent()
        {
            int eventId = 1;

            var result = await _controller.DeleteFitnessTracker(eventId);

            Assert.IsInstanceOf(typeof(NoContentResult), result);
            var eventFromDb = _context.FitnessTrackers.Find(eventId);
            Assert.IsNull(eventFromDb);
        }

         [Test]
        public async Task PostFitnessTracker_WithValidData_ReturnsCreatedAtActionResult()
        {
            var testFitnessTracker = new FitnessTracker {Id = 4 ,Workout_Date = new DateTime(2023, 7, 1), Steps = 5000, Distance_km = 5, CaloriesBurned = 750, HeartRate = 77, SleepDuration = 8 };

            var result = await _controller.PostFitnessTracker(testFitnessTracker);

            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        }

        [Test]
        public async Task PostFitnessTracker_WithValidData_ReturnsCreatedFitnessTracker()
        {
            var testFitnessTracker = new FitnessTracker { Id = 5 ,Workout_Date = new DateTime(2023, 7, 1), Steps = 750, Distance_km = 7, CaloriesBurned = 450, HeartRate = 65, SleepDuration = 7  };

            var result = await _controller.PostFitnessTracker(testFitnessTracker);

            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);

            var createdFitnessTracker = createdResult.Value as FitnessTracker;
            Assert.IsNotNull(createdFitnessTracker);
            Assert.AreEqual(testFitnessTracker.Workout_Date, createdFitnessTracker.Workout_Date);
            Assert.AreEqual(testFitnessTracker.Steps, createdFitnessTracker.Steps);
            Assert.AreEqual(testFitnessTracker.Distance_km, createdFitnessTracker.Distance_km);
            Assert.AreEqual(testFitnessTracker.CaloriesBurned, createdFitnessTracker.CaloriesBurned);
        }  
        
        [Test]
        public async Task PutFitnessTracker_Should_Update_FitnessTracker()
        {
            // Arrange
            var controller = new FitnessTrackerController(_context);
            var FitnessTracker = new FitnessTracker { 
                        Id = 10,
                        Workout_Date = new DateTime(2023, 04, 13),
                        Steps = 30,
                        Distance_km = 1,
                        CaloriesBurned = 750,
                        HeartRate = 84,
                        SleepDuration = 8
                    };
            await controller.PostFitnessTracker(FitnessTracker);

            FitnessTracker.HeartRate = 71;
            FitnessTracker.Steps = 200;

            var result = await controller.PutFitnessTracker(FitnessTracker.Id, FitnessTracker);

            Assert.That(result, Is.InstanceOf<NoContentResult>());

            var updatedFitnessTracker = await _context.FitnessTrackers.FindAsync(FitnessTracker.Id);
            Assert.That(updatedFitnessTracker, Is.Not.Null);
            Assert.That(updatedFitnessTracker.HeartRate, Is.EqualTo(FitnessTracker.HeartRate));
            Assert.That(updatedFitnessTracker.Steps, Is.EqualTo(FitnessTracker.Steps));
        }

        
        [Test]
        public async Task PutFitnessTracker_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var FitnessTracker = new FitnessTracker
            {
                Id = 10,
                Workout_Date = new DateTime(2023, 04, 13),
                Steps = 3000,
                Distance_km = 8,
                CaloriesBurned = 1000,
                HeartRate = 88,
                SleepDuration = 9
            };
            _context.FitnessTrackers.Add(FitnessTracker);
            await _context.SaveChangesAsync();

            var result = await _controller.PutFitnessTracker(2, FitnessTracker);

            Assert.IsInstanceOf(typeof(BadRequestResult), result);
        }
    }
}