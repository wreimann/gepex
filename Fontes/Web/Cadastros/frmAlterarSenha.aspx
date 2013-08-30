<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmAlterarSenha.aspx.cs" Inherits="Web.Cadastros.frmAlterarSenha"
    Title="Alterar Senha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .style1
        {
            color: #FF0000;
        }
        .style2
        {
            text-align: right;
            width:85pt;
        }
        .style8
        {
            font-weight: bold;
            color: #FF0000;
        }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
         <uc1:botao ID="botao1" runat="server" />
         <uc2:Mensagem ID="Mensagem" runat="server" />
    </div>  
    <div id="divFormulario">
        <table>
        <tr>
            <td class="style2"><b><span class="style1">*</span>Senha Atual</b>:</td>
<td><asp:TextBox ID="txtSenhaAtual" runat="server" Width="180px" TextMode="Password" 
        MaxLength="8"></asp:TextBox></td>
<td>
<asp:RequiredFieldValidator ID="rfvSenhaAtual" runat="server" 
 ControlToValidate="txtSenhaAtual" Display="Dynamic" 
 ErrorMessage="Informe o campo: Senha Atual.">*</asp:RequiredFieldValidator>
</td>            </tr>
            <tr>
<td class="style2"><b><span class="style1">*</span>Senha</b>:</td>
<td><asp:TextBox ID="txtSenha" runat="server" Width="180px" TextMode="Password" 
        MaxLength="8"></asp:TextBox></td>
<td>
<asp:RequiredFieldValidator ID="rqfSenha" runat="server" 
 ControlToValidate="txtSenha" Display="Dynamic" 
 ErrorMessage="Informe o campo: Senha.">*</asp:RequiredFieldValidator>
</td>
<td>
<asp:RegularExpressionValidator ID="revSenha" 
 runat="server" ControlToValidate="txtSenha" 
 ErrorMessage="Digite uma senha válida entre 6 e 8 caracteres." 
 ValidationExpression="^\w{6,8}$"></asp:RegularExpressionValidator>
</td>
</tr>
<tr>
<td class="style2"><b><span class="style1">*</span>Confirma Senha</b>:</td>
<td><asp:TextBox ID="txtConfirma" runat="server" Width="180px" TextMode="Password" 
        MaxLength="8"></asp:TextBox></td>
<td>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
ControlToValidate="txtConfirma" Display="Dynamic" 
ErrorMessage="Informe o campo: Confirma Senha.">*</asp:RequiredFieldValidator>
</td>
<td>
<asp:CompareValidator ID="CompareValidator4" runat="server" 
 ControlToCompare="txtSenha" ControlToValidate="txtConfirma" 
  ErrorMessage="Senha não confere."></asp:CompareValidator>
</td>
</tr>
</table>
<span class="style8">*Preenchimento obrigatório.</span>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
</div>

</asp:Content>
