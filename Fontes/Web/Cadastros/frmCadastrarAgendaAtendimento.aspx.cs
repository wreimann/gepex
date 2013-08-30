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
using System.Text.RegularExpressions;

namespace Web.Cadastros
{
    public partial class frmCadastrarAgendaAtendimento : System.Web.UI.Page, Base
    {
        private static Agenda objAgenda;
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastro de Agenda de Atendimento");
            botao1.Desabilitar(false, false, false, true, true, true, true, true, false);


            if (!IsPostBack)
            {
                try
                {

                    Session["codCompromisso"] = null;
                    objAgenda = new Agenda();

                    Docente objDocente = this.RetornaDocente();
                    ddlFuncionario.SelectedValue = objDocente.Codigo.ToString();

                    this.CarregaFuncionarios();
                    this.CarregaProfissao(objDocente);
                    this.CarregaAluno();
                    this.CarregaEspecialidadesModal();

                    //Carrega a data atual no campo Data;
                    if (Session["calDate"] != null)
                        txtData.Text = Convert.ToDateTime(Session["calDate"]).ToString("dd/MM/yyyy");
                    else
                        txtData.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            //this.Selecionar();
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

                    this.LimpaCamposLabel();
                    ddlAluno.SelectedIndex = 0;
                }
                else
                {
                    ddlProfissao.Items.Clear();
                    ListItem itemProfissoa = new ListItem();
                    itemProfissoa.Text = "(--Selecione--)";
                    itemProfissoa.Value = "0";
                    ddlProfissao.Items.Add(itemProfissoa);
                    ddlProfissao.SelectedIndex = 0;

                    this.LimpaCamposLabel();
                    ddlAluno.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAluno.SelectedIndex != 0)
            {
                Aluno objAluno = new Aluno();
                objAluno.Codigo = Convert.ToInt32(ddlAluno.SelectedValue);
                this.LimpaCamposLabel();
                this.CarregaAtendimentos(objAluno);
            }
            else
            {
                this.LimpaCamposLabel();
            }
        }
        protected void gdvListaEspera_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            
            
            int codigo = Convert.ToInt32(gdvListaEspera.DataKeys[e.NewSelectedIndex].Values["Codigo"]);
            ModalPopupExtender1.Hide();
            ddlEspecialidades.SelectedIndex = 0;
            gdvListaEspera.DataBind(); 
            
