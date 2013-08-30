<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarGradeHorario.aspx.cs" Inherits="Web.Consultas.frmConsultarGradeHorario"
    Title="Consulta Grade Horário" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .style1
    {
        text-align: right;
        width:50pt;
    }
    .ColunaTurma
    {
        width:285px;
        max-width:285px;overflow:hidden;
    }
    .ColunaDisciplina
    {
        width:100px;
        max-width:110px;overflow:hidden;
    }
    .ColunaDia
    {
        width:85px;
        max-width:85px;overflow:hidden;
    }
    .ColunaHorario
    {
        width:85px;
        max-width:85px;overflow:hidden;
    }
    .ColunaProfessor
    {
        width:130px;
        max-width:130px;overflow:hidden;
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
                <tr>
                <td class="style1">Professor:</td>
                <td>
                    <asp:TextBox ID="txtDocente" runat="server"  width="244px" 
                    ontextchanged="txtDocente_TextChanged"></asp:TextBox>
                    <asp:HiddenField ID="hflDocente" runat="server" />
                    <cc1:AutoCompleteExtender ID="acePessoa" runat="server"  
                    ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx" 
                    TargetControlID="txtDocente" ServiceMethod="ListaPedagogico" 
                    MinimumPrefixLength="1" CompletionInterval="400" FirstRowSelected="True" 
                    CompletionSetCount="10">
                    </cc1:AutoCompleteExtender>
                </td>
                </tr>
              </table>
              <br />
            <asp:GridView ID="gdvGradeHorario" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="None" 
                        OnRowDeleting="gdvGradeHorario_RowDeleting" 
                        OnRowEditing="gdvGradeHorario_RowEditing" AllowPaging="True" 
                        AllowSorting="True" onpageindexchanging="gdvGradeHorario_PageIndexChanging" 
                        onsorting="gdvGradeHorario_Sorting"
                        DataKeyNames="codigo" EnableTheming="True" 
            onrowdatabound="gdvGradeHorario_RowDataBound" >
                        <RowStyle BackColor="#E3EAEB" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="turma" HeaderText="Turma"  SortExpression="turma"
                            ControlStyle-CssClass="ColunaTurma" 
                            HeaderStyle-CssClass="ColunaTurma" ItemStyle-CssClass="ColunaTurma" />
                            <asp:BoundField DataField="disciplina" HeaderText="Disciplina" SortExpression="disciplina" 
                            ControlStyle-CssClass="ColunaDisciplina" 
                            HeaderStyle-CssClass="ColunaDisciplina" ItemStyle-CssClass="ColunaDisciplina" />
                            <asp:BoundField DataField="dia" HeaderText="Dia" SortExpression="dia" 
                            ControlStyle-CssClass="ColunaDia" 
                            HeaderStyle-CssClass="ColunaDia" ItemStyle-CssClass="ColunaDia" />
                            <asp:BoundField DataField="horario" HeaderText="Horário" SortExpression="horario" 
                            ControlStyle-CssClass="ColunaHorario" 
                            HeaderStyle-CssClass="ColunaHorario" ItemStyle-CssClass="ColunaHorario" />
                            <asp:BoundField DataField="professor" HeaderText="Professor" SortExpression="professor"
                            ControlStyle-CssClass="ColunaProfessor" 
                            HeaderStyle-CssClass="ColunaProfessor" ItemStyle-CssClass="ColunaProfessor" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/App_Themes/icones/edit.png"
                                        ToolTip="Editar" CommandName="edit" />
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
