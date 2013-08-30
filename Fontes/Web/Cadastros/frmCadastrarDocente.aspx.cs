using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;

namespace Web.Cadastros
{
    public partial class frmCadastrarDocente : BaseCadastro, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastro de Funcionários");
            botao1.Desabilitar(false, false, false, true, true, true, true, true, false);
            BarraBotaoFiltro.Desabilitar(true, false, true, false, true, true, true, true, false);

            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarDocente");
            if (objPermissa.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Docente!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                CarregaComboTipoDocumento();
                CarregaComboUF();
                CarregaComboProfissao();
                CarregaComboCorRaca();
                CarregaComboFormacao();
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
            int codigo = int.Parse(Request["codigo"]);
            Docente docente = new Docente().Selecionar(codigo);
            if (docente != null)
            {
                txtNome.Text = docente.Pessoa.Nome;
                if (docente.Situacao)
                    rdlSituacao.Items[0].Selected = true;
                else
                    rdlSituacao.Items[1].Selected = true;
                 ddlSexo.SelectedValue = docente.Pessoa.Sexo;
                txtDataNascimento.Text = Convert.ToString(docente.Pessoa.DataNascimento);
                ddlEstadoCivil.SelectedValue = docente.Pessoa.EstadoCivil;
                ddlNacionalidade.SelectedValue = docente.Pessoa.Nacionalidade;
                ddlProfissao.SelectedValue = Convert.ToString(docente.Profissao.Codigo);
                txtNaturalidade.Text = docente.Pessoa.Naturalidade;
                txtCopel.Text = Convert.ToString(docente.Pessoa.IdentificacaoCopel);
                if (docente.Pessoa.CorRaca != null)
                    ddlCorRaca.SelectedValue = docente.Pessoa.CorRaca.Codigo.ToString();
                if (docente.Formacao != null)
                    ddlFormacao.SelectedValue = docente.Formacao.Codigo.ToString();           
                txtCurso.Text = docente.Curso;
                txtTelefone.Text = Comum.InsereMascaraTelefone(docente.Pessoa.Telefone);
                txtCelular.Text = Comum.InsereMascaraTelefone(docente.Pessoa.Celular);
                txtEmail.Text = docente.Pessoa.Email;
                txtNumero.Text = Convert.ToString(docente.Pessoa.Predical);
                txtComplemento.Text = docente.Pessoa.Complemento;
                txtObservacao.Text = docente.Observacao;
                CarregaEndereco(docente.Pessoa.Endereco.Cep);
                CarregaGridTipoDocumento(docente.Pessoa.PessoaDocumento);
                Id = codigo;
            }
        }

