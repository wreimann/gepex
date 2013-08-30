using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Web.Util;
using GEPEX;
using Model.Entidade;
using System.Collections.Generic;

namespace Web.Consultas
{
    public partial class frmConsultarAtendimentoAluno : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((principal)this.Master).AlteraTitulo("Consultar Atendimento ao Aluno");
                botao1.Desabilitar(false, false, true, false, true, true, true, true, true);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarAtendimentoAluno");
                if (objPermissao.Acesso == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Consulta de Atendimentos!');location.href='../Geral/index.aspx';</script>");
                }

            }
            //this.Selecionar();
        }
        protected void gdvAtendimento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int codigo = Convert.ToInt32(gdvAtendimento.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect("../Cadastros/frmCadastrarAtendimentoAluno.aspx?codigo="+codigo);
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            Aluno objAluno = new Aluno();
            Pessoa objPessoa = new Pessoa();
            if (txtAluno.Text != string.Empty)
            {
                objPessoa = objPessoa.SelecionarPorNome(txtAluno.Text);
                objAluno = objAluno.SelecionarPorPessoa(objPessoa);

         
                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo", Type.GetType("System.Int32"));
                dt.Columns.Add("Altura", Type.GetType("System.String"));
                dt.Columns.Add("Peso", Type.GetType("System.String"));
                dt.Columns.Add("TipoSanguinio", Type.GetType("System.String"));
                dt.Columns.Add("FatorRH", Type.GetType("System.String"));
                dt.Columns.Add("Alergias", Type.GetType("System.String"));
                dt.Columns.Add("Medicamento", Type.GetType("System.String"));
                
                DataTable dtAtend = new DataTable();
                dtAtend.Columns.Add("codigo", Type.GetType("System.Int32"));
                dtAtend.Columns.Add("data", Type.GetType("System.String"));
                dtAtend.Columns.Add("profissao", Type.GetType("System.String"));
                dtAtend.Columns.Add("docente", Type.GetType("System.String"));
                dtAtend.Columns.Add("atendimento", Type.GetType("System.String"));
                
                DataRow dr = dt.NewRow();
                dr["Codigo"] = objAluno.Codigo;
                dr["Altura"] = objAluno.Altura;
                dr["Peso"] = objAluno.Peso;
                dr["TipoSanguinio"] = objAluno.TipoSanguineo;
                dr["FatorRH"] = objAluno.FatorRH;
                dr["Alergias"] = objAluno.Alergias;
                dr["Medicamento"] = objAluno.Medicamentos;
                dt.Rows.Add(dr);
                gdvProntuario.DataSource = dt;
                gdvProntuario.DataBind();

                Atendimento objAtendimento = new Atendimento();
                objAtendimento.Aluno = objAluno;
                IList<Atendimento> lsAtendimento = objAtendimento.SelecionarPorAluno();
                foreach (Atendimento atend in lsAtendimento) 
                {
                    DataRow drAtend = dtAtend.NewRow();
                    drAtend["codigo"] = atend.Codigo;
                    drAtend["data"] = atend.DataHorarioInicial.ToString("dd/MM/yyyy hh:mm");
                    drAtend["profissao"] = atend.Docente.Profissao.Descricao;
                    drAtend["docente"] = atend.Docente.Pessoa.Nome;
                    drAtend["atendimento"] = atend.Descricao;
                    dtAtend.Rows.Add(drAtend);
                }
                gdvAtendimento.DataSource = dtAtend;
                gdvAtendimento.DataBind();
                if (lsAtendimento.Count == 0)
                {
                    Parametro param = new Parametro().Selecionar(1);
                    Mensagem1.Aviso("Nenhum atendimento foi localizado para o aluno durante o período de " + param.MaximoDiasAtendimento.ToString() + " dias.");
                }
            }
            else
            {
                Mensagem1.Aviso("Informe o campo Aluno para pesquisa.");
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
            txtAluno.Text = string.Empty;
            gdvAtendimento.DataSource = null;
            gdvAtendimento.DataBind();
            gdvProntuario.DataSource = null;
            gdvProntuario.DataBind();
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
            this.botao1.imgFichaAlunoOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPlanejamentoClinicoOnClick+= new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPlanejamentoPedagogicoOnClick+= new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Cadastros/frmCadastrarAtendimentoAluno.aspx");
                    break;
                case "Pesquisar":
                    this.Selecionar();
                    break;
                case "Salvar":
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "FichaAluno":
                    Response.Redirect("../Cadastros/frmCadastarAluno.aspx");
                    break;
                case "planejamentoClinico":
                    Response.Redirect("../Cadastros/frmCadastrarPlanejamentoClinico.aspx");
                    break;
                case "planejamentoPedagogico":
                    Response.Redirect("../Cadastros/frmCadastrarPlanejamentoPedagogico.aspx");
                    break;
                case "Voltar":
                    Response.Redirect("../Geral/index.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

        protected void gdvAtendimento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Permissao objPermissao = ((principal)this.Master).Permissao("frmConsultarAtendimentoAluno");
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
