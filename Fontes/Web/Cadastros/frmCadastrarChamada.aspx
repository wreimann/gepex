<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true" 
    CodeBehind="frmCadastrarChamada.aspx.cs" Inherits="Web.Cadastros.frmCadastrarChamada" 
    Title="Cadastro de Chamadas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../botao.ascx" tagname="botao" tagprefix="uc1" %>
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
            width:40pt;
        }
       </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="divBarraBotao">
    <uc1:botao ID="botao1" runat="server" />
    <uc2:Mensagem ID="Mensagem" runat="server" />
</div>
<div id="divFormulario">
<table id="tblTurma">
<tr>
<td class="style2"><span class="style1">*</span><b>Turma</b>:</td>
<td>
    <asp:TextBox ID="txtTurma" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
</td>
</tr>
</table>
<table>
<tr>
<td class="style2"><span class="style1">*</span><b>Data</b>:</td>
<td>
<asp:TextBox ID="txtData" runat="server" MaxLength="10" 
        ontextchanged="txtData_TextChanged" AutoPostBack="True"></asp:TextBox>
<cc1:MaskedEditExtender ID="txtData_MaskedEditExtender" 
        runat="server" ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" 
        TargetControlID="txtData" UserDateFormat="DayMonthYear">
    </cc1:MaskedEditExtender>
</td>
<td>
    <asp:ImageButton ID="btnCalendario" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/icones/calendar.png" />
    <cc1:CalendarExtender ID="cldCalendario" runat="server" 
        PopupButtonID="btnCalendario" TargetControlID="txtData" Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
    <asp:CompareValidator ID="cvlData" runat="server" ControlToValidate="txtData" 
        Display="Dynamic" ErrorMessage="Informe o campo: Data." Operator="NotEqual" 
        ValueToCompare="__/__/____">*</asp:CompareValidator>
</td>
</tr>
</table>
<asp:UpdatePanel  ID="upnAluno" UpdateMode="Conditional" runat="server">
<Triggers>
<asp:AsyncPostBackTrigger ControlID="txtData" />
</Triggers>
<ContentTemplate>
<asp:Panel runat="server" ID="pnlAluno">
<table id="tblAluno">
<tr>
<td>
    <asp:GridView ID="gdvAluno" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="codigo" Visible="False" />
            <asp:BoundField DataField="aluno" HeaderText="Aluno">
                <HeaderStyle Width="380px" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkPresenca" runat="server" />
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
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<span><span class="style1">*Preenchimento obrigatório</span>.</span>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
ShowMessageBox="True" ShowSummary="False" />
</asp:Content>
