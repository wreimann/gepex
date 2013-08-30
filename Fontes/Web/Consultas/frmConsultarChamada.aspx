<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarChamada.aspx.cs" Inherits="Web.Consultas.frmConsultarChamada"
    Title="Consulta de Chamada" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
            width:50pt;
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
                <td class="style1">Série:</td>
                <td><asp:TextBox ID="txtSerie" runat="server" Width="60px" MaxLength="4"></asp:TextBox>
                </td>  
                <td class="style1">Turma:</td>
                <td>
                    <asp:TextBox ID="txtTurma" runat="server" Width="60px"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="style1">Ano Letivo:</td>
                <td><asp:DropDownList ID="ddlAnoLetivo" runat="server" AppendDataBoundItems="True" 
                        Width="65px"></asp:DropDownList>
                </td>
            </tr>
            </table>
            <table>
                <tr>
                    <td class="style1">Ensino:</td>
                    <td>
                    <asp:DropDownList ID="ddlEnsino" runat="server" AppendDataBoundItems="True" Width="250px">
                            <asp:ListItem Value="">Todos</asp:ListItem>
                            <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                            <asp:ListItem Value="P">Ensino Profissionalizante</asp:ListItem>
                        </asp:DropDownList>
                    </td>              
                </tr>
                <tr>
                    <td class="style1">Período:</td>
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
              <br />
           <asp:GridView ID="gdvChamada" runat="server" AutoGenerateColumns="False" CellPadding="4"
            ForeColor="#333333" GridLines="None"  
            OnRowEditing="gdvChamada_RowEditing" AllowPaging="True" 
            AllowSorting="True" onpageindexchanging="gdvChamada_PageIndexChanging" 
            onsorting="gdvChamada_Sorting" DataKeyNames="codigo"
            EnableTheming="True" PageSize="15" 
            onrowdatabound="gdvChamada_RowDataBound">
            <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="turma" HeaderText="Turma" SortExpression="serie">
                                <HeaderStyle Width="400px"  />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/App_Themes/icones/edit.png"
                                        ToolTip="Lançar Chamada" CommandName="edit" />
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
