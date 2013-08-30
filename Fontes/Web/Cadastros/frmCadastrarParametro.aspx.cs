using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Configuration;

namespace Web.Cadastros
{
    public partial class frmCadastrarParametro : System.Web.UI.Page,Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
           ((principal)this.Master).AlteraTitulo("Cadastro de Parâmetros");
            botao1.Desabilitar(true, true, false, true, true, true, true, true, false);
            BarraBotaoFiltro.Desabilitar(true, false, true, false, true, true, true, true, false);

            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarParametro");
            if (objPermissa.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Parâmetros!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack) {
                CarregaComboUF();
                this.Selecionar();
            }
            
        }
        #endregion
        private void CarregaComboUF()
        {
            IList<Estado> estados = new Estado().Selecionar();
            ddlUFFiltro.Items.Clear();
            foreach (Estado estado in estados)
            {
                ddlUFFiltro.Items.Add(estado.Sigla);
            }
            ddlUFFiltro.DataBind();
        }
        #region Metodos

        #region Base Members

        public void Selecionar()
        {
            try
            {
               IList<Parametro> parametro = new Parametro().Selecionar();
               foreach (Parametro param in parametro)
               {
                   txtNomeInstituicao.Text = param.Instituicao;
                   txtTelefone.Text = Comum.InsereMascaraTelefone(param.Telefone);
                   txtEmail.Text = param.Email;
                   if (param.Cnae != null)
                     txtCodigoCnae.Text = Comum.ColocarMascara(param.Cnae, "9999-9/99");
                   txtCnpj.Text = Comum.ColocarMascara(param.Cnpj, "99.999.999/9999-99");
                   txtMaxDiasAtendimento.Text = Convert.ToString(param.MaximoDiasAtendimento);
                   CarregaEndereco(param.Endereco.Cep);
                   txtNumero.Text = Convert.ToString(param.Predical);
                   txtComplemento.Text = param.Complemento;
                   txtTermo.Text = param.TermoMatricula;
               }
                
            }
            catch (Exception e){
                throw e;
            }
        }

        public bool Salvar()
        {
           Parametro parametro = new Parametro().Selecionar(1);
           if (parametro == null)
               parametro = new Parametro();
           parametro.Instituicao = txtNomeInstituicao.Text;
           parametro.Telefone = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefone.Text));
           parametro.Email = txtEmail.Text;
           parametro.Cnpj = Comum.RetiraMascara(txtCnpj.Text);
           if (Comum.RetiraMascaraCNAE(txtCodigoCnae.Text) != "") {
               parametro.Cnae = Comum.RetiraMascaraCNAE(txtCodigoCnae.Text);
           } else
               parametro.Cnae = null;
            parametro.MaximoDiasAtendimento = Convert.ToInt32(txtMaxDiasAtendimento.Text);
            
            //trata endereco
            Endereco endereco = new Endereco().SelecionarCep(Convert.ToInt32(Comum.RetiraMascaraCEP(txtCep.Text)));
            parametro.Endereco = endereco;
            if (txtComplemento.Text.Trim() != "")
                parametro.Complemento = txtComplemento.Text;
            else
                parametro.Complemento = null;
            parametro.Predical = Convert.ToInt32(txtNumero.Text);
            parametro.TermoMatricula = txtTermo.Text;
           bool retorno = false;

           try
           {
               retorno = parametro.Confirmar();
               
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
        public void Limpar()
        {

        }
        public bool Alterar()
        {
           Parametro parametro = new Parametro();

            parametro.Instituicao = txtNomeInstituicao.Text;
            parametro.Telefone = Convert.ToInt32(txtTelefone.Text);
            parametro.Email = txtEmail.Text;
            parametro.Cnpj = txtCnpj.Text;

            if (txtCodigoCnae.Text != null) {
                parametro.Cnae = txtCodigoCnae.Text;        
            }
            if (txtMaxDiasAtendimento.Text != null){
                parametro.MaximoDiasAtendimento = Convert.ToInt32(txtMaxDiasAtendimento.Text);
            }

            //trata endereco
            Endereco endereco = new Endereco().SelecionarCep(Convert.ToInt32(Comum.RetiraMascaraCEP(txtCep.Text)));
            parametro.Endereco = endereco;
            if (txtComplemento.Text.Trim() != "")
                parametro.Complemento = txtComplemento.Text;
            parametro.Predical = Convert.ToInt32(txtNumero.Text);
            bool retorno = false;

            try
            {
                retorno = parametro.Confirmar();
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
            return true;
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
            this.botao1.imgSalvarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            //tela modal
            this.BarraBotaoFiltro.imgVoltarOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
            this.BarraBotaoFiltro.imgLimparOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
            this.BarraBotaoFiltro.imgPesquisarOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    break;
                case "Pesquisar":                    
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarParametro");
                    if (objPermissa.Inclui == true)
                    {
                        if (Salvar())
                            Mensagem.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                    }
                    else
                    {
                        Mensagem.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusao"].ToString());
                    }
                    break;
                case "Voltar":
                    Response.Redirect("../Geral/index.aspx");
                    break;
                default:
                    break;
            }
        } 
                private void BarraBotaoFiltro_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Pesquisar":
                    this.CarregarGVEnderecos();
                    break;
                case "Limpar":
                    this.LimparFiltro();
                    break;
                case "Voltar":
                    ConsultaEnderecosModal.Hide();
                    break;
                default:
                    break;
            }
        }

        #endregion

        protected void CarregarGVEnderecos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cep", System.Type.GetType("System.String"));
            dt.Columns.Add("logradouro", System.Type.GetType("System.String"));
            dt.Columns.Add("bairro", System.Type.GetType("System.String"));
            dt.Columns.Add("cidade", System.Type.GetType("System.String"));
            dt.Columns.Add("Uf", System.Type.GetType("System.String"));
            if (txtLogradouroFiltro.Text.Trim() != "")
            {
                IList<Endereco> listaEndereco = new Endereco().SelecionarPorCriterios(txtLogradouroFiltro.Text, txtBairroFiltro.Text,
                                                                                       txtCidadeFiltro.Text, ddlUFFiltro.SelectedValue);
                if (listaEndereco.Count > 0)
                {
                    foreach (Endereco endereco in listaEndereco)
                    {
                        dt.Rows.Add(new String[] { Comum.InsereMascaraCEP(endereco.Cep) , endereco.Logradouro, 
                                               endereco.Bairro, endereco.Cidade.Descricao, endereco.Cidade.Estado.Sigla});
                    }
                    gdvEnderecoFiltro.DataSource = dt;
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OnLoad", "alert('Nenhum endereço foi localizado.');", true);
            }else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OnLoad", "alert('O campo logradouro é obrigatório para busca de endereços.');", true);   
               
            gdvEnderecoFiltro.DataBind();
            ConsultaEnderecosModal.Show();
        }
        
        public void LimparFiltro()
        {
            txtLogradouroFiltro.Text = string.Empty;
            txtBairroFiltro.Text = string.Empty;
            txtCidadeFiltro.Text = string.Empty;
            ddlUFFiltro.SelectedValue = "PR";
            gdvEnderecoFiltro.DataBind();
            ConsultaEnderecosModal.Show();
        }

        protected void gdvEnderecoFiltro_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string cep = Convert.ToString(gdvEnderecoFiltro.DataKeys[e.NewEditIndex].Values[0]);
            CarregaEndereco(Convert.ToInt32(cep.Replace("-", "")));
            ConsultaEnderecosModal.Hide();
            txtLogradouroFiltro.Text = string.Empty;
            txtBairroFiltro.Text = string.Empty;
            txtCidadeFiltro.Text = string.Empty;
            ddlUFFiltro.SelectedValue = "PR";
            gdvEnderecoFiltro.DataBind();
            
        }
        private void CarregaEndereco(int cep) {

            Endereco endereco = new Endereco().SelecionarCep(cep);
            txtCep.Text = Convert.ToString(cep);
            txtLogradouro.Text = endereco.Logradouro;
            txtCidade.Text = endereco.Cidade.Descricao;
            txtUF.Text = endereco.Cidade.Estado.Sigla;
            txtBairro.Text = endereco.Bairro;
        }

        protected void txtCep_TextChanged(object sender, EventArgs e)
        {
            string cep = txtCep.Text.Replace("-", "").Replace("_", "");
            if (cep != "")
            {    
                Endereco endereco = new Endereco().SelecionarCep(Convert.ToInt32(cep));
                if (endereco != null)
                {
                    txtLogradouro.Text = endereco.Logradouro;
                    txtCidade.Text = endereco.Cidade.Descricao;
                    txtUF.Text = endereco.Cidade.Estado.Sigla;
                    txtBairro.Text = endereco.Bairro;
                }
                else
                {
                    Mensagem.Aviso("Endereço não localizado.");
                    txtCep.Text = string.Empty;
                    txtLogradouro.Text = string.Empty;
                    txtCidade.Text = string.Empty;
                    txtUF.Text = string.Empty;
                    txtBairro.Text = string.Empty;
                }
            }
            else {
                txtLogradouro.Text = string.Empty;
                txtCidade.Text = string.Empty;
                txtUF.Text = string.Empty;
                txtBairro.Text = string.Empty;
            }
        }

    }
}


