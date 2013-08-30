using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Model.Entidade;
using System.Collections.Generic;

namespace GEPEX
{
    public partial class principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] nome = usuarioLogado.Pessoa.Nome.Split(' ');
            lblUsuarioLogado.Text = "Bem vindo, " + nome[0];
            this.DesabilitaMenu();
            this.CarregaPemissao();
        }
        public void AlteraTitulo(string legenda)
        {
            lblLegenda.Text = legenda;
        }

        /*Autor: Wellingthon Reimann - Data:01/09/2010
         Objetivo: Armazenar na memoria da pagina (ViewState) a direção da coluna (Crescente ou Descendente). 
         */
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }

        }

        public Usuario usuarioLogado
        {
            get
            {
                Usuario usu = (Usuario)Session["usuarioLogado"];
                if (usu == null)
                {
                    Response.Redirect("../Default.aspx");
                }
                return usu;
            }
        }

        public Permissao Permissao(string formulario)
        {
            Usuario objUsuario = new Usuario();
            objUsuario = this.usuarioLogado;
            string usuario = objUsuario.Login;
            string senha = objUsuario.Senha;
            objUsuario = objUsuario.SelecionarPorLogin(usuario);
            List<string> ls = new List<string>();

            Permissao objPemissao = new Permissao();
            IList<Permissao> lsPermissao = objPemissao.SelecionarPorPerfil(objUsuario.Perfil);
            foreach (Permissao lsPer in lsPermissao)
            {
                Formulario objFomulario = lsPer.Formulario;
                if (formulario.Equals(objFomulario.Descricao))
                {
                    objPemissao = lsPer;
                    break;
                }
            }
            return objPemissao;


        }
        private void CarregaPemissao()
        {
            Usuario objUsuario = new Usuario();
            objUsuario = this.usuarioLogado;
            Permissao objPemissao = new Permissao();

            IList<Permissao> lsPermissao = objPemissao.SelecionarPorPerfil(objUsuario.Perfil);
            if (lsPermissao != null)
            {
                foreach (Permissao lsPer in lsPermissao)
                {
                    Formulario objFomulario = lsPer.Formulario;
                    if (lsPer.Acesso)
                        this.AbilitaMenu(objFomulario.Descricao);
                }
            }
        }

        public void AbilitaMenu(string formulario)
        {
            switch (formulario)
            {
                case "frmCadastrarParametro":
                    apnAdministrado.Visible = true;
                    liCadastroParametro.Visible = true;
                    break;
                case "frmConsultarPerfil":
                    apnAdministrado.Visible = true;
                    liConsultarPerfil.Visible = true;
                    break;
                case "frmConsultarGerenciador":
                    apnAdministrado.Visible = true;
                    liConsultarGerenciador.Visible = true;
                    break;
                case "frmConsultarUsuario":
                    apnAdministrado.Visible = true;
                    liConsultarUsuario.Visible = true;
                    break;
                case "frmConsultarAlunos":
                    apnSecretaria.Visible = true;
                    liConsultarAlunos.Visible = true;
                    break;
                case "frmConsultarDisciplinas":
                    apnSecretaria.Visible = true;
                    liConsultarDisciplinas.Visible = true;
                    break;
                case "frmConsultarDocente":
                    apnSecretaria.Visible = true;
                    liConsultarDocente.Visible = true;
                    break;
                case "frmGerarMatricula":
                    apnSecretaria.Visible = true;
                    liGerarMatricula.Visible = true;
                    break;
                case "frmConsultarGradeHorario":
                    apnSecretaria.Visible = true;
                    liConsultarGradeHorario.Visible = true;
                    break;
                case "frmConsultarProfissao":
                    apnSecretaria.Visible = true;
                    liConsultarProfissao.Visible = true;
                    break;
                case "frmConsultarTipoDocumento":
                    apnSecretaria.Visible = true;
                    liConsultarTipoDocumento.Visible = true;
                    break;
                case "frmConsultarTurma":
                    apnSecretaria.Visible = true;
                    liConsultarTurma.Visible = true;
                    break;
                case "frmCadastrarAgendaAluno":
                    apnAtendimento.Visible = true;
                    liCadastarAgendaAluno.Visible = true;
                    break;
                case "frmConsultarAtendimentoAluno":
                    apnAtendimento.Visible = true;
                    liConsultarAtendimentoAluno.Visible = true;
                    break;
                case "frmConsultarAgendaAtendimento":
                    apnAtendimento.Visible = true;
                    liConsultarAgendaAtendimento.Visible = true;
                    break;
                case "frmConsultarChamada":
                    apnAtendimento.Visible = true;
                    liConsultarChamada.Visible = true;
                    break;
                case "frmConsultarPlanejamentoClinico":
                    apnAtendimento.Visible = true;
                    liConsultarPlanejamentoClinico.Visible = true;
                    break;
                case "frmConsultarPlanejamentoPedagogico":
                    apnAtendimento.Visible = true;
                    liConsultarPlanejamentoPedagogico.Visible = true;
                    break;
                case "frmAlterarSenha":
                    apnAluno.Visible = true;
                    liAlterarSenha.Visible = true;
                    break;
                case "frmConsultarAgendaAluno":
                    apnAluno.Visible = true;
                    liConsultarAgendaAluno.Visible = true;
                    break;
                case "frmRequerimentoMatricula":
                    apnAluno.Visible = true;
                    liRequerimentoMatricula.Visible = true;
                    break;
                case "GraficoAtendimento":
                    //apnRelatorios.Visible = true;
                    //liGrafico.Visible = true;
                    break;
                case "frmConsultarEndereco":
                    apnSecretaria.Visible = true;
                    liConsultarEndereco.Visible = true;
                    break;
                default:
                    break;
            }
        }
        public void DesabilitaMenu()
        {
            liCadastroParametro.Visible = false;
            liConsultarPerfil.Visible = false;
            liConsultarGerenciador.Visible = false;
            liConsultarUsuario.Visible = false;
            liConsultarAlunos.Visible = false;
            liConsultarDisciplinas.Visible = false;
            liConsultarDocente.Visible = false;
            liGerarMatricula.Visible = false;
            liConsultarGradeHorario.Visible = false;
            liConsultarProfissao.Visible = false;
            liConsultarTipoDocumento.Visible = false;
            liConsultarTurma.Visible = false;
            liCadastarAgendaAluno.Visible = false;
            liConsultarAtendimentoAluno.Visible = false;
            liConsultarAgendaAtendimento.Visible = false;
            liConsultarChamada.Visible = false;
            liConsultarPlanejamentoClinico.Visible = false;
            liConsultarPlanejamentoPedagogico.Visible = false;
            liAlterarSenha.Visible = false;
            liConsultarAgendaAluno.Visible = false;
            liRequerimentoMatricula.Visible = false;
            liGrafico.Visible = false;
            liConsultarEndereco.Visible = false;
            apnAdministrado.Visible = false;
            apnAluno.Visible = false;
            apnAtendimento.Visible = false;
            apnSecretaria.Visible = false;
            apnRelatorios.Visible = false;
        }

        protected void lbtSair_Click(object sender, EventArgs e)
        {
            Session["usuarioLogado"] = null;
            Response.Redirect("../Default.aspx");

        }
    }
}
