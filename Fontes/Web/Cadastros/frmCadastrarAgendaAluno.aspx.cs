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
    public partial class frmCadastrarAgendaAluno : System.Web.UI.Page, Base
    {
        private static AgendaAluno objAgendaAluno;
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastro de Agenda do Aluno");

            if (!IsPostBack)
            {
                Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarAgendaAluno");
                if (objPermissa.Perfil.Descricao.ToUpper() == "ALUNO")
                {
                    botao1.Desabilitar(false, false, true, false, true, true, true, true, false);   

                }
                else
                    botao1.Desabilitar(false, false, false, false, true, true, true, true, false);
                //seta a data atual no campo data.
                if(Session["dataAgendaAluno"] != null)
                    txtData.Text = Session["dataAgendaAluno"].ToString();
                else
                    txtData.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                //carrega todos os alunos cadastrados no sistema
                this.CarregaAluno();
                if (Request.QueryString["codigo"] != null)
                {
                    try
                    {
                        if (objAgendaAluno == null)
                            objAgendaAluno = new AgendaAluno();

                        this.Selecionar();
                    }
                    catch (Exception)
                    {
                        objAgendaAluno = null;                        
                    }
                }
                else
                {
                    objAgendaAluno = null;
                }
            }

        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            int codigo = Convert.ToInt32(Request.QueryString["codigo"]);

            if (objAgendaAluno == null)
                objAgendaAluno = new AgendaAluno();

            try
            {
                objAgendaAluno = objAgendaAluno.Selecionar(codigo);
                Aluno objAluno = new Aluno();
                objAluno = objAgendaAluno.Aluno;
                ddlAluno.SelectedValue = objAluno.Codigo.ToString();
                txtAnotacao.Text = objAgendaAluno.Recado;
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public bool Salvar()
        {
            bool result=false;
            try
            {
                if (objAgendaAluno == null)
                    objAgendaAluno = new AgendaAluno();
                
                Aluno objAluno = new Aluno();
                objAluno = objAluno.Selecionar(Convert.ToInt32(ddlAluno.SelectedValue));

                Usuario objUsuario = new Usuario();
                objUsuario = ((principal)this.Master).usuarioLogado;
                Docente objDocente = new Docente();
                if (objUsuario.Pessoa.Tipo == "D")
                {                    
                    objDocente = objDocente.SelecionarPorPessoa(objUsuario.Pessoa);
                    //objDocente = objDocente.Selecionar(2);//O código do Docente esta fixo, pois precisa ser recuperado por sessao
                }

                objAgendaAluno.Aluno = objAluno;
                objAgendaAluno.Docente = objDocente;
                objAgendaAluno.Data = Convert.ToDateTime(txtData.Text);
                objAgendaAluno.Recado = txtAnotacao.Text;

                result = objAgendaAluno.Confirmar();
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (Exception)
            {
                return result;
                throw;
            }
            return result;
        }

        public bool Alterar()
        {
            bool result = false;
            try
            {
                if (objAgendaAluno == null)
                    objAgendaAluno = new AgendaAluno();

                Aluno objAluno = new Aluno();
                objAluno = objAluno.Selecionar(Convert.ToInt32(ddlAluno.SelectedValue));

                Usuario objUsuario = new Usuario();
                objUsuario = ((principal)this.Master).usuarioLogado;
                Docente objDocente = new Docente();
                if (objUsuario.Pessoa.Tipo == "D")
                {                    
                    objDocente = objDocente.SelecionarPorPessoa(objUsuario.Pessoa);
                    //objDocente = objDocente.Selecionar(2);//O código do Docente esta fixo, pois precisa ser recuperado por sessao
                }

                objAgendaAluno.Aluno = objAluno;
                objAgendaAluno.Docente = objDocente;
                objAgendaAluno.Data = Convert.ToDateTime(txtData.Text);
                objAgendaAluno.Recado = txtAnotacao.Text;

                result = objAgendaAluno.Confirmar();
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (Exception)
            {
                return result;
                throw;
            }
            return result;
        }

        public bool ValidarCamposObrigatorios()
        {
            return true;
        }

        public void Limpar()
        {
            ddlAluno.SelectedIndex = 0;
            txtData.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtAnotacao.Text = string.Empty;
            objAgendaAluno = null;
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Metodos Especificos
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
                    Response.Redirect("../Cadastros/frmCadastrarAgendaAluno.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarAgendaAluno.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarAgendaAluno");
                    if (objAgendaAluno != null)
                    {
                        if (objPermissa.Altera == true)
                        {
                            if (this.Alterar())
                                Mensagem1.Aviso(ConfigurationManager.AppSettings["03_Alteracao"]);
                        }
                        else
                        {
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["10_Permissao_Alteração"]);
                        }
                    }
                    else
                    {
                        if (objPermissa.Inclui == true)
                        {
                            if (this.Salvar())
                                Mensagem1.Aviso(ConfigurationManager.AppSettings["01_Inclusao"]);
                        }
                        else
                        {
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusão"]);
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
    }
}
