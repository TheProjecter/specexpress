<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import Namespace="SpecExpress" %>
<%@ Register Namespace="SpecExpress.Web" Assembly="SpecExpress.Web" TagPrefix="spec" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit.WCSFExtensions" Namespace="AjaxControlToolkit.WCSFExtensions" TagPrefix="ajaxtoolkitwcsfextensions" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <spec:SpecificationManager ID="SpcManager" runat="server"
            OnValidationNotification="btnValidate_Invalid"
            TypeToValidate="SpecExpress.Quickstart.Domain.Entities.Provider, SpecExpress.Quickstart.Domain" />
        
        <h1>Edit Provider</h1>
       
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="true" />
                <h2><asp:Label ID="lblSuccess" runat="server" Visible="false" Text="Provider submitted successfully." /></h2>                
                  
                <div>
                    <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" Text="Contact First Name"/>
                    <asp:TextBox ID="txtFirstName" runat="server" />    
                    <spec:Validator ID="vldFirstName" runat="server" PropertyName="FirstName" ControlToValidate="txtFirstName"   />             
                </div>
                
                 <div>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtMiddle" Text="Middle Initial"/>
                    <asp:TextBox ID="txtMiddle" runat="server"  />
                    <spec:Validator ID="vldMiddle" runat="server" Display="Dynamic" 
                         ControlToValidate="txtMiddle" PropertyName="MiddleInitial"/>
                </div>
                
                 <div>
                    <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" Text="Contact Last Name"/>
                    <asp:TextBox ID="txtLastName" runat="server" />
                    <spec:Validator ID="vldLastName" runat="server"  PropertyName="LastName" Display="Dynamic" ControlToValidate="txtLastName"/>
                </div>
        
                 <div>
                    <asp:Label ID="lblCode" runat="server" AssociatedControlID="txtCode" Text="Code"/>
                    <asp:TextBox ID="txtCode" runat="server" />
                </div>
                
                 <div>
                    <asp:Label ID="lblStartDate" runat="server" AssociatedControlID="txtStartDate" Text="Start Date"/>
                    <asp:TextBox ID="txtStartDate" runat="server" />                    
                    <spec:Validator ID="Validator1" runat="server"  PropertyName="StartDate" Display="Dynamic" ControlToValidate="txtStartDate"/>
                </div>
                
             <asp:Button ID="btnSubmit" runat="server" Text="Save" onclick="btnValidate_Click"/>
       
    </div>
    </form>
</body>
</html>
