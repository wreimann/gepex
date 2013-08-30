<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmCadastrarAtendimentoAluno.aspx.cs" Inherits="Web.Cadastros.frmCadastrarAtendimentoAluno"
    Title="Atendimento ao Aluno" %>

<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
        .style2
        {
            text-align: right;
            width: 117px;
        }
        .style100
        {
            color: #FF0000;
            font-size: xx-small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
         <uc2:Mensagem ID="Mensagem1" runat="server" />
    </div>
    <div id="divFormulario">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtNome" />
            </Triggers>
            <ContentTemplate>
                <table style="width: 448px">
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                            <asp:HiddenField ID="hfdNome" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <b><span class="style1">*</span>Nome</b>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNome" runat="server" Width="300px" AutoCompleteType="Disabled"
                                AutoPostBack="True" OnTextChanged="txtNome_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="acePessoa" runat="server" ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx"
                                TargetControlID="txtNome" ServiceMethod="ListaAluno" MinimumPrefixLength="1"
                                CompletionInterval="400" FirstRowSelected="True" CompletionSetCount="10">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" 
                                ControlToValidate="txtNome" Display="Dynamic" 
                                ErrorMessage="Informe o Campo Nome">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <b>Especialidade</b>:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProfissao" runat="server" AppendDataBoundItems="True" 
                                Width="305px" Enabled="False">
                                <asp:ListItem Value="0">(--Selecione--)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="color: Red">
                            <asp:CompareValidator ID="cpvProfissao" runat="server" ControlToValidate="ddlProfissao"
                                Display="Dynamic" ErrorMessage="Selecione o Campo Profissão" Operator="NotEqual"
                                ValueToCompare="0">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <b><span class="style1">*</span>Data/Hora Inicial</b>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataInicial" runat="server" Width="160px"
                                MaxLength="16"></asp:TextBox>
                             <cc1:MaskedEditExtender ID="txtDataIni_MaskedEditExtender" runat="server" 
                                AutoComplete="False" ClearMaskOnLostFocus="False" Mask="99/99/9999 99:99" 
                                MaskType="DateTime" TargetControlID="txtDataInicial">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="color: Red">
                        <asp:CompareValidator ID="cvlDataIni" runat="server" ControlToValidate="txtDataInicial"
                                Display="Dynamic" ErrorMessage="Selecione o campo: Data/Hora Inicial." Operator="NotEqual"
                                ValueToCompare="__/__/____ __:__">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <b><span class="style1">*</span>Data/Hora Final</b>:&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtDataFinal" runat="server" MaxLength="16" 
                                Width="160px"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                AutoComplete="False" ClearMaskOnLostFocus="False" Mask="99/99/9999 99:99" 
                                MaskType="DateTime" TargetControlID="txtDataFinal">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="color: Red">
                            <asp:CompareValidator ID="cvlDataFinal" runat="server" ControlToValidate="txtDataFinal"
                                Display="Dynamic" ErrorMessage="Selecione o campo: Data/Hora Final." Operator="NotEqual"
                                ValueToCompare="__/__/____ __:__">*</asp:CompareValidator>
                        </td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" valign="top">
                            <b><span class="style1">*</span>Atendimento</b>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAtendimento" runat="server" TextMode="MultiLine" 
                                Width="305px" Height="198px"></asp:TextBox>
                        </td>
                        <td style="color: Red">
                            <asp:RequiredFieldValidator ID="rfvAtendimento" runat="server" Display="Dynamic"
                                ErrorMessage="Informe o Campo Atendimento" ControlToValidate="txtAtendimento">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <span class="style100">*Preenchimento obrigatório.</span>
    </div>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
</asp:Content>
