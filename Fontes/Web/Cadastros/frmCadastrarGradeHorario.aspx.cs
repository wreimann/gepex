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
    public partial class frmCadastrarGradeHorario : BaseCadastro, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
		{
            ((principal)this.Master).AlteraTitulo("Cadastro de Grade de Horário");
            botao1.Desabilitar(false, false, false, false, true, true, true, true, false);
            /*Verifica a permissão de acesso para a página*/
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarGradeHorario");
            if (objPermissa.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Grade de Horário!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                CarregaComboAnoLetivo();
                CarregaComboTurma();
                CarregaComboDisciplina();
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
            GradeHorario grade = new GradeHorario().Selecionar(codigo);
            if (grade != null)
            {
                ddlAula.SelectedValue = grade.Horario.ToString();
                ddlDia.SelectedValue = grade.DiaSemana.ToString();
                ddlDisciplina.SelectedValue = grade.Disciplina.Codigo.ToString();
                ddlTurma.SelectedValue = grade.Turma.Codigo.ToString();
                txtDocente.Text = grade.Docente.Pessoa.Nome;
                ddlAnoLetivo.SelectedValue = grade.Turma.AnoLetivo.ToString();
                hflDocente.Value = grade.Docente.Codigo.ToString();
                Id = grade.Codigo;
            }
            else
            {
                this.Limpar();
                Mensagem.Aviso("Código inválido!");
            }
        }

        public bool Salvar()
        {
            GradeHorario grade = new GradeHorario();
            grade.Turma = new Turma().Selecionar(Convert.ToInt32(ddlTurma.SelectedValue));
            grade.Docente = new Docente().Selecionar(Convert.ToInt32(hflDocente.Value));
            grade.Disciplina = new Disciplina().Selecionar(Convert.ToInt32(ddlDisciplina.SelectedValue));
            grade.DiaSemana = Convert.ToInt32(ddlDia.SelectedValue);
            grade.Horario   = Convert.ToInt32(ddlAula.SelectedValue);
            
            bool retorno = false;
            try
            {
                retorno = grade.Confirmar();
                Id = grade.Codigo;
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
            int codigo = Convert.ToInt32(Id);
            GradeHorario grade = new GradeHorario().Selecionar(codigo);
            if (grade.Turma.Situacao == "F")
            {
                Mensagem.Aviso("Não é permitido alterar as informações da turma de um ano letivo finalizado!");
            }
            else
            {
                grade.Codigo = codigo;
                grade.Turma = new Turma().Selecionar(Convert.ToInt32(ddlTurma.SelectedValue));
                grade.Docente = new Docente().Selecionar(Convert.ToInt32(hflDocente.Value));
                grade.Disciplina = new Disciplina().Selecionar(Convert.ToInt32(ddlDisciplina.SelectedValue));
                grade.DiaSemana = Convert.ToInt32(ddlDia.SelectedValue);
                grade.Horario = Convert.ToInt32(ddlAula.SelectedValue);
                try
                {
                    retorno = grade.Confirmar();
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
            ddlAula.SelectedValue = 
            ddlDia.SelectedValue  = "0";
            txtDocente.Text = string.Empty;
            ddlDisciplina.SelectedIndex =
            ddlTurma.SelectedIndex =
            ddlAnoLetivo.SelectedIndex = 0;
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
                    Response.Redirect("../Consultas/frmConsultarGradeHorario.aspx");
                    break;
                case "Salvar":
                    int codigo = Convert.ToInt32(Id);
                    if (codigo > 0)
                    {
                        if (this.Alterar())
                            Mensagem.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                    }
                    else
                    {
                        if (this.Salvar())
                            Mensagem.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                    }
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarGradeHorario.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void CarregaComboTurma()
        {
            ddlTurma.Items.Clear();
            if (ddlAnoLetivo.Text != "")
            {
                ListItem itemDefault = new ListItem("(--Selecione--)", "0");
                itemDefault.Selected = true;
                ddlTurma.Items.Add(itemDefault);
                IList<Turma> lista = new Turma().SelecionarPorAnoLetivo(Convert.ToInt32(ddlAnoLetivo.Text));
                foreach (Turma turma in lista)
                {
                    ListItem item = new ListItem();
                    item.Value = turma.Codigo.ToString();
                    item.Text = turma.ToString();
                    item.Selected = false;
                    ddlTurma.Items.Add(item);
                }
                ddlTurma.DataBind();
            }

        }
        private void CarregaComboDisciplina()
        {
            ddlDisciplina.Items.Clear();
            ListItem itemDefault = new ListItem("(--Selecione--)", "0");
            itemDefault.Selected = true;
            ddlDisciplina.Items.Add(itemDefault);
            IList<Disciplina> lista = new Disciplina().SelecionarAtivos();
            foreach (Disciplina disciplina in lista)
            {
                ListItem item = new ListItem();
                item.Value = disciplina.Codigo.ToString();
                item.Text = disciplina.Materia;
                item.Selected = false;
                ddlDisciplina.Items.Add(item);
            }
            ddlDisciplina.DataBind();
        }
        private void CarregaComboAnoLetivo()
        {
            ddlAnoLetivo.DataTextField = "AnoLetivo";
            ddlAnoLetivo.DataValueField = "AnoLetivo";
            Turma turma = new Turma();
            ddlAnoLetivo.DataSource = turma.ObterAnoLetivo();
            ddlAnoLetivo.DataBind();
        }

        protected void ddlAnoLetivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTurma.SelectedIndex = 0;
            CarregaComboTurma();
        }

        protected void txtDocente_TextChanged(object sender, EventArgs e)
        {
            if (txtDocente.Text.Trim() != "")
            {
                Pessoa pessoa = new Pessoa();
                Docente docente = new Docente().SelecionarPorPessoa(pessoa.SelecionarPorNome(txtDocente.Text, "D"));
                if (docente != null)
                {
                    hflDocente.Value = Convert.ToString(docente.Codigo);      
                }
                else
                {
                    txtDocente.Text = string.Empty;
                    hflDocente.Value = string.Empty;
                    ScriptManager.RegisterStartupScript(upnProfessor, upnProfessor.GetType(), "scriptAjax",
                        "alert('Professor não cadastrado.');", true);
                }
            }
            else
            {
                hflDocente.Value = string.Empty;
            }
        }
    }
}