        public bool Salvar()
        {
            //cria os objetos
            Docente docente = new Docente();
            Pessoa pessoa = new Pessoa();
            Profissao profissao = new Profissao();
            docente.Pessoa = pessoa;
            docente.Profissao = profissao.Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));
            docente.Pessoa.Tipo = "D";//docente
            //atribui os campos
            docente.Pessoa.Nome = txtNome.Text;
            docente.Situacao = rdlSituacao.Items[0].Selected;
            docente.Pessoa.Sexo = ddlSexo.SelectedValue;
            docente.Pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
            docente.Pessoa.EstadoCivil = ddlEstadoCivil.SelectedValue;
            docente.Pessoa.Nacionalidade = ddlNacionalidade.SelectedValue;
            if(txtNaturalidade.Text.Trim() != "")
                docente.Pessoa.Naturalidade = txtNaturalidade.Text;
            docente.Formacao = new Escolaridade().Selecionar(Convert.ToInt32(ddlFormacao.SelectedValue)); 
            if (txtCurso.Text.Trim() != "")
                docente.Curso = txtCurso.Text;
            if (txtTelefone.Text.Trim() != "")
                docente.Pessoa.Telefone = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefone.Text));
            if (txtCelular.Text.Trim() != "")
                docente.Pessoa.Celular  = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtCelular.Text));
            if (txtEmail.Text.Trim() != "")
                docente.Pessoa.Email = txtEmail.Text;
            //trata endereco
            Endereco endereco = new Endereco().SelecionarCep(Convert.ToInt32(Comum.RetiraMascaraCEP(txtCep.Text)));
            pessoa.Endereco = endereco;
            if (txtComplemento.Text.Trim() != "")
                pessoa.Complemento = txtComplemento.Text;
            pessoa.Predical = Convert.ToInt32(txtNumero.Text);
            docente.Pessoa.CorRaca = new CorRaca().Selecionar(Convert.ToInt32(ddlCorRaca.SelectedValue));
            if (Comum.RetiraMascara(txtCopel.Text.Trim()) != "")
                docente.Pessoa.IdentificacaoCopel = Convert.ToInt32(Comum.RetiraMascara(txtCopel.Text));
            else
                docente.Pessoa.IdentificacaoCopel = null;
            if (txtObservacao.Text.Trim() != "")
                docente.Observacao = txtObservacao.Text;
            DataTable listaDoc = (DataTable)ViewState["TipoDocumento"];
            docente.Pessoa.PessoaDocumento = new List<PessoaDocumento>();
            if (listaDoc != null)
            {
                for (int i = 0; i < listaDoc.Rows.Count; i++)
                {
                    PessoaDocumento doc = new PessoaDocumento();
                    doc.Pessoa = docente.Pessoa;
                    doc.TipoDocumento = new TipoDocumento().Selecionar(Convert.ToInt32(listaDoc.Rows[i].ItemArray[0].ToString()));
                    doc.Numero = Comum.RetiraMascara(listaDoc.Rows[i].ItemArray[2].ToString());
                    doc.OrgaoEmissor = listaDoc.Rows[i].ItemArray[4].ToString();
                    doc.UF = listaDoc.Rows[i].ItemArray[3].ToString();
                    doc.InfAdicional = listaDoc.Rows[i].ItemArray[6].ToString();
                    if (listaDoc.Rows[i].ItemArray[5].ToString() != "")
                        doc.DataEmissao = Convert.ToDateTime(listaDoc.Rows[i].ItemArray[5].ToString());
                    else
                        doc.DataEmissao = null; docente.Pessoa.PessoaDocumento.Add(doc);
                }
            }
            bool retorno = false;
            try
            {
                retorno = docente.Confirmar();
                Id = docente.Codigo;
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
            int codigo = Convert.ToInt32(Id);
            Docente docente = new Docente().Selecionar(codigo);
            Profissao profissao = new Profissao();
            docente.Profissao = profissao.Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));
            docente.Pessoa.Nome = txtNome.Text;
            docente.Situacao = rdlSituacao.Items[0].Selected;
            docente.Pessoa.Sexo = ddlSexo.SelectedValue;
            docente.Pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
            docente.Pessoa.EstadoCivil = ddlEstadoCivil.SelectedValue;
            docente.Pessoa.Nacionalidade = ddlNacionalidade.SelectedValue;
            if (txtNaturalidade.Text.Trim() != "")
                docente.Pessoa.Naturalidade = txtNaturalidade.Text;
            else
                docente.Pessoa.Naturalidade = null;
            docente.Formacao = new Escolaridade().Selecionar(Convert.ToInt32(ddlFormacao.SelectedValue));
            if (txtCurso.Text.Trim() != "")
                docente.Curso = txtCurso.Text;
            else
                docente.Curso = null;
            if (txtTelefone.Text.Trim() != "")
                docente.Pessoa.Telefone = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtTelefone.Text));
            else
                docente.Pessoa.Telefone = null;
            if (txtCelular.Text.Trim() != "")
                docente.Pessoa.Celular = Convert.ToDecimal(Comum.RetiraMascaraTelefone(txtCelular.Text));
            else
                docente.Pessoa.Celular = null;
            if (txtEmail.Text.Trim() != "")
                docente.Pessoa.Email = txtEmail.Text;
            else
                docente.Pessoa.Email = null;
            docente.Pessoa.CorRaca = new CorRaca().Selecionar(Convert.ToInt32(ddlCorRaca.SelectedValue));
            //trata endereco
            Endereco endereco = new Endereco().SelecionarCep(Convert.ToInt32(Comum.RetiraMascaraCEP(txtCep.Text)));
            docente.Pessoa.Endereco = endereco;
            if (txtComplemento.Text.Trim() != "")
                docente.Pessoa.Complemento = txtComplemento.Text;
            else
                docente.Pessoa.Complemento = null;
            docente.Pessoa.Predical = Convert.ToInt32(txtNumero.Text);
            if (Comum.RetiraMascara(txtCopel.Text.Trim()) != "")
                docente.Pessoa.IdentificacaoCopel = Convert.ToInt32(Comum.RetiraMascara(txtCopel.Text));
            else
                docente.Pessoa.IdentificacaoCopel = null;
            if (txtObservacao.Text.Trim() != "")
                docente.Observacao = txtObservacao.Text;
            else
                docente.Observacao = null;
            DataTable listaDoc = (DataTable)ViewState["TipoDocumento"];
            if (docente.Pessoa.PessoaDocumento == null)
                docente.Pessoa.PessoaDocumento = new List<PessoaDocumento>();
            docente.Pessoa.PessoaDocumento.Clear();
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
                    doc.Pessoa = docente.Pessoa;
                    doc.TipoDocumento = new TipoDocumento().Selecionar(Convert.ToInt32(listaDoc.Rows[i].ItemArray[0].ToString()));
                    doc.Numero = Comum.RetiraMascara(listaDoc.Rows[i].ItemArray[2].ToString());
                    doc.OrgaoEmissor = listaDoc.Rows[i].ItemArray[4].ToString();
                    doc.UF = listaDoc.Rows[i].ItemArray[3].ToString();
                    doc.InfAdicional = listaDoc.Rows[i].ItemArray[6].ToString();
                    if (listaDoc.Rows[i].ItemArray[5].ToString() != "")
                        doc.DataEmissao = Convert.ToDateTime(listaDoc.Rows[i].ItemArray[5].ToString());
                    else
                        doc.DataEmissao = null; 
                    docente.Pessoa.PessoaDocumento.Add(doc);
                }
            }            
            bool retorno = false;
            try
            {
                retorno = docente.Confirmar();
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
            txtNome.Text = string.Empty;
            rdlSituacao.SelectedIndex = 0;
            ddlSexo.SelectedIndex = 0;
            txtDataNascimento.Text = txtNaturalidade.Text = 
            txtCelular.Text = txtEmail.Text = txtCep.Text = txtLogradouro.Text =
            txtNumero.Text = txtComplemento.Text = txtBairro.Text =
            txtCidade.Text = txtUF.Text = txtObservacao.Text = string.Empty;
            ddlEstadoCivil.SelectedIndex = 0;
            ddlNacionalidade.SelectedValue = "B";
            ddlProfissao.SelectedIndex = 0;
            ddlTipoDocumento.SelectedIndex = 0;
            txtNumeroTipoDocumento.Text = string.Empty;
            txtOrgaoEmissor.Text = string.Empty;
            Id = -1;
            ViewState["TipoDocumento"] = null;
            gdvTipoDocumento.DataBind();
            CarregaComboTipoDocumento();
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
                    Response.Redirect("../Cadastros/frmCadastrarDocente.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarDocente.aspx");
                    break;
                case "Salvar":
                    int codigo = Convert.ToInt32(Id);
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarDocente");
                    if (codigo > 0)
                    {
                        if (objPermissa.Altera == true)
                        {
                            if (this.Alterar())
                                Mensagem.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                        }
                        else {
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
                    Response.Redirect("../Consultas/frmConsultarDocente.aspx");
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

        private void CarregaComboProfissao()
        {
            IList<Profissao> lista = new Profissao().SelecionarAtivos();
            ddlProfissao.Items.Clear();
            foreach (Profissao profissao in lista)
            {
                ListItem item = new ListItem();
                item.Value = Convert.ToString(profissao.Codigo);
                item.Text = profissao.Descricao;
                ddlProfissao.Items.Add(item);
            }
            ddlProfissao.DataBind();
        }
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
            ddlUF.Items.Clear();
            ddlUFFiltro.Items.Clear();
            foreach (Estado estado in estados)
            {
                ddlUF.Items.Add(estado.Sigla);
                ddlUFFiltro.Items.Add(estado.Sigla);
            }
            ddlUF.DataBind();
            ddlUFFiltro.DataBind();
        }
        private void CarregaComboCorRaca()
        {
            IList<CorRaca> raca = new CorRaca().Selecionar();
            ddlCorRaca.Items.Clear();
            ddlCorRaca.DataValueField = "Codigo";
            ddlCorRaca.DataTextField = "Descricao";
            ddlCorRaca.DataSource = raca;
            ddlCorRaca.DataBind();
        }

        private void CarregaComboFormacao()
        {
            IList<Escolaridade> formacao = new Escolaridade().Selecionar();
            ddlFormacao.Items.Clear();
            ddlFormacao.DataValueField = "Codigo";
            ddlFormacao.DataTextField = "Descricao";
            ddlFormacao.DataSource = formacao;
            ddlFormacao.DataBind();
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
            if (documentos.Count > 0)
            {
                DataTable listaDoc = new DataTable();
                listaDoc.Columns.Add("codigoDocumento", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("tipoDocumento", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("numero", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("Uf", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("orgao", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("dataEmissao", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("infAdicional", System.Type.GetType("System.String"));
                listaDoc.Columns.Add("Codigo", System.Type.GetType("System.String"));
                foreach (PessoaDocumento doc in documentos)
                {
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
                try
                {
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

                for (int i = 0; i < listaDoc.Rows.Count; i++)
                {
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

    }
}
