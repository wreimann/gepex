using System;
using GEPEX;
using Web.Util;
using System.Configuration;
using Model.Entidade;
using Model.Base;

namespace Web.Cadastros
{
	public partial class frmAlterarSenha : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Alterar Senha");
                botao1.Desabilitar(true, true, false, false, true, true,true,true,false);

                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissa = ((principal)this.Master).Permissao("frmAlterarSenha");
                if (objPermissa.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Alterar Senha!');location.href='../Geral/index.aspx';</script>");
                }
            }         
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            throw new NotImplementedException();
        }

        public bool Salvar()
        {

            return true;
        }

        public bool Alterar()
        {
            bool retorno = false;
            Usuario usuario = new Usuario().Selecionar(((principal)this.Master).usuarioLogado.Codigo);
            if (usuario != null)
            {
                if (txtSenhaAtual.Text == CryptographyHelper.FromBase64(usuario.Senha))
                {
                    try
                    {
                        usuario.Senha = CryptographyHelper.ToBase64(txtSenha.Text);
                        retorno = usuario.Confirmar();
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
                }
                else
                    Mensagem.Aviso("Senha Atual não confere.");
            
            } 
            else
                Mensagem.Aviso("Usuário não está logado no sistema.");
            return retorno;
        }

        public bool ValidarCamposObrigatorios()
        {
            return true;
        }

        public void Limpar()
        {
            txtSenhaAtual.Text = 
            txtSenha.Text = 
            txtConfirma.Text = string.Empty;
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
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgSalvarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Limpar":
                    this.Limpar();
                    break;
                case "Voltar":
                    Response.Redirect("../Geral/index.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmAlterarSenha");
                    if (objPermissa.Altera == true)
                    {
                        if (this.Alterar())
                            Mensagem.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                    }
                    else
                    {
                        Mensagem.Aviso(ConfigurationManager.AppSettings["10_Permissao_Alteracao"]);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
