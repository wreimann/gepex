using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Util;
using GEPEX;
using Model.Entidade;
using System.Configuration;
using System.Data;

namespace Web.Cadastros
{
    public partial class frmCadastrarPermissoes : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((principal)this.Master).AlteraTitulo("Permissões do Perfil");
                botao1.Desabilitar(true, true, false, true, true, true, true, true, false);
                /*Virifica a permissão de acesso para a página*/
                Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarPermissoes");
                if (objPermissa != null)
                {
                    if (objPermissa.Acesso == false)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Permissões de Perfil!');location.href='../Geral/index.aspx';</script>");
                    }
                }
                if (Request.QueryString["codigo"] != null)
                    this.Selecionar();
            }
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            int codigo = int.Parse(Request["codigo"]);
            Perfil perfil = new Perfil().Selecionar(codigo);
            txtPerfil.Text = perfil.Descricao;
            IList<Formulario> formulario = new Formulario().Selecionar();
            DataTable dt = new DataTable();
            dt.Columns.Add("codigo", System.Type.GetType("System.String"));
            dt.Columns.Add("descricao", System.Type.GetType("System.String"));
            foreach (Formulario form in formulario)
            {
                dt.Rows.Add(new String[] { form.Codigo.ToString(), form.Descricao });
            }
            gdvFormulario.DataSource = dt;
            gdvFormulario.DataBind();
            ViewState.Add("ListaForm", dt);
        
            //checar permissoes            
            IList<Permissao> lista = new Permissao().SelecionarPorPerfil(perfil);
            foreach (Permissao permissao in lista)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (permissao.Formulario.Codigo == Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString()))
                    {
                        if (permissao.Acesso)
                        {
                            CheckBox chk = (CheckBox)gdvFormulario.Rows[i].Cells[2].FindControl("chkAcesso");
                            chk.Checked = true;
                        }
                        if (permissao.Inclui)
                        {
                            CheckBox chk = (CheckBox)gdvFormulario.Rows[i].Cells[3].FindControl("chkIncluir");
                            chk.Checked = true;
                        }
                        if (permissao.Altera)
                        {
                            CheckBox chk = (CheckBox)gdvFormulario.Rows[i].Cells[4].FindControl("chkAlterar");
                            chk.Checked = true;
                        }
                        if (permissao.Exclui)
                        {
                            CheckBox chk = (CheckBox)gdvFormulario.Rows[i].Cells[5].FindControl("chkExcluir");
                            chk.Checked = true;
                        }
                        break;
                    }
                }
            }
        }

        public bool Salvar()
        {
            bool retorno = false;
            int codigo = int.Parse(Request["codigo"]);
            Perfil perfil = new Perfil().Selecionar(codigo);
            DataTable listaForm = (DataTable)ViewState["ListaForm"];
            if (perfil != null)
            {
                IList<Permissao> listaPermissao = new Permissao().SelecionarPorPerfil(perfil);
                for (int i = 0; i < listaForm.Rows.Count; i++)
                {
                    Permissao permissao;
                    CheckBox chkAcesso  = (CheckBox)gdvFormulario.Rows[i].Cells[2].FindControl("chkAcesso");
                    CheckBox chkIncluir = (CheckBox)gdvFormulario.Rows[i].Cells[3].FindControl("chkIncluir");
                    CheckBox chkAlterar = (CheckBox)gdvFormulario.Rows[i].Cells[4].FindControl("chkAlterar");
                    CheckBox chkExcluir = (CheckBox)gdvFormulario.Rows[i].Cells[5].FindControl("chkExcluir");
                    int codigoPermissao = 0;
                    //verifica se a chamada ja foi incluida no banco de dados
                    foreach (Permissao permissaoBanco in listaPermissao)
                    {
                        if (permissaoBanco.Formulario.Codigo == Convert.ToInt32(listaForm.Rows[i].ItemArray[0].ToString()))
                        {
                            codigoPermissao = permissaoBanco.Codigo;
                            break;
                        }
                    }
                    //atualiza
                    if (codigoPermissao > 0)
                        permissao = new Permissao().Selecionar(codigoPermissao);
                    else
                    {
                        //inclui registro na tabela
                        permissao = new Permissao();
                        permissao.Formulario = new Formulario().Selecionar(Convert.ToInt32(listaForm.Rows[i].ItemArray[0].ToString()));
                        permissao.Perfil = perfil;
                       
                    }
                    //inclui ou atualiza a permissao do perfil
                    permissao.Acesso = chkAcesso.Checked;
                    permissao.Inclui = chkIncluir.Checked;
                    permissao.Altera = chkAlterar.Checked;
                    permissao.Exclui = chkExcluir.Checked; 
                    try
                    {
                        retorno = permissao.Confirmar();
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
                    if (!retorno)
                        break;
                }
            }
            return retorno;
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
            this.botao1.imgSalvarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarPerfil.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarPermissoes");
                    if (objPermissa != null)
                    {
                        if (objPermissa.Inclui)
                        {
                            if (this.Salvar())
                                Mensagem.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                        }
                        else
                        {
                            Mensagem.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusao"].ToString());
                        }
                    }
                    break;
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarPerfil.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion
        
    }
}
