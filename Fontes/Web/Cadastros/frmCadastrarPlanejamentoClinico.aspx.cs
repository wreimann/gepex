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
using GEPEX;
using Web.Util;
using System.Text;
using Model.Entidade;
using System.Collections.Generic;

namespace Web.Cadastros
{
    public partial class frmCadastrarPlanejamentoClinico : BaseCadastro, Base
    {
        //private static PlanejamentoClinico objPlanejamentoClinico;
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Planejamento Clínico");
            botao1.Desabilitar(false, false, false, false, true, true, true, true, false);

            if (!IsPostBack)
            {
                //Carrega a combo profissao do usuario logado
                this.CarregaProfissao(null);
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
            int codigo = int.Parse(Request.QueryString["codigo"]);
            try
            {
                PlanejamentoClinico objPlanejamentoClinico = new PlanejamentoClinico().Selecionar(codigo);
                Aluno objAluno = new Aluno();
                objAluno = objPlanejamentoClinico.Aluno;
                hfdNome.Value = objAluno.Pessoa.Codigo.ToString();
                txtNome.Text = objAluno.Pessoa.Nome;
                CarregaProfissao(objPlanejamentoClinico.Profissao);
                txtCompetencias.Text = objPlanejamentoClinico.CompetenciaHabilidades;
                txtDataInicial.Text = objPlanejamentoClinico.DataInicial.ToString("dd/MM/yyyy");
                txtDataFinal.Text = objPlanejamentoClinico.DataFinal.ToString("dd/MM/yyyy");
                txtObjetivoGeralClinico.Text = objPlanejamentoClinico.ObjetivoGeral;
                txtNumeroAtendimento.Text = objPlanejamentoClinico.NumeroAtendimento.ToString();
                Id = codigo;
            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
        }

        public bool Salvar()
        {
            bool retorno = false;
            try
            {
                PlanejamentoClinico objPlanejamentoClinico = new PlanejamentoClinico();

                Aluno objAluno = new Aluno().Selecionar(Convert.ToInt32(hfdNome.Value));                
                objPlanejamentoClinico.Aluno = objAluno;
                
                Profissao objProfissao = new Profissao();
                objProfissao = objProfissao.Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));
                objPlanejamentoClinico.Profissao = objProfissao;
                objPlanejamentoClinico.CompetenciaHabilidades = txtCompetencias.Text;
                objPlanejamentoClinico.DataInicial = Convert.ToDateTime(txtDataInicial.Text);
                objPlanejamentoClinico.DataFinal = Convert.ToDateTime(txtDataFinal.Text);
                objPlanejamentoClinico.ObjetivoGeral = txtObjetivoGeralClinico.Text;
                objPlanejamentoClinico.DataCadastro = DateTime.Now;
                objPlanejamentoClinico.NumeroAtendimento = Convert.ToInt32(txtNumeroAtendimento.Text);

                retorno = objPlanejamentoClinico.Confirmar();
                Id = objPlanejamentoClinico.Codigo;
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
            catch (Exception e)
            {
                throw e;
            }
            return retorno;
        }

        public bool Alterar()
        {
            bool retorno = false;
            try
            {
                PlanejamentoClinico objPlanejamentoClinico = new PlanejamentoClinico();
                objPlanejamentoClinico.Codigo = Convert.ToInt32(Id);             
                Profissao objProfissao = new Profissao().Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));
                objPlanejamentoClinico.Profissao = objProfissao;
                //verifica a especialidade do usuario logado é a mesma do cadastro
                Usuario usuario = ((principal)this.Master).usuarioLogado;
                Docente docenteUsuario  = new Docente().SelecionarPorPessoa(usuario.Pessoa);
                if (objProfissao.Codigo == docenteUsuario.Profissao.Codigo)
                {
                    Aluno objAluno = new Aluno().Selecionar(Convert.ToInt32(hfdNome.Value));
                    objPlanejamentoClinico.Aluno = objAluno;
                                    
                    objPlanejamentoClinico.CompetenciaHabilidades = txtCompetencias.Text;
                    objPlanejamentoClinico.DataInicial = Convert.ToDateTime(txtDataInicial.Text);
                    objPlanejamentoClinico.DataFinal = Convert.ToDateTime(txtDataFinal.Text);
                    objPlanejamentoClinico.ObjetivoGeral = txtObjetivoGeralClinico.Text;
                    objPlanejamentoClinico.DataCadastro = DateTime.Now;
                    objPlanejamentoClinico.NumeroAtendimento = Convert.ToInt32(txtNumeroAtendimento.Text);

                    retorno = objPlanejamentoClinico.Confirmar();
                }
                else
                {
                    Mensagem1.Aviso("Não é permitido alterar o planejamento clínico de outra especialidade.");
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
            catch (Exception e)
            {
                throw e;
            }
            return retorno;
        }

        public bool ValidarCamposObrigatorios()
        {
            throw new NotImplementedException();
        }

        public void Limpar()
        {
            hfdNome.Value = txtNome.Text =
            txtCompetencias.Text = string.Empty;
            txtDataInicial.Text = string.Empty;
            txtDataFinal.Text = string.Empty;
            txtObjetivoGeralClinico.Text = string.Empty;
            txtNumeroAtendimento.Text = string.Empty;            
            txtObjetivoGeralClinico.Text = string.Empty;
            Id = -1;
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Metodos Especificos

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
        private void CarregaProfissao(Profissao especialidade)
        {
            ddlProfissao.Items.Clear();
            Usuario usuario = ((principal)this.Master).usuarioLogado;
            Profissao espec;
            if (especialidade == null)
            {
                Docente objDocente = new Docente().SelecionarPorPessoa(usuario.Pessoa);
                espec = objDocente.Profissao;
            }
            else 
            {
                espec = especialidade;
            }
            ListItem profissao = new ListItem();
            profissao.Value = espec.Codigo.ToString();
            profissao.Text = espec.Descricao;
            ddlProfissao.Items.Add(profissao);
            if (usuario.Perfil.Descricao.ToUpper() == "COORDENADOR") 
            {
                IList<Profissao> lista = new Profissao().SelecionarAtivosClinico();
                foreach (Profissao p in lista)
                {
                    if (p.Codigo != espec.Codigo)
                    {
                        ListItem item = new ListItem();
                        item.Value = p.Codigo.ToString();
                        item.Text = p.Descricao;
                        item.Selected = false;
                        ddlProfissao.Items.Add(item);
                    }
                }
                ddlProfissao.Enabled = true;
            }
            ddlProfissao.SelectedIndex = 0;
            ddlProfissao.DataBind();   
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
                    Response.Redirect("../Cadastros/frmCadastrarPlanejamentoClinico.aspx");
                    break;
            case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarPlanejamentoClinico.aspx");
                    break;
                case "Salvar":
                    int codigo = Convert.ToInt32(Id);
                    if (codigo > 0)
                    {
                        if (this.Alterar())
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                    }
                    else
                    {
                        if (this.Salvar())
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                    }
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

        





    }
}
