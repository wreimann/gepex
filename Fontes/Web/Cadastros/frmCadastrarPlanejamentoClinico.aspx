<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarPlanejamentoClinico.aspx.cs" Inherits="Web.Cadastros.frmCadastrarPlanejamentoClinico"
    Title="Planejamento Clínico" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
            width: 170px;
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
        <uc2:Mensagem ID="Mensagem1" runat="server" />
    </div>
    <div id="divFormulario">
        <table>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Aluno</b>:
                </td>
                <td>
                     <asp:TextBox ID="txtNome" runat="server" Width="300px" AutoCompleteType="Disabled"
                                AutoPostBack="True" OnTextChanged="txtNome_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="acePessoa" runat="server" ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx"
                                TargetControlID="txtNome" ServiceMethod="ListaAluno" MinimumPrefixLength="1"
                                CompletionInterval="400" FirstRowSelected="True" CompletionSetCount="10">
                            </cc1:AutoCompleteExtender>
                    <asp:HiddenField ID="hfdNome" runat="server" />
                </td>
                <td style="color: Red">
                    <asp:CompareValidator ID="cfvAluno" runat="server" ControlToValidate="txtNome"
                        Display="Dynamic" ErrorMessage="Selecione o campo: Nome do Aluno" Operator="NotEqual"
                        ValueToCompare="0">*</asp:CompareValidator>
                </td>
            </tr>
            <tr >
                <td class="style1">
                    <b><span class="style3">*</span>Especialidade</b>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlProfissao" runat="server" AppendDataBoundItems="True" 
                        Width="300px" Enabled="False">
                        <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="color: Red">
                    <asp:CompareValidator ID="cfvProfissao" runat="server" ControlToValidate="ddlProfissao"
                        Display="Dynamic" ErrorMessage="Selecione o Campo Profissão" Operator="NotEqual"
                        ValueToCompare="0">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Objetivo Geral</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtObjetivoGeralClinico" runat="server" TextMode="MultiLine" 
                        Width="300px" Height="56px"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvObjClinico" runat="server" ControlToValidate="txtObjetivoGeralClinico"
                        Display="Dynamic" ErrorMessage="Informe o Campo Objetivo Geral do Clinico">*</asp:RequiredFieldValidator>
                </td>
     
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Competências/Habilidades</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtCompetencias" runat="server" TextMode="MultiLine" 
                        Width="300px" Height="56px"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvCompetencias" runat="server" ControlToValidate="txtCompetencias"
                        Display="Dynamic" ErrorMessage="Informe o campo: Competências/Habilidades">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            </table>
            <table>
            <tr>
            <td class="style1">
            <b><span class="style3">*</span>Data Inicial</b>:
            </td><td>
            <asp:TextBox ID="txtDataInicial" runat="server" MaxLength="10"></asp:TextBox>
            <cc1:maskededitextender ID="txtData_MaskedEditExtender" 
            runat="server" ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" 
            TargetControlID="txtDataInicial" UserDateFormat="DayMonthYear">
            </cc1:maskededitextender>
            </td>
            <td>
            <asp:ImageButton ID="btnCalendario" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/icones/calendar.png" />
            <cc1:calendarextender ID="cldCalendario" runat="server" 
            PopupButtonID="btnCalendario" TargetControlID="txtDataInicial" Format="dd/MM/yyyy">
            </cc1:calendarextender>
            <asp:CompareValidator ID="cvlData" runat="server" ControlToValidate="txtDataInicial" 
            Display="Dynamic" ErrorMessage="Informe o campo: Data Inicial." Operator="NotEqual" 
            ValueToCompare="__/__/____">*</asp:CompareValidator>
            </td>
            </tr>
            <tr>
            <td class="style1">
            <b><span class="style3">*</span>Data Final</b>:
            </td><td>
            <asp:TextBox ID="txtDataFinal" runat="server" MaxLength="10"></asp:TextBox>
            <cc1:maskededitextender ID="txtDataFinal_MaskedEditExtender" 
            runat="server" ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" 
            TargetControlID="txtDataFinal" UserDateFormat="DayMonthYear">
            </cc1:maskededitextender>
            </td>
            <td>
            <asp:ImageButton ID="btnCalendario2" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/icones/calendar.png" />
            <cc1:calendarextender ID="cldCalendarioFinal" runat="server" 
            PopupButtonID="btnCalendario2" TargetControlID="txtDataFinal" Format="dd/MM/yyyy">
            </cc1:calendarextender>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDataFinal" 
            Display="Dynamic" ErrorMessage="Informe o campo: Data Final." Operator="NotEqual" 
            ValueToCompare="__/__/____">*</asp:CompareValidator>
            </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Número de Atendimento</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroAtendimento" runat="server" Height="20px" 
                        onkeyup="formataInteiro(this,event);" MaxLength="3"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvNumAtendimento" runat="server" ControlToValidate="txtNumeroAtendimento"
                        Display="Dynamic" ErrorMessage="Informe o campo: Nº de Atendimentos" 
                        ValidationGroup="conteudo">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            </table>
        
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
</asp:Content>
