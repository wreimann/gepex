<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
    CodeBehind="frmCadastrarGradeHorario.aspx.cs" Inherits="Web.Cadastros.frmCadastrarGradeHorario" 
    Title="Cadastro de Grade de Horário" %>
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
            width:78pt;
        }
       .style8
        {
            font-weight: bold;
            color: #FF0000;
        }
        .style9
        {
            font-size: xx-small;
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
<td class="style2"><span class="style1">*</span><b>Ano Letivo</b>:</td>
<td>
<asp:DropDownList ID="ddlAnoLetivo" runat="server" Width="100px" 
        AutoPostBack="True" onselectedindexchanged="ddlAnoLetivo_SelectedIndexChanged">
</asp:DropDownList>
</td>
</tr>
<tr>
<td class="style2"><span class="style1">*</span><b>Turma</b>:</td>
<td>
<asp:UpdatePanel ID="upnTurma" runat="server" UpdateMode="Conditional">
<Triggers>
<asp:AsyncPostBackTrigger ControlID ="ddlAnoLetivo" /> 
</Triggers>
<ContentTemplate>
<asp:Panel ID="pnlTurma" runat="server">   
<asp:DropDownList ID="ddlTurma" runat="server" Width="300px">
<asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
</asp:DropDownList>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</td>
<td style="color:Red"> 
<asp:CompareValidator ID="cvlTurma" runat="server" ControlToValidate="ddlTurma" 
Display="Dynamic" ErrorMessage="Informe o campo: Turma." Operator="NotEqual" 
ValueToCompare="0">*</asp:CompareValidator>
</td>
</tr>
<tr>
<td class="style2"><span class="style1">*</span><b>Professor</b>:</td>
<td>
<asp:UpdatePanel ID="upnProfessor" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
<Triggers>
<asp:AsyncPostBackTrigger ControlID ="txtDocente" /> 
</Triggers>
<ContentTemplate>
<asp:Panel ID="pnlProfessor" runat="server">   
    <asp:TextBox ID="txtDocente" runat="server"  width="296px" 
        ontextchanged="txtDocente_TextChanged"></asp:TextBox>
    <asp:HiddenField ID="hflDocente" runat="server" />
     <cc1:AutoCompleteExtender ID="acePessoa" runat="server"  
            ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx" 
            TargetControlID="txtDocente" ServiceMethod="ListaPedagogico" 
         MinimumPrefixLength="1" CompletionInterval="400" FirstRowSelected="True" 
        CompletionSetCount="10">
    </cc1:AutoCompleteExtender>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</td>
<td style="color:Red"> 
<asp:RequiredFieldValidator ID="rfvProfessor" runat="server" 
ControlToValidate="txtDocente" Display="Dynamic" 
ErrorMessage="Informe o campo: Professor.">*</asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td class="style2"><span class="style1">*</span><b>Dia da Semana</b>:</td>
<td>
<asp:DropDownList ID="ddlDia" runat="server" Width="300px">
<asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
<asp:ListItem Value="2">Segunda-feira</asp:ListItem>
<asp:ListItem Value="3">Terça-feira</asp:ListItem>
<asp:ListItem Value="4">Quarta-feira</asp:ListItem>
<asp:ListItem Value="5">Quinta-feira</asp:ListItem>
<asp:ListItem Value="6">Sexta-feira</asp:ListItem>
<asp:ListItem Value="7">Sábado</asp:ListItem>
</asp:DropDownList>
</td>
<td style="color:Red"> 
<asp:CompareValidator ID="cfvDia" runat="server" ControlToValidate="ddlDia" 
Display="Dynamic" ErrorMessage="Informe o campo: Dia da Semana." Operator="NotEqual" 
ValueToCompare="0">*</asp:CompareValidator>
</td>
</tr>
<tr>
<td class="style2"><span class="style1">*</span><b> Horário</b>:</td>
 <td>
 <asp:DropDownList ID="ddlAula" runat="server" Width="300px" AppendDataBoundItems="True">
 <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
 <asp:ListItem Value="1">1º Horário</asp:ListItem>
 <asp:ListItem Value="2">2º Horário</asp:ListItem>
 <asp:ListItem Value="3">3º Horário</asp:ListItem>
 <asp:ListItem Value="4">4º Horário</asp:ListItem>
 <asp:ListItem Value="5">5º Horário</asp:ListItem>
 <asp:ListItem Value="6">6º Horário</asp:ListItem>
 </asp:DropDownList>
</td>
<td style="color:Red"> 
<asp:CompareValidator ID="cfvAula" runat="server" ControlToValidate="ddlAula" 
Display="Dynamic" ErrorMessage="Selecione o campo: Horário." Operator="NotEqual" 
 ValueToCompare="0">*</asp:CompareValidator>
</td>
</tr>
<tr>
<td class="style2"><span class="style1">*</span><b>Disciplina</b>:</td>
<td>
<asp:DropDownList ID="ddlDisciplina" runat="server" Width="300px">
<asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
</asp:DropDownList>
</td>
<td style="color:Red"> 
<asp:CompareValidator ID="cfvDisciplina" runat="server" 
ControlToValidate="ddlDisciplina" Display="Dynamic" 
ErrorMessage="Selecione o campo: Disciplina." Operator="NotEqual" 
ValueToCompare="0">*</asp:CompareValidator>
</td>
</tr>
</table>
<span class="style8"><span class="style9">*Preenchimento obrigatório</span>.</span>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
ShowMessageBox="True" ShowSummary="False" />
</asp:Content>
