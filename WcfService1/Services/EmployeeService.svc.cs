using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfEmployeeService.Models;

namespace WcfEmployeeService.Interfaces
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class EmployeeService : IEmployeeService
    {
        //assume we have a database of employee
        List<Employee> employees;

        Response<Employee> IEmployeeService.AddEmployee(Employee value)
        {
            //validate the input from client
            bool valid = Employee.Validate(value);

            Response<Employee> response = new Response<Employee>();
            response.Success = valid;
            response.Message = "Try again.";

            if(employees == null)
            {
                employees = new List<Employee>();
            }

            if (valid)
            {
                //check if it's already exist
                foreach (Employee em in employees)
                {
                    if (em.EmployeeId.Equals(value.EmployeeId))
                    {
                        //once it's found that it's exist already, break and return false response.
                        response.Success = false;
                        response.Message = "Already exist, sorry.";
                    }
                }

                //safely add to datastore
                //TODO: add to database.
                employees.Add(value);
                response.Success = true;
                response.Message = "Successfully added the employee.";
                response.Data = value;
            }
            
            //otherwise, invalid
            return response;
        }

        Response<Employee> IEmployeeService.DeleteEmployee(string employeeId)
        {
            Response<Employee> response = new Response<Employee>();

            if(employeeId == null || employeeId.Equals(""))
            {
                //invalid employeeId, immediately return false
                response.Success = false;
                response.Message = "Employee ID can not be empty";
            }

            if(employees.Count > 0)
            {
                foreach (Employee em in employees)
                {
                    if (employeeId.Equals(em.EmployeeId))
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

        Response<Employee> IEmployeeService.EditEmployee(string employeeId)
        {
            throw new NotImplementedException();
        }

        Response<List<Employee>> IEmployeeService.GetAllEmployees()
        {
            Response<List<Employee>> response = new Response<List<Employee>>();
            response.Success = true;
            response.Message = "There you are!";
            response.Data = employees;
            return response;
        }

        Response<Employee> IEmployeeService.GetEmployee(string employeeId)
        {
            Employee employee = GetEmployeeWith(employeeId);
            Response<Employee> response = new Response<Employee>();
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

        private Employee GetEmployeeWith(string employeeId)
        {
            if (employeeId == null || employeeId.Equals(""))
            {
                //invalid employeeId, immediately return false
                return null;
            }

            if (employees.Count == 0)
            {
                //empty, so nothing to delete
                return null;
            }

            foreach (Employee em in employees)
            {
                if (employeeId.Equals(em.EmployeeId))
                {
                    //here it is
                    return em;
                }
            }

            return null;
        }
    }
}   
