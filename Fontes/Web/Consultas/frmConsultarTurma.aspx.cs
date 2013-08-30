using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Linq;

namespace Web.Consultas
{
    public partial class frmConsultarTurma : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAnoLetivo();
                ((principal)this.Master).AlteraTitulo("Consulta Turma");
                botao1.Desabilitar(false, false, true, false, true, true, true, true, false);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarTurma");
                if (objPermissao.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta Turma!');location.href='../Geral/index.aspx';</script>");
                }
                this.Selecionar();
            }
        }
        protected void gdvTurma_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvTurma.PageIndex = e.NewPageIndex;
            Selecionar();
        }
        protected void gdvTurma_Sorting(object sender, GridViewSortEventArgs e)
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
                gdvTurma.DataSource = l;
                gdvTurma.DataBind();
                //trata campos da grid
                for (int i = 0; i < gdvTurma.Rows.Count; i++)
                {
                    string aux = string.Empty;
                    //Ensino                       
                    string ensino = gdvTurma.Rows[i].Cells[3].Text;
                    switch (ensino)
                    {
                        case "F":
                            aux = "Fundamental";
                            break;
                        case "P":
                            aux = "Profissionalizante";
                            break;
                    };
                    gdvTurma.Rows[i].Cells[3].Text = aux;
                    //Periodo
                    aux = string.Empty;
                    string perido = gdvTurma.Rows[i].Cells[4].Text;
                    switch (perido)
                    {
                        case "M":
                            aux = "Manhã";
                            break;
                        case "T":
                            aux = "Tarde";
                            break;
                        case "I":
                            aux = "Integral";
                            break;
                    };
                    gdvTurma.Rows[i].Cells[4].Text = aux;

                }
            }
        }
        protected void gdvTurma_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Turma turma = new Turma().Selecionar(Convert.ToInt32(gdvTurma.DataKeys[e.RowIndex].Values[0]));
                turma.Situacao = "I" ;
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
        protected void gdvTurma_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvTurma.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarTurma.aspx?codigo=" + codigo);
        }
        #endregion

        #region Metodos
        #region Base Members

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
            if (txtSala.Text.Trim() != string.Empty)
            {
                criterio = true;
                turma.Sala = Convert.ToInt32(txtSala.Text);
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
                gdvTurma.DataBind();
                Mensagem.Aviso("Nenhuma turma foi localizado.");
            }
            else if (lista.Count > 0)
            {
                ViewState.Add("Grid", lista);
                gdvTurma.DataSource = lista;
                gdvTurma.DataBind();
                if (gdvTurma.Rows.Count > 0)
                {
                    for (int i = 0; i < gdvTurma.Rows.Count; i++)
                    {
                        string aux = string.Empty;
                        //Ensino                       
                        string ensino = gdvTurma.Rows[i].Cells[3].Text;
                        switch (ensino)
                        {
                            case "F":
                                aux = "Fundamental";
                                break;
                            case "P":
                                aux = "Profissionalizante";
                                break;
                        };
                        gdvTurma.Rows[i].Cells[3].Text = aux;
                        //Periodo
                        aux = string.Empty;
                        string perido = gdvTurma.Rows[i].Cells[4].Text;
                        switch (perido)
                        {
                            case "M":
                                aux = "Manhã";
                                break;
                            case "T":
                                aux = "Tarde";
                                break;
                            case "I":
                                aux = "Integral";
                                break;
                        };
                        gdvTurma.Rows[i].Cells[4].Text = aux;
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
            if(ddlAnoLetivo.Items.Count > 0)
                ddlAnoLetivo.SelectedIndex = 0;
            txtSala.Text = string.Empty;
            txtSerie.Text = string.Empty;
            txtTurma.Text = string.Empty;
            ddlEnsino.SelectedIndex = 0;
            ddlPeriodo.SelectedIndex = 0;
            gdvTurma.DataSource = null;
            gdvTurma.DataBind();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
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
                    Response.Redirect("../Cadastros/frmCadastrarTurma.aspx");
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

        protected void gdvTurma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarTurma");
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
