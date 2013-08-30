using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;

namespace Web.Consultas
{
    public partial class frmConsultarPlanejamentoPedagogico : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAnoLetivo();
                CarregaDisciplinas();
                ((principal)this.Master).AlteraTitulo("Consulta Planej. Pedagógico");
                botao1.Desabilitar(false, false, true, false, true, true, true, true, false);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarPlanejamentoPedagogico");
                if (objPermissao.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta Planejamento Pedagógico!');location.href='../Geral/index.aspx';</script>");
                }
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
        private void CarregaDisciplinas()
        {
            ddlDisciplina.Items.Clear();
            ListItem itemDefault = new ListItem("Todas", "0");
            itemDefault.Selected = true;
            ddlDisciplina.Items.Add(itemDefault);
            IList<Disciplina> lista = new Disciplina().SelecionarAtivos();
            foreach (Disciplina disciplina in lista)
            {
                ListItem item = new ListItem();
                item.Value = disciplina.Codigo.ToString();
                item.Text = disciplina.Materia;
                item.Selected = false;
                ddlDisciplina.Items.Add(item);
            }
            ddlDisciplina.DataBind();

        }
        protected void gdvPlanejamentoPedagogico_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                PlanejamentoPedagogico planejamento = new PlanejamentoPedagogico();
                planejamento.Codigo = Convert.ToInt32(gdvPlanejamentoPedagogico.DataKeys[e.RowIndex].Values[0]);
                planejamento.Excluir(planejamento.Codigo);
                Mensagem.Aviso(ConfigurationManager.AppSettings["02_Exclusao"].ToString());
                this.Limpar();

            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        protected void gdvPlanejamentoPedagogico_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvPlanejamentoPedagogico.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarPlanejamentoPedagogico.aspx?codigo=" + codigo);
        }

        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            Turma turma = new Turma();
            IList<Turma> lista = new List<Turma>();
            if (txtSerie.Text.Trim() != string.Empty)
                turma.Serie = txtSerie.Text;
            if (txtTurma.Text.Trim() != string.Empty)
                turma.SerieTurma = txtTurma.Text;
            if (ddlAnoLetivo.Text.Trim() != string.Empty)
                turma.AnoLetivo = Convert.ToInt32(ddlAnoLetivo.Text);
            if (ddlEnsino.SelectedIndex != 0)
                turma.Ensino = ddlEnsino.SelectedValue;
            if (ddlPeriodo.SelectedIndex != 0)
                turma.Periodo = ddlPeriodo.SelectedValue;
            lista = turma.SelecionarPorCriterio();
            //disciplina
            Disciplina disciplina;
            if (ddlAnoLetivo.SelectedValue != "0")
                disciplina = new Disciplina().Selecionar(Convert.ToInt32(ddlDisciplina.SelectedValue));
            else
                disciplina = null;
            IList<PlanejamentoPedagogico> planej = new PlanejamentoPedagogico().SelecionarPorCriterios(disciplina, lista);
            if (planej == null || planej.Count == 0)
            {
                gdvPlanejamentoPedagogico.DataBind();
                Mensagem.Aviso("Nenhum planejamento pedagógico foi localizada.");
            }
            else if (planej.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("periodo", System.Type.GetType("System.String"));
                dt.Columns.Add("turma", System.Type.GetType("System.String"));
                dt.Columns.Add("disciplina", System.Type.GetType("System.String"));
                dt.Columns.Add("AnoLetivo", System.Type.GetType("System.String"));
                foreach (PlanejamentoPedagogico t in planej)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.DataInicial.ToShortDateString() + " - " + 
                                               t.DataFinal.ToShortDateString(), t.Turma.ToString(), 
                                               t.Disciplina.Materia, t.Turma.AnoLetivo.ToString()});
                }
                gdvPlanejamentoPedagogico.DataSource = dt;
                gdvPlanejamentoPedagogico.DataBind();
                ViewState.Add("Grid", planej);
            }
        }

        public bool Salvar()
        {
            throw new NotImplementedException();
        }

        public bool Alterar()
        {
            throw new NotImplementedException();
        }

        public bool ValidarCamposObrigatorios()
        {
            throw new NotImplementedException();
        }

        public void Limpar()
        {
            if (ddlAnoLetivo.Items.Count > 0)
                ddlAnoLetivo.SelectedIndex = 0;
            txtSerie.Text = string.Empty;
            txtTurma.Text = string.Empty;
            ddlEnsino.SelectedIndex = 0;
            ddlPeriodo.SelectedIndex = 0;
            ddlDisciplina.SelectedIndex = 0;
            gdvPlanejamentoPedagogico.DataBind();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.botao1.imgNovoOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarPlanejamentoPedagogico.aspx");
                    break;
                case "Pesquisar":
                    this.Selecionar();
                    break;
                case "Salvar":
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

        protected void gdvPlanejamentoPedagogico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvPlanejamentoPedagogico.PageIndex = e.NewPageIndex;
            Selecionar();
        }

        protected void gdvPlanejamentoPedagogico_Sorting(object sender, GridViewSortEventArgs e)
        {
            IList<PlanejamentoPedagogico> planej = (IList<PlanejamentoPedagogico>)ViewState["Grid"];
            if (planej != null && planej.Count > 0)
            {
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("periodo", System.Type.GetType("System.String"));
                dt.Columns.Add("turma", System.Type.GetType("System.String"));
                dt.Columns.Add("disciplina", System.Type.GetType("System.String"));
                dt.Columns.Add("AnoLetivo", System.Type.GetType("System.String"));
                foreach (PlanejamentoPedagogico t in planej)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.DataInicial.ToShortDateString() + " - " + 
                                               t.DataFinal.ToShortDateString(), t.Turma.ToString(), 
                                               t.Disciplina.Materia, t.Turma.AnoLetivo.ToString()});
                }
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + sortDireciton;
                gdvPlanejamentoPedagogico.DataSource = dataView;
                gdvPlanejamentoPedagogico.DataBind();
            }
        }

        protected void gdvPlanejamentoPedagogico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarPlanejamentoPedagogico");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objPermissao.Altera == false)
                {
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    imgEditar.Visible = false;
                }
                if (objPermissao.Exclui == false)
                {
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                    imgExcluir.Visible = false;
                }
            }
        }
    }
}
