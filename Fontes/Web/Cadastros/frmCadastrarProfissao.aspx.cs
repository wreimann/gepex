using System;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Configuration;

namespace Web.Cadastros
{
    public partial class frmCadastrarProfissao : BaseCadastro, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastro de Profissões");
            botao1.Desabilitar(false, false, false, false, true, true, true, true, false);

            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarProfissao");
            if (objPermissa.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de cadastro de profissão!');location.href='../Geral/index.aspx';</script>");
            }
            
            if (!IsPostBack)
            {
                if (Request.QueryString["codigo"] != null)
                    this.Selecionar();
                else
                    this.Limpar();
                
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
                Profissao profissao = new Profissao().Selecionar(codigo);
                txtDescricao.Text = profissao.Descricao;
                ddlTipo.SelectedValue = profissao.Tipo;
                if (profissao.Situacao)
                    rdlSituacao.Items[0].Selected = true;
                else
                    rdlSituacao.Items[1].Selected = true;

                Id = codigo;
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Salvar()
        {
            Profissao profissao = new Profissao();
            profissao.Descricao = txtDescricao.Text;
            profissao.Tipo = ddlTipo.SelectedValue;
            profissao.Situacao = rdlSituacao.Items[0].Selected; 
            bool retorno = false;
            try
            {
                retorno = profissao.Confirmar();
                Id = profissao.Codigo;
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool Alterar()
        {
            Profissao profissao = new Profissao();
            profissao.Codigo =  Convert.ToInt32(Id);
            profissao.Descricao = txtDescricao.Text;
            profissao.Tipo = ddlTipo.SelectedValue;
            profissao.Situacao = rdlSituacao.Items[0].Selected;
            bool retorno = false;
            try
            {
                retorno = profissao.Confirmar();
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool ValidarCamposObrigatorios()
        {
            return true;
        }

        public void Limpar()
        {
            txtDescricao.Text = string.Empty;
            ddlTipo.SelectedIndex = 0;
            rdlSituacao.Items[0].Selected = true;
            rdlSituacao.Items[1].Selected = false;
            Id = -1;
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
                    this.Limpar();
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarProfissao.aspx");
                    break;
                case "Salvar":
                    int codigo = Convert.ToInt32(Id);
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarProfissao");
                    if (codigo > 0)
                    {
                        if (objPermissa.Altera == true)
                        {
                            if (this.Alterar())
                                Mensagem.Aviso("Registro alterado com sucesso!");
                        }
                        else
                        {
                            Mensagem.Aviso(ConfigurationManager.AppSettings["10_Permissao_Alteração"]);
                        }
                    }
                    else
                    {
                        if (objPermissa.Inclui == true)
                        {
                            if (this.Salvar())
                                Mensagem.Aviso("Registro cadastrado com sucesso!");
                        }
                        else
                        {
                            Mensagem.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusão"]);
                        }
                    }
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarProfissao.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
