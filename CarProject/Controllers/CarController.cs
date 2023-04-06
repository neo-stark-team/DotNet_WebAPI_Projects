using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarProject.Models;
using System.Data;
using Microsoft.Data.SqlClient;

public class CarController : Controller
{
    private string connectionString = "User ID=sa;password=examlyMssql@123;server=effddfdaeebcfacbdcbaeaacbbeecfcbdfe-0;Database=CarsDB;trusted_connection=false;Persist Security Info=False;Encrypt=False";

    public ActionResult Index()
    {
        List<Car> cars = new List<Car>();
try
{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Cars";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Car car = new Car();

                    car.Car_Id = Convert.ToInt32(reader["Car_Id"]);
                    car.Car_Brand = reader["Car_Brand"].ToString();
                    car.Type = reader["Type"].ToString();
                    car.Color = reader["Color"].ToString();
                    car.Mileage = reader["Mileage"].ToString();
                    car.Price = reader["Price"].ToString();

                    cars.Add(car);
                }

                reader.Close();
            }
        }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
        return View(cars);

    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Car car)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Cars (Car_Brand, Type, Color, Mileage, Price) VALUES (@Car_Brand, @Type, @Color, @Mileage, @Price)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // command.Parameters.AddWithValue("@id", car.id);
                command.Parameters.AddWithValue("@Car_Brand", car.Car_Brand);
                command.Parameters.AddWithValue("@Type", car.Type);
                command.Parameters.AddWithValue("@Color", car.Color);
                command.Parameters.AddWithValue("@Mileage", car.Mileage);
                command.Parameters.AddWithValue("@Price", car.Price);


                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index");
    }
}
