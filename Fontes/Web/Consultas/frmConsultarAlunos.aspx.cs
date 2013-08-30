using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Linq;
using System.Data;
using Web;

namespace GEPEX.Consultas
{
    public partial class frmConsultarAlunos : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Consulta de Alunos");
                botao1.Desabilitar(false, false, true, false, true, true, true, true, false);

                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarAlunos");
                if (objPermissao.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta de Alunos!');location.href='../Geral/index.aspx';</script>");
                }
            }
        }
        protected void gdvAlunos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvAlunos.PageIndex = e.NewPageIndex;
            Selecionar();
        }
        protected void gdvAlunos_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Aluno> lista = (List<Aluno>)ViewState["Grid"];
            if (lista != null && lista.Count > 0)
            {
                string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
                ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("nome", System.Type.GetType("System.String"));
                dt.Columns.Add("matricula", System.Type.GetType("System.String"));
                dt.Columns.Add("idade", System.Type.GetType("System.String"));
                dt.Columns.Add("situacao", System.Type.GetType("System.String"));
                foreach (Aluno t in lista)
                {
                    dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Pessoa.Nome, 
                                               t.Matricula.ToString(), Convert.ToString(Comum.CalculaIdade(t.Pessoa.DataNascimento)),
                                               t.SituacaoFormatada});
                }
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + sortDireciton;
                gdvAlunos.DataSource = dataView;
                gdvAlunos.DataBind();

            }
        }
        protected void gdvAlunos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvAlunos.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarAluno.aspx?codigo=" + codigo);
        }
        protected void gdvTurma_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Aluno aluno = new Aluno();
                int codigo = Convert.ToInt32(gdvAlunos.DataKeys[e.RowIndex].Values[0]);
                aluno = aluno.Selecionar(codigo);
                aluno.Situacao = "Inativo";
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
        protected void gdvAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarAlunos");
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
        #endregion

        #region Metodos
       
        #region Base Members

        public void Selecionar()
        {
            if (txtNome.Text.Trim() != string.Empty || txtMatricula.Text.Trim() != string.Empty)
            {
                Aluno aluno = new Aluno();
                IList<Aluno> lista = aluno.SelecionarPorNomeMatricula(txtNome.Text, txtMatricula.Text);
                if (lista.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                    dt.Columns.Add("nome", System.Type.GetType("System.String"));
                    dt.Columns.Add("matricula", System.Type.GetType("System.String"));
                    dt.Columns.Add("idade", System.Type.GetType("System.String"));
                    dt.Columns.Add("situacao", System.Type.GetType("System.String"));
                    foreach (Aluno t in lista)
                    {
                        dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Pessoa.Nome, 
                                                   t.Matricula.ToString(), Convert.ToString(Comum.CalculaIdade(t.Pessoa.DataNascimento)),
                                                   t.SituacaoFormatada});
                    }
                    gdvAlunos.DataSource = dt;
                    gdvAlunos.DataBind();
                    ViewState.Add("Grid", lista);
                }
                else
                {
                    Mensagem.Aviso("Nenhum aluno foi localizado.");
                    this.Limpar();
                }
            }
            else
                Mensagem.Aviso("Informe o nome ou matrícula do aluno.");
         
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
            txtMatricula.Text = string.Empty;
            gdvAlunos.DataBind();
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
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarAluno.aspx");
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





    }
}
