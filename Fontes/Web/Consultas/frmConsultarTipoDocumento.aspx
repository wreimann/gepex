<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarTipoDocumento.aspx.cs" Inherits="Web.Consultas.frmConsultarTipoDocumento"
    Title="Consulta Tipo Documento" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .style1
        {
            width: 482px;
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
                    Descrição:
                </td>
                <td>
                    <asp:TextBox ID="txtDescricao" runat="server" Width="330px" MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            </table>
            <br />
                <asp:GridView ID="gdvTipoDocumento" runat="server" AutoGenerateColumns="False" 
                        ForeColor="#333333" EnableTheming="True" 
                        onrowdeleting="gdvTipoDocumento_RowDeleting" 
                        onrowediting="gdvTipoDocumento_RowEditing" AllowPaging="True" 
                        AllowSorting="True" onpageindexchanging="gdvTipoDocumento_PageIndexChanging" 
                        onsorting="gdvTipoDocumento_Sorting"
                        CellPadding="4" GridLines="None" DataKeyNames="codigo" 
            onrowdatabound="gdvTipoDocumento_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="descricao" HeaderText="Descrição" SortExpression="Descricao">
                                <HeaderStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mascara" HeaderText="Máscara" SortExpression="Mascara">
                                <HeaderStyle Width="170px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="situacao" HeaderText="Situação" SortExpression="Situacao">
                            <HeaderStyle Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" CommandName="edit"
                                    ImageUrl="~/App_Themes/icones/edit.png" ToolTip="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgExcluir" runat="server" ImageUrl="~/App_Themes/icones/delete.png"
                                        ToolTip="Excluir" CommandName="delete" OnClientClick="javascript:return confirm('Deseja realmente excluir?')" />
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
