<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarAgendaAluno.aspx.cs" Inherits="Web.Cadastros.frmCadastrarAgendaAluno"
    Title="Cadastro de Agenda do Aluno" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
            width: 178px;
        }
        .style3
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
    </div>
    <div id="divFormulario">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:Panel ID="PanelAgendaAluno" runat="server">
            <table style="width: 240px">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <b><span class="style3">*</span>Aluno</b>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAluno" runat="server" AppendDataBoundItems="True" Width="300px">
                            <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="color: Red">
                        <asp:CompareValidator ID="cfvAluno" runat="server" ControlToValidate="ddlAluno" Display="Dynamic"
                            ErrorMessage="Selecione o Campo Aluno" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <b><span class="style3">*</span>Data</b>:
                    </td>
                    <td>
                        <asp:TextBox ID="txtData" runat="server" Width="100px" MaxLength="10" ReadOnly="True"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="txtData_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False"
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtData" UserDateFormat="DayMonthYear">
                        </cc1:MaskedEditExtender>
                    </td>
                    <td style="color: Red">
                        <asp:RequiredFieldValidator ID="rfvData" runat="server" ControlToValidate="txtData"
                            Display="Dynamic" ErrorMessage="Informe o Campo Data">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <b><span class="style3">*</span>Anotação</b>:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAnotacao" runat="server" TextMode="MultiLine" Width="300px" Height="76px"></asp:TextBox>
                    </td>
                    <td style="color: Red">
                        <asp:RequiredFieldValidator ID="rfvAnotacao" runat="server" ControlToValidate="txtAnotacao"
                            Display="Dynamic" ErrorMessage="Informe o Campo Anotação">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>

                        <br />
                        <uc2:Mensagem ID="Mensagem1" runat="server" />
                    </td>
                </tr>
            </table>
            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
</asp:Content>
