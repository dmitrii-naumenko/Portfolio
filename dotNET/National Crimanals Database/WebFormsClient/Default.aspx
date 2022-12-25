<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsClient.Default" %>

<asp:content ID="Content1" contentplaceholderid="head" runat="server">
</asp:content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Welcome to site!
    <br />
    Use "Search" menu to star search 
    <br />
    Note: You must authenticate before
    <br />
    <br />
    <asp:LoginView runat="server">
        <LoggedInTemplate>
            You are currently logging in <b>
                <asp:LoginName runat="server"/>
            </b>
            <br/>
            <asp:ChangePassword runat="server"></asp:ChangePassword>
            <br/>
            <asp:LoginStatus runat="server"/>

        </LoggedInTemplate>
        <AnonymousTemplate>
            <asp:Login ID="Login1" runat="server"></asp:Login>
            <asp:CreateUserWizard runat="server"></asp:CreateUserWizard>
            <asp:PasswordRecovery runat="server"></asp:PasswordRecovery>    
        </AnonymousTemplate>
    </asp:LoginView>
</asp:Content>

