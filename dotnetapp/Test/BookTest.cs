using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class Tests
    {
        private BookController _controller;
        [SetUp]
public void SetUp()
{
    _controller = new BookController();
}

[Test]
public void Index_Returns_BooksList()
{
    // arrange

    // act
    var result = _controller.Index() as ViewResult;

    // assert
    Assert.IsNotNull(result);

    var books= result.Model as List<Book>;
    Assert.IsNotNull(books);
    Assert.IsTrue(books.Any());
}

[Test]
public void Create_Book_RedirectToActionResult()
{
    // arrange
    var book= new Book
    {
        Book_Name = "Test Book",
        Author = "Test Author",
        No_of_pages = "100",
        Price = "10.00"
    };

    // act
    var result = _controller.Create(book) as RedirectToActionResult;

    // assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Index", result.ActionName);
}
    }
}