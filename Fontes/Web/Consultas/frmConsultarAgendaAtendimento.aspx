<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarAgendaAtendimento.aspx.cs" Inherits="Web.Consultas.frmConsultarAgendaAtendimento"
    Title="Agenda de Atendimento Clínico" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
            width: 235px;
        }
        .style3
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
    </div>
    <div id="divFormualrio">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">    
        <ContentTemplate>
        <table>
            <tr>
                <td class="style1">
                    <b><span class="style3"></span>Funcionário</b>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlFuncionario" runat="server" Width="300px" AppendDataBoundItems="True"
                        AutoPostBack="True" 
                        OnSelectedIndexChanged="ddlFuncionario_SelectedIndexChanged" Enabled="False">
                        <asp:ListItem Value="0">(--Selecionar--)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3"></span>Profissão:</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProfissao" runat="server" Width="300px" AppendDataBoundItems="True"
                        Enabled="False">
                        <asp:ListItem Value="0">(--Selecionar--)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    <asp:Calendar ID="calData" runat="server" BackColor="#E3EAEB" Width="300px" 
                        OnSelectionChanged="calData_SelectionChanged" 
                        ondayrender="calData_DayRender" SelectedDate="11/05/2010 10:36:40">
                        <OtherMonthDayStyle Font-Bold="False" Font-Strikeout="False" 
                            Font-Underline="false" ForeColor="Silver" />
                        <TitleStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    </asp:Calendar>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <fieldset style="width: 800px">
            <legend>Compromissos Agendados</legend>
            <table>
                <tr>
                    <td valign="top">
                        <asp:GridView ID="gdvAtendimento" runat="server" AutoGenerateColumns="False" CellPadding="0"
                            ForeColor="#333333" GridLines="None" DataKeyNames="Codigo, codigoAluno, codigoCompromisso"
                            OnSelectedIndexChanging="gdvAtendimento_SelectedIndexChanging" OnRowDeleting="gdvAtendimento_RowDeleting"
                            OnRowEditing="gdvAtendimento_RowEditing" Width="800px" 
                            onrowdatabound="gdvAtendimento_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" />
                            <Columns>
                                <asp:BoundField DataField="Hora" HeaderText="Horário">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Aluno" HeaderText="Aluno">
                                    <HeaderStyle Width="260px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Situacao" HeaderText="Situação">
                                    <HeaderStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Turma" HeaderText="Turma">
                                    <HeaderStyle Width="255px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sala" HeaderText="Sala">
                                    <HeaderStyle Width="45px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgExcluir" runat="server" CommandName="delete" ImageUrl="~/App_Themes/icones/delete.png"
                                            ToolTip="Excluir" OnClientClick="javascript:return confirm('Deseja realmente excluir?')" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="21px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgTrocarHorario" runat="server" CommandName="select" ImageUrl="~/App_Themes/icones/arrow_refresh_small.png"
                                            ToolTip="Trocar Horário" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="21px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lkbAtendimento" runat="server" CommandName="edit" ImageUrl="~/App_Themes/icones/pencil_add.png"
                                            ToolTip="Atendimento ao Aluno" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="21px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle Font-Bold="False" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc2:Mensagem ID="Mensagem1" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        </ContentTemplate>    
        </asp:UpdatePanel>
    </div>
</asp:Content>
