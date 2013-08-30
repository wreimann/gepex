using System;
using GEPEX;
using Web.Util;
using Model.Entidade;
using Model.Base;
using System.Configuration;

namespace Web.Cadastros
{
    public partial class frmCadastrarTurma : BaseCadastro, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastro de Turma");
            botao1.Desabilitar(false, false, false, false, true, true, true, true, false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissao = ((principal)this.Master).Permissao("frmCadastrarTurma");
            if (objPermissao.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Turma!');location.href='../Geral/index.aspx';</script>");
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
                Turma turma = new Turma().Selecionar(codigo);
                if (turma != null)
                {
                    txtSerie.Text = turma.Serie;
                    txtTurma.Text = turma.SerieTurma;
                    ddlEnsino.SelectedValue = turma.Ensino;
                    ddlPeriodo.SelectedValue = turma.Periodo;
                    txtNumMaxAlunos.Text = turma.NumeroMaximoAlunos.ToString();
                    txtSala.Text = turma.Sala.ToString();
                    txtIdadeInicial.Text = turma.AnoMinimo.ToString();
                    txtIdadeFinal.Text = turma.AnoMaximo.ToString();
                    txtAnoLetivo.Text = turma.AnoLetivo.ToString();
                    if (turma.Situacao == "A")
                        rdlSituacao.Items[0].Selected = true;
                    else if (turma.Situacao == "I")
                        rdlSituacao.Items[1].Selected = true;
                    else
                        rdlSituacao.Items[2].Selected = true;
                    txtObservacao.Text = turma.Observacao;
                    Id = codigo;
                }
                else{
                    this.Limpar();
                    Mensagem.Aviso("Código inválido!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Salvar()
        {
            Turma turma = new Turma();
            turma.Serie = txtSerie.Text;
            turma.SerieTurma = txtTurma.Text;
            turma.Ensino = ddlEnsino.SelectedValue;
            turma.Periodo = ddlPeriodo.SelectedValue;
            turma.NumeroMaximoAlunos = Convert.ToInt32(txtNumMaxAlunos.Text);
            if (txtSala.Text.Trim() != "")
                turma.Sala = Convert.ToInt32(txtSala.Text);
            turma.AnoMinimo = Convert.ToInt32(txtIdadeInicial.Text);
            if (txtIdadeFinal.Text.Trim() != "")
                turma.AnoMaximo = Convert.ToInt32(txtIdadeFinal.Text);
            turma.AnoLetivo = Convert.ToInt32(txtAnoLetivo.Text);
            turma.Situacao = "A";
            turma.Observacao = txtObservacao.Text;
            bool retorno = false;
            try
            {
                retorno = turma.Confirmar();
                Id = turma.Codigo;
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
            bool retorno = false;
            Turma turma = new Turma();
            turma.Codigo = Convert.ToInt32(Id);
            if (turma.Situacao == "F")
            {
                Mensagem.Aviso("Não é permitido alterar as informações da turma de um ano letivo finalizado!");
            }
            else
            {
                turma.Serie = txtSerie.Text;
                turma.SerieTurma = txtTurma.Text;
                turma.Ensino = ddlEnsino.SelectedValue;
                turma.Periodo = ddlPeriodo.SelectedValue;
                turma.NumeroMaximoAlunos = Convert.ToInt32(txtNumMaxAlunos.Text);
                if (txtSala.Text.Trim() != "")
                    turma.Sala = Convert.ToInt32(txtSala.Text);
                else
                    turma.Sala = null;
                turma.AnoMinimo = Convert.ToInt32(txtIdadeInicial.Text);
                if (txtIdadeFinal.Text.Trim() != "")
                    turma.AnoMaximo = Convert.ToInt32(txtIdadeFinal.Text);
                else
                    turma.AnoMaximo = null;
                turma.AnoLetivo = Convert.ToInt32(txtAnoLetivo.Text);
                turma.Observacao = txtObservacao.Text;
                               
                try
                {
                    retorno = turma.Confirmar();
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
            return retorno;
        }

        public bool ValidarCamposObrigatorios()
        {
            return true;

        }

        public void Limpar()
        {
           txtSerie.Text = string.Empty;
           txtTurma.Text = string.Empty;
           ddlEnsino.SelectedIndex = 0;
           ddlPeriodo.SelectedIndex = 0;
           txtSala.Text = string.Empty;
           txtAnoLetivo.Text = string.Empty;
           txtNumMaxAlunos.Text = string.Empty;
           txtIdadeInicial.Text = string.Empty;
           txtIdadeFinal.Text = string.Empty;
           txtObservacao.Text = string.Empty;
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
            this.botao1.imgNovoOnClick   += new botao.EventHandler(BarraBotao_Click);
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
                    Response.Redirect("../Consultas/frmConsultarTurma.aspx");
                    break;
                case "Salvar":
                    int codigo = Convert.ToInt32(Id);
                    Permissao objPermissao = ((principal)this.Master).Permissao("frmCadastrarTurma");

                    if (codigo > 0)
                    {
                        if (objPermissao.Altera)
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
                    Response.Redirect("../Consultas/frmConsultarTurma.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
