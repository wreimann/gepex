using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;

namespace Web.Consultas
{
    public partial class frmConsultarProfissao : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Consulta de Profissão");
                botao1.Desabilitar(false, false, true, false, true, true, true, true, false);

                Permissao objPermissa = ((principal)this.Master).Permissao("frmConsultarProfissao");
                if (objPermissa.Acesso == false)
                {
                    Response.Redirect("../Geral/index.aspx");
                }
                this.Selecionar();
            }
        }   
        protected void gdvProfissao_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Profissao objProfissao = new Profissao();
                objProfissao.Codigo = Convert.ToInt32(gdvProfissao.DataKeys[e.RowIndex].Values[0]);
                objProfissao.Excluir(objProfissao.Codigo);
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
        protected void gdvProfissao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvProfissao.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarProfissao.aspx?codigo=" + codigo);
        }
        protected void gdvProfissao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvProfissao.PageIndex = e.NewPageIndex;
            Selecionar();
        }
        protected void gdvProfissao_Sorting(object sender, GridViewSortEventArgs e)
        {  
            List<Profissao> lista = (List<Profissao>)ViewState["Grid"];
            if (lista != null)
            {
                //Ordena a grid
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                List<Profissao> l = new List<Profissao>();
                
                if (sortDireciton == "ASC")
                    l = lista.OrderBy(e.SortExpression).ToList();
                else
                    l = lista.OrderByDescending(e.SortExpression).ToList();             
                //Remonta a grid
                gdvProfissao.DataSource = l;
                gdvProfissao.DataBind();
                //trata a situacao
                for (int i = 0; i < gdvProfissao.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gdvProfissao.Rows[i].Cells[1].Text) == true)
                        gdvProfissao.Rows[i].Cells[1].Text = "Ativo";
                    else
                        gdvProfissao.Rows[i].Cells[1].Text = "Inativo";
                }
            }
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            Profissao profissao = new Profissao();
            IList<Profissao> lista = new List<Profissao>();
            if (txtDescricao.Text != string.Empty)
            {
                profissao.Descricao = txtDescricao.Text;
                lista = profissao.SelecionarPorDescricao();
                if (lista.Count == 0)
                {
                    Mensagem.Aviso("Nenhuma profissão foi localizada com o filtro informado.");
                    this.Limpar();
                }
            } else
                lista = profissao.SelecionarAtivos();
                
            ViewState.Add("Grid", lista);
            if (lista.Count > 0)
            {
                gdvProfissao.DataSource = lista;
                gdvProfissao.DataBind();
                if (gdvProfissao.Rows.Count > 0)
                {
                    for (int i = 0; i < gdvProfissao.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(gdvProfissao.Rows[i].Cells[1].Text) == true)
                            gdvProfissao.Rows[i].Cells[1].Text = "Ativo";
                        else
                            gdvProfissao.Rows[i].Cells[1].Text = "Inativo";
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
            gdvProfissao.DataBind();
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
                    Response.Redirect("../Cadastros/frmCadastrarProfissao.aspx");
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

        protected void gdvProfissao_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarProfissao");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objPermissa.Altera == false)
                {
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    imgEditar.Visible = false;
                }
                if (objPermissa.Exclui == false)
                {
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                    imgExcluir.Visible = false;
                }
            }
        }
    }
}
