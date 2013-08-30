<%@ Page Title="Gráfico de Atendimento Clínico" Language="C#" MasterPageFile="~/Geral/principal.Master" 
    AutoEventWireup="true" CodeBehind="GraficoAtendimento.aspx.cs" 
    Inherits="Web.Relatorio.GraficoAtendimento" %>
<%@ Register Src="../botao.ascx" TagName="botao" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBarraBotao">
     <uc1:botao ID="botao1" runat="server" />
     
</div>
<div id="divFormulario">
<table>
<tr>
    <td>Ano Letivo:</td>
    <td><asp:DropDownList ID="ddlAnoLetivo" runat="server" AppendDataBoundItems="True" 
        Width="65px"></asp:DropDownList>
    </td>
</tr>
<tr>
    <asp:UpdatePanel ID="upnAluno" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="txtAluno" />
    </Triggers>
    <ContentTemplate>
    <td>Aluno:</td>
    <td>
        <asp:TextBox ID="txtAluno" runat="server" Width="370px" AutoPostBack="True" 
            ontextchanged="txtAluno_TextChanged" MaxLength="80" 
            AutoCompleteType="Disabled"></asp:TextBox>
        <asp:HiddenField ID="hfdNome" runat="server" />       
        <cc1:autocompleteextender ID="acePessoa" runat="server"  
            ServicePath="../WebServicesInterno/PessoaAutoComplete.asmx" 
            TargetControlID="txtAluno" ServiceMethod="ListaAluno" 
            MinimumPrefixLength="1" CompletionInterval="400" FirstRowSelected="True" 
            CompletionSetCount="10">
        </cc1:autocompleteextender>
    </td>
    </ContentTemplate>
    </asp:UpdatePanel>
</tr>
</table>
    <asp:SqlDataSource ID="dsrAtendimentoGeral" runat="server" 
        ConnectionString="<%$ ConnectionStrings:gepexConnectionString %>"       
        ProviderName="<%$ ConnectionStrings:gepexConnectionString.ProviderName %>" 
        SelectCommand="GraficoAtendimentoNivel1" 
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:Parameter Name="AnoLetivo" Type="Int32" DefaultValue="" />
            <asp:Parameter Name="Aluno" Type="Int32" DefaultValue="" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsrEspecialidade" runat="server"
        ConnectionString="<%$ ConnectionStrings:gepexConnectionString %>"       
        ProviderName="<%$ ConnectionStrings:gepexConnectionString.ProviderName %>"  
        SelectCommand="GraficoAtendimentoNivel2" 
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:Parameter Name="AnoLetivo" Type="Int32" />
            <asp:Parameter Name="Aluno" Type="Int32" />
            <asp:Parameter Name="Mes" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsrDocente" runat="server"
        ConnectionString="<%$ ConnectionStrings:gepexConnectionString %>"       
        ProviderName="<%$ ConnectionStrings:gepexConnectionString.ProviderName %>"
        SelectCommand="GraficoAtendimentoNivel3" 
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:Parameter Name="AnoLetivo" Type="Int32" />
            <asp:Parameter Name="Aluno" Type="Int32" />
            <asp:Parameter Name="Especialidade" Type="Int32" />
            <asp:Parameter Name="Mes" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:UpdatePanel ID="upnAtendimento" runat="server">
    <ContentTemplate>
     <asp:Chart ID="chrBarra" runat="server" BackColor="211, 223, 240" 
            Palette="SeaGreen" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)"  
                    Width="850px" Height="221px"
                    borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                    BorderlineWidth="0" BorderlineColor="26, 59, 105" 
                    DataSourceID="dsrAtendimentoGeral" onclick="chrBarra_Click">
                    <Titles>
                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                            Text="Atendimento Clínico Visão Geral" Alignment="TopLeft" ForeColor="26, 59, 105">
                        </asp:Title>
                    </Titles>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <Series>
                        <asp:Series Name="mes" BorderColor="180, 26, 59, 105" IsValueShownAsLabel="True" 
                            IsVisibleInLegend="False" YValueMembers="quantidade" XValueMember="mesFormatado" XValueType="Single" YValueType="Int32">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                            BackGradientStyle="TopBottom">
                            <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                            </AxisY>
                            <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
    </asp:Chart>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />
    <asp:UpdatePanel ID="upnEspecialidade" runat="server">
    <Triggers>
     <asp:PostBackTrigger ControlID="chrBarra" />
    </Triggers>
    <ContentTemplate>
      <asp:Chart ID="chrEspecialidade" runat="server" BackColor="211, 223, 240" Palette="SeaGreen"
                    ImageType="Png"   Width="850px" Height="221px"
                    borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                    BorderlineWidth="0" BorderlineColor="26, 59, 105" 
                    DataSourceID="dsrEspecialidade" onclick="chrEspecialidade_Click" 
                    Visible="False">
                    <Titles>
                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                            Text="Atendimento Por Especialidade" Alignment="TopLeft" 
                            ForeColor="26, 59, 105" Name="Title1">
                        </asp:Title>
                    </Titles>
                    <Legends>
                        <asp:Legend Name="Default" BackColor="Transparent" 
                            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False">
                            <Position Y="21" Height="22" Width="18" X="73"></Position>
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <Series>
                        <asp:Series Name="mes" BorderColor="180, 26, 59, 105" 
                            IsValueShownAsLabel="True" ChartType="Pie" 
                            ChartArea="Default" Legend="Default" YValueMembers="quantidade" XValueMember="Especialidade" XValueType="String" YValueType="Int32">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                            BackGradientStyle="TopBottom">
                            <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                            </AxisY>
                            <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
    </asp:Chart>
    </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upnDocente" runat="server">
    <Triggers>
     <asp:PostBackTrigger ControlID="chrEspecialidade" />
    </Triggers>
    <ContentTemplate>
    <asp:Chart ID="chrDocente" runat="server" BackColor="211, 223, 240" 
            Palette="SeaGreen"   Width="850px" Height="221px"
                    borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                    BorderlineWidth="0" 
                    DataSourceID="dsrDocente" onclick="chrEspecialidade_Click" 
                    Visible="False">
                    <Titles>
                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                            Text="Atendimento Por Docente" Alignment="TopLeft" ForeColor="26, 59, 105">
                        </asp:Title>
                    </Titles>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <Series>
                        <asp:Series Name="mes" BorderColor="180, 26, 59, 105" 
                            IsValueShownAsLabel="True" ChartType="Bar" XValueMember="nome" 
                            YValueMembers="quantidade" XValueType="String" YValueType="Int32">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                            BackGradientStyle="TopBottom">
                            <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                            </AxisY>
                            <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
    </asp:Chart>
    </ContentTemplate>
    </asp:UpdatePanel>

</div>
</asp:Content>
