using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SearchEmployee(object sender, EventArgs e)
    {
        
        EmployeeSR.EmployeeServiceClient client = new EmployeeSR.EmployeeServiceClient();
        EmployeeSR.ResponseOfEmployeeAFc1I2Sl emp = client.GetEmployee(IdTextBox.Text);
        if (emp.Success)
        {
            EmployeeInfoLabel.Text = emp.Data.FirstName + " " + emp.Data.LastName;
        }
    }

    protected void AddEmployee(object sender, EventArgs e)
    {
        EmployeeSR.Employee emp = new EmployeeSR.Employee();
        emp.EmployeeId = EmployeeIdTextBox.Text;
        emp.FirstName = FirstNameTextBox.Text;
        emp.LastName = LastNameTextBox.Text;
        EmployeeSR.EmployeeServiceClient client = new EmployeeSR.EmployeeServiceClient();
        EmployeeSR.ResponseOfEmployeeAFc1I2Sl response = client.AddEmployee(emp);
        MessageTextBox.Text = response.Message;
    }
}