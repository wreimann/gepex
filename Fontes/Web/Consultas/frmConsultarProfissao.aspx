<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
    CodeBehind="frmConsultarProfissao.aspx.cs" Inherits="Web.Consultas.frmConsultarProfissao" 
    Title="Consulta Profissão" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
    <uc1:botao ID="botao1" runat="server" />
    <uc2:Mensagem ID="Mensagem" runat="server" />
</div>
<div id="divFormulario">
<table>
<tr>
<td>Profissão:</td>
<td><asp:TextBox ID="txtDescricao" runat="server" Width="330px" MaxLength="40"></asp:TextBox></td>
</tr>
</table>
<br />
    <asp:GridView ID="gdvProfissao" runat="server" AutoGenerateColumns="False" 
        ForeColor="#333333" EnableTheming="True" 
        onrowdeleting="gdvProfissao_RowDeleting" 
        onrowediting="gdvProfissao_RowEditing" AllowPaging="True" 
        AllowSorting="True" onpageindexchanging="gdvProfissao_PageIndexChanging" 
        onsorting="gdvProfissao_Sorting"
        CellPadding="4" GridLines="None" DataKeyNames="codigo" 
        onrowdatabound="gdvProfissao_RowDataBound">
        <RowStyle BackColor="#E3EAEB" Wrap="False" />
        <Columns>
            <asp:BoundField DataField="Descricao" HeaderText="Profissão" SortExpression="Descricao" >
                <HeaderStyle Width="270px" />
            </asp:BoundField>
            <asp:BoundField DataField="Situacao" HeaderText="Situação" SortExpression="Situacao" >
            <HeaderStyle Width="80px" />
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
                    <asp:ImageButton ID="imgExcluir" runat="server" 
                        ImageUrl="~/App_Themes/icones/delete.png" ToolTip="Excluir" 
                        CommandName="delete" 
                        onclientclick="javascript:return confirm('Deseja realmente excluir?')"/>
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
