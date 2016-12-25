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
        Response<HREmployee> AddEmployee(HREmployee value);

        [OperationContract]
        Response<HREmployee> EditEmployee(int employeeId);

        [OperationContract]
        Response<HREmployee> DeleteEmployee(int employeeId);

        [OperationContract]
        Response<List<HREmployee>> GetAllEmployees();

        [OperationContract]
        Response<HREmployee> GetEmployee(int employeeId);
    }

}
