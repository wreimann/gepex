<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
    CodeBehind="frmGerarMatricula.aspx.cs" Inherits="Web.Cadastros.frmGerarMatricula" 
    Title="Gerar Matrícula" %>
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
            width:60pt;
        }
        .style3
        {
            text-align: right;
            width:50pt;
        }
        .style8
        {
            font-weight: bold;
            color: #FF0000;
        }
   </style>
   <script type= "text/javascript">
       function OnTreeClick(evt) {
           var isNoPai = (document.activeElement.tagName.toLowerCase() != 'a');
           var src = window.event != window.undefined ? window.event.srcElement : evt.target;
           //var isExpand = (src.tagName.toLowerCase() == "img");
           if (!isNoPai) {
           //if (!isNoPai && !isExpand) {
               return confirm("Deseja realmente excluir o aluno dessa turma?");
           } else
               return true;       
           
       }
    </script>
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
    <asp:AsyncPostBackTrigger  ControlID="trvTurmas"  />
    <asp:AsyncPostBackTrigger ControlID="ddlPeriodo" />
    <asp:AsyncPostBackTrigger  ControlID="ddlAnoLetivo" />
    </Triggers>
    <ContentTemplate>
    <asp:Panel ID="pnlNomePessoa" runat="server">
    <table>
    <tr> 
    <td class="style2"><b><span class="style1">*</span>Aluno</b>:</td>
    <td>
    <asp:TextBox ID="txtNome" runat="server" Width="370px" AutoPostBack="True" 
            ontextchanged="txtNome_TextChanged" MaxLength="80" 
            AutoCompleteType="Disabled"></asp:TextBox>
        <asp:HiddenField ID="hfdNome" runat="server" />       
    <cc1:AutoCompleteExtender ID="acePessoa" runat="server"  
            ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx" 
            TargetControlID="txtNome" ServiceMethod="ListaAluno" 
         MinimumPrefixLength="1" CompletionInterval="400" FirstRowSelected="True" 
        CompletionSetCount="10">
    </cc1:AutoCompleteExtender>
    </td>
    <td>
    <asp:RequiredFieldValidator ID="rqfNome" runat="server" 
        ControlToValidate="txtNome" ErrorMessage="Informe o nome do aluno." 
        Display="Dynamic">*</asp:RequiredFieldValidator>
    </td>
    </tr>
   </table>
   <fieldset>
    <legend>Filtro da Turma</legend>
   <table>
   <tr>
   <td class="style2"><b><span class="style1">*</span>Ano Letivo</b>:</td>
    <td><asp:DropDownList ID="ddlAnoLetivo" runat="server" AppendDataBoundItems="True" 
        Width="115px" onselectedindexchanged="ddlAnoLetivo_SelectedIndexChanged" 
            AutoPostBack="True" >
    </asp:DropDownList>
    </td>
    <td class="style3">Período:</td>
    <td><asp:DropDownList ID="ddlPeriodo" runat="server" AppendDataBoundItems="True" 
        Width="186px" onselectedindexchanged="ddlPeriodo_SelectedIndexChanged" 
            AutoPostBack="True" >
        <asp:ListItem Value="">(--Selecione--)</asp:ListItem>
        <asp:ListItem Value="M">Manhã</asp:ListItem>
        <asp:ListItem Value="T">Tarde</asp:ListItem>
        <asp:ListItem Value="I">Integral</asp:ListItem>
    </asp:DropDownList>
    </td>
   </tr>
   </table>
   </fieldset>
   <fieldset>
<legend>Dados do Aluno</legend>
<table>
<tr>
<td class="style2">Matrícula:</td>
<td><asp:TextBox ID="txtMatricula" runat="server" MaxLength="10" Width="110px" 
        ReadOnly="True"></asp:TextBox></td>
<td class="style3">Situação:</td>
<td><asp:DropDownList ID="ddlSituacao" runat="server" AppendDataBoundItems="True" 
        Width="186px" Enabled="False">
    <asp:ListItem Value="I">Inativo</asp:ListItem>
    <asp:ListItem Value="A">Matrícula em Andamento</asp:ListItem>
    <asp:ListItem Value="M">Matriculado</asp:ListItem>
    <asp:ListItem Value="L">Lista de Espera</asp:ListItem>
    </asp:DropDownList>
 </td>
