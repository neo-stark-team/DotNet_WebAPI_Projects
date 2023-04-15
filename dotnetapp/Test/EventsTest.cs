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
    public class EventApiControllerTests
    {
        private EventApiDbContext _context;
        private EventApiController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventApiDbContext>()
                .UseInMemoryDatabase(databaseName: "EventApiTest")
                .Options;

            _context = new EventApiDbContext(options);

            _controller = new EventApiController(_context);

            _context.EventApis.AddRange(new List<EventApi>
            {
                new EventApi { Id = 1, Event_Name = "Event 1", Event_Type = "Type A", Start_Date = new DateTime(2023, 4, 20), End_Date = new DateTime(2023, 4, 25), Location = "Location A" },
                new EventApi { Id = 2,Event_Name = "Event 2", Event_Type = "Type B", Start_Date = new DateTime(2023, 5, 1), End_Date = new DateTime(2023, 5, 5), Location = "Location B" },
                new EventApi { Id = 3, Event_Name = "Event 3", Event_Type = "Type C", Start_Date = new DateTime(2023, 6, 1), End_Date = new DateTime(2023, 6, 5), Location = "Location C" }
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetEventApis_ReturnsAll()
        {
            var result = await _controller.GetEventApis();

            Assert.IsInstanceOf(typeof(List<EventApi>), result.Value);
            Assert.AreEqual(3, result.Value.Count());
        }

    
        [Test]
        public async Task GetEventApi_InvalidId()
        {
            int eventId = 100;

            var result = await _controller.GetEventApi(eventId);

            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
            
        }

        [Test]
        public async Task GetEventApi_ReturnsSingle()
        {
            int eventId = 2;

            var result = await _controller.GetEventApi(eventId);

            Assert.IsInstanceOf(typeof(EventApi), result.Value);
            Assert.AreEqual(eventId, result.Value.Id);
        }

        [Test]
        public async Task DeleteEventApi_DeleteEvent()
        {
            int eventId = 1;

            var result = await _controller.DeleteEventApi(eventId);

            Assert.IsInstanceOf(typeof(NoContentResult), result);
            var eventFromDb = _context.EventApis.Find(eventId);
            Assert.IsNull(eventFromDb);
        }

         [Test]
        public async Task PostEventApi_WithValidData()
        {
            var testEventApi = new EventApi {Id = 4 ,Event_Name = "Event 4", Event_Type = "Type D", Start_Date = new DateTime(2023, 7, 1), End_Date = new DateTime(2023, 7, 5), Location = "Location D" };

            var result = await _controller.PostEventApi(testEventApi);

            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        }

        [Test]
        public async Task PostEventApi_ReturnsCreatedEventApi()
        {
            var testEventApi = new EventApi { Id = 5 ,Event_Name = "Event 5", Event_Type = "Type D", Start_Date = new DateTime(2023, 7, 1), End_Date = new DateTime(2023, 7, 5), Location = "Location D"  };

            var result = await _controller.PostEventApi(testEventApi);

            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);

            var createdEventApi = createdResult.Value as EventApi;
            Assert.IsNotNull(createdEventApi);
            Assert.AreEqual(testEventApi.Event_Name, createdEventApi.Event_Name);
            Assert.AreEqual(testEventApi.Event_Type, createdEventApi.Event_Type);
            Assert.AreEqual(testEventApi.Start_Date, createdEventApi.Start_Date);
            Assert.AreEqual(testEventApi.End_Date, createdEventApi.End_Date);
        }    

        [Test]
        public async Task PutEventApi_Update()
        {
            // Arrange
            var controller = new EventApiController(_context);
            var EventApi = new EventApi { 
                        Id = 10,
                        Event_Name = "Meeting",
                        Event_Type = "conference",
                        Start_Date = new DateTime(2023, 04, 13),
                        End_Date = new DateTime(2023, 04, 21),
                        Location = "CBE"
                    };
            await controller.PostEventApi(EventApi);

            EventApi.Location = "Chennai";
            EventApi.Event_Type = "Sports";

            var result = await controller.PutEventApi(EventApi.Id, EventApi);

            Assert.That(result, Is.InstanceOf<NoContentResult>());

            var updatedEventApi = await _context.EventApis.FindAsync(EventApi.Id);
            Assert.That(updatedEventApi, Is.Not.Null);
            Assert.That(updatedEventApi.Location, Is.EqualTo(EventApi.Location));
            Assert.That(updatedEventApi.Event_Type, Is.EqualTo(EventApi.Event_Type));
        }

        
        [Test]
        public async Task PutEventApi_WhenIdsDoNotMatch()
        {
            var EventApi = new EventApi
            {
                Id = 10,
                Event_Name = "Meeting",
                Event_Type = "conference",
                Start_Date = new DateTime(2023, 04, 13),
                End_Date = new DateTime(2023, 04, 21),
                Location = "CBE"
            };
            _context.EventApis.Add(EventApi);
            await _context.SaveChangesAsync();

            var result = await _controller.PutEventApi(2, EventApi);

            Assert.IsInstanceOf(typeof(BadRequestResult), result);
        }    
    }
}
