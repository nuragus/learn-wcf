using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfEmployeeService.Models;
using WcfService1.Models;

namespace WcfEmployeeService.Interfaces
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class EmployeeService : IEmployeeService
    {
        //TODO: Should be in a config
        private static readonly string CONNECTION_STRING = @"server=DESKTOP-ISP57M6\SQLEXPRESS;database=EmployeeDB;user=hris;password=12345";
        //assume we have a database of employee
        List<HREmployee> employees;

        //TODO: Need to close the connection
        SqlConnection sqlConnection;

        Response<HREmployee> IEmployeeService.AddEmployee(HREmployee value)
        {
            //validate the input from client
            bool valid = HREmployee.Validate(value);

            Response<HREmployee> response = new Response<HREmployee>();
            response.Success = valid;
            response.Message = "Try again.";

            if(employees == null)
            {
                employees = new List<HREmployee>();
            }

            if (valid)
            {
                //check if it's already exist
                foreach (HREmployee em in employees)
                {
                    if (em.EmployeeId.Equals(value.EmployeeId))
                    {
                        //once it's found that it's exist already, break and return false response.
                        response.Success = false;
                        response.Message = "Already exist, sorry.";
                    }
                }

                //safely add to datastore
                //TODO: add to database. it does return a bool whether it's failed of not
                bool success = CreateNewEmployeeToDB(value);
                if (success)
                {
                    employees.Add(value);
                    response.Success = true;
                    response.Message = "Successfully added the employee.";
                    response.Data = value;
                }
            }
            
            //otherwise, invalid
            return response;
        }

        Response<HREmployee> IEmployeeService.DeleteEmployee(int employeeId)
        {
            Response<HREmployee> response = new Response<HREmployee>();

            if(employeeId == 0)
            {
                //invalid employeeId, immediately return false
                response.Success = false;
                response.Message = "Employee ID can not be empty";
            }

            if(employees.Count > 0)
            {
                foreach (HREmployee em in employees)
                {
                    if (employeeId == em.EmployeeId)
                    {
                        //exist, can be deleted
                        //catch before deleting to be returned
                        response.Data = em;

                        //then we can safely detele it
                        employees.Remove(em);

                        
                        response.Success = true;
                        response.Message = "The employee which has ID " + employeeId + " has been removed.";
                        return response;
                    }
                }
            }

            //at least say something
            response.Success = false;
            response.Message = "Nothing to delete.";

            return response;
        }

        Response<HREmployee> IEmployeeService.EditEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        Response<List<HREmployee>> IEmployeeService.GetAllEmployees()
        {
            Response<List<HREmployee>> response = new Response<List<HREmployee>>();
            response.Success = true;
            response.Message = "There you are!";
            response.Data = employees;
            return response;
        }

        Response<HREmployee> IEmployeeService.GetEmployee(int employeeId)
        {
            HREmployee employee = GetEmployeeWith(employeeId);
            Response<HREmployee> response = new Response<HREmployee>();
            response.Success = (employee != null);
            response.Data = employee;

            if (employee != null)
            {
                response.Message = "Here the employee which has ID: " + employeeId;
            }
            else
            {
                response.Message = "Employee with ID " + employeeId + " is not exist";
            }
            return response;
        }

        private HREmployee GetEmployeeWith(int employeeId)
        {
            if (employeeId == 0)
            {
                //invalid employeeId, immediately return false
                return null;
            }

            return GetEmployeeFromDB(employeeId); ;
        }

        private bool CreateNewEmployeeToDB(HREmployee emp)
        {
            SqlConnection con = GetSqlConnection();

            SqlCommand insert = new SqlCommand("insert into Employee(firstname, lastname, employee_id) values(@firstname, @lastname, @employee_id)", con);
            //Direct SQL Command, refer to GetEmployeeFromDB for LINQ Usage
            insert.Parameters.AddWithValue("@firstname", emp.FirstName);
            insert.Parameters.AddWithValue("@lastname", emp.LastName);
            insert.Parameters.AddWithValue("@employee_id", emp.EmployeeId);
            //execute and get response, 1 if it's successful
            int result = insert.ExecuteNonQuery();
            
            //1 is insert command successful
            return (result == 1);
        }

        private Models.HREmployee GetEmployeeFromDB(int employeeId)
        {
            SqlConnection con = GetSqlConnection();
            SqlCommand command = new SqlCommand("select * from Employee where employee_id = @employeeId", con);
            command.Parameters.AddWithValue("@employeeId", employeeId);
            command.ExecuteNonQuery();

            EmployeeContext ctx = new EmployeeContext();
            //with LINQ
            var employeeEntity = (from p in ctx.Employees where p.employee_id == employeeId select p).FirstOrDefault();

            Models.HREmployee newEmp = new Models.HREmployee();
            newEmp.EmployeeId = employeeEntity.employee_id;
            newEmp.FirstName = employeeEntity.firstname;
            newEmp.LastName = employeeEntity.lastname;
           
            return newEmp;
        }

        private SqlConnection GetSqlConnection()
        {
            //no need to create a new one if it's already exist.
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = CONNECTION_STRING;
                sqlConnection.Open();
            }

            //where to close???
            return sqlConnection;
        }
    }
}   
