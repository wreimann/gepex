<%@ Page Language="C#" MasterPageFile="~/Portal/portal.Master" AutoEventWireup="true" 
    CodeBehind="Eventos.aspx.cs" Inherits="Web.Portal.Eventos" Title="Eventos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
            color: #FFFFFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="height: 30px" width="100%">
<tr>
<td >

    <h2 style="background-image: url('../../App_Themes/Imagens/App_portal/planoFonte.png'); height: 34px;" 
        class="style7">
        Eventos</h2></td>
</tr>
</table>
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
</asp:Content>
