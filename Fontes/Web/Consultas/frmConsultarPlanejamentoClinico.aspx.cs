using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Data;

namespace Web.Consultas
{
    public partial class frmConsultarPlenejamentoClinico : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Consulta Planejamento Clínico");
                botao1.Desabilitar(false, false, true, false, true, true, true,true, false);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarPlanejamentoClinico");
                if (objPermissao.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta Planejamento Clínico!');location.href='../Geral/index.aspx';</script>");
                }
                CarregaProfissao();
                CarregaAnoLetivo();
             
            }
        }
        protected void gdvPlanejamentoClinico_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                PlanejamentoClinico planejamento = new PlanejamentoClinico().Selecionar(Convert.ToInt32(gdvPlanejamentoClinico.DataKeys[e.RowIndex].Values[0]));
                 //verifica a especialidade do usuario logado é a mesma do cadastro
                Usuario usuario = ((principal)this.Master).usuarioLogado;
                Docente docenteUsuario  = new Docente().SelecionarPorPessoa(usuario.Pessoa);
                if (planejamento.Profissao.Codigo == docenteUsuario.Profissao.Codigo)
                {
                    if (planejamento.Excluir(planejamento.Codigo))
                    {
                        Mensagem.Aviso(ConfigurationManager.AppSettings["02_Exclusao"].ToString());
                        this.Limpar();
                    }
                }
                else
                {
                    Mensagem.Aviso("Não é permitido excluir o planejamento clínico de outra especialidade.");
                }       
                
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        protected void gdvPlanejamentoClinico_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvPlanejamentoClinico.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarPlanejamentoClinico.aspx?codigo=" + codigo);
        }

        private void CarregaProfissao()
        {
            ddlProfissao.Items.Clear();
            ListItem itemDefault = new ListItem("Todas", "0");
            itemDefault.Selected = true;
            ddlProfissao.Items.Add(itemDefault);
            IList<Profissao> lista = new Profissao().SelecionarAtivosClinico();
            foreach (Profissao disciplina in lista)
            {
                ListItem item = new ListItem();
                item.Value = disciplina.Codigo.ToString();
                item.Text = disciplina.Descricao;
                item.Selected = false;
                ddlProfissao.Items.Add(item);
            }
            ddlProfissao.DataBind();

        }
        private void CarregaAnoLetivo()
        {
            ddlAnoLetivo.DataTextField = "AnoLetivo";
            ddlAnoLetivo.DataValueField = "AnoLetivo";
            Turma turma = new Turma();
            ddlAnoLetivo.DataSource = turma.ObterAnoLetivo();
            ddlAnoLetivo.DataBind();

        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            Aluno aluno;
            if (hfdNome.Value != string.Empty)
                aluno = new Aluno().Selecionar(Convert.ToInt32(hfdNome.Value));
            else
                aluno = null;
            Profissao especialidade;
            if (ddlProfissao.SelectedValue != "0")
                especialidade = new Profissao().Selecionar(Convert.ToInt32(ddlProfissao.SelectedValue));
            else
                especialidade = null;
            IList<PlanejamentoClinico> planej = new PlanejamentoClinico().SelecionarPorCriterios(especialidade, aluno,
                                                                                                 Convert.ToInt32(ddlAnoLetivo.SelectedValue));
            if (planej == null || planej.Count == 0)
            {
                gdvPlanejamentoClinico.DataBind();
                Mensagem.Aviso("Nenhum planejamento clínico foi localizado.");
            }
            else if (planej.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("periodo", System.Type.GetType("System.String"));
                dt.Columns.Add("aluno", System.Type.GetType("System.String"));
                dt.Columns.Add("especialidade", System.Type.GetType("System.String"));
                foreach (PlanejamentoClinico t in planej)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.DataInicial.Date.ToString("dd/MM/yyyy") + " - " + 
                                               t.DataFinal.Date.ToString("dd/MM/yyyy"), t.Aluno.Pessoa.Nome, 
                                               t.Profissao.Descricao});
                }
                gdvPlanejamentoClinico.DataSource = dt;
                gdvPlanejamentoClinico.DataBind();
                ViewState.Add("Grid", planej);
            }
      
        }

        public bool Salvar()
        {
            throw new NotImplementedException();
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
            if (ddlProfissao.Items.Count > 0)
                ddlProfissao.SelectedIndex = 0;
            hfdNome.Value = string.Empty;
            if (ddlAnoLetivo.Items.Count > 0)
                ddlAnoLetivo.SelectedIndex = 0;
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
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarPlanejamentoClinico.aspx");
                    break;
                case "Pesquisar":
                    this.Selecionar();
                    break;
                case "Salvar":
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

        protected void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtNome.Text.Trim() != "")
            {
                Pessoa pessoa = new Pessoa();
                Aluno aluno = new Aluno().SelecionarPorPessoa(pessoa.SelecionarPorNome(txtNome.Text, "A"));
                if (aluno != null)
                {
                    hfdNome.Value = Convert.ToString(aluno.Codigo);
                }
                else
                {
                    txtNome.Text = string.Empty;
                    hfdNome.Value = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptAjax",
                        "alert('Aluno não cadastrado.');", true);
                }

            }
            else
            {
                hfdNome.Value = string.Empty;
              
            }
        }

        protected void gdvPlanejamentoClinico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvPlanejamentoClinico.PageIndex = e.NewPageIndex;
            Selecionar();
        }

        protected void gdvPlanejamentoClinico_Sorting(object sender, GridViewSortEventArgs e)
        {
            IList<PlanejamentoClinico> planej = (IList<PlanejamentoClinico>)ViewState["Grid"];
            if (planej != null && planej.Count > 0)
            {
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("periodo", System.Type.GetType("System.String"));
                dt.Columns.Add("aluno", System.Type.GetType("System.String"));
                dt.Columns.Add("especialidade", System.Type.GetType("System.String"));
                foreach (PlanejamentoClinico t in planej)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.DataInicial.Date.ToString() + " - " + 
                                               t.DataFinal.Date.ToString(), t.Aluno.Pessoa.Nome, 
                                               t.Profissao.Descricao});
                }
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + sortDireciton;
                gdvPlanejamentoClinico.DataSource = dataView;
                gdvPlanejamentoClinico.DataBind();
            }
        }

        protected void gdvPlanejamentoClinico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarPlanejamentoClinico");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objPermissao.Altera == false)
                {
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    imgEditar.Visible = false;
                }
                if (objPermissao.Exclui == false)
                {
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                    imgExcluir.Visible = false;
                }
            }
        }





    }
}
