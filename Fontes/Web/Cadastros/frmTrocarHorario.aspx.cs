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
using Model.Entidade;
using System.Collections.Generic;
using GEPEX;

namespace Web.Cadastros
{
    public partial class frmTrocarHorario : System.Web.UI.Page, Base
    {        
        private static Compromisso objCompromisso;
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Agenda - Trocar Horário");
            botao1.Desabilitar(true, false, false, false, true, true, true, true, false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissao = ((principal)this.Master).Permissao("frmTrocarHorario");
            if (objPermissao.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela Troca de Horário!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                try
                {                    
                    
                    objCompromisso = new Compromisso();

                    this.CarregaFuncionarios();
                    this.CarregaAluno();
                    if (Request.QueryString["codigo"] != null)
                        this.Selecionar();
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Horário de origem não foi selecionado na Agenda de Clínica !');location.href='../Consultas/frmConsultarAgendaAtendimento.aspx';</script>");

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
       
        protected void ddlFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFuncionario.SelectedIndex != 0)
                {
                    Docente objDocente = new Docente();
                    objDocente.Codigo = Convert.ToInt32(ddlFuncionario.SelectedValue);
                    this.CarregaProfissao(objDocente, 1);
                }
                else
                {
                    ddlProfissao.Items.Clear();
                    ListItem itemProfissao = new ListItem();
                    itemProfissao.Text = "(--Selecione--)";
                    itemProfissao.Value = "0";
                    ddlProfissao.Items.Add(itemProfissao);
                    ddlProfissao.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlParaFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlParaFuncionario.SelectedIndex != 0)
                {
                    Docente objDocente = new Docente();
                    objDocente.Codigo = Convert.ToInt32(ddlParaFuncionario.SelectedValue);
                    this.CarregaProfissao(objDocente, 0);
                }
                else
                {
                    ddlParaProfissao.Items.Clear();
                    ListItem itemParaProfissao = new ListItem();
                    itemParaProfissao.Text = "(--Selecione--)";
                    itemParaProfissao.Value = "0";
                    ddlParaProfissao.Items.Add(itemParaProfissao);
                    ddlParaProfissao.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gdvAtendimentosPara_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            RadioButton rdo = (RadioButton)e.Row.FindControl("RadioButton1");

            if (rdo == null)
            {
                return;
            }
            string script = "SetUniqueRadioButton('gdvAtendimentosPara.*nomeGrupo',this)";
            rdo.Attributes.Add("onclick", script);
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            try
            {
                if (objCompromisso == null)
                    objCompromisso = new Compromisso();

                int codigo = Convert.ToInt32(Request.QueryString["codigo"]);
                objCompromisso = objCompromisso.Selecionar(codigo);
                                
                Agenda objAgenda = objCompromisso.Agenda;                

                Docente objDocente = objAgenda.Docente;
                ddlFuncionario.SelectedValue = objDocente.Codigo.ToString();
                CarregaProfissao(objDocente, 1);
                Profissao objProfissao = objCompromisso.Profissao;
                ddlProfissao.SelectedValue = objProfissao.Codigo.ToString();
                ddlAluno.SelectedValue = objCompromisso.Aluno.Codigo.ToString();
                txtData.Text = objAgenda.Data.ToString("dd/MM/yyyy");
                txtHorario.Text = objCompromisso.HorarioInicial.ToString("hh:mm") + " - " + objCompromisso.HorarioFinal.ToString("hh:mm");
                txtMotivo.Text = objCompromisso.Motivo;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Salvar()
        {
            bool retorno = false;
            try
            {
                int codigoCompromisso = 0;
                bool selecionado = false;
                for (int i = 0; i < gdvAtendimentosPara.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gdvAtendimentosPara.Rows[i].FindControl("RadioButton1");
                    if (chk.Checked)
                    {
                        codigoCompromisso = Convert.ToInt32(gdvAtendimentosPara.DataKeys[i].Values["CodigoCompromisso"]);
                        selecionado = true;
                        break;
                    }
                }

                if (!selecionado) 
                {
                    Mensagem1.Aviso("Selecione primeiramente o horário de destino.");
                    return false;
                }
                int codigoOrigem = Convert.ToInt32(Request.QueryString["codigo"]);

                Compromisso objCompDestino = new Compromisso().Selecionar(codigoCompromisso);          
                Compromisso objCompOrigem = new Compromisso().Selecionar(codigoOrigem);
                Compromisso troca = new Compromisso();
                retorno = troca.TrocarHorario(objCompOrigem, objCompDestino, txtMotivo.Text);
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
                Mensagem1.Aviso("Data inválida.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
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
            ddlParaFuncionario.SelectedIndex = 0;
            ddlParaProfissao.Items.Clear();
            ListItem lsProfissao = new ListItem();
            lsProfissao.Text = "(--Selecione--)";
            lsProfissao.Value = "0";
            ddlParaProfissao.Items.Add(lsProfissao);
            ddlParaProfissao.SelectedIndex = 0;
            txtParaData.Text = string.Empty;
            gdvAtendimentosPara.DataSource = null;
            gdvAtendimentosPara.DataBind();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Metodos Especificos
        private void CarregaFuncionarios()
        {
            Docente objDocente = new Docente();
            IList<Docente> lsDocente = objDocente.SelecionarClinicos();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
            dt.Columns.Add("Nome", Type.GetType("System.String"));

            foreach (Docente ls in lsDocente)
            {
                Pessoa objPessoa = new Pessoa();
                objPessoa = ls.Pessoa;
                DataRow dr = dt.NewRow();
                dr["Codigo"] = ls.Codigo;
                dr["Nome"] = objPessoa.Nome;
                dt.Rows.Add(dr);
            }
            ddlFuncionario.DataSource = dt;
            ddlFuncionario.DataTextField = "Nome";
            ddlFuncionario.DataValueField = "Codigo";
            ddlFuncionario.DataBind();

            ddlParaFuncionario.DataSource = dt;
            ddlParaFuncionario.DataTextField = "Nome";
            ddlParaFuncionario.DataValueField = "Codigo";
            ddlParaFuncionario.DataBind();
        }
        private void CarregaProfissao(Docente objDocente, int tipo)
        {
            if (objDocente == null)
                objDocente = new Docente();

            objDocente = objDocente.Selecionar(objDocente.Codigo);
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
            dt.Columns.Add("Descricao", Type.GetType("System.String"));


            DataRow dr = dt.NewRow();
            Profissao objProfissao = new Profissao();
            objProfissao = objDocente.Profissao;
            dr["Codigo"] = objProfissao.Codigo;
            dr["Descricao"] = objProfissao.Descricao;
            dt.Rows.Add(dr);

            if (tipo == 1)
            {
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
            else
            {
                ddlParaProfissao.Items.Clear();
                ListItem itemParaProfissao = new ListItem();
                itemParaProfissao.Text = "(--Selecione--)";
                itemParaProfissao.Value = "0";
                ddlParaProfissao.Items.Add(itemParaProfissao);
                ddlParaProfissao.DataSource = dt;
                ddlParaProfissao.DataTextField = "Descricao";
                ddlParaProfissao.DataValueField = "Codigo";
                ddlParaProfissao.DataBind();
                ddlParaProfissao.SelectedIndex = 1;
            }
        }
        private void CarregaAluno()
        {
            Aluno objAluno = new Aluno();
            IList<Aluno> lsAluno = objAluno.Selecionar();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
            dt.Columns.Add("Nome", Type.GetType("System.String"));

            foreach (Aluno ls in lsAluno)
            {
                Pessoa objPessoa = new Pessoa();
                objPessoa = ls.Pessoa;
                DataRow dr = dt.NewRow();
                dr["Codigo"] = ls.Codigo;
                dr["Nome"] = objPessoa.Nome;
                dt.Rows.Add(dr);
            }
            ddlAluno.DataSource = dt;
            ddlAluno.DataTextField = "Nome";
            ddlAluno.DataValueField = "Codigo";
            ddlAluno.DataBind();

        }

        private void CarregaAtendimentos()
        {
            try
            {
                Agenda objAgenda = new Agenda();

                Docente objDocente = new Docente();
                objDocente = objDocente.Selecionar(Convert.ToInt32(ddlParaFuncionario.SelectedValue));
                Profissao objProfissao = new Profissao();
                objProfissao = objProfissao.Selecionar(Convert.ToInt32(ddlParaProfissao.SelectedValue));
                objDocente.Profissao = objProfissao;                

                objAgenda.Docente = objDocente;
                try
                {
                    objAgenda.Data = Convert.ToDateTime(txtParaData.Text);
                }
                catch (FormatException)
                {
                    Mensagem1.Aviso("Data inválida.");
                    return;
                }
                IList<Agenda> lsAgenda = objAgenda.SelecionarPorCriterio();

                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
                dt.Columns.Add("CodigoCompromisso", Type.GetType("System.Int32"));
                dt.Columns.Add("CodigoAluno", Type.GetType("System.Int32"));
                dt.Columns.Add("Data", Type.GetType("System.String"));
                dt.Columns.Add("Hora", Type.GetType("System.String"));
                dt.Columns.Add("Aluno", Type.GetType("System.String"));
                dt.Columns.Add("Funcionario", Type.GetType("System.String"));
                dt.Columns.Add("Especialidade", Type.GetType("System.String"));


                foreach (Agenda ls in lsAgenda)
                {
                    IList<Compromisso> lsCompromisso = ls.Compromissos;
                    foreach (Compromisso lsComp in lsCompromisso)
                    {
                        
                        if (lsComp.Situacao == "M")
                        {
                            DataRow dr = dt.NewRow();
                            dr["Codigo"] = ls.Codigo;
                            dr["CodigoCompromisso"] = lsComp.Codigo;
                            dr["Data"] = ls.Data;
                            dr["Hora"] = lsComp.HorarioInicial.ToString("hh:mm") + " - " + lsComp.HorarioFinal.ToString("hh:mm");
                            dr["Aluno"] = lsComp.Aluno.Pessoa.Nome;
                            dr["CodigoAluno"] = lsComp.Aluno.Codigo;
                            dr["Funcionario"] = ls.Docente.Pessoa.Nome;
                            dr["Especialidade"] = ls.Docente.Profissao.Descricao;

                            dt.Rows.Add(dr);
                        }
                    }

                }
                gdvAtendimentosPara.DataSource = dt;
                gdvAtendimentosPara.DataBind();
            }
            catch (Exception)
            {
                throw;
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
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarAgendaAtendimento.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissao = ((principal)this.Master).Permissao("frmTrocarHorario");
                    if (objPermissao.Inclui == true)
                    {
                        if (this.Salvar())
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["08_TrocaHorario"].ToString());
                       
                    }
                    else
                    {
                        Mensagem1.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusao"].ToString());
                    }
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarAgendaAtendimento.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

        protected void txtParaData_TextChanged(object sender, EventArgs e)
        {
            if(txtParaData.Text.Trim() != "")
                this.CarregaAtendimentos();
        }

    


    }
}
