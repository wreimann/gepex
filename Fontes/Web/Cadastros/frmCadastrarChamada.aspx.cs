using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;

namespace Web.Cadastros
{
    public partial class frmCadastrarChamada : System.Web.UI.Page, Base
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastro de Chamadas");
            botao1.Desabilitar(true, false, false, true, true, true, true, true, false);
            /*Virifica a permissão de acesso para a página*/
            Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarChamada");
            if (objPermissa.Acesso == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Usuário sem permissão para acessar a tela de Cadastro de Chamadas!');location.href='../Geral/index.aspx';</script>");
            }
            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                if (Request.QueryString["codigo"] != null)
                    this.Selecionar();
                else
                    this.Limpar();
            }
        }
        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            int codigo = int.Parse(Request["codigo"]);
            Turma turma = new Turma().Selecionar(codigo);
            DateTime dataAtual = Convert.ToDateTime(txtData.Text);
            if (turma != null) {
                txtTurma.Text = turma.ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", System.Type.GetType("System.String"));
                dt.Columns.Add("aluno", System.Type.GetType("System.String"));
                foreach (Matricula matricula in turma.AlunosMatriculados) {
                    dt.Rows.Add(new String[] { matricula.Aluno.Codigo.ToString(), matricula.Aluno.Pessoa.Nome });              
                }
                gdvAluno.DataSource = dt;
                gdvAluno.DataBind();
                ViewState.Add("ListaAlunos", dt);
                //checar preseça dos alunos
                IList<Chamada> listaChamada = new Chamada().SelecionarPorTurmaData(turma, dataAtual);
                foreach (Chamada chamada in listaChamada) {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (chamada.Aluno.Codigo == Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString())){
                            if (chamada.Presenca)
                            {
                                CheckBox chk = (CheckBox)gdvAluno.Rows[i].Cells[2].FindControl("chkPresenca");
                                chk.Checked = true;
                            }
                            break;
                        }
                    }
                }
            
            } else
                ViewState["ListaAlunos"] = null;
        }

        public bool Salvar()
        {
            bool retorno = false;
            int codigo = int.Parse(Request["codigo"]);
            Turma turma = new Turma().Selecionar(codigo);
            DateTime dataSelecionada = Convert.ToDateTime(txtData.Text);
            DateTime dataAtual = DateTime.Now.Date;
            if (dataSelecionada > dataAtual)
            {
                Mensagem.Aviso("Data da chamada deve ser menor ou igual a Data atual.");

            }
            else
            {

                DataTable listaAlunos = (DataTable)ViewState["ListaAlunos"];
                if (turma != null && listaAlunos != null)
                {
                    //checar preseça dos alunos
                    IList<Chamada> listaChamada = new Chamada().SelecionarPorTurmaData(turma, dataSelecionada);
                    for (int i = 0; i < listaAlunos.Rows.Count; i++)
                    {
                        Chamada chamada;
                        CheckBox chk = (CheckBox)gdvAluno.Rows[i].Cells[2].FindControl("chkPresenca");
                        int codigoChamada = 0;
                        //verifica se a chamada ja foi incluida no banco de dados
                        foreach (Chamada chamadaBanco in listaChamada)
                        {
                            if (chamadaBanco.Aluno.Codigo == Convert.ToInt32(listaAlunos.Rows[i].ItemArray[0].ToString()))
                            {
                                codigoChamada = chamadaBanco.Codigo;
                                break;
                            }
                        }
                        //atualiza
                        if (codigoChamada > 0)
                            chamada = new Chamada().Selecionar(codigoChamada);
                        else
                        {
                            //inclui registro na tabela
                            chamada = new Chamada();
                            chamada.Aluno = new Aluno().Selecionar(Convert.ToInt32(listaAlunos.Rows[i].ItemArray[0].ToString()));
                            chamada.Turma = turma;
                            chamada.Data = dataSelecionada;
                            chamada.Presenca = chk.Checked;
                        }
                        //inclui ou atualiza a presença do aluno
                        chamada.Presenca = chk.Checked;
                        try
                        {
                            retorno = chamada.Confirmar();
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
            txtTurma.Text =
            txtData.Text = string.Empty;
            gdvAluno.DataBind();
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
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    break;
                case "Pesquisar":
                    Response.Redirect("../Consultas/frmConsultarChamada.aspx");
                    break;
                case "Salvar":
                    Permissao objPermissa = ((principal)this.Master).Permissao("frmCadastrarChamada");
                    if (objPermissa.Inclui == true)
                    {
                        if (this.Salvar())
                            Mensagem.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                    }
                    else
                    {
                        Mensagem.Aviso(ConfigurationManager.AppSettings["09_Permissao_Inclusao"].ToString());
                    }
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "Voltar":
                    Response.Redirect("../Consultas/frmConsultarChamada.aspx");
                    break;
                default:
                    break;
            }
        }

        #endregion

        protected void txtData_TextChanged(object sender, EventArgs e)
        {
            if(txtData.Text != "__/__/____" )
                this.Selecionar();
        }


    }
}
