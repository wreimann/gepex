<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
    CodeBehind="frmConsultarDisciplinas.aspx.cs" Inherits="GEPEX.Consultas.frmConsultarDisciplinas" 
    Title="Consulta de Disciplinas" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.tamanhoColunaMat
{
 width:150px;
 max-width:150px;overflow:hidden;
}
.tamanhoColunaDesc
{
 width:350px;
 max-width:350px;overflow:hidden;
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
           <td>Disciplina:</td>
           <td><asp:TextBox ID="txtDescricao" runat="server" Width="300" MaxLength="40"></asp:TextBox>
           </td> 
    </tr>
    </table>
    <br />
    <asp:GridView ID="gdvMateria" runat="server" AutoGenerateColumns="False" 
                    Width="500px" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" DataKeyNames="codigo" 
                    onrowdeleting="gdvMateria_RowDeleting" 
                    onrowediting="gdvMateria_RowEditing" AllowPaging="True" 
                    onpageindexchanging="gdvMateria_PageIndexChanging" AllowSorting="True" 
                    EnableTheming="True" onsorting="gdvMateria_Sorting" 
                    PageSize="15" onrowdatabound="gdvMateria_RowDataBound" CssClass="Tabela">
                    <RowStyle BackColor="#E3EAEB"  Wrap="False" />
                    <Columns>                    
                        <asp:BoundField DataField="materia" HeaderText="Disciplina" 
                            SortExpression="materia" ControlStyle-CssClass="tamanhoColunaMat" 
                            HeaderStyle-CssClass="tamanhoColunaMat" ItemStyle-CssClass="tamanhoColunaMat"/>
                        <asp:BoundField DataField="descricao" HeaderText="Descrição" 
                            SortExpression="descricao" ControlStyle-CssClass="tamanhoColunaDesc" 
                            HeaderStyle-CssClass="tamanhoColunaDesc" ItemStyle-CssClass="tamanhoColunaDesc" />
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
