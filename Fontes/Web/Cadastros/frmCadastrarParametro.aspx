<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarParametro.aspx.cs" Inherits="Web.Cadastros.frmCadastrarParametro"
    Title="Cadastro de Parâmetros" %>
<%@ Register Src="../botao.ascx" TagName="botao1" TagPrefix="uc1" %>
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
            width: 90px;
        }
        .style3
        {
            text-align: right;
            width: 60px;
        }
        .style7
        {
            text-align: right;
            width: 59px;
        }
        .style10
        {
            text-align: right;
            width: 60px;
            font-weight: bold;
            font-size: xx-small;
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao1 ID="botao1" runat="server" />
        <uc2:Mensagem ID="Mensagem" runat="server" />
    </div>
    <div id="divFormulario">
        <table width="78%">
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>Instituição</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtNomeInstituicao" runat="server" Width="395px" 
                        MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNomeInstituicao" runat="server" ControlToValidate="txtNomeInstituicao"
                        Display="Dynamic" ErrorMessage="Informe o campo: Nome Instituição">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>Telefone</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtTelefone" runat="server" onkeyup="formataTelefone(this,event);"
                       MaxLength="14" Width="395px" AutoCompleteType="Disabled" 
                        AutoPostBack="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTelefone" runat="server" ControlToValidate="txtTelefone"
                        Display="Dynamic" ErrorMessage="Informe o campo: Telefone">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <fieldset>
            <legend>Endereço</legend>
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
                                        ClearMaskOnLostFocus="False" Mask="99999-999" MaskType="Number" TargetControlID="txtCep">
                                    </cc1:MaskedEditExtender>
                                    <asp:RequiredFieldValidator ID="rfvCep" runat="server" ControlToValidate="txtCep"
                                        Display="Dynamic" ErrorMessage="Informe o Campo: CEP.">*</asp:RequiredFieldValidator>
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
                            <asp:TextBox ID="txtNumero" runat="server" Width="60px" MaxLength="5" onkeyup="formataInteiro(this,event);"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNumeroEndereco" runat="server" ControlToValidate="txtNumero"
                                Display="Dynamic" ErrorMessage="Informe o Campo: Número.">* </asp:RequiredFieldValidator>
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
        </fieldset>
        <table width="78%">
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>E-mail</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="395px"></asp:TextBox>
                </td>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="E-mail inválido!"
                    ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ErrorMessage="Informe o campo: E-mail">*</asp:RequiredFieldValidator>
            </tr>
            <tr>
                <td class="style2">
                    CNAE:
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoCnae" runat="server" MaxLength="9" Width="395px" 
                     AutoCompleteType="Disabled"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="meeCNAE" runat="server"
                      AutoComplete="False" ClearMaskOnLostFocus="False" CultureName="en-US" Mask="9999-9/99"
                      TargetControlID="txtCodigoCnae">
                    </cc1:MaskedEditExtender>
                                    
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>CNPJ</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtCnpj" runat="server" MaxLength="15" 
                        Width="395px" AutoCompleteType="Disabled"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="metCNPJ" runat="server"
                      AutoComplete="False" ClearMaskOnLostFocus="False" CultureName="en-US" Mask="99.999.999/9999-99"
                      TargetControlID="txtCnpj">
                    </cc1:MaskedEditExtender>
                    <asp:CompareValidator ID="cvlCNPJ" runat="server" ControlToValidate="txtCnpj" 
                            ErrorMessage="Informe o campo: CNPJ." Operator="NotEqual" ValueToCompare="__.___.___/____-__"> </asp:CompareValidator>
                     
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <b><span class="style1">*</span>Máximo Dias Atend.</b>:
                </td>
                <td>
                    <asp:TextBox ID="txtMaxDiasAtendimento" runat="server" MaxLength="3" Width="395px" onkeyup="formataInteiro(this,event);"
                     AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMaxDiasAtendimento" runat="server" ControlToValidate="txtMaxDiasAtendimento"
                        Display="Dynamic" ErrorMessage="Informe o campo: Máximo Dias de Atendimento">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
            <td class="style2">
                    <b><span class="style1">*</span>Termo de Matrícula</b>:
                </td>
            <td>
            <asp:TextBox ID="txtTermo" runat="server" TextMode="MultiLine" Width="395px" 
            Height="305px" ></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvTermo" runat="server" ControlToValidate="txtTermo"
                        Display="Dynamic" ErrorMessage="Informe o campo: Termo de Matrícula">*</asp:RequiredFieldValidator>
            </td>
            </tr>
           </table>    
           <span class="style3"></span> <span class="style10">*Preenchimento obrigatório.</span>
    </div>
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
                    <uc1:botao1 ID="BarraBotaoFiltro" runat="server" />
                </div>
                <div id="Filtros">
                    <table>
                        <tr>
                            <td class="style2">
                                Logradouro:
                            </td>
                            <td>
                                <asp:TextBox ID="txtLogradouroFiltro" runat="server" Width="470" MaxLength="80"></asp:TextBox>
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
                        AllowSorting="True" PageSize="10" DataKeyNames="cep" OnRowEditing="gdvEnderecoFiltro_RowEditing">
                        <RowStyle BackColor="#E3EAEB" />
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
    <cc1:modalpopupextender ID="ConsultaEnderecosModal" runat="server" TargetControlID="btnEndereco"
        PopupControlID="pnlConsultaEndereco" BackgroundCssClass="modalBackground" BehaviorID="ConsultaEnderecosModal"
        DropShadow="True">
    </cc1:modalpopupextender>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
     ShowSummary="False" />

</asp:Content>
