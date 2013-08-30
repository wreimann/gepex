<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarEndereco.aspx.cs" Inherits="GEPEX.Consultas.frmConsultarEndereco"
    Title="Consulta de Endereços" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tamanhoColunaMat
        {
            width: 100px;
            max-width: 100px;
            overflow: hidden;
        }
        .tamanhoColunaDesc
        {
            width: 250px;
            max-width: 250px;
            overflow: hidden;
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
                <td>
                    CEP:
                </td>
                <td>
                    <asp:TextBox ID="txtCEP" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
                    <cc1:maskededitextender id="txtCep_MaskedEditExtender" runat="server" autocomplete="False"
                        clearmaskonlostfocus="False" culturename="en-US" mask="99999-999" masktype="Number"
                        targetcontrolid="txtCEP">
                    </cc1:maskededitextender>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gdvMateria" runat="server" AutoGenerateColumns="False" Width="500px"
            CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="codigo" OnRowEditing="gdvMateria_RowEditing"
            AllowPaging="True" OnPageIndexChanging="gdvMateria_PageIndexChanging" AllowSorting="True"
            EnableTheming="True" OnSorting="gdvMateria_Sorting" PageSize="15" OnRowDataBound="gdvMateria_RowDataBound"
            CssClass="Tabela">
            <RowStyle BackColor="#E3EAEB" Wrap="False" />
            <Columns>
                <asp:BoundField DataField="cep" HeaderText="CEP" SortExpression="cep"
                    ControlStyle-CssClass="tamanhoColunaMat" HeaderStyle-CssClass="tamanhoColunaMat"
                    ItemStyle-CssClass="tamanhoColunaMat" />
                <asp:BoundField DataField="logradouro" HeaderText="Logradouro" SortExpression="logradouro"
                    ControlStyle-CssClass="tamanhoColunaDesc" HeaderStyle-CssClass="tamanhoColunaDesc"
                    ItemStyle-CssClass="tamanhoColunaDesc" />
                <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade"
                    ControlStyle-CssClass="tamanhoColunaDesc" HeaderStyle-CssClass="tamanhoColunaDesc"
                    ItemStyle-CssClass="tamanhoColunaDesc" />    
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEditar" runat="server" CommandName="edit" ImageUrl="~/App_Themes/icones/edit.png"
                            ToolTip="Editar" />
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
