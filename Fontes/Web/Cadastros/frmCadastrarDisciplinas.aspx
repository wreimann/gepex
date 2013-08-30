<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" 
    AutoEventWireup="true" CodeBehind="frmCadastrarDisciplinas.aspx.cs" 
    Inherits="GEPEX.Cadastros.frmCadastrarDisciplinas" 
    Title="Cadastro de Disciplina" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
        .style2
        {
            text-align: right;
            width:45pt;
        }
        .style3
     {
         color: #FF0000;
         font-size: 8pt;
     }
   </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
    <uc1:botao ID="botao1" runat="server" />
    <uc2:Mensagem ID="Mensagem" runat="server" />
    </div>
<div id="divFormulario">
    <table style="width:478px">
        
        <tr>
            <td class="style2"><b><span class="style1">*</span>Disciplina</b>:</td>
            <td>
                <asp:TextBox ID="txtDisciplina" runat="server" Width="100%" MaxLength="40"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvDisciplina" runat="server" 
                                    ErrorMessage="Informe o Campo Disciplina" ControlToValidate="txtDisciplina" 
                                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="style2"><b><span class="style1">*</span>Descrição</b>:</td>
            <td class="style1">
                <asp:TextBox ID="txtDescricao" runat="server" Width="100%" Height="167px" MaxLength="1000" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
                                        <td class="style1">
                                <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" 
                                    ErrorMessage="Informe o Campo Descrição" ControlToValidate="txtDescricao" 
                                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="style2"><b><span class="style1">*</span>Situação</b>:</td>
            <td>
                <asp:RadioButtonList ID="rdlStatus" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Ativo</asp:ListItem>
                    <asp:ListItem>Inativo</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <span class="style3">* Preenchimento obrigatório.</span>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" />
</div>
</asp:Content>
