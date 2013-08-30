using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Configuration;

namespace Web.Cadastros
{
    public partial class frmRequerimentoMatricula : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Requerimento de Matrícula");
            botao1.Desabilitar(true, true, false, false, false, true, true, true, false);
            BarraBotaoFiltro.Desabilitar(true, false, true, false, true, true, true, true, false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissao = ((principal)this.Master).Permissao("frmRequerimentoMatricula");
            if (objPermissao.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela Requerimento de Matrícula!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                CarregaComboUF();
                this.Selecionar();
            }
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            Usuario usuario = ((principal)this.Master).usuarioLogado;
            if (usuario != null)
            {
                Aluno aluno = new Aluno().SelecionarPorPessoa(usuario.Pessoa);
                if (aluno == null)
                {
                    Mensagem.Aviso("Usuário logado não está relacionado a um aluno. Usuário sem acesso!");
                }
                else
                {
                    txtNome.Text = aluno.Pessoa.Nome;
                    cbxSites.Checked = aluno.Sites;
                    cbxMedicar.Checked = aluno.Medicar;
                    txtCep.Text = aluno.Pessoa.Endereco.Cep.ToString();
                    txtEmail.Text = aluno.Pessoa.Email;
                    txtLogradouro.Text = aluno.Pessoa.Endereco.Logradouro;
                    txtNumero.Text = aluno.Pessoa.Predical.ToString();
                    txtBairro.Text = aluno.Pessoa.Endereco.Bairro;
                    txtCidade.Text = aluno.Pessoa.Endereco.Cidade.Descricao;
                    txtUF.Text = aluno.Pessoa.Endereco.Cidade.Estado.Sigla;
                    txtTelefone.Text = Comum.InsereMascaraTelefone(aluno.Pessoa.Telefone);
                    txtEmergencia.Text = Comum.InsereMascaraTelefone(aluno.Emergencia);
                    txtFalarCom.Text = aluno.Contato;
                    txtConvenioMedico.Text = aluno.ConvenioMedico;
                    txtTelefoneMedico.Text = Comum.InsereMascaraTelefone(aluno.TelefoneConvenio);
                    txtCarteirinha.Text = aluno.CarteirinhaConvenio;
                    txtAlergia.Text = aluno.Alergias;
                    txtMedicamentos.Text = aluno.Medicamentos;
                    txtComplemento.Text = aluno.Pessoa.Complemento;
                    IList<Parametro> parametro = new Parametro().Selecionar();
                    txtTermo.Text = parametro[0].TermoMatricula;
                }
            }
        }

        public bool Salvar()
        {
            bool retorno = false;
            Usuario usuario = ((principal)this.Master).usuarioLogado;
            if (usuario != null)
            {
                Aluno aluno = new Aluno().SelecionarPorPessoa(usuario.Pessoa);
                if (aluno == null)
                {
                    Mensagem.Aviso("Usuário logado não está relacionado a um aluno. Usuário sem acesso!");
                }
                else
                {
                    if (!cbxConcorda.Checked)
                    {
                        Mensagem.Aviso("Para finalizar o cadastro é preciso aceitar os termos da matrícula.");
                    }
                    else
                    {
                        aluno.Pessoa.Nome = txtNome.Text;
                        aluno.Sites = cbxSites.Checked;
                        aluno.Medicar = cbxMedicar.Checked;
                        Endereco endereco = new Endereco().SelecionarCep(Convert.ToInt32(Comum.RetiraMascaraCEP(txtCep.Text)));
                        aluno.Pessoa.Endereco = endereco;
                        if (txtComplemento.Text.Trim() != "")
                            aluno.Pessoa.Complemento = txtComplemento.Text;
                        aluno.Pessoa.Predical = Convert.ToInt32(txtNumero.Text);
                        if (txtEmail.Text != "")
                            aluno.Pessoa.Email = txtEmail.Text;
                        else
                            aluno.Pessoa.Email = null;
                        if (txtTelefone.Text.Trim() != "")
                            aluno.Pessoa.Telefone = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefone.Text));
                        else
                            aluno.Pessoa.Telefone = null;
                        aluno.Emergencia = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtEmergencia.Text));
                        aluno.Contato = txtFalarCom.Text;
                        aluno.ConvenioMedico = txtConvenioMedico.Text;
                        if (txtTelefoneMedico.Text.Trim() != "")
                            aluno.TelefoneConvenio = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefoneMedico.Text));
                        else
                            aluno.TelefoneConvenio = null;
                        aluno.CarteirinhaConvenio = txtCarteirinha.Text;
                        aluno.Alergias = txtAlergia.Text;
                        aluno.Medicamentos = txtMedicamentos.Text;
                        aluno.Pessoa.Complemento = txtComplemento.Text;
                        if (aluno.Situacao == "I")
                            aluno.Situacao = "A";
                        try
                        {
                            retorno = aluno.Confirmar();
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
                }
            }
            return retorno;
        }
        private void Imprimir()
        {
            Usuario usuario = ((principal)this.Master).usuarioLogado;
            if (usuario != null)
            {
                Aluno aluno = new Aluno().SelecionarPorPessoa(usuario.Pessoa);
                if (aluno == null)
                {
                    Mensagem.Aviso("Usuário logado não está relacionado a um aluno. Usuário sem acesso!");
                }
                else
                {
                    if (!cbxConcorda.Checked)
                    {
                        Mensagem.Aviso("Para finalizar o cadastro é preciso aceitar os termos da matrícula.");
                    }
                    else
                    {
                        string url = "frmRelatorioRequerimentoMatricula.aspx?Codigo="+usuario.Pessoa.Codigo;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "window.open('"+url+"','page','toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=800,height=800');", true);
                    }
                }
            }
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
            txtNome.Text = string.Empty;
            cbxSites.Checked = false;
            cbxMedicar.Checked = false;
            txtCep.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtLogradouro.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtUF.Text = string.Empty;
            txtTelefone.Text = string.Empty; ;
            txtEmergencia.Text = string.Empty;
            txtFalarCom.Text = string.Empty;
            txtConvenioMedico.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtCarteirinha.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtAlergia.Text = string.Empty;
            txtMedicamentos.Text = string.Empty;
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
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgImprimirOnClick += new botao.EventHandler(BarraBotao_Click);
            //Barra de botao da tela de modal
            this.BarraBotaoFiltro.imgVoltarOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
            this.BarraBotaoFiltro.imgLimparOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
            this.BarraBotaoFiltro.imgPesquisarOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
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
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Salvar":
                    if (this.Salvar()) 
                        Mensagem.Aviso("Atualização do cadastro do Aluno realizado com sucesso!");
                    break;
                case "Imprimir":
                    this.Imprimir();
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
            }
            else
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
        private void CarregaEndereco(int cep)
        {

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
            else
            {
                txtLogradouro.Text = string.Empty;
                txtCidade.Text = string.Empty;
                txtUF.Text = string.Empty;
                txtBairro.Text = string.Empty;
            }
        }
        private void CarregaComboUF()
        {
            IList<Estado> estados = new Estado().Selecionar();
            foreach (Estado estado in estados)
            {
                ddlUFFiltro.Items.Add(estado.Sigla);
            }
            ddlUFFiltro.DataBind();
        }
        
    }
}
