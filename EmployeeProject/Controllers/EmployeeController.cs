using System;
using System.Collections.Generic;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll();
    void Add(Employee employee);
}

public class EmployeeRepository : IEmployeeRepository
{
    private string connectionString = "User ID=sa;password=examlyMssql@123;server=effddfdaeebcfacbdcbaeaacbbeecfcbdfe-0;Database=empdb;trusted_connection=false;Persist Security Info=False;Encrypt=False";

    private readonly SqlConnection _connection;

    public EmployeeRepository()
    {
        _connection = new SqlConnection(connectionString);
    }

    public IEnumerable<Employee> GetAll()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            string query = "SELECT * FROM Employees";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                _connection.Open();

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
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return employees;
    }

    public void Add(Employee employee)
    {
        string query = "INSERT INTO Employees (Name, Email, Department) VALUES (@Name, @Email, @Department)";

        using (SqlCommand command = new SqlCommand(query, _connection))
        {
            _connection.Open();
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@Department", employee.Department);
            command.ExecuteNonQuery();
        }
    }
}

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _repository;

    public EmployeeController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public ActionResult Index()
    {
        var employees = _repository.GetAll();
        return View(employees);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Employee employee)
    {
        _repository.Add(employee);
        return RedirectToAction("Index");
    }
}
