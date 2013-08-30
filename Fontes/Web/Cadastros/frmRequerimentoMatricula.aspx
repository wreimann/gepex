<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmRequerimentoMatricula.aspx.cs" Inherits="Web.Cadastros.frmRequerimentoMatricula"
    Title="Requerimento de Matrícula" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
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
            width:73pt;
        }
        .style3
        {
            text-align: right;
            width:60pt;
        }
         .style6
        {
            font-size: 7pt;
        }
        .style7
        {
            text-align: right;
            width: 59px;
        }
        .style8
        {
            font-weight: bold;
            color: #FF0000;
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
<td class="style2"><span class="style1">*</span><b>Nome</b>:</td>
<td><asp:TextBox ID="txtNome" runat="server" Width="383px" MaxLength="80" 
        ReadOnly="True" BackColor="#EEEEEE"></asp:TextBox></td>
</tr>
</table>
<table>
<tr>
<td class="style2">Telefone:</td>
<td><asp:TextBox ID="txtTelefone" runat="server" 
        onkeyup="formataTelefone(this,event);" MaxLength="14" Width="110px"></asp:TextBox></td>
</tr>
<tr>
<td class="style2"><span class="style1">*</span><b>Emergência</b>:</td>
<td><asp:TextBox ID="txtEmergencia" runat="server" onkeyup="formataTelefone(this,event);" MaxLength="14" Width="110px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmergencia" runat="server" 
        ControlToValidate="txtEmergencia" Display="Dynamic" 
        ErrorMessage="Informe o Campo Emergencia">*</asp:RequiredFieldValidator>
    </td>
<td class="style3"><span class="style1">*</span><b>Falar com</b>:</td>
<td><asp:TextBox ID="txtFalarCom" runat="server" Width="183px" MaxLength="20"></asp:TextBox></td>
<td> <asp:RequiredFieldValidator ID="rfvFalarCom" runat="server" 
        ControlToValidate="txtFalarCom" Display="Dynamic" 
        ErrorMessage="Informe o Campo: Falar Com.">*</asp:RequiredFieldValidator></td>
</tr>

</table>
<table>
<tr>
<td class="style2">E-mail:</td>
<td><asp:TextBox ID="txtEmail" runat="server" Width="383px" MaxLength="80"></asp:TextBox></td>
<td>
<asp:RegularExpressionValidator
        ID="revEmail" runat="server" ErrorMessage="E-mail inválido!" 
        ControlToValidate="txtEmail" Display="Dynamic" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
</td>
</tr>
</table>
<fieldset>
<legend>Informações Adicionais</legend>
<table>
<tr>
<td class="style2"><asp:CheckBox ID="cbxSites" runat="server" Text="Sites"/></td>
<td class="style6">&nbsp;(Sistema Integrado de Transporte para o Ensino Especial)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
<td><asp:CheckBox ID="cbxMedicar" runat="server" Text="Medicar na Escola"/></td>
</tr>
</table>
<table>
<tr>
<td class="style2">Medicamentos:</td>
<td><asp:TextBox ID="txtMedicamentos" runat="server" TextMode="MultiLine" 
        Height="65px" Width="380px"></asp:TextBox></td>
</tr>
<tr>
<td class="style2">Alergias:</td>
<td><asp:TextBox ID="txtAlergia" runat="server" TextMode="MultiLine" 
        Height="65px" Width="380px"></asp:TextBox></td>
</tr>
</table>
<table>
<tr>
<td class="style2">Convênio Médico:</td>
<td><asp:TextBox ID="txtConvenioMedico" runat="server" Width="380px" MaxLength="70"></asp:TextBox></td>
</tr>
</table>
<table>
<tr>
<td class="style2">Telefone:</td>
<td><asp:TextBox ID="txtTelefoneMedico" runat="server" 
        onkeyup="formataTelefone(this,event);" MaxLength="14" Width="110px"></asp:TextBox></td>
<td class="style3">Carteirinha:</td>
<td><asp:TextBox ID="txtCarteirinha" runat="server" Width="187px" MaxLength="20"></asp:TextBox></td>
</tr>
</table>
</fieldset>
<fieldset>
<legend>Endereço</legend>
<asp:Panel ID="pnlEndereco" runat="server">
<table>
<tr>
<td class="style2">
    <span class="style1">*</span><b>CEP</b>:</td>
<td>
<asp:UpdatePanel ID="upnCEP" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:TextBox ID="txtCep" runat="server" Width="110px" 
        ontextchanged="txtCep_TextChanged" MaxLength="9" AutoPostBack="True" 
        AutoCompleteType="Disabled" ></asp:TextBox>
    <cc1:MaskedEditExtender ID="txtCep_MaskedEditExtender" runat="server" 
        AutoComplete="False" ClearMaskOnLostFocus="False" Mask="99999-999" 
        MaskType="Number" TargetControlID="txtCep">
    </cc1:MaskedEditExtender>
    <asp:RequiredFieldValidator ID="rfvCep" runat="server" 
        ControlToValidate="txtCep" Display="Dynamic" 
        ErrorMessage="Informe o Campo: CEP.">*
</asp:RequiredFieldValidator>
</ContentTemplate>
<Triggers >
<asp:PostBackTrigger ControlID="txtCep"/>
</Triggers>
</asp:UpdatePanel>
</td>
<td>
    <asp:ImageButton ID="btnEndereco" runat="server" BorderStyle="None" 
        ImageUrl="~/App_Themes/icones/search.png" 
        ToolTip="Consulta de Endereço" CausesValidation="False" OnClientClick="ShowModalDialog()" />   
        
    </td>
    <td>
        <asp:RegularExpressionValidator ID="revCEP" runat="server" ErrorMessage="Cep inválido!" Display="Dynamic" ControlToValidate="txtCep" ValidationExpression="^\d{5}-\d{3}$"></asp:RegularExpressionValidator>
    </td>
</tr>
</table>
<table>
<tr>
<td class="style2">Logradouro:</td>
<td><asp:TextBox ID="txtLogradouro" runat="server" Width="260px" ReadOnly="True" 
        BackColor="#EEEEEE" MaxLength="100"></asp:TextBox></td>
<td class="style7"><span class="style1">*</span><b>Número</b>:</td> 
<td><asp:TextBox ID="txtNumero" runat="server" Width="60px" 
        onkeyup="formataInteiro(this,event);" MaxLength="5"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNumeroEndereco" runat="server" 
        ControlToValidate="txtNumero" Display="Dynamic" 
        ErrorMessage="Informe o Campo: Número.">*
    </asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td class="style2">Complemento:</td>
<td><asp:TextBox ID="txtComplemento" runat="server" Width="260px" MaxLength="40"></asp:TextBox></td>
</tr>
<tr>
<td class="style2">Bairro:</td>
<td><asp:TextBox ID="txtBairro" runat="server" Width="260px" ReadOnly="True" 
        BackColor="#EEEEEE" MaxLength="50"></asp:TextBox></td>
</tr>
</table>
<table>
<tr>
<td class="style2">Cidade:</td>
<td><asp:TextBox ID="txtCidade" runat="server" Width="260px" ReadOnly="True" 
        BackColor="#EEEEEE" MaxLength="50"></asp:TextBox></td>
<td  class="style7">UF:</td>
<td><asp:TextBox ID="txtUF" runat="server" Width="60px" ReadOnly="True" 
        BackColor="#EEEEEE" MaxLength="2"></asp:TextBox></td>
</tr>
</table>
</asp:Panel>
</fieldset>
<fieldset>
<legend>Termo de Compromisso e Responsabilidade</legend>
<table width="95%">
<tr>
<td>
<asp:TextBox ID="txtTermo" runat="server" TextMode="MultiLine" Width="100%" 
        Height="305px" ReadOnly="True"></asp:TextBox>
 </td>
</tr>
<tr>
 <td><asp:CheckBox ID="cbxConcorda" runat="server" Text="Li e concordo com os termos da matrícula." 
         style="font-weight: 700" />      
 </td>
</tr>
</table>
</fieldset>
<span class="style8">*Preenchimento obrigatório.</span>
</div>

    <asp:UpdatePanel ID="upnEndereco" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <asp:Panel ID="pnlConsultaEndereco" runat="server" Style="display: none" CssClass="modalPopup">
        <asp:Panel ID="pnlTituloModal" runat="server">
        <div style="width: 100%">
            <h3 id="Cabecalho" style="text-indent: 2%">Consulta de Endereço</h3>
        </div>
        </asp:Panel>
        <div id="BotaoFiltro" style="margin-top: 3px;">
            <uc1:botao ID="BarraBotaoFiltro" runat="server"/>
        </div>
        <div id="Filtros">
            <table width="100%">
             <tr>
                <td class="style2">Logradouro:</td>
                <td><asp:TextBox ID="txtLogradouroFiltro" runat="server" Width="470" MaxLength="80"></asp:TextBox>
                </td>
             </tr>
             <tr>
                <td class="style2">Bairro:</td>
                <td><asp:TextBox ID="txtBairroFiltro" runat="server" Width="300" MaxLength="40"></asp:TextBox></td>
             </tr>
             <tr>
                <td class="style2">Cidade:</td>
                <td><asp:TextBox ID="txtCidadeFiltro" Width="300" MaxLength="40" runat="server"></asp:TextBox></td>
             </tr>
             <tr>
                <td class="style2">UF:</td>
                <td><asp:DropDownList ID="ddlUFFiltro" runat="server" AppendDataBoundItems="True" Width="60px">
                    </asp:DropDownList>
                </td>
                
             </tr>
            </table>
        </div>
        <div id="GirdResultados" style="margin-top: 3px;">
             <asp:GridView ID="gdvEnderecoFiltro" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="None" 
                EnableTheming="True" 
                AllowPaging="True" AllowSorting="True" PageSize="10" DataKeyNames="cep" 
                 onrowediting="gdvEnderecoFiltro_RowEditing">
                <RowStyle BackColor="#E3EAEB" />
                <Columns>
                    <asp:BoundField DataField="cep" HeaderText="CEP" SortExpression="CEP">
                        <HeaderStyle Width="75px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="logradouro" HeaderText="Logradouro" SortExpression="Logradouro">
                        <HeaderStyle Width="210px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="bairro" HeaderText="Bairro" SortExpression="Bairro">
                        <HeaderStyle Width="110px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="Cidade">
                        <HeaderStyle Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="UF">
                        <HeaderStyle Width="30px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                          <asp:ImageButton ID="imgSelecionar" runat="server" CommandName="edit" 
                                ImageUrl="~/App_Themes/icones/zoom.png" ToolTip="Selecionar" 
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
        </div>
    </asp:Panel>    
    </ContentTemplate> 
    <Triggers>
    <asp:PostBackTrigger ControlID="BarraBotaoFiltro" />
    <asp:PostBackTrigger ControlID="gdvEnderecoFiltro" />
    </Triggers>
    </asp:UpdatePanel>
    
<cc1:modalpopupextender ID="ConsultaEnderecosModal" runat="server"
            TargetControlID="btnEndereco"
            PopupControlID="pnlConsultaEndereco" 
            BackgroundCssClass="modalBackground" 
            BehaviorID="ConsultaEnderecosModal" DropShadow="True">
</cc1:modalpopupextender>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" 
                        ShowSummary="False" />
</asp:Content>
