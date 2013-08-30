using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Collections.Generic;

namespace Web.Cadastros
{
    public partial class frmCadastrarAluno : BaseCadastro, Base
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
           ((principal)this.Master).AlteraTitulo("Cadastro de Aluno");
            botao1.Desabilitar(false, false, false, true, true, true, true, true, false);
            BarraBotaoFiltro.Desabilitar(true, false, true, false, true, true, true, true, false);

            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarAluno");
            if (objPermissa.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Aluno!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                CarregaComboCorRaca();
                CarregaComboTipoDocumento();
                CarregaComboUF();
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
                txtDataNascimento.TextChanged += null;
                Id = int.Parse(Request["codigo"]); 
                Aluno aluno = new Aluno().Selecionar(Id);
                if (aluno != null)
                {
                    txtNome.Text = aluno.Pessoa.Nome;
                    txtMatricula.Text = Convert.ToString(aluno.Matricula);
                    ddlSituacao.SelectedValue = aluno.Situacao;
                    ddlSexo.SelectedValue = aluno.Pessoa.Sexo;
                    txtDataNascimento.Text = Convert.ToString(aluno.Pessoa.DataNascimento);
                    txtIdade.Text = Convert.ToString(Comum.CalculaIdade(aluno.Pessoa.DataNascimento));
                    txtTelefone.Text = Comum.InsereMascaraTelefone(aluno.Pessoa.Telefone);
                    txtEmergencia.Text = Comum.InsereMascaraTelefone(aluno.Emergencia);
                    txtFalarCom.Text = aluno.Contato;
                    txtEmail.Text = aluno.Pessoa.Email;
                    txtPai.Text = aluno.Pai;
                    txtMae.Text = aluno.Mae;
                    cbxSites.Checked = aluno.Sites;
                    cbxMedicar.Checked = aluno.Medicar;
                    cbxBolsaFamilia.Checked = aluno.BolsaFamilia;
                    txtOutrosBeneficios.Text = aluno.OutrosBeneficios;
                    txtOutrosTransportes.Text = aluno.OutrosTransportes;
                    txtConvenioMedico.Text = aluno.ConvenioMedico;
                    txtTelefoneMedico.Text = Comum.InsereMascaraTelefone(aluno.TelefoneConvenio);
                    txtCarteirinha.Text = aluno.CarteirinhaConvenio;
                    txtAlergia.Text = aluno.Alergias;
                    txtMedicamentos.Text = aluno.Medicamentos;
                    txtObservacao.Text = aluno.Observacao;
                    CarregaEndereco(aluno.Pessoa.Endereco.Cep);
                    txtNumero.Text = Convert.ToString(aluno.Pessoa.Predical);
                    txtComplemento.Text = aluno.Pessoa.Complemento;
                    txtCopel.Text = Convert.ToString(aluno.Pessoa.IdentificacaoCopel);
                    txtPeso.Text = String.Format("{0:f}", aluno.Peso);
                    txtAltura.Text = String.Format("{0:f}", aluno.Altura);
                    txtIMC.Text = string.Format("{0:f}", Comum.CalculaIMC(aluno.Altura, aluno.Peso));
                    ddlEstadoCivil.SelectedValue = aluno.Pessoa.EstadoCivil;
                    if (aluno.Pessoa.CorRaca != null)
                        ddlCorRaca.SelectedValue = aluno.Pessoa.CorRaca.Codigo.ToString();
                    ddlNacionalidade.SelectedValue = aluno.Pessoa.Nacionalidade;
                    txtNaturalidade.Text = aluno.Pessoa.Naturalidade;
                    if (aluno.FatorRH != "")
                        ddlFatorRH.SelectedValue = aluno.FatorRH;
                    else
                        ddlFatorRH.SelectedValue = "0";
                    
                    if (aluno.TipoSanguineo != "")
                        ddlTipoSanguineo.SelectedValue = aluno.TipoSanguineo;
                    else
                        ddlTipoSanguineo.SelectedValue = "0";
                    CarregaGridTipoDocumento(aluno.Pessoa.PessoaDocumento);
                }
                else
                {
                    this.Limpar();
                    Mensagem.Aviso("Código inválido!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally {
                txtDataNascimento.TextChanged += txtDataNascimento_TextChanged;
            }
        }

        public bool Salvar()
        {
            //cria os objetos
            Aluno aluno = new Aluno();
            Pessoa pessoa = new Pessoa();
            aluno.Pessoa = pessoa;
            aluno.Pessoa.Tipo = "A";//aluno
            //atribui os campos
            aluno.Pessoa.Nome = txtNome.Text;
            aluno.Matricula = Convert.ToDecimal(txtMatricula.Text);          
            aluno.Situacao = ddlSituacao.SelectedValue;
            aluno.Pessoa.Sexo = ddlSexo.SelectedValue;
            if (ddlTipoSanguineo.SelectedValue != "0")
                aluno.TipoSanguineo = ddlTipoSanguineo.SelectedValue;
            else
                aluno.TipoSanguineo = null;
            if (ddlFatorRH.SelectedValue != "0")
                aluno.FatorRH = ddlFatorRH.SelectedValue;
            else
                aluno.FatorRH = null;
            aluno.Pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
            if (txtTelefone.Text.Trim() != "")
                aluno.Pessoa.Telefone = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefone.Text));
            aluno.Emergencia = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtEmergencia.Text));
            aluno.Contato = txtFalarCom.Text;
            if (txtEmail.Text.Trim() != "")
                aluno.Pessoa.Email = txtEmail.Text;
            if (txtPai.Text.Trim() != "")
                aluno.Pai = txtPai.Text;
            if (txtMae.Text.Trim() != "")
                aluno.Mae = txtMae.Text;
            aluno.Sites = cbxSites.Checked;
            aluno.Medicar = cbxMedicar.Checked;
            aluno.BolsaFamilia = cbxBolsaFamilia.Checked;
            aluno.Pessoa.EstadoCivil = ddlEstadoCivil.SelectedValue;
            aluno.Pessoa.Nacionalidade = ddlNacionalidade.SelectedValue;
            if (txtNaturalidade.Text.Trim() != "")
                aluno.Pessoa.Naturalidade = txtNaturalidade.Text;
            if (txtConvenioMedico.Text.Trim() != "")
                aluno.ConvenioMedico = txtConvenioMedico.Text;
            if (txtTelefoneMedico.Text.Trim() != "")
                aluno.TelefoneConvenio = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefoneMedico.Text));
            if (txtCarteirinha.Text.Trim() != "")
                aluno.CarteirinhaConvenio = txtCarteirinha.Text;
            //trata endereco
            Endereco endereco = new Endereco().SelecionarCep(Convert.ToInt32(Comum.RetiraMascaraCEP(txtCep.Text)));
            pessoa.Endereco = endereco;
            if (txtComplemento.Text.Trim() != "")
                pessoa.Complemento = txtComplemento.Text;
            pessoa.Predical = Convert.ToInt32(txtNumero.Text);
            if (Comum.RetiraMascara(txtCopel.Text.Trim()) != "")
                aluno.Pessoa.IdentificacaoCopel = Convert.ToInt32(Comum.RetiraMascara(txtCopel.Text));
            else
                aluno.Pessoa.IdentificacaoCopel = null;
            //prontuario
            if (txtAltura.Text != "_,__")
                aluno.Altura = Convert.ToDecimal(txtAltura.Text.Replace("_",""));
            if (txtPeso.Text.Trim() != "")
                aluno.Peso = Convert.ToDecimal(txtPeso.Text.Replace("_", ""));
            if (txtObservacao.Text.Trim() != "")
                aluno.Observacao = txtObservacao.Text;
            if (txtAlergia.Text.Trim() != "")
                aluno.Alergias = txtAlergia.Text;
            if (txtMedicamentos.Text.Trim() != "")
                aluno.Medicamentos = txtMedicamentos.Text;
            if (txtOutrosTransportes.Text.Trim() != "")
                aluno.OutrosTransportes = txtOutrosTransportes.Text;
            else
                aluno.OutrosTransportes = null;
            if (txtOutrosBeneficios.Text.Trim() != "")
                aluno.OutrosBeneficios = txtOutrosBeneficios.Text;
            else
                aluno.OutrosBeneficios = null;
            aluno.Pessoa.CorRaca = new CorRaca().Selecionar(Convert.ToInt32(ddlCorRaca.SelectedValue));
            DataTable listaDoc = (DataTable)ViewState["TipoDocumento"];
            aluno.Pessoa.PessoaDocumento = new List<PessoaDocumento>();
            if (listaDoc != null){
                for (int i = 0; i < listaDoc.Rows.Count; i++)
                {
                    PessoaDocumento doc = new PessoaDocumento();
                    doc.Pessoa = aluno.Pessoa;
                    doc.TipoDocumento = new TipoDocumento().Selecionar(Convert.ToInt32(listaDoc.Rows[i].ItemArray[0].ToString()));
                    doc.Numero = Comum.RetiraMascara(listaDoc.Rows[i].ItemArray[2].ToString());
                    doc.OrgaoEmissor = listaDoc.Rows[i].ItemArray[4].ToString();
                    doc.UF = listaDoc.Rows[i].ItemArray[3].ToString();
                    doc.InfAdicional = listaDoc.Rows[i].ItemArray[6].ToString();
                    if (listaDoc.Rows[i].ItemArray[5].ToString() != "")
                        doc.DataEmissao = Convert.ToDateTime(listaDoc.Rows[i].ItemArray[5].ToString());
                    else
                        doc.DataEmissao = null;
                    aluno.Pessoa.PessoaDocumento.Add(doc);
                }
            }
            bool retorno = false;
            try
            {
                retorno = aluno.Confirmar();
                Id = aluno.Codigo;
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
              Mensagem.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem.Aviso(ex.Message);
            }
            catch (FormatException)
            {
                Mensagem.Aviso("Data de Nascimento inválida.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public bool Alterar()
        {
            //cria os objetos
            int codigo = Convert.ToInt32(Id);
            Aluno aluno = new Aluno().Selecionar(codigo);
            //atribui os campos
            aluno.Pessoa.Nome = txtNome.Text;
            aluno.Matricula = Convert.ToDecimal(txtMatricula.Text);
            aluno.Situacao = ddlSituacao.SelectedValue;
            aluno.Pessoa.Sexo = ddlSexo.SelectedValue;
            aluno.Pessoa.EstadoCivil = ddlEstadoCivil.SelectedValue;
            aluno.Pessoa.Nacionalidade = ddlNacionalidade.SelectedValue;
            if (txtNaturalidade.Text.Trim() != "")
                aluno.Pessoa.Naturalidade = txtNaturalidade.Text;
            if (ddlFatorRH.SelectedValue != "0")
                aluno.FatorRH = ddlFatorRH.SelectedValue;
            else
                aluno.FatorRH = null;
            if (ddlTipoSanguineo.SelectedValue != "0")
                aluno.TipoSanguineo = ddlTipoSanguineo.SelectedValue;
            else
                aluno.TipoSanguineo = null;
            aluno.Pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
            if (txtTelefone.Text.Trim() != "")
                aluno.Pessoa.Telefone = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefone.Text));
            else
                aluno.Pessoa.Telefone = null;
            aluno.Emergencia = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtEmergencia.Text));
            aluno.Contato = txtFalarCom.Text;
            aluno.Pessoa.CorRaca = new CorRaca().Selecionar(Convert.ToInt32(ddlCorRaca.SelectedValue));
            if (txtEmail.Text.Trim() != "")
                aluno.Pessoa.Email = txtEmail.Text;
            else
                aluno.Pessoa.Email = null;
            if (txtPai.Text.Trim() != "")
                aluno.Pai = txtPai.Text;
            else
                aluno.Pai = null;
            if (txtMae.Text.Trim() != "")
                aluno.Mae = txtMae.Text;
            else
                aluno.Mae = null;
            aluno.Sites = cbxSites.Checked;
            aluno.Medicar = cbxMedicar.Checked;
            aluno.BolsaFamilia = cbxBolsaFamilia.Checked;
            if (txtConvenioMedico.Text.Trim() != "")
                aluno.ConvenioMedico = txtConvenioMedico.Text;
            else
                aluno.ConvenioMedico = null;
            if (txtTelefoneMedico.Text.Trim() != "")
                aluno.TelefoneConvenio = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefoneMedico.Text));
            else
                aluno.TelefoneConvenio = null;
            if (txtCarteirinha.Text.Trim() != "")
                aluno.CarteirinhaConvenio = txtCarteirinha.Text;
            else
                aluno.CarteirinhaConvenio = null;
            //trata endereco
            Endereco endereco = new Endereco().SelecionarCep(Convert.ToInt32(Comum.RetiraMascaraCEP(txtCep.Text)));
            aluno.Pessoa.Endereco = endereco;
            if (txtComplemento.Text.Trim() != "")
                aluno.Pessoa.Complemento = txtComplemento.Text;
            else
                aluno.Pessoa.Complemento = null;
            aluno.Pessoa.Predical = Convert.ToInt32(txtNumero.Text);
            if (Comum.RetiraMascara(txtCopel.Text.Trim()) != "")
                aluno.Pessoa.IdentificacaoCopel = Convert.ToInt32(Comum.RetiraMascara(txtCopel.Text));
            else
                aluno.Pessoa.IdentificacaoCopel = null;
            //prontuario
            if (txtAltura.Text != "_,__")
                aluno.Altura = Convert.ToDecimal(txtAltura.Text.Replace("_", ""));
            else
                aluno.Altura = null;
            if (txtPeso.Text.Trim() != "")
                aluno.Peso = Convert.ToDecimal(txtPeso.Text.Replace("_", ""));
            else
                aluno.Peso = null;
            if (txtObservacao.Text.Trim() != "")
                aluno.Observacao = txtObservacao.Text;
            else
                aluno.Observacao = null;
            if (txtAlergia.Text.Trim() != "")
                aluno.Alergias = txtAlergia.Text;
            else
                aluno.Alergias = null;
            if (txtMedicamentos.Text.Trim() != "")
                aluno.Medicamentos = txtMedicamentos.Text;
            else
                aluno.Medicamentos = null;
            if (txtOutrosTransportes.Text.Trim() != "")
                aluno.OutrosTransportes = txtOutrosTransportes.Text;
            else
                aluno.OutrosTransportes = null;
            if (txtOutrosBeneficios.Text.Trim() != "")
                aluno.OutrosBeneficios = txtOutrosBeneficios.Text;
            else
                aluno.OutrosBeneficios = null;
            DataTable listaDoc = (DataTable)ViewState["TipoDocumento"];
            if (aluno.Pessoa.PessoaDocumento == null)
                aluno.Pessoa.PessoaDocumento = new List<PessoaDocumento>();
            aluno.Pessoa.PessoaDocumento.Clear();
            if (listaDoc != null)
            {
                for (int i = 0; i < listaDoc.Rows.Count; i++)
                {
                    PessoaDocumento doc; 
                    int codigoBanco = Convert.ToInt32(listaDoc.Rows[i].ItemArray[7].ToString());
                    if (codigoBanco > 0)
                        doc = new PessoaDocumento().Selecionar(codigoBanco);
                    else
                        doc = new PessoaDocumento();
                    doc.Pessoa = aluno.Pessoa;
                    doc.TipoDocumento = new TipoDocumento().Selecionar(Convert.ToInt32(listaDoc.Rows[i].ItemArray[0].ToString()));
                    doc.Numero = Comum.RetiraMascara(listaDoc.Rows[i].ItemArray[2].ToString());
                    doc.OrgaoEmissor = listaDoc.Rows[i].ItemArray[4].ToString();
                    doc.UF = listaDoc.Rows[i].ItemArray[3].ToString();
                    doc.InfAdicional = listaDoc.Rows[i].ItemArray[6].ToString();
                    if (listaDoc.Rows[i].ItemArray[5].ToString() != "")
                        doc.DataEmissao = Convert.ToDateTime(listaDoc.Rows[i].ItemArray[5].ToString());
                    else
                        doc.DataEmissao = null;
                    aluno.Pessoa.PessoaDocumento.Add(doc);
                }
            }
            bool retorno = false;
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
            catch (FormatException)
            {
                Mensagem.Aviso("Data de Nascimento inválida.");
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
            try
            {
                txtDataNascimento.TextChanged -= null;
                txtCep.TextChanged -= null;
                txtNome.Text = string.Empty;
                txtMatricula.Text = string.Empty;
                txtDataNascimento.Text = string.Empty;
                ddlSituacao.SelectedIndex = 0;
                ddlSexo.SelectedIndex = 0;
                txtIdade.Text = string.Empty;
                cbxSites.Checked = false;
                cbxMedicar.Checked = false;
                cbxBolsaFamilia.Checked = false;
                txtPai.Text = string.Empty;
                txtMae.Text = string.Empty;
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
                ddlTipoDocumento.SelectedIndex = 0;
                txtNumero.Text = string.Empty;
                ddlUF.SelectedIndex = 0;
                ddlTipoDocumento.SelectedIndex = 0;
                txtNumeroTipoDocumento.Text = string.Empty;
                txtOrgaoEmissor.Text = string.Empty;
                txtAltura.Text = string.Empty;
                txtPeso.Text = string.Empty;
                ddlFatorRH.SelectedIndex = 0;
                ddlTipoSanguineo.SelectedIndex = 0;
                txtAlergia.Text = string.Empty;
                txtMedicamentos.Text = string.Empty;
                txtObservacao.Text = string.Empty;
                gdvTipoDocumento.DataBind();
                txtTelefoneMedico.Text = txtDataExpedicao.Text = 
                txtOutrosBeneficios.Text = txtOutrosTransportes.Text =
                txtCopel.Text = txtIMC.Text =  string.Empty;
                Id = -1;
                ViewState["TipoDocumento"] = null;
            }
            finally {
                txtDataNascimento.TextChanged += txtDataNascimento_TextChanged;
                txtCep.TextChanged += txtCep_TextChanged;
            }
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
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            //Barra de botao da tela de modal
            this.BarraBotaoFiltro.imgVoltarOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
            this.BarraBotaoFiltro.imgLimparOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
            this.BarraBotaoFiltro.imgPesquisarOnClick += new botao.EventHandler(BarraBotaoFiltro_Click);
            
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarAluno.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarAlunos.aspx");
                    break;
                case "Salvar":
                    int codigo = Convert.ToInt32(Id);
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarAluno");
                    if (codigo > 0)
                    {
                        if (objPermissa.Altera == true)
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
                        if (objPermissa.Inclui == true)
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
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarAlunos.aspx");
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

        protected void txtDataNascimento_TextChanged(object sender, EventArgs e)
        {
            string data = txtDataNascimento.Text;
            try
            {
                if (data != "__/__/____")
                    txtIdade.Text = Convert.ToString(Comum.CalculaIdade(Convert.ToDateTime(data)));
                else
                    txtIdade.Text = string.Empty;
            }
            catch{
                txtDataNascimento.Text = "__/__/____";
                txtIdade.Text = string.Empty;
                Mensagem.Aviso("Data inválida.");
            }
        }

        private void CarregaComboUF(){
            IList<Estado> estados = new Estado().Selecionar();
            ddlUF.Items.Clear();
            ddlUFFiltro.Items.Clear();
            foreach(Estado estado in estados){
                ddlUF.Items.Add(estado.Sigla);
                ddlUFFiltro.Items.Add(estado.Sigla);
            }
            ddlUF.DataBind();
            ddlUFFiltro.DataBind();
        }
        private void CarregaComboCorRaca() {
            IList<CorRaca> raca = new CorRaca().Selecionar();
            ddlCorRaca.Items.Clear();
            ddlCorRaca.DataValueField = "Codigo";
            ddlCorRaca.DataTextField = "Descricao";
            ddlCorRaca.DataSource = raca;
            ddlCorRaca.DataBind();
        }
        
        private void CarregaComboTipoDocumento()
        {
            IList<TipoDocumento> lista = new TipoDocumento().SelecionarAtivos();
            ddlTipoDocumento.Items.Clear();
            if (lista != null && lista.Count > 0)
            {
                foreach (TipoDocumento documento in lista)
                {
                    ListItem item = new ListItem();
                    item.Value = Convert.ToString(documento.Codigo);
                    item.Text = documento.Descricao;
                    ddlTipoDocumento.Items.Add(item);
                }
                ddlTipoDocumento.DataBind();
                txtNumeroTipoDocumento.Text = string.Empty;
                TipoDocumento tipoDoc = new TipoDocumento().Selecionar(Convert.ToInt32(ddlTipoDocumento.SelectedValue));
                if (tipoDoc.Mascara != "")
                {
                    txtNumeroTipoDocumento_MaskedEditExtender.PromptCharacter = "_";
                    txtNumeroTipoDocumento_MaskedEditExtender.Mask = tipoDoc.Mascara;
                }
                else
                {
                    txtNumeroTipoDocumento_MaskedEditExtender.PromptCharacter = "";
                    txtNumeroTipoDocumento_MaskedEditExtender.Mask = "??????????????????????????????????";
                }
            }
        }
        private void CarregaGridTipoDocumento(IList<PessoaDocumento> documentos)
        {
            if (documentos.Count > 0) {
                DataTable listaDoc = new DataTable(); 
                listaDoc.Columns.Add("codigoDocumento", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("tipoDocumento", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("numero", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("Uf", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("orgao", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("dataEmissao", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("infAdicional", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("Codigo", System.Type.GetType("System.String"));
                foreach (PessoaDocumento doc in documentos) {
                    listaDoc.Rows.Add(new String[] { Convert.ToString(doc.TipoDocumento.Codigo), doc.TipoDocumento.Descricao, 
                                                    (doc.TipoDocumento.Mascara == null || doc.TipoDocumento.Mascara == "") ? doc.Numero : Comum.ColocarMascara(doc.Numero,doc.TipoDocumento.Mascara), 
                                                     doc.UF, doc.OrgaoEmissor, doc.DataEmissao == null ? "": doc.DataEmissao.Value.ToString("dd/MM/yyyy"),
                                                     doc.InfAdicional, doc.Codigo.ToString()});    
                }
                ViewState.Add("TipoDocumento", listaDoc);
                gdvTipoDocumento.DataSource = listaDoc;
                gdvTipoDocumento.DataBind();   
            }
        }
        protected void imgAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string data; 
            if (Comum.RetiraMascara(txtNumeroTipoDocumento.Text) != "")
            {
                try {
                    if (Comum.RetiraMascara(txtDataExpedicao.Text) != "")
                        data = Convert.ToDateTime(txtDataExpedicao.Text).ToString("dd/MM/yyyy");
                    else
                        data = "";
                }
                catch (FormatException)
                {
                    Mensagem.Aviso("Data de Nascimento inválida.");
                    return;
                }               
                                
                DataTable listaDoc = (DataTable)ViewState["TipoDocumento"];
                if (listaDoc == null)
                {
                    listaDoc = new DataTable();
                    listaDoc.Columns.Add("codigoDocumento", System.Type.GetType("System.String"));
                    listaDoc.Columns.Add("tipoDocumento", System.Type.GetType("System.String"));
                    listaDoc.Columns.Add("numero", System.Type.GetType("System.String"));
                    listaDoc.Columns.Add("Uf", System.Type.GetType("System.String"));
                    listaDoc.Columns.Add("orgao", System.Type.GetType("System.String"));
                    listaDoc.Columns.Add("dataEmissao", System.Type.GetType("System.String"));
                    listaDoc.Columns.Add("infAdicional", System.Type.GetType("System.String"));
                    listaDoc.Columns.Add("Codigo", System.Type.GetType("System.String"));
                }

                for (int i = 0; i < listaDoc.Rows.Count; i++) {
                    if (listaDoc.Rows[i].ItemArray[0].ToString() == ddlTipoDocumento.SelectedValue)
                    {
                        Mensagem.Aviso("Tipo de documento já informado.");
                        return;
                    }                
                }
                    listaDoc.Rows.Add(new String[] { ddlTipoDocumento.SelectedValue, ddlTipoDocumento.SelectedItem.Text, 
                                                txtNumeroTipoDocumento.Text, ddlUF.SelectedValue, 
                                                txtOrgaoEmissor.Text, data, txtInfAdicional.Text, "0"});
                ViewState.Add("TipoDocumento", listaDoc);
                gdvTipoDocumento.DataSource = listaDoc;
                gdvTipoDocumento.DataBind();   
                txtNumeroTipoDocumento.Text = string.Empty;
                txtOrgaoEmissor.Text = txtInfAdicional.Text =
                txtDataExpedicao.Text = string.Empty;
            }
        }

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocumento.SelectedValue != "")
            {
                txtNumeroTipoDocumento.Text = string.Empty;
                TipoDocumento tipoDoc = new TipoDocumento().Selecionar(Convert.ToInt32(ddlTipoDocumento.SelectedValue));
                if (tipoDoc.Mascara != "")
                {
                    txtNumeroTipoDocumento_MaskedEditExtender.PromptCharacter = "_";
                    txtNumeroTipoDocumento_MaskedEditExtender.Mask = tipoDoc.Mascara;
                }
                else
                {
                    txtNumeroTipoDocumento_MaskedEditExtender.PromptCharacter = "";
                    txtNumeroTipoDocumento_MaskedEditExtender.Mask = "??????????????????????????????????";
                }
            }
        }

        protected void gdvTipoDocumento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable listaDoc = (DataTable)ViewState["TipoDocumento"];
            listaDoc.Rows[e.RowIndex].Delete();
            ViewState.Add("TipoDocumento", listaDoc);
            gdvTipoDocumento.DataSource = listaDoc;
            gdvTipoDocumento.DataBind();
        }

        protected void txtAltura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtIMC.Text = string.Format("{0:f}", Comum.CalculaIMC(Convert.ToDecimal(txtAltura.Text.Replace("_","")),
                                                                      Convert.ToDecimal(txtPeso.Text)));
            }
            catch {
                Mensagem.Aviso("Valor inválido. Verifique o valor informado para a altura e peso do aluno.");
            }
        }

        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtIMC.Text = string.Format("{0:f}", Comum.CalculaIMC(Convert.ToDecimal(txtAltura.Text.Replace("_", "")),
                                                                      Convert.ToDecimal(txtPeso.Text)));
            }
            catch {
                Mensagem.Aviso("Valor inválido. Verifique o valor informado para a altura e peso do aluno.");
            }                                                                 
        }
    }
}
