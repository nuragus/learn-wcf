<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="IdTextBox" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="SearchEmployee" Text="Search ID" />
    
    </div>
        <asp:Label ID="EmployeeInfoLabel" runat="server" Text="???"></asp:Label>
        <p>
            &nbsp;</p>
        <p>
            First Name: <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
            Last Name: <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
            Employee ID: <asp:TextBox ID="EmployeeIdTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="AddButton" runat="server" OnClick="AddEmployee" Text="Add New Employee" />
        </p>
        <asp:Label ID="MessageTextBox" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
