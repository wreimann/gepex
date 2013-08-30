<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarProfissao.aspx.cs" Inherits="Web.Cadastros.frmCadastrarProfissao"
    Title="Cadastro de Profissão" %>
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
        }
        .style3
        {
            font-size: xx-small;
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
                <td class="style2">
                    <span class="style1">*</span><b>Profissão</b>:
                </td>             
                <td>
                    <asp:TextBox ID="txtDescricao" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                </td>
                <td style="color:Red"> 
                    <asp:RequiredFieldValidator ID="rfvProfissao" runat="server" 
                        ControlToValidate="txtDescricao" Display="Dynamic" 
                        ErrorMessage="Informe o Campo Descrição">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <span class="style1">*</span><b>Grupo</b>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipo" runat="server" Width="310px" 
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="C">Clínico</asp:ListItem>
                    <asp:ListItem Value="P">Pedagógico</asp:ListItem>
                    <asp:ListItem Value="G">Geral</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr  valign="top">
                <td class="style2">
                    <span class="style1">*</span><b>Situação</b>:
                </td>
                     <td valign="top">
                        <asp:RadioButtonList ID="rdlSituacao" runat="server" RepeatDirection="Horizontal">
                          <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
                          <asp:ListItem Value="0">Inativo</asp:ListItem>
                        </asp:RadioButtonList>        
                    </td>
                <td style="color:Red">&nbsp;</td>
            </tr>
        </table>
    <span class="style3">* Preenchimento obrigatório.</span>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" />
</div>
</asp:Content>
