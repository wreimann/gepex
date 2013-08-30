<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarAlunos.aspx.cs" Inherits="GEPEX.Consultas.frmConsultarAlunos"
    Title="Consultar Alunos" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
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
                <td> <asp:TextBox ID="txtNome" runat="server" MaxLength="80" Width="320px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">Matrícula:</td>
                <td>    <asp:TextBox ID="txtMatricula" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                    <cc3:FilteredTextBoxExtender ID="txtMatricula_FilteredTextBoxExtender" runat="server"
                        FilterType="Numbers" TargetControlID="txtMatricula">
                    </cc3:FilteredTextBoxExtender>
                </td>
            </tr>
            </table>
            <br />
                    <asp:GridView ID="gdvAlunos" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" 
            EnableTheming="true" OnRowEditing="gdvAlunos_RowEditing"
                        OnRowDeletin="gvdAlunos_RowDeleting" AllowPaging="True" 
            AllowSorting="True" PageSize="15"
                        OnPageIndexChanging="gdvAlunos_PageIndexChanging" 
            OnSorting="gdvAlunos_Sorting" DataKeyNames="codigo" 
            onrowdatabound="gdvAlunos_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome">
                                <HeaderStyle Width="240px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="matricula" HeaderText="Matrícula" SortExpression="matricula">
                            <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="idade" HeaderText="Idade" SortExpression="idade">
                            <HeaderStyle Width="45px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="situacao" HeaderText="Situação" SortExpression="situacao">
                            <HeaderStyle Width="130px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" CommandArgument="edit" 
                                        CommandName="edit"
                                        ImageUrl="~/App_Themes/icones/page_edit.png"
                                        ToolTip="Editar" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgExcluir" runat="server" CommandName="delete" ImageUrl="~/App_Themes/icones/delete.png"
                                        ToolTip="Inativar" OnClientClick="javascript:return confirm('Deseja realmente excluir?')" />
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
