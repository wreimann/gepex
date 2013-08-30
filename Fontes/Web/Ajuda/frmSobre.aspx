<%@ Page Title="Sobre..." Language="C#" MasterPageFile="~/Geral/principal.Master" 
    AutoEventWireup="true" CodeBehind="frmSobre.aspx.cs" Inherits="Web.Ajuda.frmSobre" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
<uc1:botao ID="botao1" runat="server" /> 
</div>
<div id="divFormulario">
 <fieldset>
 <legend><b>Sobre</b></legend>
    <asp:Image ID="imgGepex" runat="server" 
        ImageUrl="../App_Themes/Imagens/App_portal/GEPEX.png" Width="234px" />
<hr />
<table>
<tr>
<td>
 <b>&nbsp;&nbsp;&nbsp; Data</b>:</td>
 <td>15/11/2010</td>
</tr>

<tr>
<td>
 <b>&nbsp;Versão</b>:</td>
 <td>1.0.0.0</td>
</tr>
<tr>
<td><b>Autores</b>:</td>
<td>Andressa Hatsue Iwazaki da Silva (dut_andressa@hotmail.com)</td>
</tr>
<tr>
<td></td>
<td>Fernando Mecias Dalprá (fernando_bsi@hotmail.com)</td>
</tr>
<tr>
<td></td>
<td>Wellingthon Reimann (wreimann@hotmail.com)</td>
</tr>
</table>
     <hr />
Trabalho de Conclusão de Curso apresentado ao Curso de Bacharelado de Sistemas de Informação, Faculdades SPEI.
     <br />
     Orientador: Douglas Rocha Mendes

</fieldset>
</div>
</asp:Content>
