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
    public partial class frmCadastrarAtendimentoAluno : System.Web.UI.Page, Base
    {
        private static Atendimento objAtendimento;
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Atendimento ao Aluno");
            botao1.Desabilitar(false, false, false, false, true, false, false, true, false, true);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarAtendimentoAluno");
            if (objPermissa.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Atendimento ao Aluno!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                try
                {

                    objAtendimento = new Atendimento();

                    if (Request.QueryString["Aluno"] != null)
                    {
                        Aluno objAluno = new Aluno();
                        objAluno = objAluno.Selecionar(Convert.ToInt32(Request.QueryString["Aluno"]));
                        hfdNome.Value = objAluno.Codigo.ToString();
                        txtNome.Text = objAluno.Pessoa.Nome;
                    }
                    if (Request.QueryString["codigo"] != null)
                    {
                        this.Selecionar();
                    }
                    else
                    {
                        Usuario usuario = ((principal)this.Master).usuarioLogado;
                        Docente objDocente = new Docente().SelecionarPorPessoa(usuario.Pessoa);
                        CarregaProfissao(objDocente);
                    }
                }
                catch (Exception)
                {
                    objAtendimento = null;
                }
            }
        }
        protected void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtNome.Text.Trim() != string.Empty)
            {
                Pessoa pessoa = new Pessoa().SelecionarPorNome(txtNome.Text);
                if (pessoa != null)
                {
                    Aluno objAluno = new Aluno().SelecionarPorPessoa(pessoa);
                    hfdNome.Value = objAluno.Codigo.ToString();
                }
                else
                {
                    hfdNome.Value = "0";
                    Mensagem1.Aviso(txtNome.Text + " não foi localizado(a) no cadastro de Aluno.");
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
                if (objAtendimento == null)
                    objAtendimento = new Atendimento();

                int codigo = Convert.ToInt32(Request.QueryString["codigo"]);
                
                objAtendimento = objAtendimento.Selecionar(codigo);

                txtNome.Text = objAtendimento.Aluno.Pessoa.Nome;
                CarregaProfissao(objAtendimento.Docente);
                ddlProfissao.SelectedValue = objAtendimento.Profissao.Codigo.ToString();
                txtDataInicial.Text = objAtendimento.DataHorarioInicial.ToString("dd/MM/yyyy HH:mm");
                txtDataFinal.Text = objAtendimento.DataHorarioFinal.ToString("dd/MM/yyyy HH:mm");
                txtAtendimento.Text = objAtendimento.Descricao;
                hfdNome.Value = objAtendimento.Aluno.Codigo.ToString();
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
        }

        public bool Salvar()
        {
            bool result = false;
            try
            {
                if (objAtendimento == null)
                    objAtendimento = new Atendimento();

                Usuario usuario = ((principal)this.Master).usuarioLogado;
                Docente docenteUsuario = new Docente().SelecionarPorPessoa(usuario.Pessoa);
                objAtendimento.Docente = docenteUsuario;
                objAtendimento.Profissao = docenteUsuario.Profissao;

                Compromisso objCompromisso = new Compromisso();
                if (Request.QueryString["Compromisso"] != null)
                {
                    objCompromisso = objCompromisso.Selecionar(Convert.ToInt32(Request.QueryString["Compromisso"]));
                    objCompromisso.Situacao = "A";//Situação atendido.
                }

                if (objCompromisso == null || objCompromisso.Codigo == 0)
                    objAtendimento.Compromisso = null;
                else
                    objAtendimento.Compromisso = objCompromisso;


                Aluno objAluno = new Aluno();
                objAluno = objAluno.Selecionar(Convert.ToInt32(hfdNome.Value));
                objAtendimento.Aluno = objAluno;
                objAtendimento.Descricao = txtAtendimento.Text;
                objAtendimento.DataHorarioInicial = Convert.ToDateTime(txtDataInicial.Text);
                objAtendimento.DataHorarioFinal = Convert.ToDateTime(txtDataFinal.Text);

                result = objAtendimento.Confirmar();
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (FormatException)
            {
                Mensagem1.Aviso("Data Inválida.");
            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
            return result;
        }

        public bool Alterar()
        {
            bool result = false;
            try
            {
                if (objAtendimento == null)
                {
                    objAtendimento = new Atendimento();
                    int codigo = Convert.ToInt32(Request.QueryString["codigo"]);
                    objAtendimento = objAtendimento.Selecionar(codigo);
                    
                }
                                
                Docente objDocente = new Docente();
                objDocente = objDocente.Selecionar(objAtendimento.Docente.Codigo);
                //verifica se o usuario logado é o mesmo que cadastro o atendimento caso contrario aborta o processo.
                Usuario usuario = ((principal)this.Master).usuarioLogado;
                Docente docenteUsuario  = new Docente().SelecionarPorPessoa(usuario.Pessoa);
                if (docenteUsuario == null)
                {
                    Mensagem1.Aviso("Não é permitido alterar o atendimento. Usuário logado não esta vinculado a um docente.");
                }
                else if (docenteUsuario.Codigo != objDocente.Codigo) 
                {
                    Mensagem1.Aviso("<b>Não é permitido alterar o atendimento realizado por outro profissional.</b><br /> Entre em contato com o Sr(a). " 
                        + docenteUsuario.Pessoa.Nome + " para conversar sobre esse atendimento.");
                }
                else
                {
                    objAtendimento.Docente = objDocente;
                    objAtendimento.Profissao = objDocente.Profissao;
                    Compromisso objCompromisso = new Compromisso();
                    if (objCompromisso.Codigo == 0)
                        objAtendimento.Compromisso = null;
                    else
                        objAtendimento.Compromisso = objCompromisso;


                    Aluno objAluno = new Aluno();
                    objAluno = objAluno.Selecionar(Convert.ToInt32(hfdNome.Value));
                    objAtendimento.Aluno = objAluno;
                    objAtendimento.Descricao = txtAtendimento.Text;
                    objAtendimento.DataHorarioInicial = Convert.ToDateTime(txtDataInicial.Text);
                    objAtendimento.DataHorarioFinal = Convert.ToDateTime(txtDataFinal.Text);

                    result = objAtendimento.Confirmar();
                }
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (FormatException)
            {
                Mensagem1.Aviso("Data Inválida.");
            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
            return result;
        }

        public bool ValidarCamposObrigatorios()
        {
            return true;
        }

        public void Limpar()
        {
            txtNome.Text = string.Empty;
            hfdNome.Value = string.Empty;            
            //ddlProfissao.SelectedIndex = 0;
            txtDataInicial.Text = string.Empty;
            txtDataFinal.Text = string.Empty;
            txtAtendimento.Text = string.Empty;
            objAtendimento = null;
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Metodos Especificos
        private void CarregaProfissao(Docente objDocente)
        {
            if (objDocente == null)
            {
                Mensagem1.Aviso("Não é permitido a inclusão de atendimento. Usuário logado não esta vinculado a um docente.");
            }
            else
            {

                objDocente = objDocente.Selecionar(objDocente.Codigo);
                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
                dt.Columns.Add("Descricao", Type.GetType("System.String"));

                Profissao objProfissao = new Profissao();
                objProfissao = objDocente.Profissao;
                DataRow dr = dt.NewRow();
                dr["Codigo"] = objProfissao.Codigo;
                dr["Descricao"] = objProfissao.Descricao;
                dt.Rows.Add(dr);

                ddlProfissao.Items.Clear();
                ListItem itemProfissao = new ListItem();
                itemProfissao.Text = "(--Selecione--)";
                itemProfissao.Value = "0";
                ddlProfissao.Items.Add(itemProfissao);
                ddlProfissao.DataSource = dt;
                ddlProfissao.DataTextField = "Descricao";
                ddlProfissao.DataValueField = "Codigo";
                ddlProfissao.DataBind();
                ddlProfissao.SelectedIndex = 1;
            }
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
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgNovoOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgFichaAlunoOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPlanejamentoClinicoOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPlanejamentoPedagogicoOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarAtendimentoAluno.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarAtendimentoAluno.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarAtendimentoAluno");
                    if (objAtendimento.Codigo == 0)
                    {
                        if (objPermissa.Inclui == true)
                        {
                            if (this.Salvar())
                                Mensagem1.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                        }
                        else
                        {
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusao"].ToString());
                        }
                    }
                    else
                    {
                        if (objPermissa.Altera == true)
                        {
                            if (this.Alterar())
                            {
                                Mensagem1.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                            }
                        }
                        else
                        {
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["10_Permissao_Alteracao"].ToString());                            
                        }
                    }
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "FichaAluno":
                    if (hfdNome.Value != "")
                        Response.Redirect("../Cadastros/frmCadastrarAluno.aspx?codigo=" + hfdNome.Value);
                    else
                        Mensagem1.Aviso("Informe o nome do Aluno.");
                    break;                
                case "PlanejamentoClinico":
                    if (hfdNome.Value != "")
                        Response.Redirect("../Cadastros/frmCadastrarPlanejamentoClinico.aspx?codigo=" + hfdNome.Value);
                    else
                        Mensagem1.Aviso("Informe o nome do Aluno.");
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
