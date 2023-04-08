using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class Tests
    {
        private HotelController _controller;
        [SetUp]
public void SetUp()
{
    _controller = new HotelController();
}

[Test]
public void Index_Returns_ViewResult_With_Hotels()
{
    // arrange

    // act
    var result = _controller.Index() as ViewResult;

    // assert
    Assert.IsNotNull(result);

    var hotels = result.Model as List<Hotel>;
    Assert.IsNotNull(hotels);
    Assert.IsTrue(hotels.Any());
}

[Test]
public void Create_Returns_RedirectToActionResult_When_Valid_Hotel()
{
    // arrange
    var hotel = new Hotel
    {
        Hotel_Name = "Test Hotel",
        City = "Test City",
        No_of_Rooms = "5",
        Rating = "5"
    };

    // act
    var result = _controller.Create(hotel) as RedirectToActionResult;

    // assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Index", result.ActionName);
}
    }
}