using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;

namespace Web.Consultas
{
	public partial class frmConsultarTipoDocumento : System.Web.UI.Page, Base

    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Consulta Tipo Documento");
                botao1.Desabilitar(false, false, true, false, true, true, true, true, false);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarTipoDocumento");
                if (objPermissao.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta Consulta Tipo Documento!');location.href='../Geral/index.aspx';</script>");
                }
                this.Selecionar();
            }
        }
        protected void gdvTipoDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvTipoDocumento.PageIndex = e.NewPageIndex;
            Selecionar();
        }
        protected void gdvTipoDocumento_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<TipoDocumento> lista = (List<TipoDocumento>)ViewState["Grid"];
            if (lista != null)
            {
                //Ordena a grid
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                List<TipoDocumento> l = new List<TipoDocumento>();

                if (sortDireciton == "ASC")
                    l = lista.OrderBy(e.SortExpression).ToList();
                else
                    l = lista.OrderByDescending(e.SortExpression).ToList();
                //Remonta a grid
                gdvTipoDocumento.DataSource = l;
                gdvTipoDocumento.DataBind();
                //trata a situacao
                for (int i = 0; i < gdvTipoDocumento.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gdvTipoDocumento.Rows[i].Cells[2].Text) == true)
                        gdvTipoDocumento.Rows[i].Cells[2].Text = "Ativo";
                    else
                        gdvTipoDocumento.Rows[i].Cells[2].Text = "Inativo";
                }
            }
        }
        protected void gdvTipoDocumento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvTipoDocumento.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarTipoDocumento.aspx?codigo=" + codigo);
        }

        protected void gdvTipoDocumento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                TipoDocumento objTipoDoc = new TipoDocumento();
                objTipoDoc.Codigo = Convert.ToInt32(gdvTipoDocumento.DataKeys[e.RowIndex].Values[0]);
                objTipoDoc.Excluir(objTipoDoc.Codigo);
                this.Selecionar();
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
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            TipoDocumento tipoDoc = new TipoDocumento();
            IList<TipoDocumento> lista = new List<TipoDocumento>();
            if (txtDescricao.Text != string.Empty)
            {
                tipoDoc.Descricao = txtDescricao.Text;
                lista = tipoDoc.SelecionarPorDescricao();
                if (lista.Count == 0)
                {
                    Mensagem.Aviso("Nenhum tipo de documento foi localizado com o filtro informado.");
                    this.Limpar();
                }
            }
            else
                lista = tipoDoc.SelecionarAtivos();

            ViewState.Add("Grid", lista);
            if (lista.Count > 0)
            {
                gdvTipoDocumento.DataSource = lista;
                gdvTipoDocumento.DataBind();
                if (gdvTipoDocumento.Rows.Count > 0)
                {
                    for (int i = 0; i < gdvTipoDocumento.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(gdvTipoDocumento.Rows[i].Cells[2].Text) == true)
                            gdvTipoDocumento.Rows[i].Cells[2].Text = "Ativo";
                        else
                            gdvTipoDocumento.Rows[i].Cells[2].Text = "Inativo";
                    }
                }
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
            txtDescricao.Text = string.Empty;
            gdvTipoDocumento.DataBind();
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
                    Response.Redirect("../Cadastros/frmCadastrarTipoDocumento.aspx");
                    break;
                case "Pesquisar":
                    this.Selecionar();
                    break;
                case "Salvar":
                    break;
                case "Limpar":
                    this.Limpar();
                    this.Selecionar();
                    break;
                case "Voltar":
                    Response.Redirect("../Geral/index.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

        protected void gdvTipoDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarTipoDocumento");
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
