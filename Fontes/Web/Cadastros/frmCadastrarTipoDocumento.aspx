<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
CodeBehind="frmCadastrarTipoDocumento.aspx.cs" Inherits="Web.Cadastros.frmCadastrarTipoDocumento" 
Title="Cadastro Tipo Documento" %>

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
        }
        .style3
        {
            font-size: xx-small;
            font-weight: bold;
            color: #FF0000;
        }
        .style4
        {
            color: #000000;
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
                    <span class="style1">*</span><b>Descrição</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtDescricao" runat="server" Width="300px" MaxLength="30"></asp:TextBox>
                </td>
                <td style="color:Red"> 
                    <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" 
                        ControlToValidate="txtDescricao" Display="Dynamic" 
                        ErrorMessage="Informe o Campo Descrição">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Máscara:
                </td>
                <td>
                    <asp:TextBox ID="txtMascara" runat="server" Width="300px" MaxLength="30"></asp:TextBox>
                </td>
                <td>
                    <cc1:FilteredTextBoxExtender ID="ftbMascara" runat="server" Enabled="True" 
                        TargetControlID="txtMascara" FilterType="Custom" ValidChars="-/.,:L9">
                    </cc1:FilteredTextBoxExtender>
                </td>              
            </tr>
            <tr>
            <td class="style2"></td>
            <td class="style3">Caracteres válidos {&quot;9&quot;<span class="style4">,</span> &quot;L&quot;<span 
                    class="style4">,</span> &quot;.&quot;<span class="style4">,</span> &quot;,&quot;<span 
                    class="style4">,</span> &quot;/&quot;<span class="style4">,</span> &quot;-&quot;
                <span class="style4">,</span> &quot;:&quot;} </td>
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
                <td></td>
            </tr>
        </table>
    <span class="style3">* Preenchimento obrigatório.</span>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" />
</div>
</asp:Content>
