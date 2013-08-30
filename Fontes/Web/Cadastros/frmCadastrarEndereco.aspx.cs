using System;
using System.Configuration;
using Model.Entidade;
using Web.Util;
using System.Collections.Generic;
using System.Data;
namespace GEPEX.Cadastros
{
    public partial class frmCadastrarEndereco : BaseCadastro, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Cadastro de Endereço");
                botao1.Desabilitar(false, false, false, true, true, true, true, true, false);

                Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarEndereco");
                if (objPermissa.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Endereço!');location.href='../Geral/index.aspx';</script>");
                }

                if (Request.QueryString["codigo"] != null)
                    this.Selecionar();
                CarregaCidade();
            }
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            try
            {
                Id = int.Parse(Request["codigo"]);
                Endereco objEndereco = new Endereco().Selecionar(Id);
                if (objEndereco != null)
                {
                    txtCEP.Text = objEndereco.Cep.ToString();
                    txtLogradouro.Text = objEndereco.Logradouro;
                    txtBairro.Text = objEndereco.Bairro;
                    ddlCidade.SelectedValue = objEndereco.Cidade.Codigo.ToString();
                }
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        public bool Salvar()
        {
            Endereco objEndereco = new Endereco();
            string cep = txtCEP.Text.Replace("-", "").Replace("_", "");
            objEndereco.Cep = Convert.ToInt32(cep);
            objEndereco.Logradouro = txtLogradouro.Text;
            objEndereco.Bairro = txtBairro.Text;
            objEndereco.Cidade = new Cidade().Selecionar(Convert.ToInt32(ddlCidade.SelectedValue));
            bool retorno = false;
            try
            {
                retorno = objEndereco.Confirmar();
                Id = objEndereco.Codigo;
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
            Endereco objEndereco = new Endereco().Selecionar(Id);
            string cep = txtCEP.Text.Replace("-", "").Replace("_", "");
            objEndereco.Cep = Convert.ToInt32(cep);
            objEndereco.Logradouro = txtLogradouro.Text;
            objEndereco.Bairro = txtBairro.Text;
            objEndereco.Cidade = new Cidade().Selecionar(Convert.ToInt32(ddlCidade.SelectedValue));
            bool retorno = false;
            try
            {
                retorno = objEndereco.Confirmar();
                Id = objEndereco.Codigo;
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
            txtCEP.Text = txtLogradouro.Text = txtBairro.Text = "";
            ddlCidade.SelectedValue = "0";
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

        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarEndereco.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarEndereco.aspx");
                    break;
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarEndereco.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarEndereco");
                    if (Id > 0)
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
        private void CarregaCidade()
        {
            IList<Cidade> listaCidade = new Cidade().Selecionar();

            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
            dt.Columns.Add("Descricao", Type.GetType("System.String"));

            foreach (Cidade cidade in listaCidade)
            {
                DataRow dr = dt.NewRow();
                dr["Codigo"] = cidade.Codigo;
                dr["Descricao"] = cidade.ToString();
                dt.Rows.Add(dr);
            }
            ddlCidade.DataSource = dt;
            ddlCidade.DataTextField = "Descricao";
            ddlCidade.DataValueField = "Codigo";
            ddlCidade.DataBind();

        }

    }
}
