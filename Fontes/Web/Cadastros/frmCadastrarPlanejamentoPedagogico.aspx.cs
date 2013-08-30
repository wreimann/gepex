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
using System.Text;
using Model.Entidade;
using System.Collections.Generic;

namespace Web.Cadastros
{
    public partial class frmCadastrarPlanejamentoPedagogico : System.Web.UI.Page, Base
    {
        private static PlanejamentoPedagogico objPlanejamentoPedagogico;
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastrar Planejamento Pedagógico");
            botao1.Desabilitar(false, false, false, false, true, true, true, true,false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarPlanejamentoPedagogico");
            if (objPermissa.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Aluno!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                objPlanejamentoPedagogico = new PlanejamentoPedagogico();
                    CarregaComboAnoLetivo();
                    //Carrega as turmas ativas.
                    this.CarregaTurmas();
                    //Carrega as disciplinas ativas
                    this.CarregaDisciplinas();

                    if (Request.QueryString["codigo"] != null)
                    {
                        //Selecionar os dados do plano pedagogico
                        this.Selecionar();
                    }
                


            }

        }
        protected void btnAdicionarConteudo_Click(object sender, EventArgs e)
        {
            try
            {
                //Salva os dados do planejamento pedagogico
                this.Salvar();

                ConteudoPedagogico objConteudoPedagogico = new ConteudoPedagogico();
                if (Session["codigoConteudo"] != null)
                    objConteudoPedagogico.Codigo = Convert.ToInt32(Session["codigoConteudo"]);

                objConteudoPedagogico.Planejamento = objPlanejamentoPedagogico;
                objConteudoPedagogico.NumeroAulas = Convert.ToInt32(txtNumeroAulas.Text);
                objConteudoPedagogico.DataInicial = DateTime.Now.Date;
                objConteudoPedagogico.DataFinal = DateTime.Now.Date;
                objConteudoPedagogico.ObjetivoEspecifico = txtObjetivoEspecifico.Text;
                objConteudoPedagogico.Conteudo = txtConteudo.Text;
                objConteudoPedagogico.Metodo = txtMetodo.Text;

                objConteudoPedagogico.Confirmar();

            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
            finally
            {
                //Limpa os campos apos adicionar no gridview
                txtNumeroAulas.Text = string.Empty;
                txtConteudoDataInicial.Text = string.Empty;
                txtConteudoDataFinal.Text = string.Empty;
                txtObjetivoEspecifico.Text = string.Empty;
                txtConteudo.Text = string.Empty;
                txtMetodo.Text = string.Empty;
                this.CarregaConteudoPedagogico();
                Session["codigoConteudo"] = null;
            }
        }
        protected void imgAdicionar_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                if (ValidaDatas())
                {
                    //Salva os dados do planejamento pedagogico
                    this.Salvar();

                    ConteudoPedagogico objConteudoPedagogico = new ConteudoPedagogico();
                    if (Session["codigoConteudo"] != null)
                        objConteudoPedagogico.Codigo = Convert.ToInt32(Session["codigoConteudo"]);

                    objConteudoPedagogico.Planejamento = objPlanejamentoPedagogico;
                    objConteudoPedagogico.NumeroAulas = Convert.ToInt32(txtNumeroAulas.Text);
                    objConteudoPedagogico.DataInicial = DateTime.Now.Date;
                    objConteudoPedagogico.DataFinal = DateTime.Now.Date;
                    objConteudoPedagogico.ObjetivoEspecifico = txtObjetivoEspecifico.Text;
                    objConteudoPedagogico.Conteudo = txtConteudo.Text;
                    objConteudoPedagogico.Metodo = txtMetodo.Text;

                    objConteudoPedagogico.Confirmar();
                }

            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
            finally
            {
                //Limpa os campos apos adicionar no gridview
                txtNumeroAulas.Text = string.Empty;
                txtConteudoDataInicial.Text = string.Empty;
                txtConteudoDataFinal.Text = string.Empty;
                txtObjetivoEspecifico.Text = string.Empty;
                txtConteudo.Text = string.Empty;
                txtMetodo.Text = string.Empty;
                this.CarregaConteudoPedagogico();
                Session["codigoConteudo"] = null;
            }


        }

