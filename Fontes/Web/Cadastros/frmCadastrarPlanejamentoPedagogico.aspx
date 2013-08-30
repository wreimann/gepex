<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarPlanejamentoPedagogico.aspx.cs" Inherits="Web.Cadastros.frmCadastrarPlanejamentoPedagogico"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        { 
            text-align: right;
            width: 178px;
        }
        .style3
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        &nbsp;<uc1:botao ID="botao1" runat="server" />
    </div>
    <div id="divFormulario">
        <table>
            <tr>
                <td class="style6">
                </td>
            </tr>
            <tr>
<td class="style1">
                    <b><span class="style3">*</span>Ano Letivo</b>:
                </td>
<td>
<asp:DropDownList ID="ddlAnoLetivo" runat="server" Width="100px" 
        AutoPostBack="True" onselectedindexchanged="ddlAnoLetivo_SelectedIndexChanged">
</asp:DropDownList>
</td>
</tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Turma</b>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlTurma" runat="server" AppendDataBoundItems="True" Width="300px">
                        <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                        <asp:ListItem>f</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="color: Red">
                    <asp:CompareValidator ID="cfvTurma" runat="server" ErrorMessage="Selecione o campo turma"
                        ControlToValidate="ddlTurma" Display="Dynamic" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp; <b><span class="style3">*</span>Carga Horária</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtCargaHoraria" runat="server"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="txtCargaHoraria_FilteredTextBoxExtender" runat="server"
                        FilterType="Numbers" TargetControlID="txtCargaHoraria">
                    </cc1:FilteredTextBoxExtender>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvCargaHoraria" runat="server" ControlToValidate="txtCargaHoraria"
                        Display="Dynamic" ErrorMessage="Informe o Campo Carga Horaria">*</asp:RequiredFieldValidator>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="color: Red">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Disciplina</b>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDisciplina" runat="server" AppendDataBoundItems="True" Width="300px">
                        <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                        <asp:ListItem>d</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="color: Red">
                    <asp:CompareValidator ID="cfvDisciplina" runat="server" ErrorMessage="Selecione o campo disciplina"
                        ControlToValidate="ddlDisciplina" Display="Dynamic" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="color: Red">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Data Inicial</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtDataInicial" runat="server" MaxLength="10"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtDataInicial_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False"
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataInicial" UserDateFormat="DayMonthYear">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfvDataInicial" runat="server" ControlToValidate="txtDataInicial"
                        Display="Dynamic" ErrorMessage="Informe o Data Inicial">*</asp:RequiredFieldValidator>
                </td>
                <td style="color: Red">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Data Final</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtDataFinal" runat="server" MaxLength="10"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtDataFinal_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False"
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataFinal" UserDateFormat="DayMonthYear">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfvDataFinal" runat="server" ControlToValidate="txtDataFinal"
                        Display="Dynamic" ErrorMessage="Informe o Campo Data Final">*</asp:RequiredFieldValidator>
                </td>
                <td style="color: Red">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Ementa</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtEmenta" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvEmenta" runat="server" ControlToValidate="txtEmenta"
                        Display="Dynamic" ErrorMessage="Informe o Campo Ementa">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Competências/Habilidades</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtCompetencias" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvCompetencia" runat="server" ControlToValidate="txtCompetencias"
                        Display="Dynamic" ErrorMessage="Informe o Campo Competencia">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Objetivo Geral da Disciplina</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtObjetivoGeralDisciplina" runat="server" TextMode="MultiLine"
                        Width="300px"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvObjDisciplina" runat="server" ControlToValidate="txtObjetivoGeralDisciplina"
                        Display="Dynamic" ErrorMessage="Informe o Campo Objetivo Geral da Disciplina">*</asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;
                </td>
                <td>
                    ----------------------Conteudo Programatico--------------------
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Número de Aulas</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroAulas" runat="server" Height="20px"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="txtNumeroAulas_FilteredTextBoxExtender" runat="server"
                        FilterType="Numbers" TargetControlID="txtNumeroAulas">
                    </cc1:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvEmentaNumeroAulas" runat="server" ControlToValidate="txtNumeroAulas"
                        Display="Dynamic" ErrorMessage="Informe o Número de Aulas" ValidationGroup="Conteudo">*</asp:RequiredFieldValidator>
                </td>
                <td style="color: Red">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Data Inicial</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtConteudoDataInicial" runat="server"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtConteudoDataInicial_MaskedEditExtender" runat="server"
                        ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtConteudoDataInicial"
                        UserDateFormat="DayMonthYear">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfvConteudoDataInicial" runat="server" ControlToValidate="txtConteudoDataInicial"
                        Display="Dynamic" ErrorMessage="Informe o Data Inicial do Conteudo" ValidationGroup="Conteudo">*</asp:RequiredFieldValidator>
                </td>
                <td style="color: Red">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Data Final</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtConteudoDataFinal" runat="server"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtConteudoDataFinal_MaskedEditExtender" runat="server"
                        ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtConteudoDataFinal"
                        UserDateFormat="DayMonthYear">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfvConteudoDataFinal" runat="server" ControlToValidate="txtConteudoDataFinal"
                        Display="Dynamic" ErrorMessage="Informe o Campo Data Final do Conteudo" ValidationGroup="Conteudo">*</asp:RequiredFieldValidator>
                </td>
                <td style="color: Red">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Objetivo Especifico</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtObjetivoEspecifico" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvObjetivoEspecifico" runat="server" ControlToValidate="txtObjetivoEspecifico"
                        Display="Dynamic" ErrorMessage="Informe o Campo Objetivo Especifico" ValidationGroup="Conteudo">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Conteudo</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtConteudo" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvConteudo" runat="server" ControlToValidate="txtConteudo"
                        Display="Dynamic" ErrorMessage="Informe o Campo Conteudo" ValidationGroup="Conteudo">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Método</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtMetodo" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
                <td style="color: Red">
                    <asp:RequiredFieldValidator ID="rfvMetodo" runat="server" ControlToValidate="txtMetodo"
                        Display="Dynamic" ErrorMessage="Informe o Campo Metodo" ValidationGroup="Conteudo">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnAdicionarConteudo" runat="server" OnClick="btnAdicionarConteudo_Click"
                        Text="Adicionar Conteudo" ValidationGroup="Conteudo" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gdvConteudoProgramaticoPedagogico" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="gdvConteudoProgramaticoPedagogico_RowDeleting"
                        OnRowEditing="gdvConteudoProgramaticoPedagogico_RowEditing" DataKeyNames="codigo">
                        <RowStyle BackColor="#E3EAEB" />
                        <Columns>
                            <asp:BoundField DataField="NumeroAulas" HeaderText="Nº Aulas">
                                <HeaderStyle Width="120px" Wrap="False" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DataInicial" HeaderText="Data Inicial" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DataFinal" HeaderText="Data Final" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ObjetivoEspecifico" HeaderText="Objetivo Esp.">
                                <HeaderStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Conteudo" HeaderText="Conteudo">
                                <HeaderStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Metodo" HeaderText="Método">
                                <HeaderStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" CommandName="edit" ImageUrl="~/App_Themes/icones/edit.png"
                                        ToolTip="Editar" CausesValidation="False" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgExcluir" runat="server" CommandName="delete" ImageUrl="~/App_Themes/icones/delete.png"
                                        ToolTip="Excluir" OnClientClick="javascript:return confirm('Deseja realmente excluir?')"
                                        CausesValidation="False" />
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
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:Mensagem ID="Mensagem1" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <span class="style3">*</span> <span class="style3">Preenchimento obrigatório.</span>
        <span>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </span><span>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Conteudo" />
        </span>
    </div>
</asp:Content>
