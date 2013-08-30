using System;
using System.Configuration;
using Model.Entidade;
using Web.Util;
namespace GEPEX.Cadastros
{
    public partial class frmCadastrarDisciplinas : BaseCadastro, Base
    {
        #region Eventos
        private static Disciplina objDisciplina;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Cadastro de Disciplina");
                botao1.Desabilitar(false, false, false, false, true, true, true, true, false);

                Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarDisciplinas");
                if (objPermissa.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Disciplina!');location.href='../Geral/index.aspx';</script>");
                }

                if (Request.QueryString["codigo"] != null)
                {
                    objDisciplina = new Disciplina();
                    this.Selecionar();
                }
                else
                {
                    objDisciplina = null;
                }
            }
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            try
            {
                int codigo = int.Parse(Request["codigo"]);

                objDisciplina = objDisciplina.Selecionar(codigo);

                txtDescricao.Text = objDisciplina.Descricao;
                txtDisciplina.Text = objDisciplina.Materia;

                if (objDisciplina.Situacao)
                    rdlStatus.Items[0].Selected = true;
                else
                    rdlStatus.Items[1].Selected = true;

                Id = codigo;
            }
            catch (Exception e)
            {
                objDisciplina = null;
                throw e;
  
            }
        }

        public bool Salvar()
        {
            //Disciplina objDisciplina = new Disciplina();
            if (objDisciplina == null)
                objDisciplina = new Disciplina();

            objDisciplina.Descricao = txtDescricao.Text;
            objDisciplina.Materia = txtDisciplina.Text;

            if (rdlStatus.Items[0].Selected)
                objDisciplina.Situacao = true;
            else
                objDisciplina.Situacao = false;

            bool retorno = false;
            try
            {
                retorno = objDisciplina.Confirmar();
                Id = objDisciplina.Codigo;
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem.Aviso(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool Alterar()
        {
            objDisciplina.Descricao = txtDescricao.Text;
            objDisciplina.Materia = txtDisciplina.Text;

            if (rdlStatus.Items[0].Selected)
                objDisciplina.Situacao = true;
            else
                objDisciplina.Situacao = false;

            bool retorno = false;
            try
            {
                retorno = objDisciplina.Confirmar();
                Id = objDisciplina.Codigo;
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem.Aviso(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool ValidarCamposObrigatorios()
        {
            throw new NotImplementedException();
        }

        public void Limpar()
        {
            txtDescricao.Text = string.Empty;
            txtDisciplina.Text = string.Empty;
            rdlStatus.Items[0].Selected = true;
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
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgSalvarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgNovoOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarDisciplinas.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarDisciplinas.aspx");
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarDisciplinas.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarDisciplinas");
                    if (objDisciplina != null)
                    {
                        if (objPermissa.Altera == true)
                        {
                            if (this.Alterar())
                                Mensagem.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                        }
                        else
                        {
                            Mensagem.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusao"].ToString());
                        }
                    }
                    else
                    {
                        if (objPermissa.Inclui == true)
                        {
                            if (this.Salvar())
                                Mensagem.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                        }
                        else
                        {
                            Mensagem.Aviso(ConfigurationManager.AppSettings["10_Permissao_Alteracao"].ToString());
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        
    }
}