</tr>
</table>
<table>
<tr>
<td class="style2">Data de Nasc:</td>
<td><asp:TextBox ID="txtDataNascimento" runat="server" ReadOnly="True" 
        Width="110px" MaxLength="10"></asp:TextBox>
    <cc1:MaskedEditExtender ID="txtDataNascimento_MaskedEditExtender" 
        runat="server" ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" 
        TargetControlID="txtDataNascimento" UserDateFormat="DayMonthYear">
    </cc1:MaskedEditExtender>
</td>
<td class="style3">Idade:</td>
<td><asp:TextBox ID="txtIdade" runat="server" ReadOnly="True" Width="40px"></asp:TextBox></td>
<td>anos</td>
</tr>
<tr>
<td class="style2">Sexo:</td>
<td><asp:DropDownList ID="ddlSexo" runat="server" AppendDataBoundItems="True" 
        Width="115px" Enabled="False">
    <asp:ListItem Value="M">Masculino</asp:ListItem>
    <asp:ListItem Value="F">Feminino</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<td class="style2"></td>
<td><asp:CheckBox ID="cbxSites" runat="server" Text="Sites" Enabled="False" /></td>
</tr>
<tr>
<td class="style2"></td>
<td><asp:CheckBox ID="cbxMedicar" runat="server" Text="Medicar na Escola" Enabled="False" /></td>
</tr>
</table>
<table width="95%">
<tr>
<td class="style2">Observações:</td>
<td>
<asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" 
  Width="99%" Height="65px" ReadOnly="True"></asp:TextBox>
</td>
</tr>
</table>
</fieldset>
<fieldset>
<legend>Dados Turma</legend>
<table>
<tr>
<td><asp:TreeView ID="trvTurmas" runat="server" ImageSet="Simple" NodeIndent="10" 
        onselectednodechanged="trvTurmas_SelectedNodeChanged">
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" 
            HorizontalPadding="0px" VerticalPadding="0px" />
        <Nodes>
            <asp:TreeNode ShowCheckBox="True" 
                Text="Turma A - Quantidade Maxima de Alunos 10 - Nº de Alunos 4" 
                Value="Turma A - Quantidade Maxima de Alunos 10 - Nº de Alunos 4" SelectAction="None">
                <asp:TreeNode ImageUrl="~/App_Themes/icones/delete.png" 
                    Text="Aluno - Fernando Mecias Dalpra" Value="Aluno - Fernando Mecias Dalpra">
                </asp:TreeNode>
                <asp:TreeNode ImageUrl="~/App_Themes/icones/delete.png" 
                    Text="Aluno - Andressa da Silva" Value="Aluno - Andressa da Silva">
                </asp:TreeNode>
                <asp:TreeNode ImageUrl="~/App_Themes/icones/delete.png" 
                    Text="Aluno - José antonio" Value="Aluno - José antonio">
                </asp:TreeNode>
                <asp:TreeNode ImageUrl="~/App_Themes/icones/delete.png" 
                    Text="Aluno - Rodrigo Benedito" Value="Aluno - Rodrigo Benedito">
                </asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode ShowCheckBox="True" 
                Text="Turma B - Quantidade Maxima de Alunos 10 - Nº de Alunos 3" 
                Value="Turma A - Quantidade Maxima de Alunos 10 - Nº de Alunos 4">
                <asp:TreeNode ImageUrl="~/App_Themes/icones/delete.png" 
                    Text="Aluno - Fabricio Dalpra" Value="Aluno - Fernando Mecias Dalpra">
                </asp:TreeNode>
                <asp:TreeNode ImageUrl="~/App_Themes/icones/delete.png" 
                    Text="Aluno - Marcela" Value="Aluno - Andressa da Silva">
                </asp:TreeNode>
                <asp:TreeNode ImageUrl="~/App_Themes/icones/delete.png" 
                    Text="Aluno - Jessica" Value="Aluno - José antonio">
                </asp:TreeNode>
            </asp:TreeNode>
        </Nodes>
        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
            HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
    </asp:TreeView>
    </td>
</tr>
</table>
</fieldset>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>

<span class="style8">*Preenchimento obrigatório.</span>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
 ShowMessageBox="True" ShowSummary="False" />

</asp:Content>
