using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookProject.Models;
using System.Data;
using Microsoft.Data.SqlClient;

public class BookController : Controller
{
    private string connectionString = "User ID=sa;password=examlyMssql@123;server=effddfdaeebcfacbdcbaeaacbbeecfcbdfe-0;Database=bookDB;trusted_connection=false;Persist Security Info=False;Encrypt=False";

    public ActionResult Index()
    {
        List<Book> books = new List<Book>();
try
{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Book";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();

                    book.id = Convert.ToInt32(reader["id"]);
                    book.Book_Name = reader["Book_Name"].ToString();
                    book.Author = reader["Author"].ToString();
                    book.No_of_pages = reader["No_of_pages"].ToString();
                    book.Price = reader["Price"].ToString();

                    books.Add(book);
                }

                reader.Close();
            }
        }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
        return View(books);

    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Book book)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Book (Book_Name, Author, No_of_pages, Price) VALUES (@Book_Name, @Author, @No_of_pages, @Price)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // command.Parameters.AddWithValue("@id", Book.id);
                command.Parameters.AddWithValue("@Book_Name", book.Book_Name);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@No_of_pages", book.No_of_pages);
                command.Parameters.AddWithValue("@Price", book.Price);


                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index");
    }
}
