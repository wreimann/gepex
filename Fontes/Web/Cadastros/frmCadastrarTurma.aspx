<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
        CodeBehind="frmCadastrarTurma.aspx.cs" Inherits="Web.Cadastros.frmCadastrarTurma" 
        Title="Cadastro de Turma" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
            width:130px;
        }
        .style3
        {
            color: #FF0000;
        }
        .style4
        {
            width: 39px;
        }
        .style5
        {
            text-align: right;
            width: 144px;
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
            <td class="style1"><b><span class="style3">*</span>Série</b>:</td>
            <td><asp:TextBox ID="txtSerie" runat="server" Width="60px" MaxLength="4"></asp:TextBox></td>
             <td><asp:RequiredFieldValidator ID="rfvSerie" runat="server" 
                                    ControlToValidate="txtSerie" Display="Dynamic" 
                                    ErrorMessage="Informe o campo: Série">*</asp:RequiredFieldValidator>
            </td>
            <td class="style5">
                <b><span class="style3">*</span>Turma</b>:</td>
            <td><asp:TextBox ID="txtTurma" runat="server" MaxLength ="4" Width="60px"></asp:TextBox></td>
             <td><asp:RequiredFieldValidator ID="rfvTurma" runat="server" 
                   ControlToValidate="txtTurma" Display="Dynamic" 
                   ErrorMessage="Informe o campo: Turma">*</asp:RequiredFieldValidator>
            </td>
           </tr>
         </table>
         <table>
        <tr>
            <td class="style1"><b><span class="style3">*</span>Ensino</b>:</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlEnsino" runat="server" AppendDataBoundItems="True" 
                 Width="277px">
                    <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                    <asp:ListItem Value="P">Ensino Profissionalizante</asp:ListItem>
                </asp:DropDownList>          
            </td>
        </tr>
        <tr>
            <td class="style1"><b><span class="style3">*</span>Período</b>:</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPeriodo" runat="server" AppendDataBoundItems="True" 
                    Width="277px">
                    <asp:ListItem Value="M">Manhã</asp:ListItem>
                    <asp:ListItem Value="T">Tarde</asp:ListItem>
                    <asp:ListItem Value="I">Integral</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        </table>
        <table>
        <tr>
            <td class="style1"><b><span class="style3">*</span>Máximo de Alunos</b>:</td>
            <td class="style4">
                <asp:TextBox ID="txtNumMaxAlunos" runat="server" MaxLength="2" width="60px"></asp:TextBox> 
                <cc1:filteredtextboxextender ID="txtNumMaxAlunos_FilteredTextBoxExtender" 
                    runat="server" FilterType="Numbers" TargetControlID="txtNumMaxAlunos">
                </cc1:filteredtextboxextender>
            </td>
            <td>    <asp:RequiredFieldValidator ID="rfvNumMaximoalunos" runat="server" 
                    ControlToValidate="txtNumMaxAlunos" Display="Dynamic" 
                    ErrorMessage="Informe o campo: Nº Maximo de Alunos"> * </asp:RequiredFieldValidator>
            </td>
            <td class="style5">Sala:</td>
            <td><asp:TextBox ID="txtSala" runat="server" MaxLength="4" Width="60px"></asp:TextBox>
                <cc1:filteredtextboxextender ID="txtSala_FilteredTextBoxExtender" 
                    runat="server" FilterType="Numbers" TargetControlID="txtSala">
                </cc1:filteredtextboxextender>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <b><span class="style3">*</span>Ano Nasc. Mínimo</b>:</td>
            <td class="style4"><asp:TextBox ID="txtIdadeInicial" runat="server" Width="60px" MaxLength="4"></asp:TextBox>
                <cc1:filteredtextboxextender ID="txtIdadeInicial_FilteredTextBoxExtender" 
                    runat="server" FilterType="Numbers" TargetControlID="txtIdadeInicial">
                </cc1:filteredtextboxextender>
            </td>
            <td><asp:RequiredFieldValidator ID="rfvIdadeInicial" runat="server" 
                    ControlToValidate="txtIdadeInicial" Display="Dynamic" 
                    ErrorMessage="Informe o campo: Ano Nasc. Mínimo">*</asp:RequiredFieldValidator>
            </td>
            <td class="style5">Ano Nasc. Máximo:</td>
            <td><asp:TextBox ID="txtIdadeFinal" runat="server" Width="60px" MaxLength="4"></asp:TextBox>
                <cc1:filteredtextboxextender ID="txtIdadeFinal_FilteredTextBoxExtender" 
                    runat="server" FilterType="Numbers" TargetControlID="txtIdadeFinal">
                </cc1:filteredtextboxextender>
            </td>
        </tr>
        <tr>
            <td class="style1"><b><span class="style3">*</span>Ano Letivo</b>:</td>
            <td>
                <asp:TextBox ID="txtAnoLetivo" runat="server" MaxLength ="4" Width="60px"></asp:TextBox>
                <cc1:filteredtextboxextender ID="txtAnoLetivo_FilteredTextBoxExtender" 
                    runat="server" FilterType="Numbers" TargetControlID="txtAnoLetivo">
                </cc1:filteredtextboxextender>
            </td>
            <td> <asp:RequiredFieldValidator ID="rfvAnoLetivo" runat="server" 
                    ControlToValidate="txtAnoLetivo" Display="Dynamic" 
                    ErrorMessage="Informe o campo: Ano Letivo">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        </table>
        <table>
        <tr>
            <td class="style1">Situação:</td>
            <td colspan="3">
                <asp:RadioButtonList ID="rdlSituacao" runat="server" 
                    RepeatDirection="Horizontal" Width="133px" Enabled="False">
                    <asp:ListItem Value="A">Ativo</asp:ListItem>
                    <asp:ListItem Value="I">Inativo</asp:ListItem>
                    <asp:ListItem Value="F">Finalizada</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="style1">Observação:</td>
            <td colspan="3">
                <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" 
                    Width="278px" Height="75px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
             
            </td>
            <td colspan="3">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" Width="240px" />
            </td>
            
        </tr>
        </table>
        <span class="style3">*</span> <span class="style3">Preenchimento obrigatório.</span>
 
</div>
</asp:Content>
