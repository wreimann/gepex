using System;
using System.Data;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Collections.Generic;
using System.Linq;

namespace Web.Consultas
{
	public partial class frmConsultarChamada : System.Web.UI.Page, Base
	{
		#region Eventos
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                CarregaAnoLetivo();
                ((principal)this.Master).AlteraTitulo("Consulta de Chamada");
                botao1.Desabilitar(true, false, true, false, true, true, true, true, false);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarChamada");
                if (objPermissao.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta de Chamada!');location.href='../Geral/index.aspx';</script>");
                }
                this.Selecionar();
            }
        }
        protected void gdvChamada_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvChamada.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarChamada.aspx?codigo=" + codigo);
        }

        #endregion

        #region Metodos
        #region Base Members

        private void CarregaAnoLetivo()
        {
            ddlAnoLetivo.DataTextField = "AnoLetivo";
            ddlAnoLetivo.DataValueField = "AnoLetivo";
            Turma turma = new Turma();
            ddlAnoLetivo.DataSource = turma.ObterAnoLetivo();
            ddlAnoLetivo.DataBind();

        }
        public void Selecionar()
        {
            Turma turma = new Turma();
            IList<Turma> lista = new List<Turma>();
            bool criterio = false;
            //selecionar conforme os parametros informados pelo usuario
            if (txtSerie.Text.Trim() != string.Empty)
            {
                criterio = true;
                turma.Serie = txtSerie.Text;
            }
            if (txtTurma.Text.Trim() != string.Empty)
            {
                criterio = true;
                turma.SerieTurma = txtTurma.Text;
            }
            if (ddlAnoLetivo.Text.Trim() != string.Empty)
            {
                criterio = true;
                turma.AnoLetivo = Convert.ToInt32(ddlAnoLetivo.Text);
            }
            if (ddlEnsino.SelectedIndex != 0)
            {
                criterio = true;
                turma.Ensino = ddlEnsino.SelectedValue;
            }
            if (ddlPeriodo.SelectedIndex != 0)
            {
                criterio = true;
                turma.Periodo = ddlPeriodo.SelectedValue;
            }
            if (criterio)
                lista = turma.SelecionarPorCriterio();
            else
                lista = turma.SelecionarPorAnoLetivoAtual();

            if (lista.Count == 0)
            {
                gdvChamada.DataBind();
                Mensagem.Aviso("Nenhuma turma foi localizado.");
            }
            else if (lista.Count > 0)
            {
                ViewState.Add("Grid", lista);
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("turma", System.Type.GetType("System.String"));
                foreach (Turma t in lista)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.ToString() });
                }
                gdvChamada.DataSource = dt;
                gdvChamada.DataBind();
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
            if (ddlAnoLetivo.Items.Count > 0)
                ddlAnoLetivo.SelectedIndex = 0;
            txtSerie.Text = string.Empty;
            txtTurma.Text = string.Empty;
            ddlEnsino.SelectedIndex = 0;
            ddlPeriodo.SelectedIndex = 0;
            gdvChamada.DataBind();
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
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
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

        protected void gdvChamada_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvChamada.PageIndex = e.NewPageIndex;
            Selecionar();
        }

        protected void gdvChamada_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Turma> lista = (List<Turma>)ViewState["Grid"];
            if (lista != null)
            {
                //Ordena a grid
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                List<Turma> l = new List<Turma>();

                if (sortDireciton == "ASC")
                    l = lista.OrderBy(e.SortExpression).ToList();
                else
                    l = lista.OrderByDescending(e.SortExpression).ToList();
                //Remonta a grid
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("turma", System.Type.GetType("System.String"));
                foreach (Turma t in l)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.ToString() });
                }
                gdvChamada.DataSource = dt;
                gdvChamada.DataBind();
            }
        }

        protected void gdvChamada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarChamada");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objPermissao.Altera == false)
                {
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    imgEditar.Visible = false;
                }
            }
        }

       

    }
}
