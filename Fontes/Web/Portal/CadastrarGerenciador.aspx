<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
        CodeBehind="CadastrarGerenciador.aspx.cs" Inherits="Web.Portal.CadastrarGerenciador" 
        Title="Cadastro de Contéudo do Portal" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

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
        color: #000000;
        }
       </style>
       <script type= "text/javascript">
        function OnClick() {
            return confirm("Deseja realmente excluir essa imagem?");
        }
        function desabilitarCampos(opcao) {
            if (opcao == "3")
                $(document).ready(function() { $("#<%=Editor1.ClientID %>").hide(); });           
            if (opcao != "1")
                $(document).ready(function() { $("#<%=trData.ClientID %>").hide(); });
        }
     

        	
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
    <uc2:Mensagem ID="Mensagem1" runat="server" />
  <uc1:botao ID="botao1" runat="server" />  
</div>
<div id="divFormulario">
    <asp:UpdatePanel ID="upnOpcao" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <Triggers>
        <asp:PostBackTrigger ControlID="rdlOpcao"  />
    </Triggers>
    <ContentTemplate>
    
    <fieldset>
<legend></legend>
<table>
<tr>
<td>
    <asp:RadioButtonList ID="rdlOpcao" runat="server" RepeatDirection="Horizontal" 
       onselectedindexchanged="rdlOpcao_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Selected="True" Value="1">Eventos</asp:ListItem>
        <asp:ListItem Value="2">Notícias</asp:ListItem>
        <asp:ListItem Value="3">Colaboradores/Patrocinadores</asp:ListItem>
    </asp:RadioButtonList>
</td>
</tr>
</table>
<table id="tblPrincipal" runat="server">
<tr id="trTitulo" runat="server">
<td class="style2"><span class="style1">*</span><span class="style3"><b><asp:Label ID="lblTitulo" runat="server"
        Text="Título"></asp:Label></span>:</td>
<td><asp:TextBox ID="txtTitulo" runat="server" Width="400px"></asp:TextBox></td>
<td>    <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" 
                    ControlToValidate="txtTitulo" Display="Dynamic" 
                    ErrorMessage="Informe o campo: Título">*</asp:RequiredFieldValidator>
</td>
</tr>
</table>
<table>
<tr id="trData" runat="server">
<td class="style2">Data/Hora:</td>
<td><asp:TextBox ID="txtData" runat="server" Width="120px"></asp:TextBox>
    <cc1:MaskedEditExtender ID="txtData_MaskedEditExtender" runat="server" 
        AutoComplete="False" ClearMaskOnLostFocus="False" Mask="99/99/9999 99:99" 
        MaskType="DateTime" TargetControlID="txtData">
    </cc1:MaskedEditExtender>
</td>
<td>
    <asp:ImageButton ID="btnCalendario" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/icones/calendar.png" />
    <cc1:CalendarExtender ID="cldCalendario" runat="server" 
        PopupButtonID="btnCalendario" TargetControlID="txtData" Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
</td>
</tr>
</table>
    <cc2:Editor ID="Editor1" runat="server" Height="280px" Width="99%"></cc2:Editor>
</fieldset>
</ContentTemplate>
</asp:UpdatePanel>

<fieldset>
<legend>Adicionar Imagens</legend>
<table  id="trImagem" runat="server">
<tr>
<td class="style2"><span class="style1">*</span><span class="style3"><b><asp:Label ID="Label2" runat="server"
        Text="Imagem"></asp:Label></span>:</td>
<td>
    <input ID="file" type="file" runat="server" size="42"/>
</td>
<td><asp:Button id="btnUpload" runat="server" Text="Adicionar" 
        onclick="btnUpload_Click" CausesValidation="False" Height="21px"  
        Width="65px"/></td>
</tr>
</table>         
</fieldset>
  <br/>
<span><span class="style1">*Preenchimento obrigatório</span>.</span>
  <br/>
  <br/>

   <asp:Panel ID="pnlGaleria" runat="server" >
   </asp:Panel>

</div>
</asp:Content>
