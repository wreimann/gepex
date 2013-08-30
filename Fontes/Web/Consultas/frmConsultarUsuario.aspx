<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
    CodeBehind="frmConsultarUsuario.aspx.cs" Inherits="Web.Consultas.frmConsultarUsuario" 
    Title="Consulta de Usuários" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
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
<td class="style1">Nome:</td>
<td><asp:TextBox ID="txtNome" runat="server" Width="300px" MaxLength="80"></asp:TextBox></td>
</tr>
<tr>
<td class="style1">Perfil:</td>
<td><asp:DropDownList ID="ddlPerfil" runat="server" AppendDataBoundItems="True" 
        Width="304px">
    <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr>
<td class="style1">Login:</td>
<td><asp:TextBox ID="txtLogin" runat="server" Width="300px" MaxLength="10"></asp:TextBox></td>
</tr>
</table>
<br />
    <asp:GridView ID="gdvUsuario" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        onrowdeleting="gdvUsuario_RowDeleting" 
        onrowediting="gdvUsuario_RowEditing"
        DataKeyNames="codigo" EnableTheming="True" AllowPaging="True" 
        AllowSorting="True" onpageindexchanging="gdvUsuario_PageIndexChanging" 
        onsorting="gdvUsuario_Sorting" onrowdatabound="gdvUsuario_RowDataBound">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome">
                <HeaderStyle Width="210px" />
            </asp:BoundField>
            <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login">
                <HeaderStyle Width="130px" />
            </asp:BoundField>
            <asp:BoundField DataField="perfil" HeaderText="Perfil" SortExpression="perfil">
                <HeaderStyle Width="130px" />
            </asp:BoundField>
            <asp:BoundField DataField="situacao" HeaderText="Situação" SortExpression="situacao" />
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
                        ImageUrl="~/App_Themes/icones/delete.png" 
                        onclientclick="javascript:return confirm('Deseja realmente excluir?')" 
                        ToolTip="Excluir" />
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
