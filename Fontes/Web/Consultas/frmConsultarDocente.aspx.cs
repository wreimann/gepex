using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Web.Util;
using Model.Entidade;
using System.Collections.Generic;
using Web;

namespace GEPEX.Consultas
{
    public partial class frmConsultarDocente : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Consulta de Funcionários");
            botao1.Desabilitar(false, false, true, false, true, true, true, true, false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarDocente");
            if (objPermissao.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta de Docentes!');location.href='../Geral/index.aspx';</script>");
            }
        }
        protected void gdvDocente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvDocente.PageIndex = e.NewPageIndex;
            Selecionar();
        }
        protected void gdvDocente_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Docente> lista = (List<Docente>)ViewState["Grid"];
            if (lista != null && lista.Count > 0)
            {
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("nome", System.Type.GetType("System.String"));
                dt.Columns.Add("profissao", System.Type.GetType("System.String"));
                dt.Columns.Add("situacao", System.Type.GetType("System.String"));
                foreach (Docente t in lista)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Pessoa.Nome, 
                                                   t.Profissao.Descricao, t.Situacao ? "Ativo" : "Inativo"});
                }
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + sortDireciton;
                gdvDocente.DataSource = dataView;
                gdvDocente.DataBind();
                
            }
        }
        protected void gdvDocente_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Docente docente = new Docente();
                docente.Codigo = Convert.ToInt32(gdvDocente.DataKeys[e.RowIndex].Values[0]);
                docente.Situacao = false;
                docente.Confirmar();
                Mensagem.Aviso(ConfigurationManager.AppSettings["02_Exclusao"].ToString());
                this.Limpar();
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
        protected void gdvDocente_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvDocente.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarDocente.aspx?codigo=" + codigo);
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {            
            if (txtNome.Text.Trim() != string.Empty)
            {
                Docente docente = new Docente();
                IList<Docente> lista = docente.SelecionarPorNome(txtNome.Text);
                if (lista.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                    dt.Columns.Add("nome", System.Type.GetType("System.String"));
                    dt.Columns.Add("profissao", System.Type.GetType("System.String"));
                    dt.Columns.Add("situacao", System.Type.GetType("System.String"));
                    foreach (Docente t in lista)
                    {
                        dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Pessoa.Nome, 
                                                   t.Profissao.Descricao, t.Situacao ? "Ativo" : "Inativo"});
                    }
                    gdvDocente.DataSource = dt;
                    gdvDocente.DataBind();
                    ViewState.Add("Grid", lista);
                }
                else
                {
                    Mensagem.Aviso("Nenhum docente foi localizado.");
                    this.Limpar();
                }
            } 
            else
                Mensagem.Aviso("Informe o nome do docente.");
            
            
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
            gdvDocente.DataSource = null;
            gdvDocente.DataBind();
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
                    Response.Redirect("../Cadastros/frmCadastrarDocente.aspx");
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

        protected void gdvDocente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarDocente");
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
