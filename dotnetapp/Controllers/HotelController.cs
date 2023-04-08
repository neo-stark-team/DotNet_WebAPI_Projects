using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnetapp.Models;
using System.Data;
using Microsoft.Data.SqlClient;

public class HotelController : Controller
{
    private string connectionString = "User ID=sa;password=examlyMssql@123;server=effddfdaeebcfacbdcbaeaacbbeecfcbdfe-0;Database=HotelDB;trusted_connection=false;Persist Security Info=False;Encrypt=False";

    public ActionResult Index()
    {
        List<Hotel> hotels = new List<Hotel>();
try
{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Hotel";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Hotel hotel = new Hotel();

                    hotel.id = Convert.ToInt32(reader["id"]);
                    hotel.Hotel_Name = reader["Hotel_Name"].ToString();
                    hotel.City = reader["City"].ToString();
                    hotel.No_of_Rooms= reader["No_of_Rooms"].ToString();
                    hotel.Rating = reader["Rating"].ToString();

                    hotels.Add(hotel);
                }

                reader.Close();
            }
        }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
        return View(hotels);

    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Hotel hotel)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Hotel (Hotel_Name, City, No_of_Rooms, Rating) VALUES (@Hotel_Name, @City, @No_of_Rooms, @Rating)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
               
                command.Parameters.AddWithValue("@Hotel_Name", hotel.Hotel_Name);
                command.Parameters.AddWithValue("@city", hotel.City);
                command.Parameters.AddWithValue("@No_of_Rooms", hotel.No_of_Rooms);
                command.Parameters.AddWithValue("@Rating", hotel.Rating);


                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index");
    }
}
