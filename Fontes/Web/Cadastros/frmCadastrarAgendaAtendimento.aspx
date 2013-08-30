<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarAgendaAtendimento.aspx.cs" Inherits="Web.Cadastros.frmCadastrarAgendaAtendimento"
    Title="Cadastro de Agenda de Atendimento" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function ShowModalDialog() {
            var x = $find("ModalPopupExtender1");
            x.show(); 

        }
    </script>

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
        .style4
        {
            width: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
        <uc2:Mensagem ID="Mensagem1" runat="server" />
    </div>
    <div id="divFormulario">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" 
            UpdateMode="Conditional">
        <Triggers>
        <asp:PostBackTrigger ControlID="txtData" />
        <asp:PostBackTrigger ControlID="ddlAluno" /> 
        </Triggers>
            <ContentTemplate>
                <fieldset>
                    <table>
                        <tr>
                            <td class="style1">
                                <b><span class="style3">*</span>Funcionário</b>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFuncionario" runat="server" Width="300px" AppendDataBoundItems="True"
                                    AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlFuncionario_SelectedIndexChanged">
                                    <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                <asp:CompareValidator ID="cpvFuncionario" runat="server" ControlToValidate="ddlFuncionario"
                                    Display="Dynamic" ErrorMessage="Selecione o Campo Funcionario" Operator="NotEqual"
                                    ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <b><span class="style3">*</span>Especialidade</b>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProfissao" runat="server" Width="300px" 
                                    AppendDataBoundItems="True" Enabled="False">
                                    <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                <asp:CompareValidator ID="cpvProfissao" runat="server" ControlToValidate="ddlProfissao"
                                    Display="Dynamic" ErrorMessage="Selecione o Campo Profissão" Operator="NotEqual"
                                    ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <b><span class="style3">*</span>Data</b>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtData" runat="server" onkeyup="formataData(this,event);" MaxLength="10"
                                    Width="99px" AutoPostBack="True" ontextchanged="txtData_TextChanged"></asp:TextBox>
                                <asp:ImageButton ID="btnCalendario" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/icones/calendar.png" />
                            </td>
                            <td class="style4">
                                <cc1:CalendarExtender ID="cldCalendario" runat="server" PopupButtonID="btnCalendario"
                                    TargetControlID="txtData" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rqfData" runat="server" ControlToValidate="txtData"
                                    Display="Dynamic" ErrorMessage="Informe o Campo Data">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table>
                    <tr>
                        <td class="style1">
                            <b><span class="style3">*</span>Horário Inicial</b>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtHorarioInicial" runat="server" Width="100px" onkeyup="formataHora(this,event);"
                                MaxLength="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfHorarioInicial" runat="server" ControlToValidate="txtHorarioInicial"
                                Display="Dynamic" ErrorMessage="Informe o Campo Horario Inicial">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <b><span class="style3">*</span>Horário Final</b>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtHorarioFinal" runat="server" Width="100px" onkeyup="formataHora(this,event);"
                                MaxLength="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfHorarioFinal" runat="server" ControlToValidate="txtHorarioFinal"
                                Display="Dynamic" ErrorMessage="Informe o Campo Horario Final">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <b><span class="style3">*</span>Aluno</b>:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAluno" runat="server" Width="350px" AppendDataBoundItems="True"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
                                <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CompareValidator ID="cpvAluno" runat="server" ControlToValidate="ddlAluno" Display="Dynamic"
                                ErrorMessage="Selecione o Campo Aluno" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgListaEspera" runat="server" ImageUrl="~/App_Themes/icones/listaEspera2.png"
                                ToolTip="Lista de Espera" CausesValidation="False" OnClientClick="ShowModalDialog()" />
                        </td>
                    </tr>
                </table>
                <fieldset>
                    <legend>Atendimentos</legend>
                    <table>
                        <tr>
                            <td>
                                Último Atend. da Especialidade:
                            </td>
                            <td>
                                <asp:Label ID="lblUltimoAtendimento" runat="server" Width="150px"></asp:Label>
                            </td>
                            <td>
                                Funcionário:
                            </td>
                            <td>
                                <asp:Label ID="lblFuncionario" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Quantidade Planejada:
                            </td>
                            <td>
                                <asp:Label ID="lblQuantidadePlanejada" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                N° de Atendimento Agendados:
                            </td>
                            <td>
                                <asp:Label ID="lblNumeroAtendimentosPendentes" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Quantidade Realizada:
                            </td>
                            <td>
                                <asp:Label ID="lblQuantidadeRealizada" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Atendimentos Pendentes:
                            </td>
                            <td>
                                <asp:Label ID="lblAtendimentoPrevisto" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <legend>Atendimentos Agendados</legend>
                    <table>
                        <asp:GridView ID="gdvAtendimentos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="600px">
                            <RowStyle BackColor="#E3EAEB" />
                            <Columns>
                                <asp:BoundField DataField="Data" HeaderText="Data" />
                                <asp:BoundField DataField="Horario" HeaderText="Horário" />
                                <asp:BoundField DataField="Funcionario" HeaderText="Funcionário">
                                    <HeaderStyle Width="300px" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                </div>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imgListaEspera"
                    PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader" DropShadow="true"
                    BackgroundCssClass="ModalPopupBG">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel1" Style="display: none" runat="server" CssClass="modalPopup">
                    <div>
                        <table>
                            <tr>
                                <td>Especialidade:</td>
                                <td>
                                    <asp:DropDownList ID="ddlEspecialidades" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Width="300px" 
                                    OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged">
                                    <asp:ListItem Text="(--Selecione--)" Value="0">                           
                                    </asp:ListItem>
                                    </asp:DropDownList>
                                </td>  
                                <td>
                                    <asp:LinkButton ID="lnkVoltar" runat="server" CausesValidation="false" OnClick="lnkVoltar_Click">Voltar</asp:LinkButton>
                                </td>                              
                            </tr>
                        </table>
                        <fieldset>
                        <legend>Lista de Espera</legend>
                        <table>
                            <asp:GridView ID="gdvListaEspera" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanging="gdvListaEspera_SelectedIndexChanging"
                                DataKeyNames="Codigo" CellPadding="4" ForeColor="#333333" GridLines="None" Width="600px">
                                <RowStyle BackColor="#E3EAEB" />
                                <Columns>
                                    <asp:BoundField DataField="Aluno" HeaderText="Aluno" />
                                    <asp:BoundField DataField="QuantidadePlanejada" HeaderText="Quantidade Planejada" />
                                    <asp:BoundField DataField="QuantidadeRealizada" HeaderText="Quantidade Realizada" />
                                    <asp:BoundField DataField="QuantidadePrevista" HeaderText="Quantidade Prevista" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgSelecionar" runat="server" CommandName="select" ImageUrl="~/App_Themes/icones/check.png"
                                                ToolTip="Selecionar" CausesValidation="False" />
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
                        </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
</asp:Content>
