<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarDocente.aspx.cs" Inherits="GEPEX.Consultas.frmConsultarDocente"
    Title="Consulta de Funcionários" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>

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
                <td>
                    Nome:
                </td>
                <td>
                    <asp:TextBox ID="txtNome" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            </table>
            <br />
                    <asp:GridView ID="gdvDocente" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" 
                        DataKeyNames="codigo" OnRowDeleting="gdvDocente_RowDeleting"
                        OnRowEditing="gdvDocente_RowEditing" AllowPaging="True" OnPageIndexChanging="gdvDocente_PageIndexChanging"
                        AllowSorting="True" EnableTheming="True" 
                         OnSorting="gdvDocente_Sorting" PageSize="15" 
            onrowdatabound="gdvDocente_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome">
                                <HeaderStyle Width="230px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="profissao" HeaderText="Profissão" SortExpression="profissao">
                                <HeaderStyle Width="110px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="situacao" HeaderText="Situação" SortExpression="situacao">
                             <HeaderStyle Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" CommandName="edit" ImageUrl="~/App_Themes/icones/edit.png"
                                        ToolTip="Editar" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgExcluir" runat="server" CommandName="delete" ImageUrl="~/App_Themes/icones/delete.png"
                                        ToolTip="Excluir" OnClientClick="javascript:return confirm('Deseja realmente excluir?')" />
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
