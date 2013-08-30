using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Data;

namespace Web.Consultas
{
    public partial class frmConsultarGradeHorario : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAnoLetivo();
                ((principal)this.Master).AlteraTitulo("Consulta de Grade Horário");
                botao1.Desabilitar(false, false, true, false, true, true, true, true, false);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarGradeHorario");
                if (objPermissao.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta de Grade Horário!');location.href='../Geral/index.aspx';</script>");
                }
            }
        }
        protected void gdvGradeHorario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvGradeHorario.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarGradeHorario.aspx?codigo=" + codigo);
        }

        protected void gdvGradeHorario_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GradeHorario grade = new GradeHorario();
                grade.Codigo = Convert.ToInt32(gdvGradeHorario.DataKeys[e.RowIndex].Values[0]);
                grade.Excluir(grade.Codigo);
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
            //professor
            Docente professor;
            if (hflDocente.Value != "")
                professor = new Docente().Selecionar(Convert.ToInt32(hflDocente.Value));
            else
                professor = null;
            IList<GradeHorario> grade = new GradeHorario().SelecionarPorCriterios(professor, lista);
            if (grade == null || grade.Count == 0)
            {
                gdvGradeHorario.DataBind();
                Mensagem.Aviso("Nenhuma grade de horário foi localizada.");
            }
            else if (grade.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo",System.Type.GetType("System.String"));
                dt.Columns.Add("turma", System.Type.GetType("System.String"));
                dt.Columns.Add("disciplina", System.Type.GetType("System.String"));
                dt.Columns.Add("dia", System.Type.GetType("System.String"));
                dt.Columns.Add("horario", System.Type.GetType("System.String"));
                dt.Columns.Add("professor", System.Type.GetType("System.String"));
                foreach (GradeHorario t in grade)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Turma.ToString(), t.Disciplina.Materia,
                                               t.DiaSemanaFormatado, t.HorarioFormatado, t.Docente.Pessoa.Nome });
                }
                gdvGradeHorario.DataSource = dt;
                gdvGradeHorario.DataBind();
                ViewState.Add("Grid", grade);
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
            txtDocente.Text = string.Empty;
            hflDocente.Value = string.Empty;
            gdvGradeHorario.DataBind();
        }
        private void CarregaAnoLetivo()
        {
            ddlAnoLetivo.DataTextField = "AnoLetivo";
            ddlAnoLetivo.DataValueField = "AnoLetivo";
            Turma turma = new Turma();
            ddlAnoLetivo.DataSource = turma.ObterAnoLetivo();
            ddlAnoLetivo.DataBind();

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
                    Response.Redirect("../Cadastros/frmCadastrarGradeHorario.aspx");
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

        protected void txtDocente_TextChanged(object sender, EventArgs e)
        {
            if (txtDocente.Text.Trim() != "")
            {
                Pessoa pessoa = new Pessoa();
                Docente docente = new Docente().SelecionarPorPessoa(pessoa.SelecionarPorNome(txtDocente.Text, "D"));
                if (docente != null)
                {
                    hflDocente.Value = Convert.ToString(docente.Codigo);
                }
                else
                {
                    txtDocente.Text = string.Empty;
                    hflDocente.Value = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptAjax",
                        "alert('Professor não cadastrado.');", true);
                }
            }
            else
            {
                hflDocente.Value = string.Empty;
            }
        }

        protected void gdvGradeHorario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvGradeHorario.PageIndex = e.NewPageIndex;
            Selecionar();
        }

        protected void gdvGradeHorario_Sorting(object sender, GridViewSortEventArgs e)
        {
            IList<GradeHorario> grade = (IList<GradeHorario>)ViewState["Grid"];
            if (grade != null && grade.Count > 0) {
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("turma", System.Type.GetType("System.String"));
                dt.Columns.Add("disciplina", System.Type.GetType("System.String"));
                dt.Columns.Add("dia", System.Type.GetType("System.String"));
                dt.Columns.Add("horario", System.Type.GetType("System.String"));
                dt.Columns.Add("professor", System.Type.GetType("System.String"));
                foreach (GradeHorario t in grade)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Turma.ToString(), t.Disciplina.Materia,
                                               t.DiaSemanaFormatado, t.HorarioFormatado, t.Docente.Pessoa.Nome });
                }
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + sortDireciton;
                gdvGradeHorario.DataSource = dataView;
                gdvGradeHorario.DataBind();
                
            }
        }

        protected void gdvGradeHorario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarGradeHorario");
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
