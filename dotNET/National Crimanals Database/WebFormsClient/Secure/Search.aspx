<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebFormsClient.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 >Search parameters</h2>
    <div style="text-align: center">
        <table style="text-align: left" cellspacing="0" cellpadding="8" rules="none" width="600">
            <tr>
                <td valign="top" style="text-align: right">First name:</td> 
                <td valign="top" >
                    <asp:TextBox ID="tbFirstName" runat="server" Width="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: right">Last name:</td> 
                <td valign="top" >
                    <asp:TextBox ID="tbLastName" runat="server" Width="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: right">Sex:</td> 
                <td valign="top" >
                    <asp:DropDownList runat="server" ID="tbSex" >
                        <asp:ListItem>Any</asp:ListItem>
                        <asp:ListItem>Man</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td valign="top" style="text-align: right">E-mail:</td> 
                <td valign="top" >
                    <asp:TextBox ID="tbEmail" runat="server" Width="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: right">Max results:</td> 
                <td valign="top" >
                    <asp:TextBox ID="tbmaxResults" runat="server" Width="300"></asp:TextBox>
                    <div>
                        <asp:RangeValidator runat="server" ControlToValidate="tbmaxResults" 
                            MinimumValue="0" MaximumValue="1000" Type="Integer"
                            ErrorMessage="Maximum results count must be in range from 0 to 1000" 
                            EnableClientScript="False"></asp:RangeValidator>
                     </div>
                </td>
            </tr>
            <tr>
                <td  align="center"  colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="Submit" Width="50%" OnClick="OnSubmit" />
                </td>
            </tr>
        </table>
        
        

    </div>
</asp:Content>
