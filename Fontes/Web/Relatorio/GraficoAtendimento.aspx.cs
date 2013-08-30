using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using GEPEX;
using Model.Entidade;


namespace Web.Relatorio
{
    public partial class GraficoAtendimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Gráfico de Atendimento Clínico");
            botao1.Desabilitar(true, false, true, false, true, true, true, true, false);
            if (!IsPostBack)
            {
                CarregaAnoLetivo();
                this.Limpar();
            }
            this.chrBarra.Series[0].PostBackValue = "#AXISLABEL";
            this.chrBarra.Series[0].ToolTip = "#AXISLABEL - #VAL{N}";
            this.chrEspecialidade.Series[0].PostBackValue = "#AXISLABEL";
            this.chrEspecialidade.Series[0].ToolTip = "#AXISLABEL - #VAL{N}";
            this.chrDocente.Series[0].ToolTip = "#AXISLABEL - #VAL{N}";
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick  += new botao.EventHandler(BarraBotao_Click);

        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Pesquisar":
                    this.Selecionar();
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
               case "Voltar":
                    Response.Redirect("../Geral/index.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

        public void Selecionar()
        {
            try
            {
                this.dsrAtendimentoGeral.SelectParameters["AnoLetivo"].DefaultValue = ddlAnoLetivo.SelectedValue;
                this.dsrAtendimentoGeral.SelectParameters["Aluno"].DefaultValue = hfdNome.Value;
                this.dsrAtendimentoGeral.DataBind();
                chrBarra.Visible = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Limpar()
        {
            txtAluno.Text = string.Empty;
            hfdNome.Value = string.Empty;
            chrBarra.Visible = 
            chrDocente.Visible = 
            chrEspecialidade.Visible = false;
            ddlAnoLetivo.SelectedIndex = 0;
        }


        private string MesSelecionado
        {

            get
            {
                string aux = ViewState["MesSelecionado"] == null ? "" : (string)ViewState["MesSelecionado"];
                switch (aux)
                {
                    case ("Jan."):
                        aux = "1";
                        break;
                    case ("Fev."):
                        aux = "2";
                        break;
                    case ("Mar."):
                        aux = "3";
                        break;
                    case ("Abr."):
                        aux = "4";
                        break;
                    case ("Maio"):
                        aux = "5";
                        break;
                    case ("Jun."):
                        aux = "6";
                        break;
                    case ("Jul."):
                        aux = "7";
                        break;
                    case ("Ago."):
                        aux = "8";
                        break;
                    case ("Set."):
                        aux = "9";
                        break;
                    case ("Out."):
                        aux = "10";
                        break;
                    case ("Nov."):
                        aux = "11";
                        break;
                    case ("Dez."):
                        aux = "12";
                        break;
                }

                return aux;

            }
            set { ViewState["MesSelecionado"] = value; }
        }
    

        protected void chrBarra_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                this.dsrEspecialidade.SelectParameters["AnoLetivo"].DefaultValue = ddlAnoLetivo.SelectedValue;
                this.dsrEspecialidade.SelectParameters["Aluno"].DefaultValue = hfdNome.Value;
                MesSelecionado = e.PostBackValue;
                this.dsrEspecialidade.SelectParameters["Mes"].DefaultValue = MesSelecionado;
                this.dsrEspecialidade.DataBind();
                chrEspecialidade.Visible = true;
            }
            catch (Exception ex) 
            {
                throw ex;
            }

        }


        protected void chrEspecialidade_Click(object sender, ImageMapEventArgs e)
        {

            try
            {
                Profissao profissao = new Profissao().SelecionarPorEspecialidade(e.PostBackValue.ToString());
                this.dsrDocente.SelectParameters["AnoLetivo"].DefaultValue = ddlAnoLetivo.SelectedValue;
                this.dsrDocente.SelectParameters["Aluno"].DefaultValue = hfdNome.Value;
                this.dsrDocente.SelectParameters["Mes"].DefaultValue = MesSelecionado;
                this.dsrDocente.SelectParameters["Especialidade"].DefaultValue = profissao.Codigo.ToString();
                this.dsrDocente.DataBind();
                chrDocente.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtAluno_TextChanged(object sender, EventArgs e)
        {
            if (txtAluno.Text.Trim() != "")
            {
                Pessoa pessoa = new Pessoa();
                Aluno aluno = new Aluno().SelecionarPorPessoa(pessoa.SelecionarPorNome(txtAluno.Text, "A"));
                if (aluno != null)
                {
                    hfdNome.Value = Convert.ToString(aluno.Codigo);
                }
                else
                {
                    txtAluno.Text = string.Empty;
                    hfdNome.Value = string.Empty;
                    ScriptManager.RegisterStartupScript(upnAluno, upnAluno.GetType(), "scriptAjax",
                        "alert('Aluno não cadastrado.');", true);
                }

            }
            else
            {
                hfdNome.Value = string.Empty;

            }
        }
        private void CarregaAnoLetivo()
        {
            ddlAnoLetivo.DataTextField = "AnoLetivo";
            ddlAnoLetivo.DataValueField = "AnoLetivo";
            Turma turma = new Turma();
            ddlAnoLetivo.DataSource = turma.ObterAnoLetivo();
            ddlAnoLetivo.DataBind();

        }
    }
}
