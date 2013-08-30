<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmConsultarAtendimentoAluno.aspx.cs" Inherits="Web.Consultas.frmConsultarAtendimentoAluno"
    Title="Consulta de Atendimento ao Aluno" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
    </div>
    <div id="divFormulario">
        <table>
            <tr>
                <td>
                    Aluno:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="txtAluno" Width="300px" AutoComplete="Off"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="acePessoa" runat="server" ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx"
                        TargetControlID="txtAluno" ServiceMethod="ListaAluno" MinimumPrefixLength="1"
                        CompletionInterval="400" FirstRowSelected="True" CompletionSetCount="5">
                    </cc1:AutoCompleteExtender>
                </td>
            </tr>
            </table>
        <fieldset style="width:700px">
        <legend>Prontuário do Aluno</legend>
        <table id="tblProntuario">
            <tr>
                <td>
                    <asp:GridView ID="gdvProntuario" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="Altura" HeaderText="Altura" >
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Peso" HeaderText="Peso" >
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoSanguinio" HeaderText="Tipo Sanguinio" >
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FatorRH" HeaderText="Fator RH" >
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Alergias" HeaderText="Alergias" >
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" >
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            </table>
            </fieldset>
            <fieldset style="width:700px">
            <legend>Atendimentos</legend>
            <tr>
                <td>
                    <asp:GridView ID="gdvAtendimento" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="100%" 
                        OnRowEditing="gdvAtendimento_RowEditing" DataKeyNames="codigo" 
                        onrowdatabound="gdvAtendimento_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="data" HeaderText="Data">
                                    <HeaderStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="profissao" HeaderText="Profissão">
                                    <HeaderStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="docente" HeaderText="Docente">
                                    <HeaderStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="atendimento" HeaderText="Atendimento" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgVisualizar" runat="server" CommandName="edit" ImageUrl="~/App_Themes/icones/zoom.png"
                                        ToolTip="Visualizar" />
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
                </td>
            </tr>
            <tr>
            <td>
                <uc2:Mensagem ID="Mensagem1" runat="server" />
                </td>
            </tr>
        </table>
        </fieldset>
    </div>
</asp:Content>
