<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarEndereco.aspx.cs" Inherits="GEPEX.Cadastros.frmCadastrarEndereco"
    Title="Cadastro de Endereço" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
        .style2
        {
            text-align: right;
            width: 45pt;
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
        <table style="width: 478px">
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>CEP</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtCEP" runat="server" Width="30%" MaxLength="40"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtCep_MaskedEditExtender" runat="server" AutoComplete="False"
                        ClearMaskOnLostFocus="False" CultureName="en-US" Mask="99999-999" MaskType="Number"
                        TargetControlID="txtCEP">
                    </cc1:MaskedEditExtender>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvCEP" runat="server" ErrorMessage="Informe o Campo: CEP."
                        ControlToValidate="txtCEP" Display="Dynamic">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>Logradouro</b>:
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtLogradouro" runat="server" Width="400px" MaxLength="40"></asp:TextBox>
                </td>
                <td class="style1">
                    <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" ErrorMessage="Informe o Campo: Logradouro."
                        ControlToValidate="txtLogradouro" Display="Dynamic">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>Cidade</b>:
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlCidade" runat="server" Width="400px">
                    </asp:DropDownList>
                </td>
                <td class="style1">
                    <asp:CompareValidator ID="cvdCidade" runat="server" ErrorMessage="*" Display="Static"
                        Font-Italic="False" ControlToValidate="ddlCidade" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>Bairro</b>:
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtBairro" runat="server" Width="400px" MaxLength="40"></asp:TextBox>
                </td>
                <td class="style1">
                    <asp:RequiredFieldValidator ID="rfvBairro" runat="server" ErrorMessage="Informe o Campo: Bairoo."
                        ControlToValidate="txtBairro" Display="Dynamic">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <span class="style3">* Preenchimento obrigatório.</span>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </div>
</asp:Content>
