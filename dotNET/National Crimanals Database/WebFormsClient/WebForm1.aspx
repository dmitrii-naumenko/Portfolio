<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebFormsClient.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Style.css" > 
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
        <div class="left">
            <h3>First Name:</h3>    
        </div>
        <div class="right">
            <asp:TextBox ID="tbFirstName"  runat="server"></asp:TextBox>    
        </div>
        </div>
    
    </div>
    </form>
</body>
</html>
