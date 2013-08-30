<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master"  
    AutoEventWireup="true" CodeBehind="frmCadastrarUsuario.aspx.cs" Inherits="Web.Cadastros.frmCadastrarUsuario" 
    Title="Cadastro de Usuário" %>
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
    <asp:UpdatePanel ID="upnNome" runat="server" UpdateMode="Conditional" 
        ChildrenAsTriggers="False">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="txtNome" />
    </Triggers>
    <ContentTemplate>
    <asp:Panel ID="pnlNomePessoa" runat="server">
    <table>
    <tr> 
    <td class="style2"><b><span class="style1">*</span>Nome</b>:</td>
    <td>
    <asp:TextBox ID="txtNome" runat="server" Width="320px" AutoPostBack="True" 
            ontextchanged="txtNome_TextChanged" MaxLength="80" 
            AutoCompleteType="Disabled"></asp:TextBox>
        <asp:HiddenField ID="hfdNome" runat="server" />       
    <cc1:AutoCompleteExtender ID="acePessoa" runat="server"  
            ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx" 
            TargetControlID="txtNome" ServiceMethod="CompletarList" 
         MinimumPrefixLength="1" CompletionInterval="400" FirstRowSelected="True" 
        CompletionSetCount="10">
    </cc1:AutoCompleteExtender>
    </td>
    <td>
    <asp:RequiredFieldValidator ID="rqfNome" runat="server" 
        ControlToValidate="txtNome" ErrorMessage="Informe o Nome do Aluno ou Docente já cadastrado no sistema." 
        Display="Dynamic">*</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td class="style2">Tipo:</td>
    <td>
    <asp:RadioButtonList ID="rdlTipo" runat="server" 
        RepeatDirection="Horizontal" Enabled="False">
        <asp:ListItem Value="D">Docente</asp:ListItem>
        <asp:ListItem Value="A">Aluno</asp:ListItem>
    </asp:RadioButtonList>
    </td>
    </tr>   
    <tr>
<td class="style2"><b><span class="style1">*</span>E-mail</b>:</td>
<td><asp:TextBox ID="txtEmail" runat="server" Width="320px" MaxLength="80"></asp:TextBox></td>
<td>
    <asp:RequiredFieldValidator ID="rqfEmail" runat="server" 
        ControlToValidate="txtEmail" Display="Dynamic" 
        ErrorMessage="Informe o campo: E-mail">*</asp:RequiredFieldValidator>
</td>
<td>  <asp:RegularExpressionValidator
        ID="revEmail" runat="server" ErrorMessage="E-mail inválido!" 
        ControlToValidate="txtEmail" Display="Dynamic" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
</td>
</tr>

    </table>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
<table>
<tr>
<td class="style2"><b><span class="style1">*</span>Login</b>:</td>
<td><asp:TextBox ID="txtLogin" runat="server" Width="180px" MaxLength="10"></asp:TextBox></td>
<td>
<asp:RequiredFieldValidator ID="rqfLogin" runat="server" 
ControlToValidate="txtLogin" ErrorMessage="Informe o campo: Login." 
Display="Dynamic">*</asp:RequiredFieldValidator>
</td>
<td>
 <asp:RegularExpressionValidator ID="revLogin" runat="server" 
 ControlToValidate="txtLogin" 
 ErrorMessage="Digite um login válido: entre 6 e 10 carecteres." 
 ValidationExpression="^\w{6,10}$"></asp:RegularExpressionValidator>
 
</td>
</tr>
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
<table>
<tr>
<td class="style2"><b><span class="style1">*</span>Perfil</b>:</td>
<td><asp:DropDownList ID="ddlPerfil" runat="server" Width="320px" 
        AppendDataBoundItems="True">
    <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr>
<td class="style2">Situação:</td>
<td>
    <asp:RadioButtonList ID="rdlSituacao" runat="server" 
        RepeatDirection="Horizontal">
        <asp:ListItem Selected="True" Value="A">Ativo</asp:ListItem>
        <asp:ListItem Value="I">Inativo</asp:ListItem>
        <asp:ListItem Value="B">Bloqueado</asp:ListItem>
    </asp:RadioButtonList>
</td>
</tr>
<tr>
<td class="style2">Motivo:</td>
<td>
    <asp:TextBox ID="txtMotivo" runat="server" TextMode="MultiLine" 
        Width="320px" Height="62px" MaxLength="80"></asp:TextBox>
</td>
</tr>
</table>
<span class="style8">*Preenchimento obrigatório.</span>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
</div>
</asp:Content>
