using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using GEPEX;
using Model.Base;
using Model.Entidade;
using Web.Util;
using System.Web.UI.WebControls;

namespace Web.Cadastros
{
    public partial class frmCadastrarUsuario : BaseCadastro, Base
    {
        #region Metodos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastro de Usuário");
            botao1.Desabilitar(false, false, false, false, true, true, true, true, false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissao = ((principal)this.Master).Permissao("frmCadastrarUsuario");
            if (objPermissao.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Usuários!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                CarregaComboPerfil();
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
            txtSenha.Attributes.Remove("value");
            txtConfirma.Attributes.Remove("value");
            int codigo = int.Parse(Request["codigo"]);
            Usuario usuario = new Usuario().Selecionar(codigo);
            if (usuario != null)
            {
                txtNome.Text = usuario.Pessoa.Nome;
                if (usuario.Pessoa.Tipo == "D")
                    rdlTipo.Items[0].Selected = true;
                else
                    rdlTipo.Items[1].Selected = true;
                
                switch(usuario.Situacao){
                    case("A"):
                        rdlSituacao.Items[0].Selected = true;
                        break;
                    case ("I"):
                        rdlSituacao.Items[1].Selected = true;
                        break;
                    case ("B"):
                        rdlSituacao.Items[2].Selected = true;
                        break;
                }
                txtLogin.Text = usuario.Login;
                txtEmail.Text = usuario.Pessoa.Email;
                ddlPerfil.SelectedValue = Convert.ToString(usuario.Perfil.Codigo);
                txtSenha.Attributes.Add("value", CryptographyHelper.FromBase64(usuario.Senha));
                txtConfirma.Attributes.Add("value", CryptographyHelper.FromBase64(usuario.Senha));
                txtMotivo.Text = usuario.Motivo;
                Id = codigo;
            }
        }

        public bool Salvar()
        {
            Usuario usuario = new Usuario();
            Perfil perfil = new Perfil();
            usuario.Perfil = perfil.Selecionar(Convert.ToInt32(ddlPerfil.SelectedValue));
            Pessoa pessoa = new Pessoa();
            usuario.Pessoa = pessoa.Selecionar(Convert.ToInt32(hfdNome.Value)); 
            usuario.Login = txtLogin.Text;
            usuario.Pessoa.Email = txtEmail.Text;
            usuario.Senha = CryptographyHelper.ToBase64(txtSenha.Text);
            usuario.Motivo = txtMotivo.Text;
            usuario.Situacao = rdlSituacao.SelectedValue;
            bool retorno = false;
            try
            {
                retorno = usuario.Confirmar();
                Id = usuario.Codigo;
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
            txtSenha.Attributes.Add("value", CryptographyHelper.FromBase64(usuario.Senha));
            txtConfirma.Attributes.Add("value", CryptographyHelper.FromBase64(usuario.Senha));
            return retorno;
        }

        public bool Alterar()
        {
            int codigo = Convert.ToInt32(Id);
            Usuario usuario = new Usuario().Selecionar(codigo);
            Perfil perfil = new Perfil();        
            usuario.Perfil = perfil.Selecionar(Convert.ToInt32(ddlPerfil.SelectedValue));
            usuario.Pessoa.Nome = txtNome.Text;
            usuario.Login = txtLogin.Text;
            usuario.Pessoa.Email = txtEmail.Text;
            usuario.Senha = CryptographyHelper.ToBase64(txtSenha.Text);
            usuario.Motivo = txtMotivo.Text;
            usuario.Situacao = rdlSituacao.SelectedValue;
            bool retorno = false;
            try
            {
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
            txtSenha.Attributes.Add("value", CryptographyHelper.FromBase64(usuario.Senha));
            txtConfirma.Attributes.Add("value", CryptographyHelper.FromBase64(usuario.Senha));
            return retorno;
        }

        public bool ValidarCamposObrigatorios()
        {
            throw new NotImplementedException();
        }

        public void Limpar()
        {
            txtNome.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtConfirma.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlPerfil.SelectedIndex = 0;
            rdlSituacao.Items[0].Selected = true;
            rdlSituacao.Items[1].Selected = false;
            rdlSituacao.Items[2].Selected = false;
            rdlTipo.Items[0].Selected = false;
            rdlTipo.Items[1].Selected = false;
            txtMotivo.Text = string.Empty;
            hfdNome.Value = string.Empty;
            txtSenha.Attributes.Remove("value");
            txtConfirma.Attributes.Remove("value");
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
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    this.Limpar();
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarUsuario.aspx");
                    break;
                case "Salvar":
                    int codigo = Convert.ToInt32(Id);
                    Permissao objPermissao = ((principal)this.Master).Permissao("frmCadastrarUsuario");

                    if (codigo > 0)
                    {
                        if (objPermissao.Altera == true)
                        {
                            if (this.Alterar())
                                Mensagem.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                        }
                        else
                        {
                            Mensagem.Aviso(ConfigurationManager.AppSettings["10_Permissao_Alteracao"].ToString());
                        }
                    }
                    else
                    {
                        if (objPermissao.Inclui == true)
                        {
                            if (this.Salvar())
                                Mensagem.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                        }
                        else
                        {
                            Mensagem.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusao"].ToString());
                        }
                    }
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarUsuario.aspx");
                    break;
                default:
                    break;
            }
            
        }

        #endregion

        private void CarregaComboPerfil()
        {
            ddlPerfil.Items.Clear();
            Perfil perfil = new Perfil();
            IList<Perfil> lista = perfil.Selecionar();
            ddlPerfil.DataSource = lista;
            ddlPerfil.DataValueField = "Codigo";
            ddlPerfil.DataTextField = "Descricao";
            ddlPerfil.DataBind();
          
        }
        protected void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtNome.Text.Trim() != "")
            {
                Pessoa pessoa = new Pessoa().SelecionarPorNome(txtNome.Text);
                if (pessoa != null)
                {
                    rdlTipo.SelectedValue = pessoa.Tipo;
                    txtEmail.Text = pessoa.Email;
                    hfdNome.Value = pessoa.Codigo.ToString();
                }
                else
                {
                    hfdNome.Value = string.Empty;
                    txtEmail.Text = string.Empty;
                    rdlTipo.Items[0].Selected = false;
                    rdlTipo.Items[1].Selected = false;
                    ScriptManager.RegisterStartupScript(upnNome, upnNome.GetType(), "scriptAjax", 
                        "alert('" + txtNome.Text + " não foi localizado no cadastro de Docente ou Aluno.');", true);
                    hfdNome.Value = string.Empty;
                }

            }
            else 
            {
                hfdNome.Value = string.Empty;
                txtNome.Text = string.Empty;
                txtEmail.Text = string.Empty;
                rdlTipo.Items[0].Selected = false;
                rdlTipo.Items[1].Selected = false;
            }
        }

    }
}
