using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;

namespace Web.Cadastros
{
    public partial class frmGerarMatricula : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Gerar Matrícula");
            botao1.Desabilitar(true, true, false, false, true, true, true, true, false, false, false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissao = ((principal)this.Master).Permissao("frmGerarMatricula");
            if (objPermissao.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela Gerar Matrícula!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                this.Limpar();
                trvTurmas.Attributes.Add("onclick", "return OnTreeClick(event)");
                CarregaAnoLetivo();

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
            bool retorno = false;
            int quantidade = QuantidadeTurmaMarcada();
            if ( quantidade == 1)
            {
                Matricula matricula = new Matricula();
                Aluno aluno = new Aluno();
                Turma turma = new Turma();
                matricula.Aluno = aluno.Selecionar(Convert.ToInt32(hfdNome.Value));
                matricula.Data = DateTime.Now;
                matricula.Turma = TurmaSelecionada();
                if (matricula.Turma.Situacao == "F")
                {
                    Mensagem.Aviso("Não é permitido alterar as informações da turma de um ano letivo finalizado!");
                }
                else
                {
                    try
                    {
                        retorno = matricula.Confirmar();
                        if (retorno)
                        {
                            ddlSituacao.SelectedValue = matricula.Aluno.Situacao;
                            CarregarTurmas();
                        }
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
            else if (quantidade > 1) 
                Mensagem.Aviso("Não é permitido selecionar mais que uma turma.");
            else if (quantidade == 0) 
                Mensagem.Aviso("Selecione uma turma.");
            return retorno;
        }

        public bool Alterar()
        {
            bool retorno = false;
            Aluno aluno = new Aluno().Selecionar(Convert.ToInt32(hfdNome.Value));
            if (aluno.Situacao != "M" && aluno.Situacao != "I")
            {
                try
                {
                    if (aluno.Situacao == "A")
                        aluno.Situacao = "L";
                    else if (aluno.Situacao == "L")
                        aluno.Situacao = "I";
                    retorno = aluno.Confirmar();
                    if (retorno)
                    {
                        ddlSituacao.SelectedValue = aluno.Situacao;
                        CarregarTurmas();
                    }
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
            else if (aluno.Situacao == "M")
                Mensagem.Aviso("O aluno já está matriculado. Operação inválida.");
            else
                Mensagem.Aviso("Aluno deve preencher o requerimento de matrícula.");
            return retorno;
        }

        public bool FinalizarAnoLetivo()
        {
            bool retorno = false;
            int quantidade = QuantidadeTurmaMarcada();
            if (ddlAnoLetivo.SelectedValue != "" && quantidade == 0 && hfdNome.Value == "") 
            {
                Turma turma = new Turma();
                retorno = turma.FinalizarAnoLetivo(Convert.ToInt32(ddlAnoLetivo.SelectedValue));
                retorno = false;
                Aluno aluno = new Aluno();
                retorno = aluno.FinalizarAnoLetivo();
            }
            else
            {
                if (ddlAnoLetivo.SelectedValue == "")
                    Mensagem.Aviso("Informe o Ano Letivo que deseja finalizar.");
                else
                    Mensagem.Aviso("Geração de matrícula está em andamento. É necessário finalizar o processo. <br /> <b>Dica</b>: Pressione o botão limpar da toolbar.");
                    
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
            hfdNome.Value = string.Empty;
            txtMatricula.Text = string.Empty;
            ddlSituacao.SelectedIndex = 0;
            txtDataNascimento.Text = string.Empty;
            txtIdade.Text = string.Empty;
            ddlSexo.SelectedIndex = 0;
            cbxMedicar.Checked = false;
            cbxSites.Checked = false;
            txtObservacao.Text = string.Empty;
            trvTurmas.Nodes.Clear();
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
            this.botao1.imgListaEsperaOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgFinalizarAnoLetivoOnClick += new botao.EventHandler(BarraBotao_Click);
            
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Salvar":
                    if (this.Salvar())
                        Mensagem.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "ListaEspera":
                    Permissao objPermissao = ((principal)this.Master).Permissao("frmGerarMatricula");
                    if (objPermissao.Altera == true)
                    {
                        if (this.Alterar())
                            Mensagem.Aviso("Situação do aluno atualizado com sucesso!");
                    }
                    else
                    {
                        Mensagem.Aviso(ConfigurationManager.AppSettings["10_Permissao_Alteracao"].ToString());
                    }
                    break;
                case "FinalizarAnoLetivo":
                    Permissao objPermissao2 = ((principal)this.Master).Permissao("frmGerarMatricula");
                    if (objPermissao2.Altera == true)
                    {
                        if (this.FinalizarAnoLetivo())
                            Mensagem.Aviso("Ano Letivo Finalizado com sucesso!");
                    }
                    else
                    {
                        Mensagem.Aviso(ConfigurationManager.AppSettings["10_Permissao_Alteracao"].ToString());
                    }
                    break;
                case "Voltar":
                    Response.Redirect("../Geral/index.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

        private int QuantidadeTurmaMarcada() {
            int count = 0;
            foreach (TreeNode tn in trvTurmas.Nodes){
                if (tn.Checked)
                    count++;
            }
            return count;
        }
        private void CarregaAnoLetivo()
        {
            ddlAnoLetivo.DataTextField = "AnoLetivo";
            ddlAnoLetivo.DataValueField = "AnoLetivo";
            Turma turma = new Turma();
            ddlAnoLetivo.DataSource = turma.ObterAnoLetivo();
            ddlAnoLetivo.DataBind();
            
        }
        
        private Turma TurmaSelecionada()
        {
            Turma turma = new Turma();
            foreach (TreeNode tn in trvTurmas.Nodes)
            {
                if (tn.Checked)
                    return turma.Selecionar(Convert.ToInt32(tn.Value));
            }
            return null;
        }
        
        
        protected void CarregarTurmas()
        {
            Turma turma = new Turma();
            IList<Turma> lista = turma.SelecionarPorMatricula(Convert.ToInt32(ddlAnoLetivo.SelectedValue),
                ddlPeriodo.SelectedValue, (Convert.ToDateTime(txtDataNascimento.Text).Year));
            trvTurmas.Nodes.Clear();
            foreach (Turma t in lista) {
                IList<Matricula> matriculados = new Matricula().SelecionarPorTurma(t);
                TreeNode masterNode = new TreeNode(" <b>Turma</b>:" + t.Serie + " - " + t.SerieTurma + 
                                                   "  <b>Período</b>: " + Comum.FormatarPeriodo(t.Periodo) +
                                                   "  <b>Ano Letivo</b>:" + t.AnoLetivo.ToString() + 
                                                   "  <b>Máximo Alunos</b>: " + t.NumeroMaximoAlunos.ToString() +
                                                   "  <b>Total Aluno</b>: " + Convert.ToInt32(matriculados.Count) +
                                                   "  <b>Ano Nasc.</b>: " + t.AnoMinimo.ToString() + " - " + 
                                                   t.AnoMaximo.ToString(),t.Codigo.ToString());
                masterNode.ShowCheckBox = true;
                masterNode.SelectAction = TreeNodeSelectAction.None;
                trvTurmas.Nodes.Add(masterNode);
                
                foreach (Matricula a in matriculados) {
                    TreeNode childNode = new TreeNode(" " + a.Aluno.Pessoa.Nome, a.Codigo.ToString());
                    childNode.ImageToolTip = "Excluir aluno.";
                    childNode.ImageUrl = "~/App_Themes/icones/delete.png";
                    childNode.SelectAction = TreeNodeSelectAction.Select;
                    masterNode.ChildNodes.Add(childNode);
                }
            }
            trvTurmas.ExpandAll();
            trvTurmas.DataBind();            
        }

        protected void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtNome.Text.Trim() != "")
            {
                Pessoa pessoa = new Pessoa();
                Aluno aluno = new Aluno().SelecionarPorPessoa(pessoa.SelecionarPorNome(txtNome.Text, "A"));
                if (aluno != null)
                {
                    hfdNome.Value = Convert.ToString(aluno.Codigo);
                    txtMatricula.Text = Convert.ToString(aluno.Matricula);
                    ddlSituacao.SelectedValue = aluno.Situacao;
                    txtDataNascimento.Text = Convert.ToString(aluno.Pessoa.DataNascimento); 
                    txtIdade.Text = Convert.ToString(Comum.CalculaIdade(aluno.Pessoa.DataNascimento));
                    ddlSexo.SelectedValue = aluno.Pessoa.Sexo;
                    cbxMedicar.Checked = aluno.Medicar;
                    cbxSites.Checked = aluno.Sites;
                    txtObservacao.Text = aluno.Observacao;
                    CarregarTurmas();
                }
                else
                {
                    txtNome.Text = string.Empty;
                    hfdNome.Value = string.Empty;
                    txtMatricula.Text = string.Empty;
                    ddlSituacao.SelectedIndex = 0;
                    txtDataNascimento.Text = string.Empty;
                    txtIdade.Text = string.Empty;
                    ddlSexo.SelectedIndex = 0;
                    cbxMedicar.Checked = false;
                    cbxSites.Checked = false;
                    txtObservacao.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(upnNome, upnNome.GetType(), "scriptAjax",
                        "alert('Aluno não cadastrado.');", true);                
                }

            }
            else
            {
                hfdNome.Value = string.Empty;
                txtMatricula.Text = string.Empty;
                ddlSituacao.SelectedIndex = 0;
                txtDataNascimento.Text = string.Empty;
                txtIdade.Text = string.Empty;
                ddlSexo.SelectedIndex = 0;
                cbxMedicar.Checked = false;
                cbxSites.Checked = false;
                txtObservacao.Text = string.Empty;
            }
        }

        protected void trvTurmas_SelectedNodeChanged(object sender, EventArgs e)
        {
             bool retorno = false;
             try
             {
                int codigo = Convert.ToInt32(trvTurmas.SelectedNode.Value);
                Matricula matricula = new Matricula().Selecionar(codigo);
                if (matricula.Turma.Situacao == "F")
                {
                    ScriptManager.RegisterStartupScript(upnNome, upnNome.GetType(), "scriptAjax",
                        "alert('Não é permitido alterar as informações da turma de um ano letivo finalizado!');", true);
                }
                else
                {
                    retorno = matricula.Excluir(codigo);
                    if (retorno)
                    {
                        ddlSituacao.SelectedValue = matricula.Aluno.Situacao;
                        CarregarTurmas();
                        ScriptManager.RegisterStartupScript(upnNome, upnNome.GetType(), "scriptAjax",
                             "alert('Aluno foi excluído com sucesso!');", true);
                    }
                }
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

        protected void ddlAnoLetivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(hfdNome.Value != "")
                CarregarTurmas();
        }

        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hfdNome.Value != "")
                CarregarTurmas();
        }

    }
}
