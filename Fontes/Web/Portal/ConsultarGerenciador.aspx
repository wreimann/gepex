<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarGerenciador.aspx.cs" Inherits="Web.Portal.GerenciadorPortal"
    Title="Consulta Contéudo do Portal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
            width:40px
        }
        .tamanhoColunaData
        {
            width:70px;
            max-width:70px;
            overflow:hidden;
        }
        .tamanhoColunaTipo
        {
            width:90px;
            max-width:90px;
            overflow:hidden;
        }
        .tamanhoColunaTitulo
        {
            width:170px;
            max-width:170px;
            overflow:hidden;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
        <uc2:Mensagem ID="Mensagem1" runat="server" />
    </div>
    <div id="divFormulario">
        <table>
            <tr>
                <td class="style1">
                    Título:
                </td>
                <td>
                    <asp:TextBox ID="txtTitulo" runat="server" Width="430px" MaxLength="80"></asp:TextBox>
                </td>
            </tr>
            </table>
            <table>
            <tr>
            <td class="style1">
                Data:
            </td>
            <td>
            <asp:TextBox ID="txtData" runat="server" MaxLength="10"></asp:TextBox>
            <cc1:maskededitextender ID="txtData_MaskedEditExtender" 
               runat="server" ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" 
               TargetControlID="txtData" UserDateFormat="DayMonthYear">
            </cc1:maskededitextender>
            </td>
            <td>
            <asp:ImageButton ID="btnCalendario" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/icones/calendar.png" />
            <cc1:calendarextender ID="cldCalendario" runat="server" 
                PopupButtonID="btnCalendario" TargetControlID="txtData" Format="dd/MM/yyyy">
            </cc1:calendarextender>
            </td>
            </tr>
            </table>
            <table>
            <tr>
            <td class="style1">
            </td>
            <td>
                <asp:RadioButtonList ID="rdlTipo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">Eventos</asp:ListItem>
                    <asp:ListItem Value="2">Noticías</asp:ListItem>
                    <asp:ListItem Value="3">Colaboradores</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
        </table>
        <br />
            <asp:GridView ID="gdvPortal" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" 
                onpageindexchanging="gdvPortal_PageIndexChanging" 
                AllowSorting="True" DataKeyNames="codigo" onrowdeleting="gdvPortal_RowDeleting" 
                onsorting="gdvPortal_Sorting" PageSize="15" 
                onrowediting="gdvPortal_RowEditing" Width="560px" CssClass="Tabela">
                <RowStyle BackColor="#E3EAEB" Wrap="False" />
                <Columns>
                    <asp:BoundField DataField="data" HeaderText="Data" SortExpression="data" 
                        DataFormatString="{0:dd/MM/yyyy}" 
                        ControlStyle-CssClass="tamanhoColunaData" 
                        HeaderStyle-CssClass="tamanhoColunaData" 
                        ItemStyle-CssClass="tamanhoColunaData" />  
                    <asp:BoundField DataField="tipoFormatado" HeaderText="Tipo" SortExpression="tipoFormatado" 
                        ControlStyle-CssClass="tamanhoColunaTipo" 
                        HeaderStyle-CssClass="tamanhoColunaTipo" 
                        ItemStyle-CssClass="tamanhoColunaTipo"/>
                    <asp:BoundField DataField="titulo" HeaderText="Título" SortExpression="titulo" 
                            ControlStyle-CssClass="tamanhoColunaTitulo" 
                            HeaderStyle-CssClass="tamanhoColunaTitulo" 
                            ItemStyle-CssClass="tamanhoColunaTitulo" HeaderStyle-Wrap="False" ItemStyle-Wrap="False" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgAlterar" runat="server" CommandName="edit" 
                                ImageUrl="~/App_Themes/icones/page_edit.png" ToolTip="Editar" />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgExcluir" runat="server" CommandName="delete" 
                                ImageUrl="~/App_Themes/icones/delete.png" ToolTip="Excluir" 
                                onclientclick="javascript:return confirm('Deseja realmente excluir?')"/>
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