        protected void gdvConteudoProgramaticoPedagogico_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ConteudoPedagogico objConteudoPedagogico = new ConteudoPedagogico();
            objConteudoPedagogico.Codigo = Convert.ToInt32(gdvConteudoProgramaticoPedagogico.DataKeys[e.RowIndex].Values[0]);
            try
            {
                objConteudoPedagogico.Excluir(objConteudoPedagogico.Codigo);
            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
            finally
            {
                this.CarregaConteudoPedagogico();
            }
        }

        protected void gdvConteudoProgramaticoPedagogico_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvConteudoProgramaticoPedagogico.DataKeys[e.NewEditIndex].Values[0]);
            ConteudoPedagogico objConteudoPedagogico = new ConteudoPedagogico();
            try
            {
                objConteudoPedagogico = objConteudoPedagogico.Selecionar(codigo);
                txtNumeroAulas.Text = objConteudoPedagogico.NumeroAulas.ToString();
                txtConteudoDataInicial.Text = objConteudoPedagogico.DataInicial.ToString("dd/MM/yyyy");
                txtConteudoDataFinal.Text = objConteudoPedagogico.DataFinal.ToString("dd/MM/yyyy");
                txtObjetivoEspecifico.Text = objConteudoPedagogico.ObjetivoEspecifico;
                txtConteudo.Text = objConteudoPedagogico.Conteudo;
                txtMetodo.Text = objConteudoPedagogico.Metodo;
                Session["codigoConteudo"] = objConteudoPedagogico.Codigo;
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
                Mensagem1.Aviso(ex.ToString());
            }
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            int codigo = Convert.ToInt32(Request.QueryString["codigo"]);

