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
    public partial class frmConsultarAgendaAtendimento : System.Web.UI.Page, Base
    {
        private static bool flagDia = false;
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    ((principal)this.Master).AlteraTitulo("Agenda de Atendimento Clínico");
                    botao1.Desabilitar(false, false, true, false, true, true, true, true, false);

                    this.CarregaFuncionarios();
                    //Seta o Funcionario conforme o login em sessão;
                    Docente objDocente = new Docente();
                    objDocente = this.RetornaDocente();

                    ddlFuncionario.SelectedValue = objDocente.Codigo.ToString();
                    this.CarregaProfissao(objDocente);
                    this.Selecionar();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        protected void ddlFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFuncionario.SelectedIndex != 0)
                {
                    Docente objDocente = new Docente();
                    objDocente.Codigo = Convert.ToInt32(ddlFuncionario.SelectedValue);
                    this.CarregaProfissao(objDocente);
                    flagDia = false;
                    this.Selecionar();
                }
                else
                {
                    ddlProfissao.Items.Clear();
                    ListItem itemProfissoa = new ListItem();
                    itemProfissoa.Text = "(--Selecione--)";
                    itemProfissoa.Value = "0";
                    ddlProfissao.Items.Add(itemProfissoa);
                    ddlProfissao.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gdvAtendimento_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvAtendimento.DataKeys[e.NewSelectedIndex].Values["codigoCompromisso"]);
            Compromisso objCompromisso = new Compromisso().Selecionar(codigo);
            if (objCompromisso.Situacao == "M")
                Response.Redirect("../Cadastros/frmTrocarHorario.aspx?codigo=" +codigo.ToString() );
            else
                Mensagem1.Aviso("Atendimento Clínico já realizado para o compromisso. Operação inválida.");
        }
        protected void gdvAtendimento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Compromisso objCompromisso = new Compromisso().Selecionar(Convert.ToInt32(gdvAtendimento.DataKeys[e.RowIndex].Values["codigoCompromisso"]));
                if (objCompromisso.Excluir(objCompromisso))
                {
                    Mensagem1.Aviso(ConfigurationManager.AppSettings["02_Exclusao"].ToString());
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
            Session["calDate"] = calData.SelectedDate;
            flagDia = true;
            this.Selecionar();
        }
        protected void gdvAtendimento_RowEditing(object sender, GridViewEditEventArgs e)
        {      
            int codigoAluno = Convert.ToInt32(gdvAtendimento.DataKeys[e.NewEditIndex].Values["codigoAluno"]);
            int codigoCompromisso = Convert.ToInt32(gdvAtendimento.DataKeys[e.NewEditIndex].Values["codigoCompromisso"]);
            Compromisso objCompromisso = new Compromisso().Selecionar(codigoCompromisso);
            if (objCompromisso.Situacao == "M")
                Response.Redirect("../Cadastros/frmCadastrarAtendimentoAluno.aspx?Aluno=" + codigoAluno + "&Compromisso=" + codigoCompromisso);
            else
                Mensagem1.Aviso("Atendimento Clínico já realizado para o compromisso. Operação inválida.");
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            try
            {
                Agenda objAgenda = new Agenda();

                Docente objDocente = new Docente();
                objDocente = objDocente.Selecionar(Convert.ToInt32(ddlFuncionario.SelectedValue));
                Profissao objProfissao = new Profissao();
                objProfissao = objProfissao.Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));
                objDocente.Profissao = objProfissao;
                if (flagDia)
                    objAgenda.Data = calData.SelectedDate.Date;
                else
                    objAgenda.Data = DateTime.Now.Date;

                objAgenda.Docente = objDocente;

                IList<Agenda> lsAgenda = objAgenda.SelecionarPorCriterio();

                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
                dt.Columns.Add("CodigoAluno", Type.GetType("System.Int32"));
                dt.Columns.Add("CodigoCompromisso", Type.GetType("System.Int32"));
                dt.Columns.Add("Hora", Type.GetType("System.String"));
                dt.Columns.Add("Aluno", Type.GetType("System.String"));
                dt.Columns.Add("Situacao", Type.GetType("System.String"));
                dt.Columns.Add("Turma", Type.GetType("System.String"));
                dt.Columns.Add("Sala", Type.GetType("System.String"));

                foreach (Agenda lsAg in lsAgenda)
                {
                    IList<Compromisso> lsCompromissos = lsAg.Compromissos;
                    foreach (Compromisso ls in lsCompromissos)
                    {
                        Matricula objMatricula = new Matricula();
                        objMatricula.Aluno = ls.Aluno;
                        objMatricula = objMatricula.SelecionarPorCriterio();

                        DataRow dr = dt.NewRow();
                        dr["CodigoCompromisso"] = ls.Codigo;
                        dr["Codigo"] = ls.Agenda.Codigo;
                        dr["Situacao"] = ls.Situacao == "M" ? "Marcado" : "Atendido";

                        dr["Hora"] = ls.HorarioInicial.ToString("hh:mm") + " - " + ls.HorarioFinal.ToString("hh:mm");
                        if (ls.Aluno != null)
                        {
                            dr["Aluno"] = ls.Aluno.Pessoa.Nome;
                            dr["CodigoAluno"] = ls.Aluno.Codigo;
                        }
                        if (objMatricula != null)
                        {
                            dr["Turma"] = objMatricula.Turma.ToString();
                            dr["Sala"] = objMatricula.Turma.Sala;
                        }
                        dr["codigoCompromisso"] = ls.Codigo;
                        dt.Rows.Add(dr);
                    }
                }

                gdvAtendimento.DataSource = dt;
                gdvAtendimento.DataBind();
            }
            catch (Exception)
            {

                throw;
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
            gdvAtendimento.DataSource = null;
            gdvAtendimento.DataBind();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Metodos Especificos
        private void CarregaFuncionarios()
        {
            Docente objDocente = new Docente();
            IList<Docente> lsDocente = objDocente.SelecionarClinicos();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
            dt.Columns.Add("Nome", Type.GetType("System.String"));

            foreach (Docente ls in lsDocente)
            {
                Pessoa objPessoa = new Pessoa();
                objPessoa = ls.Pessoa;
                DataRow dr = dt.NewRow();
                dr["Codigo"] = ls.Codigo;
                dr["Nome"] = objPessoa.Nome;
                dt.Rows.Add(dr);
            }
            ddlFuncionario.DataSource = dt;
            ddlFuncionario.DataTextField = "Nome";
            ddlFuncionario.DataValueField = "Codigo";
            ddlFuncionario.DataBind();
        }
        private void CarregaProfissao(Docente objDocente)
        {
            if (objDocente == null)
                objDocente = new Docente();

            objDocente = objDocente.Selecionar(objDocente.Codigo);
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
            dt.Columns.Add("Descricao", Type.GetType("System.String"));

            Profissao objProfissoa = new Profissao();
            objProfissoa = objDocente.Profissao;
            DataRow dr = dt.NewRow();
            dr["Codigo"] = objProfissoa.Codigo;
            dr["Descricao"] = objProfissoa.Descricao;
            dt.Rows.Add(dr);

            ddlProfissao.Items.Clear();
            ListItem itemProfissao = new ListItem();
            itemProfissao.Text = "(--Selecione--)";
            itemProfissao.Value = "0";
            ddlProfissao.Items.Add(itemProfissao);
            ddlProfissao.DataSource = dt;
            ddlProfissao.DataTextField = "Descricao";
            ddlProfissao.DataValueField = "Codigo";
            ddlProfissao.DataBind();
            ddlProfissao.SelectedIndex = 1;
        }
        private Docente RetornaDocente()
        {
            Usuario objUsuario = ((principal)this.Master).usuarioLogado;
            Docente objDocente = new Docente();
            Perfil objPerfil = objUsuario.Perfil;
            if (objPerfil.Descricao.ToUpper() != "COORDENADOR")
            {
                botao1.Desabilitar(true, false, true, false, true, true, true, true, false);
            }
            else
                ddlFuncionario.Enabled = true;
            return objDocente = objDocente.SelecionarPorPessoa(objUsuario.Pessoa);
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
                    Response.Redirect("../Cadastros/frmCadastrarAgendaAtendimento.aspx");
                    break;
                case "Pesquisar":
                    if (ddlFuncionario.SelectedIndex != 0)
                        this.Selecionar();
                    else
                        Mensagem1.Aviso("Selecione o Campo Funcionario");
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



        protected void calData_DayRender(object sender, DayRenderEventArgs e)
        {

            Agenda objAgenda = new Agenda();

            Docente objDocente = new Docente();
            objDocente = objDocente.Selecionar(Convert.ToInt32(ddlFuncionario.SelectedValue));
            Profissao objProfissao = new Profissao();
            objProfissao = objProfissao.Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));
            objDocente.Profissao = objProfissao;
            objAgenda.Data = e.Day.Date;

            objAgenda.Docente = objDocente;

            IList<Agenda> lsAgenda = objAgenda.SelecionarPorCriterio();
            foreach (Agenda lsAg in lsAgenda)
            {
                if (lsAg.Data == e.Day.Date)
                {
                    e.Cell.BackColor = System.Drawing.Color.Pink;
                }
            }
        }

        protected void gdvAtendimento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarAgendaAtendimento");
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
                if (objPermissao.Perfil.Descricao.ToUpper() != "COORDENADOR")
                {
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                    imgExcluir.Visible = false;

                    ImageButton imgTrocarHorario = (ImageButton)e.Row.FindControl("imgTrocarHorario");
                    imgTrocarHorario.Visible = false;
                }
            }
        }
    }
}
