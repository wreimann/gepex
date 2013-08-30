<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="botao.ascx.cs" Inherits="GEPEX.botao" %>
<asp:ImageButton ID="imgNovo" runat="server" 
    ImageUrl="~/App_Themes/icones/novo.png" ToolTip="Novo" CommandName="Novo" 
    Width="32px" CausesValidation="False" />
<asp:ImageButton ID="imgPesquisar" runat="server" CommandName="Pesquisar" 
    ImageUrl="~/App_Themes/icones/pesquisar.png" ToolTip="Pesquisar" 
    CausesValidation="False" />
<asp:ImageButton ID="imgSalvar" runat="server" 
    ImageUrl="~/App_Themes/icones/salvar2.png" ToolTip="Salvar" 
    CommandName="Salvar" />
<asp:ImageButton ID="imgLimpar" runat="server" 
    ImageUrl="~/App_Themes/icones/limpar.png" ToolTip="Limpar" 
    CommandName="Limpar" CausesValidation="False" />
<asp:ImageButton ID="imgImprimir" runat="server" CommandName="Imprimir" 
    ImageUrl="~/App_Themes/icones/imprimir.png" ToolTip="Imprimir" 
    CausesValidation="False" />
<asp:ImageButton ID="imgFichaAluno" runat="server" CommandName="FichaAluno" 
    ImageUrl="~/App_Themes/icones/fichaAluno.png" ToolTip="Ficha do Aluno" CausesValidation="False" />
<asp:ImageButton ID="imgPlanejamentoClinivo" runat="server" 
    CommandName="PlanejamentoClinico" 
    ImageUrl="~/App_Themes/icones/planejamentoClinico.png" 
    ToolTip="Planejamento Clinico" CausesValidation="False" />
<asp:ImageButton ID="imgPlanejamentoPedagogico" runat="server" 
    CommandName="Planejamento Pedagogico" 
    ImageUrl="~/App_Themes/icones/planejamentoPedagogico.png" 
    ToolTip="Planejamento Pedagogico" CausesValidation="False" />
<asp:ImageButton ID="imgListaEspera" runat="server" 
    ImageUrl="~/App_Themes/icones/listaEspera.png" ToolTip="Lista de Espera" 
    CommandName="ListaEspera" CausesValidation="False" Visible="false" /> 
<asp:ImageButton ID="imgFinalizarAnoLetivo" runat="server" 
    ImageUrl="~/App_Themes/icones/calendar_link.png" ToolTip="Finalizar Ano Letivo" 
    CommandName="FinalizarAnoLetivo" CausesValidation="False" 
    Visible="false" onclientclick="javascript:return confirm('Deseja realmente finalizar o Ano Letivo selecionado?')" /> 
<asp:ImageButton ID="imgVoltar" runat="server" 
    ImageUrl="~/App_Themes/icones/voltar.png" ToolTip="Voltar" 
    CommandName="Voltar" CausesValidation="False" />
   