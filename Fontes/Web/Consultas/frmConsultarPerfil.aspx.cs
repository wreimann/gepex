using System;
using System.Data;
using System.Web.UI.WebControls;
using GEPEX;
using Web.Util;
using Model.Entidade;
using System.Collections.Generic;
using System.Linq;

namespace Web.Consultas
{
	public partial class frmConsultarPerfil : System.Web.UI.Page, Base
    {

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Perfil de Acesso");
                botao1.Desabilitar(true, true, true, true, true, true, true, true, false);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarPerfil");
                if (objPermissao != null)
                {
                    if (objPermissao.Acesso == false)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Perfil de Acesso!');location.href='../Geral/index.aspx';</script>");
                    }
                }
                this.Selecionar();
            }
        }
        protected void gdvPerfil_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvPerfil.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarPermissoes.aspx?codigo=" + codigo);
        }

        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            IList<Perfil> lista = new Perfil().Selecionar();
            ViewState.Add("Grid", lista);
            gdvPerfil.DataSource = lista;
            gdvPerfil.DataBind();
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
            throw new NotImplementedException();
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
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Voltar":
                    Response.Redirect("../Geral/index.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

        protected void gdvPerfil_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Perfil> lista = (List<Perfil>)ViewState["Grid"];
            if (lista != null)
            {
                //Ordena a grid
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                List<Perfil> l = new List<Perfil>();

                if (sortDireciton == "ASC")
                    l = lista.OrderBy(e.SortExpression).ToList();
                else
                    l = lista.OrderByDescending(e.SortExpression).ToList();
                //Remonta a grid
                gdvPerfil.DataSource = l;
                gdvPerfil.DataBind();    
            }

        }

        protected void gdvPerfil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarPerfil");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objPermissao != null)
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
}
