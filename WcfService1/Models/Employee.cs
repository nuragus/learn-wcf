using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfEmployeeService.Models
{
    [DataContract]
    public class Employee
    {
        string firstName;
        string lastName;
        string employeeId;

        public static bool Validate(Employee employee)
        {
            //really?
            if (employee == null)
            {
                //invalid
                return false;
            }
            //lets assume firstname and employeeId are mandatories
            if (employee.FirstName == null || employee.FirstName.Equals(""))
            {
                //invalid
                return false;
            }

            if (employee.EmployeeId == null || employee.EmployeeId.Equals(""))
            {
                //invalid
                return false;
            }
            //valid
            return true;
        }

        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [DataMember]
        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }
    }
}