using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeProject.Models;
using System.Data;
using Microsoft.Data.SqlClient;

public class EmployeeController : Controller
{
    private string connectionString = "User ID=sa;password=examlyMssql@123;server=effddfdaeebcfacbdcbaeaacbbeecfcbdfe-0;Database=empdb;trusted_connection=false;Persist Security Info=False;Encrypt=False";

    public ActionResult Index()
    {
        List<Employee> employees = new List<Employee>();
try
{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Employees";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                    employee.Name = reader["Name"].ToString();
                    employee.Email = reader["Email"].ToString();
                    employee.Department = reader["Department"].ToString();

                    employees.Add(employee);
                }

                reader.Close();
            }
        }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
        return View(employees);

    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Employee employee)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Employees (Name, Email, Department) VALUES (@Name, @Email, @Department)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Department", employee.Department);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index");
    }
}
