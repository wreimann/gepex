<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarPerfil.aspx.cs" Inherits="Web.Consultas.frmConsultarPerfil"
    Title="Perfil de Acesso" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        &nbsp;<uc1:botao ID="botao1" runat="server" />
    </div>
    <div id="divFormulario">
        <table>
            <tr>
                <td>
                  <asp:GridView ID="gdvPerfil" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" EnableTheming="True"
                        ForeColor="#333333" GridLines="None" 
                        DataKeyNames="codigo"
                        OnRowEditing="gdvPerfil_RowEditing" AllowSorting="True" 
                        onsorting="gdvPerfil_Sorting" onrowdatabound="gdvPerfil_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="codigo" Visible="False" />
                            <asp:BoundField DataField="descricao" HeaderText="Perfil" SortExpression="Descricao">
                                <HeaderStyle Width="250px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/App_Themes/icones/edit.png"
                                        ToolTip="Permissões" CommandName="edit" />
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
