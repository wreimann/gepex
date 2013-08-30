<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarAgendaAluno.aspx.cs" Inherits="Web.Consultas.frmConsultarAgendaAluno"
    Title="Agenda do Aluno" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
        <uc2:Mensagem ID="Mensagem1" runat="server" />
    </div>
    <div id="divFormualrio">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
        <ContentTemplate>
        <table>
            <tr>
                <td>
                    Aluno
                </td>
                <td>
                    <asp:TextBox ID="txtAluno" runat="server" Width="345px"  
                    MaxLength="80" AutoCompleteType="Disabled" 
                        ontextchanged="txtAluno_TextChanged" ReadOnly="True" AutoPostBack="True"></asp:TextBox>
        <asp:HiddenField ID="hfdNome" runat="server" />       
    <cc1:autocompleteextender ID="acePessoa" runat="server"  
            ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx" 
            TargetControlID="txtAluno" ServiceMethod="ListaAluno" 
         MinimumPrefixLength="1" CompletionInterval="400" FirstRowSelected="True" 
        CompletionSetCount="10">
    </cc1:autocompleteextender>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtData" runat="server" Width="100px" MaxLength="10" 
                        onkeyup="formataData(this,event);" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Calendar ID="calData" runat="server" BackColor="#E3EAEB" Width="300px" 
                        SelectedDate="11/05/2010 10:36:40" ondayrender="calData_DayRender" 
                        onselectionchanged="calData_SelectionChanged">
                        <OtherMonthDayStyle Font-Bold="False" Font-Strikeout="False" 
                            Font-Underline="false" ForeColor="Silver" />
                        <TitleStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    </asp:Calendar>
                </td>
            </tr>
        </table>
        <fieldset>
        <legend>Agenda do Aluno</legend>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gdvAgendaAluno" runat="server" AutoGenerateColumns="False" Width="627px"
                        CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Codigo" 
                        onselectedindexchanging="gdvAgendaAluno_SelectedIndexChanging" 
                        onrowdeleting="gdvAgendaAluno_RowDeleting" 
                        onrowdatabound="gdvAgendaAluno_RowDataBound" AllowPaging="True" 
                        onpageindexchanging="gdvAgendaAluno_PageIndexChanging">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" />
                            <asp:BoundField DataField="Profissional" HeaderText="Profissional" />
                            <asp:BoundField DataField="Especialidade" HeaderText="Profissão" />
                            <asp:BoundField DataField="Anotacao" HeaderText="Anotação" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/App_Themes/icones/page_edit.png"
                                        ToolTip="Editar" CommandName="select" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/App_Themes/icones/delete.png"
                                        ToolTip="Excluir" CommandName="delete" onclientclick="javascript:return confirm('Deseja realmente excluir?')"/>
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
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
                </td>
            </tr>
        </table>
        </fieldset>
        <fieldset>
        <legend>Grade de Horário</legend>
        <table>
            <tr>               
                <td valign="top">
                    <asp:GridView ID="gdvGradeHorario" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="312px" DataKeyNames="Codigo" 
                        AllowPaging="True" onpageindexchanging="gdvGradeHorario_PageIndexChanging" 
                        PageSize="15">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="Dia" HeaderText="Dia" />
                            <asp:BoundField DataField="Aula" HeaderText="Aula" />
                            <asp:BoundField DataField="Disciplina" HeaderText="Disciplina" />
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle Font-Bold="False" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    &nbsp;
                    <br />
                </td>
            </tr>
        </table>
        </fieldset>
        <fieldset>
        <legend>Atendimentos</legend>
        <table>
        <tr><td>
        <asp:GridView ID="gdvAtendimento" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="624px" 
                        DataKeyNames="Codigo" AllowPaging="True" 
                onpageindexchanging="gdvAtendimento_PageIndexChanging">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="Horario" HeaderText="Horário" >
                                <HeaderStyle Width="500px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Especialidade" HeaderText="Especialidade" >
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Profissional" HeaderText="Profissional" >
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:TemplateField></asp:TemplateField>
                            <asp:TemplateField></asp:TemplateField>
                            <asp:TemplateField></asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle Font-Bold="False" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
        </td></tr>
        </table>
        </fieldset>
        </ContentTemplate>       
        </asp:UpdatePanel>
    </div>
</asp:Content>
