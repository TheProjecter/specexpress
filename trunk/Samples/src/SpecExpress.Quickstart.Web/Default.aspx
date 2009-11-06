<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import Namespace="SpecExpress" %>
<%@ Register Namespace="SpecExpress.Web" Assembly="SpecExpress" TagPrefix="spec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <h1>
            
            Edit Provider</h1>
            
            
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="true" />
                <h2><asp:Label ID="lblSuccess" runat="server" Visible="false" Text="Provider submitted successfully." /></h2>                
                  
                <div>
                    <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" Text="First Name"/>
                    <asp:TextBox ID="txtFirstName" runat="server" />    
                    <spec:SpecExpressProxyValidator ID="vldFirstName" runat="server" PropertyName="FirstName" ControlToValidate="txtFirstName"   />             
                </div>
                
                 <div>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtMiddle" Text="Middle"/>
                    <asp:TextBox ID="txtMiddle" runat="server"  />
                    <spec:SpecExpressProxyValidator ID="vldMiddle" runat="server"  PropertyName="MiddleInitial" Display="Dynamic" ControlToValidate="txtMiddle"/>
                </div>
                
                 <div>
                    <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" Text="Last Name"/>
                    <asp:TextBox ID="txtLastName" runat="server" />
                    <spec:SpecExpressProxyValidator ID="vldLastName" runat="server"  PropertyName="LastName" Display="Dynamic" ControlToValidate="txtLastName"/>
                </div>
        
              <spec:ValidationButton ID="btnValidate" runat="server" Text="Save w Validation" 
                    OnValidationNotification="btnValidate_Invalid" 
                    OnGetObject="btnValidate_GetObject"
                    onclick="btnValidate_Click" />
        
            </ContentTemplate>            
        </asp:UpdatePanel>    
    </div>
    </form>
</body>
</html>
