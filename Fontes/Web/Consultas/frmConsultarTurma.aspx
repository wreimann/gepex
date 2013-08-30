<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarTurma.aspx.cs" Inherits="Web.Consultas.frmConsultarTurma"
    Title="Consulta Turma" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Série:</td>
                    <td><asp:TextBox ID="txtSerie" runat="server" Width="60px" MaxLength="4"></asp:TextBox></td>
                    <td>Turma:</td> 
                    <td><asp:TextBox ID="txtTurma" runat="server" Width="60px" MaxLength="4"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
                <tr>
                <td>Ano Letivo:</td>
                <td><asp:DropDownList ID="ddlAnoLetivo" runat="server" AppendDataBoundItems="True" 
                        Width="65px"></asp:DropDownList>
                </td>
                    <td>&nbsp;&nbsp;&nbsp;Sala:</td>
                    <td><asp:TextBox ID="txtSala" runat="server"  Width="60px" MaxLength="4"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtSala_FilteredTextBoxExtender" 
                            runat="server" FilterType="Numbers" TargetControlID="txtSala">
                        </cc1:FilteredTextBoxExtender>
                    </td>              
                </tr>
               </table>
               <table>
                <tr>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ensino:</td>
                    <td>
                    <asp:DropDownList ID="ddlEnsino" runat="server" AppendDataBoundItems="True" Width="250px">
                            <asp:ListItem Value="">Todos</asp:ListItem>
                            <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                            <asp:ListItem Value="P">Ensino Profissionalizante</asp:ListItem>
                        </asp:DropDownList>
                    </td>              
                </tr>
                <tr>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp; Período:</td>
                    <td>
                    <asp:DropDownList ID="ddlPeriodo" runat="server" AppendDataBoundItems="True" Width="250px">
                        <asp:ListItem Value="">Todos</asp:ListItem>
                        <asp:ListItem Value="M">Manhã</asp:ListItem>
                        <asp:ListItem Value="T">Tarde</asp:ListItem>
                        <asp:ListItem Value="I">Integral</asp:ListItem>
                    </asp:DropDownList>
                    </td>              
                </tr>
              </table>
             <br>  
            <asp:GridView ID="gdvTurma" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="None" 
                EnableTheming="True" onrowdeleting="gdvTurma_RowDeleting" 
                onrowediting="gdvTurma_RowEditing" 
                AllowPaging="True" AllowSorting="True" PageSize="15" DataKeyNames="codigo"
                onpageindexchanging="gdvTurma_PageIndexChanging" 
                onsorting="gdvTurma_Sorting" onrowdatabound="gdvTurma_RowDataBound">
                <RowStyle BackColor="#E3EAEB" />
                <Columns>
                    <asp:BoundField DataField="anoLetivo" HeaderText="Ano Letivo" SortExpression="AnoLetivo">
                        <HeaderStyle Width="75px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="serie" HeaderText="Série" SortExpression="Serie">
                        <HeaderStyle Width="50px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="serieTurma" HeaderText="Turma" SortExpression="SerieTurma">
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ensino" HeaderText="Ensino" SortExpression="Ensino">
                        <HeaderStyle Width="105px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="periodo" HeaderText="Período" SortExpression="Periodo">
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sala" HeaderText="Sala" SortExpression="Sala">
                        <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="numeroMaximoAlunos" HeaderText="Máx. de Alunos" SortExpression="NumeroMaximoAlunos">
                        <HeaderStyle Width="100px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" />
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
                                ImageUrl="~/App_Themes/icones/delete.png" ToolTip="Excluir" 
                                onclientclick="javascript:return confirm('Deseja realmente excluir?')" />
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
