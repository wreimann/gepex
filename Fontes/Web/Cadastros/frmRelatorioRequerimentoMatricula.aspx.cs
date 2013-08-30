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

namespace Web.Cadastros
{
    public partial class frmRelatorioRequerimentoMatricula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Selecionar();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "javascript:window.print()", true);
            }

        }
        private void Selecionar()
        {
            Pessoa objPessoa = new Pessoa().Selecionar(Convert.ToInt32(Request.QueryString["Codigo"]));
            Aluno aluno = new Aluno().SelecionarPorPessoa(objPessoa);


            lblNome.Text = aluno.Pessoa.Nome.ToString();
            lblTelefone.Text = aluno.Pessoa.Telefone.ToString();
            lblEmergencia.Text = aluno.Emergencia.ToString();
            lblComoAjudar.Text = aluno.Contato.ToString();
            lblEmail.Text = aluno.Pessoa.Email.ToString();
            lblSites.Text = aluno.Sites.ToString();
            lblMedicarEscola.Text = aluno.Medicar.ToString();
            lblMedicamentos.Text = aluno.Medicamentos.ToString();
            lblAlergia.Text = aluno.Alergias.ToString();
            lblConvenioMedico.Text = aluno.ConvenioMedico.ToString();
            lblTelefoneInformacoesAdicionais.Text = aluno.TelefoneConvenio.ToString();
            lblCarteirinha.Text = aluno.CarteirinhaConvenio.ToString();
            lblCep.Text = aluno.Pessoa.Endereco.Cep.ToString();
            lblLogradouro.Text = aluno.Pessoa.Endereco.Logradouro.ToString();
            lblNumero.Text = aluno.Pessoa.Predical.ToString();
            lblComplemento.Text = aluno.Pessoa.Complemento.ToString();
            lblBairro.Text = aluno.Pessoa.Endereco.Bairro.ToString();
            lblCidade.Text = aluno.Pessoa.Endereco.Cidade.Descricao.ToString();
            lblUF.Text = aluno.Pessoa.Endereco.Cidade.Estado.Descricao.ToString();
            IList<Parametro> parametro = new Parametro().Selecionar();
            lblTermo.Text = parametro[0].TermoMatricula;
        }
    }
}
