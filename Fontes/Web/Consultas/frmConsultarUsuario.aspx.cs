using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;

namespace Web.Consultas
{
    public partial class frmConsultarUsuario : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Consulta de Usuários");

            botao1.Desabilitar(false, false, true, false, true, true,true,true,false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarUsuario");
            if (objPermissao.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta de Usuários!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
                CarregaComboPerfil();

        }
        protected void gdvUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvUsuario.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarUsuario.aspx?codigo=" + codigo);
        }

        protected void gdvUsuario_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Aluno aluno = new Aluno();
                aluno.Codigo = Convert.ToInt32(gdvUsuario.DataKeys[e.RowIndex].Values[0]);
                aluno.Situacao = "I";
                aluno.Confirmar();
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
        #endregion
        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            Usuario usuario = new Usuario();
            Perfil perfil = null;
            if (ddlPerfil.SelectedValue.Trim() != "")
                perfil = new Perfil().Selecionar(Convert.ToInt32(ddlPerfil.SelectedValue));
            IList<Usuario> lista = usuario.SelecionarPorCriterio(txtNome.Text, txtLogin.Text, perfil);
            if (lista != null && lista.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("nome", System.Type.GetType("System.String"));
                dt.Columns.Add("login", System.Type.GetType("System.String"));
                dt.Columns.Add("perfil", System.Type.GetType("System.String"));
                dt.Columns.Add("situacao", System.Type.GetType("System.String"));
                foreach (Usuario t in lista)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Pessoa.Nome, 
                                               t.Login, t.Perfil.Descricao, t.SituacaoFormatada });
                }
                gdvUsuario.DataSource = dt;
                gdvUsuario.DataBind();
                ViewState.Add("Grid", lista);
            }
            else
            {
                Mensagem.Aviso("Nenhum usuário foi localizado.");
                this.Limpar();
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
            txtLogin.Text = string.Empty;
            ddlPerfil.SelectedIndex = 0;
            txtNome.Text = string.Empty;
            gdvUsuario.DataBind();
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
                    Response.Redirect("../Cadastros/frmCadastrarUsuario.aspx?editar=0");
                    break;
                case "Pesquisar":
                    if (txtLogin.Text == string.Empty)
                    {
                        if (ddlPerfil.SelectedIndex == 0)
                        {
                            if (txtNome.Text == string.Empty)
                            {
                            }
                            else
                            {
                                this.Selecionar();
                            }
                        }
                        else
                        {
                            this.Selecionar();
                        }
                    }
                    else
                    {
                        this.Selecionar();
                    }               
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

        protected void gdvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvUsuario.PageIndex = e.NewPageIndex;
            Selecionar();
        }

        protected void gdvUsuario_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Usuario> lista = (List<Usuario>)ViewState["Grid"];
            if (lista != null && lista.Count > 0)
            {
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("nome", System.Type.GetType("System.String"));
                dt.Columns.Add("login", System.Type.GetType("System.String"));
                dt.Columns.Add("perfil", System.Type.GetType("System.String"));
                dt.Columns.Add("situacao", System.Type.GetType("System.String"));
                foreach (Usuario t in lista)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Pessoa.Nome, 
                                               t.Login, t.Perfil.Descricao,t.SituacaoFormatada });
                }
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + sortDireciton;
                gdvUsuario.DataSource = dataView;
                gdvUsuario.DataBind();

            }
        }

        private void CarregaComboPerfil()
        {
            
            ddlPerfil.Items.Clear();
            ListItem itemDefault = new ListItem("Todos", "0");
            itemDefault.Selected = true;
            ddlPerfil.Items.Add(itemDefault);
            IList<Perfil> lista = new Perfil().Selecionar();
            foreach (Perfil perfil in lista)
            {
                ListItem item = new ListItem();
                item.Value = perfil.Codigo.ToString();
                item.Text = perfil.Descricao;
                item.Selected = false;
                ddlPerfil.Items.Add(item);
            }
            ddlPerfil.DataBind();
        }

        protected void gdvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarUsuario");
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
