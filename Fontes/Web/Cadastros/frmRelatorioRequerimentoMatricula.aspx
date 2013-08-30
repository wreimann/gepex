<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRelatorioRequerimentoMatricula.aspx.cs"
    Inherits="Web.Cadastros.frmRelatorioRequerimentoMatricula" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="4" style="font-weight: 700; text-align: center;">
                    Escola 29 de Março 
                    <br />
                    Rua das Laranjeiras, 72 Bairro Alto, Curitiba - PR
                    <br />
                    Telefone:
                    (41) 3205-6692 - <a href="mailto:contato@escola29demarco.com">contato@escola29demarco.com</a>
                    <br />
                    <span class="style1">Requerimento de Matrícula</span></td>
            </tr>
            <tr>
                <td colspan="4" style="font-weight: 700; text-align: center;">
                    <hr width="100%" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Nome:
                </td>
                <td>
                    <asp:Label ID="lblNome" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Telefone:
                </td>
                <td>
                    <asp:Label ID="lblTelefone" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Emergência:
                </td>
                <td>
                    <asp:Label ID="lblEmergencia" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    Falar Com:
                </td>
                <td>
                    <asp:Label ID="lblComoAjudar" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    E-Mail:
                </td>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        <fieldset>
            <legend>Informações Adicionais</legend>
            <table>
                <tr>
                    <td>
                        Sites:
                    </td>
                    <td>
                        <asp:Label ID="lblSites" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        Medicar na Escola:
                    </td>
                    <td>
                        <asp:Label ID="lblMedicarEscola" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Medicamentos:
                    </td>
                    <td>
                        <asp:Label ID="lblMedicamentos" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Alergia:
                    </td>
                    <td>
                        <asp:Label ID="lblAlergia" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Convênio Médico:
                    </td>
                    <td>
                        <asp:Label ID="lblConvenioMedico" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Telefone:
                    </td>
                    <td>
                        <asp:Label ID="lblTelefoneInformacoesAdicionais" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        Carterinha:
                    </td>
                    <td>
                        <asp:Label ID="lblCarteirinha" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>Endereço</legend>
            <table>
                <tr>
                    <td>
                        CEP:
                    </td>
                    <td>
                        <asp:Label ID="lblCep" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Logradouro:
                    </td>
                    <td>
                        <asp:Label ID="lblLogradouro" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        Número:
                    </td>
                    <td>
                        <asp:Label ID="lblNumero" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Complemento:
                    </td>
                    <td>
                        <asp:Label ID="lblComplemento" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bairro:
                    </td>
                    <td>
                        <asp:Label ID="lblBairro" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cidade:
                    </td>
                    <td>
                        <asp:Label ID="lblCidade" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        UF:
                    </td>
                    <td>
                        <asp:Label ID="lblUF" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>Termo de compromissi e responsabilidade</legend>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblTermo" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Assinatura:
                </td>
                <td>
                    ______________________________________________________
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