            ddlAluno.SelectedValue = codigo.ToString();
            if (ddlAluno.SelectedIndex != 0)
            {
                Aluno objAluno = new Aluno();
                objAluno.Codigo = Convert.ToInt32(ddlAluno.SelectedValue);
                this.CarregaAtendimentos(objAluno);
            }

        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEspecialidades.SelectedIndex = 0;
            gdvListaEspera.DataBind(); 
            this.CarregaPlanejamentosListaEspera();
            this.ModalPopupExtender1.Show();
            
        }
        protected void lnkVoltar_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            ddlEspecialidades.SelectedIndex = 0;
            gdvListaEspera.DataSource = null;
            gdvListaEspera.DataBind();
            
        }

        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            return;
        }

        public bool Salvar()
        {
            bool result = false;
            if (Session["codCompromisso"] != null)
            {
                Mensagem1.Aviso("Não é permitido alterar um compromisso.");
                return false;
            }

            try
            {
                if (objAgenda == null)
                    objAgenda = new Agenda();

                bool boolAgenda = false;
                bool boolCompromisso = false;

                Docente objDocente = new Docente();
                objDocente = objDocente.Selecionar(Convert.ToInt32(ddlFuncionario.SelectedValue));
                Profissao objProfissao = new Profissao();
                objProfissao = objDocente.Profissao;
                Aluno objAluno = new Aluno();
                objAluno = objAluno.Selecionar(Convert.ToInt32(ddlAluno.SelectedValue));


                #region Grava Agenda
                objAgenda.Data = Convert.ToDateTime(txtData.Text);
                objAgenda.Docente = objDocente;
                objAgenda = objAgenda.SelecionarPorData();
                if (objAgenda == null)
                {
                    objAgenda = new Agenda();
                    objAgenda.Docente = objDocente;
                    try
                    {
                        objAgenda.Data = Convert.ToDateTime(txtData.Text);
                    }
                    catch (FormatException fe)
                    {
                        Mensagem1.Aviso("Data inválida.");
                        throw fe;
                    }
                    boolAgenda = objAgenda.Confirmar();
                }
                #endregion

                #region Grava Compromisso
                Compromisso objCompromisso = new Compromisso();
                objCompromisso.Aluno = objAluno;
                objCompromisso.Agenda = objAgenda;
                try
                {
                    objCompromisso.HorarioInicial = Convert.ToDateTime(objAgenda.Data.Date.ToString("dd/MM/yyyy") + " " + txtHorarioInicial.Text);
                    objCompromisso.HorarioFinal = Convert.ToDateTime(objAgenda.Data.Date.ToString("dd/MM/yyyy") + " " + txtHorarioFinal.Text);
                }
                catch (FormatException fe)
                {
                    Mensagem1.Aviso("Horário inválida.");
                    throw fe;
                }
                //verificar campo
                objCompromisso.Situacao = "M";
                objCompromisso.Motivo = string.Empty;
                //verificar campo
                boolCompromisso = objCompromisso.Confirmar();
                Session["codCompromisso"] = objCompromisso.Codigo;
                #endregion
                result = true;
                this.CarregaAtendimentos(objAluno);
            }
            catch (Model.Base.GepexException.EBancoDados)
            {
                Mensagem1.Aviso("Não é permitido a inclusão de um novo compromisso para a mesma especialidade na mesma data.");
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
            return result;
        }

        public bool Alterar()
        {
            return true;
        }

        public bool ValidarCamposObrigatorios()
        {
            throw new NotImplementedException();
        }

        public void Limpar()
        {
            ddlFuncionario.SelectedIndex = 0;
            ddlProfissao.SelectedIndex = 0;
            txtData.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtHorarioInicial.Text = string.Empty;
            txtHorarioFinal.Text = string.Empty;
            ddlAluno.SelectedIndex = 0;
            gdvAtendimentos.DataSource = null;
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
        private void CarregaAluno()
        {
            Aluno objAluno = new Aluno();
            IList<Aluno> lsAluno = objAluno.SelecionarMatriculados();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
            dt.Columns.Add("Nome", Type.GetType("System.String"));

            foreach (Aluno ls in lsAluno)
            {
                Pessoa objPessoa = new Pessoa();
                objPessoa = ls.Pessoa;
                DataRow dr = dt.NewRow();
                dr["Codigo"] = ls.Codigo;
                dr["Nome"] = objPessoa.Nome;
                dt.Rows.Add(dr);
            }
            ddlAluno.DataSource = dt;
            ddlAluno.DataTextField = "Nome";
            ddlAluno.DataValueField = "Codigo";
            ddlAluno.DataBind();

        }
        private void CarregaAtendimentos(Aluno objAluno)
        {
            try
            {
                objAluno = objAluno.Selecionar(objAluno.Codigo);
                Atendimento objAtendimento = new Atendimento();
                objAtendimento.Aluno = objAluno;

                objAtendimento.Profissao = new Profissao().Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));

                //Recupera os planejamentos
                PlanejamentoClinico objPlanejamentoClinico = new PlanejamentoClinico();
                objPlanejamentoClinico.Aluno = objAluno;
                objPlanejamentoClinico.Profissao = objAtendimento.Profissao;
                objPlanejamentoClinico.DataInicial = Convert.ToDateTime(txtData.Text);
                IList<PlanejamentoClinico> lsPlanejamento = objPlanejamentoClinico.SelecionarPorCriterio();

                if (lsPlanejamento.Count <= 0)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OnLoad", "alert('Aluno sem planejamento Clínico para o período da data selecionada.');", true);
                    

                int numeroAtendimentosPlanejados = 0;
                //Soma a quantidade de atendimentos planejados
                if(lsPlanejamento.Count > 0)
                    numeroAtendimentosPlanejados = lsPlanejamento[lsPlanejamento.Count - 1].NumeroAtendimento;
                lblQuantidadePlanejada.Text = numeroAtendimentosPlanejados.ToString();

                //Calculo do numero de atendimentos pendentes
                //numero atendimento planejado - numero atendimentos realizados
                int numeroAtendimentos = objAtendimento.NumeroAtendimentos();

                //Quantidade de atendimentos realizados.
                lblQuantidadeRealizada.Text = numeroAtendimentos.ToString();



                IList<Atendimento> lsAtendimentos = objAtendimento.SelecionarPorCriterio();
                if (lsAtendimentos.Count > 0)
                {
                    lblFuncionario.Text = lsAtendimentos[lsAtendimentos.Count - 1].Docente.Pessoa.Nome;
                    lblUltimoAtendimento.Text = lsAtendimentos[lsAtendimentos.Count - 1].DataHorarioFinal.ToString("dd/MM/yyyy");
                }
                else
                {
                    lblFuncionario.Text = string.Empty;
                    lblUltimoAtendimento.Text = string.Empty;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Data", Type.GetType("System.DateTime"));
                dt.Columns.Add("Horario", Type.GetType("System.String"));
                dt.Columns.Add("Funcionario", Type.GetType("System.String"));

                Profissao objProfissao = new Profissao().Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue)); ;

                //List<Agenda> lsAgenda = objAgenda.SelecionarAgendas(objProfissao, Convert.ToInt32(ddlAluno.SelectedValue));
                Docente objDocente = new Docente().Selecionar(Convert.ToInt32(ddlFuncionario.SelectedValue));
                List<Agenda> lsAgenda = objAgenda.SelecionarAgendas(objDocente, Convert.ToInt32(ddlAluno.SelectedValue));

                foreach (Agenda lsAg in lsAgenda)
                {
                    IList<Compromisso> lsCompromisso = lsAg.Compromissos;
                    foreach (Compromisso lsComp in lsCompromisso)
                    {
                        DateTime data = lsAg.Data;
                        string hora = lsComp.HorarioInicial.TimeOfDay.ToString() + " - " + lsComp.HorarioFinal.TimeOfDay.ToString();

                        DataRow dr = dt.NewRow();
                        dr["Data"] = data;
                        dr["Horario"] = hora;
                        dr["Funcionario"] = lsComp.Aluno.Pessoa.Nome;

                        dt.Rows.Add(dr);
                    }
                }
                lblNumeroAtendimentosPendentes.Text = dt.Rows.Count.ToString();
                lblAtendimentoPrevisto.Text = Convert.ToString(numeroAtendimentosPlanejados - dt.Rows.Count - numeroAtendimentos);

                gdvAtendimentos.DataSource = dt;
                gdvAtendimentos.DataBind();
            }
            catch (Exception )
            {
                //Response.Redirect("../Geral/frmErro.aspx");
            }
        }
        private void LimpaCamposLabel()
        {
            lblUltimoAtendimento.Text = string.Empty;
            lblFuncionario.Text = string.Empty;
            lblQuantidadePlanejada.Text = string.Empty;
            lblNumeroAtendimentosPendentes.Text = string.Empty;
            lblQuantidadeRealizada.Text = string.Empty;
            lblAtendimentoPrevisto.Text = string.Empty;
            gdvAtendimentos.DataSource = null;
            gdvAtendimentos.DataBind();
        }
        private void CarregaPlanejamentosListaEspera()
        {
            try
            {
                PlanejamentoClinico objPlanejamentoClinico = new PlanejamentoClinico();
                Profissao objProfissao = new Profissao();
                objProfissao = objProfissao.Selecionar(Convert.ToInt32(ddlEspecialidades.SelectedValue));
                objPlanejamentoClinico.Profissao = objProfissao;
                objPlanejamentoClinico.DataInicial = Convert.ToDateTime(txtData.Text);
                IList<PlanejamentoClinico> lsPlanejamento = objPlanejamentoClinico.SelecionarPorEspecialidade();

                if (lsPlanejamento.Count == 0) 
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OnLoad", "alert('Nenhum planejamento clínico foi localizado no período da data informada.');", true);
                    return;
                }


                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
                dt.Columns.Add("Aluno", Type.GetType("System.String"));
                dt.Columns.Add("QuantidadePlanejada", Type.GetType("System.String"));
                dt.Columns.Add("QuantidadeRealizada", Type.GetType("System.String"));
                dt.Columns.Add("QuantidadePrevista", Type.GetType("System.String"));

                foreach (PlanejamentoClinico ls in lsPlanejamento)
                {
                    DataRow dr = dt.NewRow();
                    dr["Codigo"] = ls.Aluno.Codigo;
                    dr["Aluno"] = ls.Aluno.Pessoa.Nome;
                    Atendimento objAtendimento = new Atendimento();
                    objAtendimento.Aluno = ls.Aluno;
                    objAtendimento.Profissao = new Profissao().Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));
                    //List<Agenda> lsAgenda = objAgenda.SelecionarAgendas(objAtendimento.Profissao, ls.Aluno.Codigo);
                    Docente objDocente = new Docente().Selecionar(Convert.ToInt32(ddlFuncionario.SelectedValue));
                    List<Agenda> lsAgenda = objAgenda.SelecionarAgendas(objDocente, ls.Aluno.Codigo);

                    //Soma a quantidade de atendimentos planejados
                    int numeroAtendimentosPlanejados = ls.NumeroAtendimento;

                    dr["QuantidadePlanejada"] = numeroAtendimentosPlanejados;
                    int numeroAtendimentos = objAtendimento.NumeroAtendimentos();
                    dr["QuantidadeRealizada"] = numeroAtendimentos;
                    dr["QuantidadePrevista"] = numeroAtendimentosPlanejados - lsAgenda.Count - numeroAtendimentos;

                    dt.Rows.Add(dr);
                }
                gdvListaEspera.DataSource = dt;
                gdvListaEspera.Visible = true;
                gdvListaEspera.DataBind();
            }
            catch (Exception)
            {
                //Response.Redirect("../Geral/frmErro.aspx");
            }

        }
        private void CarregaEspecialidadesModal()
        {
            Profissao objProfissao = new Profissao();
            ddlEspecialidades.DataSource = objProfissao.SelecionarAtivosClinico();
            ddlEspecialidades.DataTextField = "Descricao";
            ddlEspecialidades.DataValueField = "Codigo";
            ddlEspecialidades.DataBind();

        }
        
        private bool validaHorario()
        {
            bool valida = true;
            Regex r = new Regex(@"([0-1][0-9]|2[0-3]):[0-5][0-9]");
            Match horarioInicial = r.Match(txtHorarioInicial.Text);
            Match horarioFinal = r.Match(txtHorarioFinal.Text);
            if (!horarioInicial.Success)
            {
                Mensagem1.Aviso("Horário Inicial inválido.");
                valida = false ;
            }
            else
            {
                if (!horarioFinal.Success)
                {
                    Mensagem1.Aviso("Horário final inválido.");
                    valida= false;
                }
            }
            return valida;
        }
        private Docente RetornaDocente()
        {
            Usuario objUsuario = ((principal)this.Master).usuarioLogado;
            Docente objDocente = new Docente();
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
            this.botao1.imgSalvarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarAgendaAtendimento.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarAgendaAtendimento.aspx");
                    break;
                case "Salvar":

                    if (validaHorario())
                    {
                        if (this.Salvar())
                          Mensagem1.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());

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

        protected void txtData_TextChanged(object sender, EventArgs e)
        {
            this.LimpaCamposLabel();
            ddlAluno.SelectedIndex = 0;
        }



    }
}
