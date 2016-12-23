using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfEmployeeService.Models;

namespace WcfEmployeeService.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        Response<Employee> AddEmployee(Employee value);

        [OperationContract]
        Response<Employee> EditEmployee(string employeeId);

        [OperationContract]
        Response<Employee> DeleteEmployee(string employeeId);

        [OperationContract]
        Response<List<Employee>> GetAllEmployees();

        [OperationContract]
        Response<Employee> GetEmployee(string employeeId);
    }

}
