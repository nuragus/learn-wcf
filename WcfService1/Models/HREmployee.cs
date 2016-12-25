using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfEmployeeService.Models
{
    [DataContract]
    public class HREmployee
    {
        string firstName;
        string lastName;
        int employeeId;

        public static bool Validate(HREmployee employee)
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

            if (employee.EmployeeId == 0)
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
        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }
    }
}