using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Model.Entidade;
using Web.Util;

namespace GEPEX.Consultas
{
    public partial class frmConsultarDisciplinas : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Consulta de Disciplinas");
            botao1.Desabilitar(false, false, true, false, true, true, true, true, false);
            /*Virifica a permissão de acesso para a página*/

            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarDisciplinas");
            if (objPermissao.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta de Disciplinas!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
                this.Selecionar();
        }
        protected void gdvMateria_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvMateria.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarDisciplinas.aspx?codigo=" + codigo);
        }

        protected void gdvMateria_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvMateria.PageIndex = e.NewPageIndex;
            this.Selecionar();
        }
        protected void gdvMateria_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Disciplina objDisciplina = new Disciplina();
                objDisciplina.Codigo = Convert.ToInt32(gdvMateria.DataKeys[e.RowIndex].Values[0]);
                objDisciplina.Excluir(objDisciplina.Codigo);
                this.Selecionar();
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
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            Disciplina objDisciplina = new Disciplina();

            try
            {
                IList<Disciplina> lsDisciplinas = null;
                if (txtDescricao.Text == string.Empty)
                {
                    lsDisciplinas = objDisciplina.SelecionarAtivos();
                }
                else
                {
                    objDisciplina.Materia = txtDescricao.Text;
                    lsDisciplinas = objDisciplina.SelecionarPorDescricao();
                }
                DataSet dsDisciplinas = objDisciplina.ToDataSet(lsDisciplinas);
                if (dsDisciplinas != null)
                {
                    ViewState.Add("Grid", lsDisciplinas);
                    gdvMateria.DataSource = dsDisciplinas;
                    gdvMateria.DataBind();
                }
                else
                {
                    gdvMateria.DataBind();
                    Mensagem.Aviso("Nenhuma disciplina foi localizado com o filtro informado.");
                }
            }
            catch (Model.Base.GepexException.EBancoDados e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
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
            gdvMateria.DataBind();
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
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarDisciplinas.aspx");
                    break;
                case "Pesquisar":
                    this.Selecionar();
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


        protected void gdvMateria_Sorting(object sender, GridViewSortEventArgs e)
        {
            IList<Disciplina> lista = (IList<Disciplina>)ViewState["Grid"];
            if (lista != null && lista.Count > 0)
            {
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                Disciplina objDisciplina = new Disciplina();
                DataSet dataTable = objDisciplina.ToDataSet(lista);
                DataView dataView = new DataView(dataTable.Tables[0]);
                dataView.Sort = e.SortExpression + " " + sortDireciton;
                gdvMateria.DataSource = dataView;
                gdvMateria.DataBind();
                
            }
        }

        protected void gdvMateria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarDisciplinas");
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
