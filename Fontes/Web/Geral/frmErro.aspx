<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmErro.aspx.cs" Inherits="Web.Geral.frmErro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <p style="text-align: center">
        &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/Geral/error.png" />
    </p>
    <p style="text-align: center">
        <asp:LinkButton ID="lnkMenuPrincipal" runat="server" 
            PostBackUrl="~/Geral/index.aspx">Menu Principal</asp:LinkButton>
    </p>
    <div>
    
    </div>
    </form>
</body>
</html>
