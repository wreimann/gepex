<%@ Page Language="C#" MasterPageFile="~/Geral/principal.Master" AutoEventWireup="true"
    CodeBehind="frmTrocarHorario.aspx.cs" Inherits="Web.Cadastros.frmTrocarHorario"
    Title="Agenda - Trocar Horário" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register src="../Componente/Mensagem.ascx" tagname="Mensagem" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
function SetUniqueRadioButton(nameregex, current)    
{
   re = new 
   RegExp(nameregex);
   
   for(i = 0; i < document.forms[0].elements.length; i++)
   {
      elm = document.forms[0].elements[i]
      if (elm.type == 'radio')
      {
         if (re.test(elm.name))
         {
            elm.checked = false;
         }
      }
   }
   
   current.checked = true;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
        <uc1:botao ID="botao1" runat="server" />
    </div>
    <div id="divFormulario">
        <fieldset>
            <legend>De</legend>
            <table>
                <tr>
                <td class="style1">
                    <b><span class="style3"></span>Funcionário</b>:
                </td>
                    <td>
                        <asp:DropDownList ID="ddlFuncionario" runat="server" AppendDataBoundItems="True"
                            AutoPostBack="True" Width="300px" 
                            OnSelectedIndexChanged="ddlFuncionario_SelectedIndexChanged" Enabled="False">
                            <asp:ListItem Value="0">(--Selecionar--)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td class="style1">
                    <b>Especialidade</b>:
                </td>
                    <td>
                        <asp:DropDownList ID="ddlProfissao" runat="server" AppendDataBoundItems="True" 
                            Width="300px" Enabled="False">
                            <asp:ListItem Value="0">(--Selecionar--)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td class="style1">
                    <b><span class="style3"></span>Aluno</b>:
                </td>
                    <td>
                        <asp:DropDownList ID="ddlAluno" runat="server" AppendDataBoundItems="True" 
                            Width="300px" Enabled="False">
                            <asp:ListItem Value="0">(--Selecionar--)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td class="style1">
                    <b><span class="style3"></span>Data</b>:
                </td>
                    <td>
                        <asp:TextBox ID="txtData" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td class="style1">
                    <b><span class="style3"></span>Horário</b>:
                </td>
                    <td>
                        <asp:TextBox ID="txtHorario" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>Para</legend>
            <table>
                <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Funcionário</b>:
                </td>
                    <td>
                        <asp:DropDownList ID="ddlParaFuncionario" runat="server" AppendDataBoundItems="True"
                            Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlParaFuncionario_SelectedIndexChanged">
                            <asp:ListItem Value="0">(--Selecionar--)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CompareValidator ID="cpvFuncionario" runat="server" 
                            ErrorMessage="Selecione o Campo Funcionario" 
                            ControlToValidate="ddlParaFuncionario" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Especialidade</b>:
                </td>
                    <td>
                        <asp:DropDownList ID="ddlParaProfissao" runat="server" AppendDataBoundItems="True"
                            Width="300px" Enabled="False">
                            <asp:ListItem Value="0">(--Selecionar--)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CompareValidator ID="cpvProfissao" runat="server" 
                            ErrorMessage="Selecione o Campo Profissão" 
                            ControlToValidate="ddlParaProfissao" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                    </td>
                </tr>
                </table>
                <table>
                <tr>
                <td class="style1">
                    <b><span class="style3">*</span>Data</b>:
                </td>
                    <td valign="top">
                        <asp:TextBox ID="txtParaData" runat="server" onkeyup="formataData(this,event);" 
                            MaxLength="10" ontextchanged="txtParaData_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvData" runat="server" 
                            ControlToValidate="txtParaData" Display="Dynamic" 
                            ErrorMessage="Informe o Campo data">*</asp:RequiredFieldValidator>
                    </td>
                    
                    <td>
                    <asp:ImageButton ID="btnCalendario" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/icones/calendar.png" />
                    <cc1:CalendarExtender ID="cldCalendario" runat="server" PopupButtonID="btnCalendario"
                                    TargetControlID="txtParaData" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                        
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gdvAtendimentosPara" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="None" 
                            DataKeyNames="Codigo,CodigoAluno,CodigoCompromisso" 
                            onrowdatabound="gdvAtendimentosPara_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" />
                            <Columns>
                                <asp:BoundField DataField="Hora" HeaderText="Horário">
                                    <HeaderStyle Width="95px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Aluno" HeaderText="Aluno">
                                    <HeaderStyle Width="280px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Funcionario" HeaderText="Funcionário">
                                    <HeaderStyle Width="280px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Especialidade" HeaderText="Especialidade">
                                    <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="nomeGrupo"/>
                                    </ItemTemplate>
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
        </fieldset>
        <fieldset>
            <legend>Motivo</legend>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMotivo" runat="server" Width="600px" TextMode="MultiLine" 
                            Height="70px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    
                    
                        <uc2:Mensagem ID="Mensagem1" runat="server" />
                    
                    
                    </td>
                </tr>
                <tr>
                <td>
                
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" />
                </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
