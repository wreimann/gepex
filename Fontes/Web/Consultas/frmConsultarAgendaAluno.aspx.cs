using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Web.Util;
using GEPEX;
using Model.Entidade;
using System.Collections.Generic;

namespace Web.Consultas
{
    public partial class frmConsultarAgendaAluno : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    ((principal)this.Master).AlteraTitulo("Agenda do Aluno");                    

                    Usuario objUsuario = new Usuario();
                    objUsuario = ((principal)this.Master).usuarioLogado;
                    Pessoa objPessoa = objUsuario.Pessoa;
                    if (objPessoa.Tipo == "A")
                    {
                        txtAluno.Text = objPessoa.Nome;//recupera o aluno logado
                        Aluno aluno = new Aluno().SelecionarPorPessoa(objPessoa);
                        hfdNome.Value =  aluno.Codigo.ToString();
                        txtData.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                        this.CarregarGradeHorario();
                        this.Selecionar();
                    }                 
                    Perfil objPerfil = objUsuario.Perfil;
                    if (objPerfil.Descricao.ToUpper() == "ALUNO")
                    {
                        botao1.Desabilitar(true, false, true, false, true, true, true, true, false);
                    }
                    else
                    {
                        botao1.Desabilitar(false, false, false, false, true, true, true, true, false);
                        txtAluno.ReadOnly = false;
                    }
                }
                catch (Exception)
                {
                }
            }
            //this.Selecionar();
        }
        protected void gdvAgendaAluno_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                AgendaAluno objAgendaAluno = new AgendaAluno();
                int codigo = Convert.ToInt32(gdvAgendaAluno.DataKeys[e.RowIndex].Values["Codigo"]);
                if (objAgendaAluno.Excluir(codigo))
                {
                    this.Selecionar();
                }

            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void calData_SelectionChanged(object sender, EventArgs e)
        {
            txtData.Text = calData.SelectedDate.ToString("dd/MM/yyyy");
            Session["dataAgendaAluno"] = calData.SelectedDate.ToString("dd/MM/yyyy");
            this.Selecionar();
        }

        protected void gdvAgendaAluno_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvAgendaAluno.DataKeys[e.NewSelectedIndex].Values["Codigo"]);
            Response.Redirect("../Cadastros/frmCadastrarAgendaAluno.aspx?codigo=" + codigo);
        }
        protected void calData_DayRender(object sender, DayRenderEventArgs e)
        {
            if (hfdNome.Value != "")
            {
                Aluno objAluno = new Aluno().Selecionar(Convert.ToInt32(hfdNome.Value));
                AgendaAluno objAgenda = new AgendaAluno();
                objAgenda.Aluno = objAluno;
                IList<AgendaAluno> lsAgendaAluno = objAgenda.SelecionarPorAluno();

                foreach (AgendaAluno lsAg in lsAgendaAluno)
                {
                    if (lsAg.Data == e.Day.Date)
                    {
                        e.Cell.BackColor = System.Drawing.Color.Pink;
                    }
                }
            }
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            if (hfdNome.Value == "")
            {

                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "scriptAjax",
                        "alert('Informe o nome do aluno!');", true);
            }
            else
            {

                try
                {
                    CarregarGradeHorario();
                    AgendaAluno objAgendaAluno = new AgendaAluno();
                    Aluno objAluno = new Aluno().Selecionar(Convert.ToInt32(hfdNome.Value));
                    objAgendaAluno.Aluno = objAluno;
                    objAgendaAluno.Data = Convert.ToDateTime(txtData.Text);
                    IList<AgendaAluno> lsAgendaAluno = objAgendaAluno.SelecionarPorCriterio();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
                    dt.Columns.Add("Profissional", Type.GetType("System.String"));
                    dt.Columns.Add("Especialidade", Type.GetType("System.String"));
                    dt.Columns.Add("Data", Type.GetType("System.String"));
                    dt.Columns.Add("Anotacao", Type.GetType("System.String"));

                    foreach (AgendaAluno ls in lsAgendaAluno)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Codigo"] = ls.Codigo;
                        dr["Profissional"] = ls.Docente.Pessoa.Nome;
                        dr["Especialidade"] = ls.Docente.Profissao.Descricao;
                        dr["Data"] = ls.Data.ToString("dd/MM/yyyy");
                        dr["Anotacao"] = ls.Recado;

                        dt.Rows.Add(dr);
                    }
                    gdvAgendaAluno.DataSource = dt;
                    gdvAgendaAluno.DataBind();

                    DataTable dtAtendimentos = new DataTable();
                    dtAtendimentos.Columns.Add("Codigo", Type.GetType("System.Int32"));
                    dtAtendimentos.Columns.Add("Horario", Type.GetType("System.String"));
                    dtAtendimentos.Columns.Add("Especialidade", Type.GetType("System.String"));
                    dtAtendimentos.Columns.Add("Profissional", Type.GetType("System.String"));

                    Atendimento objAtendimento = new Atendimento();
                    objAtendimento.Aluno = objAluno;
                    objAtendimento.Data = Convert.ToDateTime(txtData.Text);

                    IList<Atendimento> lsAtendimento = objAtendimento.SelecionarPorCriterio();
                    foreach (Atendimento lsAt in lsAtendimento)
                    {
                        DataRow dr = dtAtendimentos.NewRow();
                        dr["Codigo"] = lsAt.Codigo;
                        dr["Horario"] = lsAt.DataHorarioInicial.ToString("hh:mm") + " - " + lsAt.DataHorarioFinal.ToString("hh:mm");
                        dr["Especialidade"] = lsAt.Profissao.Descricao;
                        dr["Profissional"] = lsAt.Docente.Pessoa.Nome;

                        dtAtendimentos.Rows.Add(dr);
                    }
                    gdvAtendimento.DataSource = dtAtendimentos;
                    gdvAtendimento.DataBind();
              
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }         
        }

        public void CarregarGradeHorario() 
        {
            GradeHorario objGradeHorario = new GradeHorario();
            Matricula objMatricula = new Matricula();
            objMatricula.Aluno = new Aluno().Selecionar(Convert.ToInt32(hfdNome.Value));
            objMatricula.AnoLetivo = calData.SelectedDate.Year ;
            objMatricula = objMatricula.SelecionarPorCriterio();
            if (objMatricula != null)
            {
                
                DataTable dtGradeHorario = new DataTable();
                dtGradeHorario.Columns.Add("Codigo", Type.GetType("System.Int32"));
                dtGradeHorario.Columns.Add("Aula", Type.GetType("System.String"));
                dtGradeHorario.Columns.Add("Dia", Type.GetType("System.String"));
                dtGradeHorario.Columns.Add("Disciplina", Type.GetType("System.String"));

                foreach (GradeHorario ls in objMatricula.Turma.GradeHorario)
                {
                    DataRow dr = dtGradeHorario.NewRow();
                    dr["Codigo"] = ls.Codigo;
                    dr["Aula"] = ls.HorarioFormatado;
                    dr["Dia"] = ls.DiaSemanaFormatado;
                    dr["Disciplina"] = ls.Disciplina.Materia;

                    dtGradeHorario.Rows.Add(dr);
                }
                gdvGradeHorario.DataSource = dtGradeHorario;
                
            }
            gdvGradeHorario.DataBind();
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
            txtAluno.Text = string.Empty;
            txtData.Text = string.Empty;
            hfdNome.Value = string.Empty;
            gdvAgendaAluno.DataSource = null;
            gdvGradeHorario.DataSource = null;
            gdvAtendimento.DataSource = null;
            gdvAgendaAluno.DataBind();
            gdvAtendimento.DataBind();
            gdvGradeHorario.DataBind();
            
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
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarAgendaAluno.aspx");
                    break;
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

        protected void gdvAgendaAluno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarAgendaAluno");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objPermissao.Altera == false)
                {
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    imgEditar.Visible = false;
                }
                if (objPermissao.Exclui == false)
                {
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgDelete");
                    imgExcluir.Visible = false;
                }
                if (objPermissao.Perfil.Descricao.ToUpper() == "ALUNO")
                {
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgDelete");
                    imgExcluir.Visible = false;

                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    imgEditar.Visible = false;
                }
            }
        }

        protected void txtAluno_TextChanged(object sender, EventArgs e)
        {
            gdvAgendaAluno.DataBind();
            gdvAtendimento.DataBind();
            gdvGradeHorario.DataBind();
                    
            if (txtAluno.Text.Trim() != "")
            {
                Pessoa pessoa = new Pessoa().SelecionarPorNome(txtAluno.Text,"A");
                Aluno aluno = new Aluno().SelecionarPorPessoa(pessoa);
                if (aluno != null)
                {
                    hfdNome.Value = Convert.ToString(aluno.Codigo);
                    CarregarGradeHorario();
                }
                else
                {
                    txtAluno.Text = string.Empty;
                    hfdNome.Value = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptAjax",
                        "alert('Aluno não cadastrado.');", true);
                }

            }
            else
            {
                hfdNome.Value = string.Empty;   
            }
        }

        protected void gdvAgendaAluno_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvAgendaAluno.PageIndex = e.NewPageIndex;
            Selecionar();
        }

        protected void gdvGradeHorario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvGradeHorario.PageIndex = e.NewPageIndex;
            CarregarGradeHorario();
        }

        protected void gdvAtendimento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvAtendimento.PageIndex = e.NewPageIndex;
            Selecionar();
        }

       

     


    }
}
