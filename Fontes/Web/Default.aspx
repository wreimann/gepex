<%@ Page Language="C#" MasterPageFile="~/Portal/portal.Master" 
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Portal.Default" 
    Title="Escola de Educação Especial 29 de Março" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 259px;
        }
        .style4
        {
            height: 29px;
        }
        .style8
        {
            height: 20px;
            color: #3366FF;
            font-weight: bold;
            background-color: #FFFFFF;
        }
        .style10
        {
            font-size: large;
            color: #FFFFFF;
        }
        .style15
        {
            width: 340px;
            color: #FF6600;
            font-weight: bold;
            text-align:left;
            vertical-align:text-top;
            display: table-cell;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="tabImagens" style="width: 99%">
<tr>
<td colspan="3" style="background-image: url('../../App_Themes/Imagens/App_portal/planoFonte.png'); height: 34px;" 
        class="style8"><a href="<%=ResolveClientUrl("~/Default.aspx")%>" class="style10">Início</a>
</td>
</tr>
<tr>
<td>
    <a rel="gal" href="upload/principal/img_1.jpg" class="nyroModal" title="">
    <img src="upload/principal/img_1.jpg" alt="" style="width: 195px"/></a>
</td>
<td>
    <a rel="gal" href="upload/principal/img_2.jpg" class="nyroModal" title="">
    <img src="upload/principal/img_2.jpg"  alt="" style="width: 195px"/></a>
</td>
 <td>
     <a rel="gal" href="upload/principal/img_3.jpg" class="nyroModal" title="">
     <img src="upload/principal/img_3.jpg" alt="" style="width: 195px"/></a>
</td>
</tr>
</table>
<table style="width: 99%">
<tr>
<td class="style15">
    <h2 style="background-image: url('../../App_Themes/Imagens/App_portal/planoFonte.png'); height: 34px;">
        <a href="../Portal/Eventos.aspx" class="style10">Eventos</a></h2></td>
    <td  class="style15">
        <h2 style="background-image: url('../../App_Themes/Imagens/App_portal/planoFonte.png'); height: 34px;">
        <a href="../Portal/Noticias.aspx" class="style10">Notícias</a></h2></td>
</tr>
<tr>
<td class="style15" >
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    </td>
    <td class="style15">
    <asp:Panel ID="Panel2" runat="server">    
    </asp:Panel>    
    </td>
    
</tr>
</table>
</asp:Content>

