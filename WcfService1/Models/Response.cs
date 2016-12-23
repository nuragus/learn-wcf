using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfEmployeeService.Models
{
    [DataContract]
    public class Response<T>
    {
        private bool success;
        private string message;
        private T data;

        [DataMember]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        [DataMember]
        public T Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}