<%@ Page Title="Permissões do Perfil" Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
         CodeBehind="frmCadastrarPermissoes.aspx.cs" Inherits="Web.Cadastros.frmCadastrarPermissoes" %>
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
            width:40pt;
        }
       </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="divBarraBotao">
    <uc1:botao ID="botao1" runat="server" />
    <uc2:Mensagem ID="Mensagem" runat="server" />
</div>
<div id="divFormulario">
<table id="tblForm">
<tr>
<td class="style2"><span class="style1">*</span><b>Perfil</b>:</td>
<td>
    <asp:TextBox ID="txtPerfil" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
</td>
</tr>
</table>
<table id="tblAluno">
<tr>
<td>
    <asp:GridView ID="gdvFormulario" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="codigo" Visible="False" />
            <asp:BoundField DataField="descricao" HeaderText="Formulário">
                <HeaderStyle Width="360px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Acesso">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAcesso" runat="server" TextAlign="Right" />
                </ItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Incluir">
                <ItemTemplate>
                    <asp:CheckBox ID="chkIncluir" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Alterar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAlterar" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Excluir">
                <ItemTemplate>
                    <asp:CheckBox ID="chkExcluir" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </td>
</tr>
</table>
</div>
</asp:Content>

