<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarAluno.aspx.cs" Inherits="Web.Cadastros.frmCadastrarAluno"
    Title="Cadastro de Aluno" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Src="../Componente/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function ShowModalDialog() {
            var x = $find("ConsultaEnderecosModal");
            x.show();

        }
    </script>

    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
        .style2
        {
            text-align: right;
            width: 90pt;
        }
        .style3
        {
            text-align: right;
            width: 70pt;
        }
        .style7
        {
            text-align: right;
            width: 59px;
        }
        .style10
        {
            text-align: right;
            width: 39px;
        }
        .style12
        {
            width: 90px;
        }
        .style8
        {
            font-weight: bold;
            color: #FF0000;
            font-size: 7pt;
        }
        .style9
        {
            height: 20px;
        }
        .style100
        {
            font-size: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
        <uc2:Mensagem ID="Mensagem" runat="server" />
    </div>
    <div id="divFormulario">
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" Width="100%"
            Height="320px" ScrollBars="Auto">
            <cc1:TabPanel ID="tpnDadosBasicos" runat="server" HeaderText="Dados Básicos">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="style2">
                                <span class="style1">*</span><b>Nome</b>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNome" runat="server" Width="383px" MaxLength="80"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNome" runat="server" ErrorMessage="Informe o campo: Nome do Aluno."
                                    ControlToValidate="txtNome" Display="Dynamic">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                <span class="style1">*</span><b>Matrícula</b>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMatricula" runat="server" MaxLength="10" Width="110px"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtMatricula_FilteredTextBoxExtender" runat="server"
                                    FilterType="Numbers" TargetControlID="txtMatricula" Enabled="True">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvMatricula" runat="server" ControlToValidate="txtMatricula"
                                    Display="Dynamic" ErrorMessage="Informe o campo: Matrícula">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="style3">
                                <span class="style1">*</span><b>Situação</b>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSituacao" runat="server" AppendDataBoundItems="True" Width="186px"
                                    Enabled="False">
                                    <asp:ListItem Value="I">Inativo</asp:ListItem>
                                    <asp:ListItem Value="A">Matrícula em Andamento</asp:ListItem>
                                    <asp:ListItem Value="M">Matriculado</asp:ListItem>
                                    <asp:ListItem Value="L">Lista de Espera</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <span class="style1">*</span><b>Sexo</b>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSexo" runat="server" AppendDataBoundItems="True" Width="110px">
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                <span class="style1">*</span><b>Data Nasc.</b>:
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlDataNasc" runat="server">
                                            <asp:TextBox ID="txtDataNascimento" runat="server" MaxLength="10" Width="110px" AutoCompleteType="Disabled"
                                                AutoPostBack="True" OnTextChanged="txtDataNascimento_TextChanged">
                                            </asp:TextBox>
                                            <cc1:MaskedEditExtender ID="txtDataNascimento_MaskedEditExtender" runat="server"
                                                ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataNascimento"
                                                UserDateFormat="DayMonthYear">
                                            </cc1:MaskedEditExtender>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="txtDataNascimento" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvDataNascimento" runat="server" ControlToValidate="txtDataNascimento"
                                    ErrorMessage="Informe o campo: Data de Nascimento.">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="style3">
                                Idade:
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdade" runat="server" MaxLength="3" Width="30px" ReadOnly="True"
                                    BackColor="#EEEEEE" BorderStyle="None"></asp:TextBox>
                            </td>
                            <td>
                                anos
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                <span class="style1">*</span><b>Cor/Raça</b>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCorRaca" runat="server" AppendDataBoundItems="True" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <span class="style1">*</span><b>Estado Civil</b>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEstadoCivil" runat="server" AppendDataBoundItems="True"
                                    Width="200px">
                                    <asp:ListItem Value="S">Solteiro(a)</asp:ListItem>
                                    <asp:ListItem Value="C">Casado(a)</asp:ListItem>
                                    <asp:ListItem Value="V">Viúvo(a)</asp:ListItem>
                                    <asp:ListItem Value="J">Separado(a)</asp:ListItem>
                                    <asp:ListItem Value="D">Divorciado(a)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <span class="style1">*</span><b>Nacionalidade</b>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNacionalidade" runat="server" AppendDataBoundItems="True"
                                    Width="200px">
                                    <asp:ListItem Value="B">Brasileiro(a)</asp:ListItem>
                                    <asp:ListItem Value="E">Estrangeiro(a)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Naturalidade:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNaturalidade" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Telefone:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelefone" runat="server" onkeyup="formataTelefone(this,event);"
                                    MaxLength="14" Width="110px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                <span class="style1">*</span><b>Emergência</b>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmergencia" runat="server" onkeyup="formataTelefone(this,event);"
                                    MaxLength="14" Width="110px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmergencia" runat="server" ControlToValidate="txtEmergencia"
                                    Display="Dynamic" ErrorMessage="Informe o Campo Emergencia">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="style3">
                                <span class="style1">*</span><b>Falar com</b>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFalarCom" runat="server" Width="183px" MaxLength="20"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvFalarCom" runat="server" ControlToValidate="txtFalarCom"
                                    Display="Dynamic" ErrorMessage="Informe o Campo: Falar Com.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                E-mail:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="383px" MaxLength="80"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="E-mail inválido!"
                                    ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Nome do Pai:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPai" runat="server" Width="383px" MaxLength="80"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Nome da Mãe:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMae" runat="server" Width="383px" MaxLength="80"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tpnInfAdicional" runat="server" HeaderText="Informações Adicionais">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="cbxBolsaFamilia" runat="server" Text="Bolsa Família" />
                            </td>
                            <td>
                                Outros benefícios?
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtOutrosBeneficios" runat="server" Width="383px" Height="50px"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="cbxSites" runat="server" Text="SITES" />
                            </td>
                            <td>
                                Outro transporte escolar?
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtOutrosTransportes" runat="server" Width="383px" Height="50px"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                Convênio Médico:
                            </td>
                            <td>
                                <asp:TextBox ID="txtConvenioMedico" runat="server" Width="383px" MaxLength="70"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                Telefone:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelefoneMedico" runat="server" onkeyup="formataTelefone(this,event);"
                                    MaxLength="14" Width="110px"></asp:TextBox>
                            </td>
                            <td class="style3">
                                Carteirinha:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCarteirinha" runat="server" Width="187px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tpnEndereco" runat="server" HeaderText="Endereço">
                <ContentTemplate>
                    <asp:Panel ID="pnlEndereco" runat="server">
                        <table>
                            <tr>
                                <td class="style2">
                                    <span class="style1">*</span><b>CEP</b>:
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnCEP" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCep" runat="server" Width="110px" OnTextChanged="txtCep_TextChanged"
                                                MaxLength="9" AutoPostBack="True" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="txtCep_MaskedEditExtender" runat="server" AutoComplete="False"
                                                ClearMaskOnLostFocus="False" CultureName="en-US" Mask="99999-999" MaskType="Number"
                                                TargetControlID="txtCep">
                                            </cc1:MaskedEditExtender>
                                            <asp:RequiredFieldValidator ID="rfvCep" runat="server" ControlToValidate="txtCep"
                                                Display="Dynamic" ErrorMessage="Informe o Campo: CEP.">*
                                            </asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="txtCep" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnEndereco" runat="server" BorderStyle="None" ImageUrl="~/App_Themes/icones/search.png"
                                        ToolTip="Consulta de Endereço" CausesValidation="False" OnClientClick="ShowModalDialog()" />
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="revCEP" runat="server" ErrorMessage="Cep inválido!"
                                        Display="Dynamic" ControlToValidate="txtCep" ValidationExpression="^\d{5}-\d{3}$"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="style2">
                                    Identif. COPEL:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCopel" runat="server" Width="110px" MaxLength="11"></asp:TextBox>
                                 
                                    <cc1:FilteredTextBoxExtender ID="txtCopel_FilteredTextBoxExtender" 
                                        runat="server" FilterType="Numbers" TargetControlID="txtCopel">
                                    </cc1:FilteredTextBoxExtender>
                                 
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    Logradouro:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLogradouro" runat="server" Width="260px" ReadOnly="True" BackColor="#EEEEEE"
                                        MaxLength="100"></asp:TextBox>
                                </td>
                                <td class="style7">
                                    <span class="style1">*</span><b>Número</b>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumero" runat="server" Width="60px" onkeyup="formataInteiro(this,event);"
                                        MaxLength="5"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNumeroEndereco" runat="server" ControlToValidate="txtNumero"
                                        Display="Dynamic" ErrorMessage="Informe o Campo: Número.">*
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    Complemento:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComplemento" runat="server" Width="260px" MaxLength="40"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    Bairro:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBairro" runat="server" Width="260px" ReadOnly="True" BackColor="#EEEEEE"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="style2">
                                    Cidade:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCidade" runat="server" Width="260px" ReadOnly="True" BackColor="#EEEEEE"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="style7">
                                    UF:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUF" runat="server" Width="60px" ReadOnly="True" BackColor="#EEEEEE"
                                        MaxLength="2"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:UpdatePanel ID="upnEndereco" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlConsultaEndereco" runat="server" Style="display: none" CssClass="modalPopup">
                                <asp:Panel ID="pnlTituloModal" runat="server">
                                    <div style="width: 100%">
                                        <h3 id="Cabecalho" style="text-indent: 2%">
                                            Consulta de Endereço</h3>
                                    </div>
                                </asp:Panel>
                                <div id="BotaoFiltro" style="margin-top: 3px;">
                                    <uc1:botao ID="BarraBotaoFiltro" runat="server" />
                                </div>
                                <div id="Filtros">
                                    <table width="100%">
                                        <tr>
                                            <td class="style2">
                                                <span class="style1">*</span><b>Logradouro</b>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLogradouroFiltro" runat="server" Width="470" MaxLength="80"></asp:TextBox>
                                            </td>
                                            <td>
                                                *
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                Bairro:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBairroFiltro" runat="server" Width="300" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                Cidade:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCidadeFiltro" Width="300" MaxLength="40" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                UF:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUFFiltro" runat="server" AppendDataBoundItems="True" Width="60px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="GirdResultados" style="margin-top: 3px;">
                                    <asp:GridView ID="gdvEnderecoFiltro" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" EnableTheming="True" AllowPaging="True"
                                        AllowSorting="True" PageSize="10" DataKeyNames="cep" OnRowEditing="gdvEnderecoFiltro_RowEditing"
                                        CssClass="Tabela">
                                        <RowStyle BackColor="#E3EAEB" Wrap="False" />
                                        <Columns>
                                            <asp:BoundField DataField="cep" HeaderText="CEP" SortExpression="CEP">
                                                <HeaderStyle Width="75px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="logradouro" HeaderText="Logradouro" SortExpression="Logradouro">
                                                <HeaderStyle Width="210px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="bairro" HeaderText="Bairro" SortExpression="Bairro">
                                                <HeaderStyle Width="110px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="Cidade">
                                                <HeaderStyle Width="130px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="UF">
                                                <HeaderStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgSelecionar" runat="server" CommandName="edit" ImageUrl="~/App_Themes/icones/zoom.png"
                                                        ToolTip="Selecionar" CausesValidation="False" />
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
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BarraBotaoFiltro" />
                            <asp:PostBackTrigger ControlID="gdvEnderecoFiltro" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <cc1:ModalPopupExtender ID="ConsultaEnderecosModal" runat="server" TargetControlID="btnEndereco"
                        PopupControlID="pnlConsultaEndereco" BackgroundCssClass="modalBackground" BehaviorID="ConsultaEnderecosModal"
                        DropShadow="True" DynamicServicePath="" Enabled="True">
                    </cc1:ModalPopupExtender>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tpnDocumentos" runat="server" HeaderText="Documentos">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="style2">
                                Tipo Documento:
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlTipoDocumento" runat="server">
                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="True"
                                                Width="260px" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                                                <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="ddlTipoDocumento" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Número:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroTipoDocumento" runat="server" Width="260px" MaxLength="35"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="txtNumeroTipoDocumento_MaskedEditExtender" runat="server"
                                    AutoComplete="False" ClearMaskOnLostFocus="False" CultureName="en-US" Mask="999.999.999-99"
                                    TargetControlID="txtNumeroTipoDocumento" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="$"
                                    CultureDateFormat="MDY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                    CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                </cc1:MaskedEditExtender>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                Orgão Emissor:
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrgaoEmissor" runat="server" Width="90px" MaxLength="20"></asp:TextBox>
                            </td>
                            <td class="style10">
                                UF:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUF" runat="server" AppendDataBoundItems="True" Width="47px">
                                </asp:DropDownList>
                            </td>
                            <td class="style2">
                                Data Expedição:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataExpedicao" runat="server" MaxLength="10" Width="90px" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="meeDataExpedicao" runat="server" ClearMaskOnLostFocus="False"
                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataExpedicao" UserDateFormat="DayMonthYear"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True">
                                </cc1:MaskedEditExtender>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style2">
                                Inf. Adicional:
                            </td>
                            <td>
                                <asp:TextBox ID="txtInfAdicional" runat="server" Width="260px" MaxLength="60"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgAdicionar" runat="server" ImageUrl="~/App_Themes/icones/add.png"
                                    ToolTip="Incluir Documento" CausesValidation="False" OnClick="imgAdicionar_Click" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="gdvTipoDocumento" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" OnRowDeleting="gdvTipoDocumento_RowDeleting"
                                    DataKeyNames="codigoDocumento" Width="680px" CssClass="Tabela">
                                    <RowStyle BackColor="#E3EAEB" Wrap="False" />
                                    <Columns>
                                        <asp:BoundField DataField="codigoDocumento" Visible="False" />
                                        <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo Documento">
                                            <HeaderStyle Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero" HeaderText="Número">
                                            <HeaderStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="orgao" HeaderText="Org. Emissor">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uf" HeaderText="UF">
                                            <HeaderStyle Width="35px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dataEmissao" HeaderText="Dt. Emissão">
                                            <HeaderStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="infAdicional" HeaderText="Inf. Adicional">
                                            <HeaderStyle Width="115px" />
                                        </asp:BoundField>
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
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tpnProntuario" runat="server" HeaderText="Prontuário">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upnIMC" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td class="style2">
                                        Altura:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAltura" runat="server" MaxLength="3" Width="90px" OnTextChanged="txtAltura_TextChanged"
                                            AutoPostBack="True"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9.99" TargetControlID="txtAltura"
                                            ClearMaskOnLostFocus="False" Enabled="True" MaskType="Number">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td class="style3">
                                        Peso:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPeso" runat="server" onkeyup="formataDouble(this,event);" MaxLength="6"
                                            Width="90px" OnTextChanged="txtPeso_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="style7">
                                        IMC:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIMC" runat="server" Width="50px" ReadOnly="True" BackColor="#EEEEEE"
                                            BorderStyle="None"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="txtAltura" />
                            <asp:PostBackTrigger ControlID="txtPeso" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <table>
                        <tr>
                            <td class="style2">
                                Tipo Sanguíneo:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoSanguineo" runat="server" AppendDataBoundItems="True"
                                    Width="90px" MaxLength="2">
                                    <asp:ListItem Value="0">(Selecione)</asp:ListItem>
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                    <asp:ListItem>O</asp:ListItem>
                                    <asp:ListItem>AB</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style3">
                                Fator RH:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFatorRH" runat="server" AppendDataBoundItems="True" Width="90px">
                                    <asp:ListItem Value="0">(Selecione)</asp:ListItem>
                                    <asp:ListItem>+</asp:ListItem>
                                    <asp:ListItem>-</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="95%">
                        <tr>
                            <td class="style9">
                                Alergias:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAlergia" runat="server" TextMode="MultiLine" Width="99%" Height="65px"
                                    AutoPostBack="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="cbxMedicar" runat="server" Text="Medicar na Escola" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Medicamentos:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtMedicamentos" runat="server" TextMode="MultiLine" Width="99%"
                                    Height="65px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <fieldset>
            <legend>Observações</legend>
            <table width="95%">
                <tr>
                    <td>
                        <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Width="100%"
                            Height="65px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <span class="style8">*Preenchimento obrigatório.</span>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </div>
</asp:Content>
