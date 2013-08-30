<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
        CodeBehind="frmConsultarPlanejamentoClinico.aspx.cs" 
        Inherits="Web.Consultas.frmConsultarPlenejamentoClinico" 
        Title="Consulta Planjemanto Clínico" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
        .style2
        {
            text-align: right;
            width:60pt;
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
    <td class="style2">Ano Letivo:</td>
    <td><asp:DropDownList ID="ddlAnoLetivo" AppendDataBoundItems="True"  runat="server" Width="115px" >
    </asp:DropDownList>
    </td>
    </tr>
    <tr> 
    <td class="style2">Aluno:</td>
    <td>
    <asp:TextBox ID="txtNome" runat="server" Width="345px"  
            ontextchanged="txtNome_TextChanged" MaxLength="80" 
            AutoCompleteType="Disabled"></asp:TextBox>
        <asp:HiddenField ID="hfdNome" runat="server" />       
    <cc1:autocompleteextender ID="acePessoa" runat="server"  
            ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx" 
            TargetControlID="txtNome" ServiceMethod="ListaAluno" 
         MinimumPrefixLength="1" CompletionInterval="400" FirstRowSelected="True" 
        CompletionSetCount="10">
    </cc1:autocompleteextender>
    </td>
    </tr>
    <tr>
    <td class="style2">Especialidade:</td>
    <td>
    <asp:DropDownList ID="ddlProfissao" runat="server" AppendDataBoundItems="True" 
            Width="350px">
    <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
    </asp:DropDownList>
    </td>

    </tr>
    </table>
    <br />
        <asp:GridView ID="gdvPlanejamentoClinico" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" 
            onrowdeleting="gdvPlanejamentoClinico_RowDeleting" 
            onrowediting="gdvPlanejamentoClinico_RowEditing" AllowPaging="True" 
        AllowSorting="True" DataKeyNames="codigo" 
        onpageindexchanging="gdvPlanejamentoClinico_PageIndexChanging" 
        onsorting="gdvPlanejamentoClinico_Sorting" 
        onrowdatabound="gdvPlanejamentoClinico_RowDataBound">
            <RowStyle BackColor="#E3EAEB" Wrap="False" />
            <Columns>
                <asp:BoundField DataField="periodo" HeaderText="Período" SortExpression="periodo">
                <HeaderStyle Width="180px" />
                </asp:BoundField>
                <asp:BoundField DataField="aluno" HeaderText="Aluno" SortExpression="aluno">
                    <HeaderStyle Width="280px" />
                </asp:BoundField>
                <asp:BoundField DataField="especialidade" HeaderText="Especialidade" SortExpression="especialidade">
                    <HeaderStyle Width="180px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEditar" runat="server" CommandName="edit" 
                            ImageUrl="~/App_Themes/icones/edit.png" ToolTip="Editar" />
                    </ItemTemplate>
                    <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgExcluir" runat="server" CommandName="delete" 
                            ImageUrl="~/App_Themes/icones/delete.png" ToolTip="Excluir" onclientclick="javascript:return confirm('Deseja realmente excluir?')"/>
                    </ItemTemplate>
                    <HeaderStyle Width="20px" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
</div>
</asp:Content>