            try
            {
                objPlanejamentoPedagogico = objPlanejamentoPedagogico.Selecionar(codigo);
                Turma objTurma = new Turma();
                objTurma = objPlanejamentoPedagogico.Turma;
                ddlTurma.SelectedValue = objTurma.Codigo.ToString();
                Disciplina objDisciplina = new Disciplina();
                objDisciplina = objPlanejamentoPedagogico.Disciplina;
                ddlDisciplina.SelectedValue = objDisciplina.Codigo.ToString();
                txtDataInicial.Text = objPlanejamentoPedagogico.DataInicial.ToString("dd/MM/yyyy");
                txtDataFinal.Text = objPlanejamentoPedagogico.DataFinal.ToString("dd/MM/yyyy");
                txtCargaHoraria.Text = objPlanejamentoPedagogico.CargaHoraria.ToString();
                txtEmenta.Text = objPlanejamentoPedagogico.Ementa;
                txtCompetencias.Text = objPlanejamentoPedagogico.CompetenciaHabilidades;
                txtObjetivoGeralDisciplina.Text = objPlanejamentoPedagogico.ObjetivoGeral;

                //Carrega o grid view com o conteudo pedagogico.
                this.CarregaConteudoPedagogico();
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
                Mensagem1.Aviso(ex.ToString());
            }

        }

        public bool Salvar()
        {
            Turma objTurma = new Turma();
            Disciplina objDisciplina = new Disciplina();
            bool retorno = false;
            try
            {

                if (objPlanejamentoPedagogico == null)
                    objPlanejamentoPedagogico = new PlanejamentoPedagogico();

                objTurma = objTurma.Selecionar(Convert.ToInt32(ddlTurma.SelectedValue));
                objPlanejamentoPedagogico.Turma = objTurma;
                objDisciplina = objDisciplina.Selecionar(Convert.ToInt32(ddlDisciplina.SelectedValue));
                objPlanejamentoPedagogico.Disciplina = objDisciplina;
                objPlanejamentoPedagogico.Ementa = txtEmenta.Text;
                objPlanejamentoPedagogico.CompetenciaHabilidades = txtCompetencias.Text;
                objPlanejamentoPedagogico.ObjetivoGeral = txtObjetivoGeralDisciplina.Text;
                objPlanejamentoPedagogico.CargaHoraria = Convert.ToInt32(txtCargaHoraria.Text);
                objPlanejamentoPedagogico.DataCadastro = DateTime.Now.Date;

                //Rever os campos da tela com do banco de dados
                objPlanejamentoPedagogico.DataInicial = Convert.ToDateTime(txtDataInicial.Text);
                objPlanejamentoPedagogico.DataFinal = Convert.ToDateTime(txtDataFinal.Text);

                retorno = objPlanejamentoPedagogico.Confirmar();

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
                Mensagem1.Aviso(ex.ToString());
            }
            finally
            {
                objTurma = null;
                objDisciplina = null;
            }
            return retorno;
        }

        public bool Alterar()
        {
            Turma objTurma = new Turma();
            Disciplina objDisciplina = new Disciplina();
            bool retorno = false;
            try
            {

                if (objPlanejamentoPedagogico == null)
                    objPlanejamentoPedagogico = new PlanejamentoPedagogico();

                objTurma = objTurma.Selecionar(Convert.ToInt32(ddlTurma.SelectedValue));
                objPlanejamentoPedagogico.Turma = objTurma;
                objDisciplina = objDisciplina.Selecionar(Convert.ToInt32(ddlDisciplina.SelectedValue));
                objPlanejamentoPedagogico.Disciplina = objDisciplina;
                objPlanejamentoPedagogico.Ementa = txtEmenta.Text;
                objPlanejamentoPedagogico.CompetenciaHabilidades = txtCompetencias.Text;
                objPlanejamentoPedagogico.ObjetivoGeral = txtObjetivoGeralDisciplina.Text;
                objPlanejamentoPedagogico.CargaHoraria = Convert.ToInt32(txtCargaHoraria.Text);
                objPlanejamentoPedagogico.DataCadastro = DateTime.Now.Date;

                //Rever os campos da tela com do banco de dados
                objPlanejamentoPedagogico.DataInicial = Convert.ToDateTime(txtDataInicial.Text);
                objPlanejamentoPedagogico.DataFinal = Convert.ToDateTime(txtDataFinal.Text);

                retorno = objPlanejamentoPedagogico.Confirmar();

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
                Mensagem1.Aviso(ex.ToString());
            }
            finally
            {
                objTurma = null;
                objDisciplina = null;
            }
            return retorno;
        }

        public bool ValidarCamposObrigatorios()
        {
            return true;
        }

        public void Limpar()
        {
            ddlTurma.SelectedIndex = 0;            
            ddlDisciplina.SelectedIndex = 0;
            txtCargaHoraria.Text = string.Empty;
            txtDataInicial.Text = string.Empty;
            txtDataFinal.Text = string.Empty;
            txtEmenta.Text = string.Empty;
            txtCompetencias.Text = string.Empty;
            txtObjetivoGeralDisciplina.Text = string.Empty;
            txtNumeroAulas.Text = string.Empty;
            txtConteudoDataInicial.Text = string.Empty;
            txtConteudoDataFinal.Text = string.Empty;
            txtObjetivoEspecifico.Text = string.Empty;
            txtConteudo.Text = string.Empty;
            txtMetodo.Text = string.Empty;
            gdvConteudoProgramaticoPedagogico.DataSource = null;
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Metodos Especificos

        private void CarregaComboAnoLetivo()
        {
            ddlAnoLetivo.DataTextField = "AnoLetivo";
            ddlAnoLetivo.DataValueField = "AnoLetivo";
            Turma turma = new Turma();
            ddlAnoLetivo.DataSource = turma.ObterAnoLetivo();
            ddlAnoLetivo.DataBind();
        }
        private void CarregaTurmas()
        {
            ddlTurma.Items.Clear();
            ListItem itemDefault = new ListItem("(--Selecione--)", "0");
            itemDefault.Selected = true;
            ddlTurma.Items.Add(itemDefault);
            if (ddlAnoLetivo.Text != "")
            {
                IList<Turma> lista = new Turma().SelecionarPorAnoLetivo(Convert.ToInt32(ddlAnoLetivo.Text));
                foreach (Turma turma in lista)
                {
                    ListItem item = new ListItem();
                    item.Value = turma.Codigo.ToString();
                    item.Text = turma.ToString();
                    item.Selected = false;
                    ddlTurma.Items.Add(item);
                }
            }
            ddlTurma.DataBind();
        }
        private void CarregaDisciplinas()
        {
            Disciplina objDisciplinas = new Disciplina();
            try
            {
                IList<Disciplina> lsDisciplinas = objDisciplinas.SelecionarAtivos();
                ddlDisciplina.DataSource = lsDisciplinas;
                ddlDisciplina.DataTextField = "Materia";
                ddlDisciplina.DataValueField = "Codigo";
                ddlDisciplina.DataBind();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                objDisciplinas = null;
            }
        }
        private void CarregaConteudoPedagogico()
        {
            ConteudoPedagogico objConteudoPedagogico = new ConteudoPedagogico();
            objConteudoPedagogico.Planejamento = objPlanejamentoPedagogico;
            IList<ConteudoPedagogico> lsConteudoPedagogico = objConteudoPedagogico.SelecionarPorCriterio();

            gdvConteudoProgramaticoPedagogico.DataSource = lsConteudoPedagogico;
            gdvConteudoProgramaticoPedagogico.DataBind();
        }
        private bool ValidaDatas()
        {
            Nullable<DateTime> dtaInicial = null;
            Nullable<DateTime> dtaFinal = null;
            if (txtDataInicial.Text != "__/__/____")
                dtaInicial = Convert.ToDateTime(txtDataInicial.Text);

            if (txtDataFinal.Text != "__/__/____")
                dtaFinal = Convert.ToDateTime(txtDataFinal.Text);
            
            Nullable<DateTime> dtaIncialConteudo = null;
            Nullable<DateTime> dtaFinalConteudo = null;
            if (txtConteudoDataInicial.Text != "__/__/____")
                dtaIncialConteudo = Convert.ToDateTime(txtConteudoDataInicial.Text);
            if(txtConteudoDataInicial.Text != "__/__/____")
                dtaFinalConteudo = Convert.ToDateTime(txtConteudoDataInicial.Text);
            bool valida = true;
            if (dtaInicial > dtaFinal)
            {
                Mensagem1.Aviso("A data Inicial não pode ser maior que a data final.");
                valida = false;
                return valida;
            }
            if (dtaFinal < dtaInicial)
            {
                Mensagem1.Aviso("A data Inicial não pode ser maior que a data final.");
                valida = false;
                return valida;
            }

            if (dtaIncialConteudo > dtaFinalConteudo)
            {
                Mensagem1.Aviso("A data Inicial do conteudo não pode ser maior que a data final do Conteudo.");
                valida = false;
                return valida;
            }
            if (dtaFinalConteudo < dtaIncialConteudo)
            {
                Mensagem1.Aviso("A data Final do conteudo não pode ser maior que a data incial do conteudos.");
                valida = false;
                return valida;
            }
            if (dtaIncialConteudo > dtaInicial)
            {
                Mensagem1.Aviso("A data Inicial do conteudo não pode ser maior que a data incial do planejamento.");
                valida = false;
                return valida;
            }
            if (dtaFinalConteudo > dtaFinal)
            {
                Mensagem1.Aviso("A data final do conteudo não pode ser maior que a data final do planejamento.");
                valida = false;
                return valida;
            }
            
            return valida;

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
            this.botao1.imgSalvarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarPlanejamentoPedagogico.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarPlanejamentoPedagogico.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarPlanejamentoPedagogico");
                    if (objPlanejamentoPedagogico.Codigo != 0)
                    {
                        if (objPermissa.Altera == true)
                        {
                            if (ValidaDatas())
                            {
                                if (this.Alterar())
                                    Mensagem1.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (objPermissa.Inclui == true)
                        {
                            if (ValidaDatas())
                            {
                                if (this.Salvar())
                                    Mensagem1.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                            }
                        }
                    }
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

        protected void ddlAnoLetivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTurma.SelectedIndex = 0;
            CarregaTurmas();
        }



    }
}
