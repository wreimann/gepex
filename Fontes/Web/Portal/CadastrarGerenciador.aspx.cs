using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Entidade;
using Web.Util;
using System.Web.UI;
using System.Collections;
using System.Drawing.Imaging;


namespace Web.Portal
{
    public partial class CadastrarGerenciador : BaseCadastro, Base
    {
        
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Cadastro de Conteúdo do Portal");
            botao1.Desabilitar(false, false, false, true, true, true, true, true, false);
            if (!IsPostBack)
            {
                if (Request.QueryString["codigo"] != null)
                    this.Selecionar();
            }
            this.DesenharImagem();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Id < 1) {
                    throw new Exception("Não é possível adicionar uma imagem antes de Salvar o contéudo do portal.");
                }
                //Extensões permitidas
                string[] extensoes = new string[] { "image/pjpeg", "image/jpeg", "image/gif", "image/x-png", "image/png" };
                if (file.PostedFile.FileName == string.Empty) {
                    throw new Exception("Informe o caminho da imagem.");
                }
                FileInfo infoArquivo = new FileInfo(file.PostedFile.FileName);
                bool arquivoInvalido = false;
                if (file.PostedFile.ContentLength > 4000000)
                {
                    Mensagem1.Aviso("Tamanho da imagem é deve ser menor que 4 mb.");
                    arquivoInvalido = true;
                }
                if (!arquivoInvalido)
                {
                    arquivoInvalido = true;
                    for (int i = 0; i < extensoes.Length; i++)
                    {
                        if (file.PostedFile.ContentType == extensoes[i])
                        {
                            arquivoInvalido = false;
                            break;
                        }
                    }
                    if (arquivoInvalido)
                    {
                        Mensagem1.Aviso("Extensão do arquivo inválido. Só é permitido arquivos com extensão: .jpg, .png e .gif");
                    }
                }
                if (!arquivoInvalido)
                {

                    string arquivo = DateTime.Now.ToString().Replace(":", "").Replace("/", "-") + infoArquivo.Extension;
                    string caminho = ConfigurationManager.AppSettings["caminhoFTP"].ToString();
                    file.PostedFile.SaveAs(caminho + arquivo);
                    Util.Util util = new Util.Util();
                    util.RedimensionarImagem(arquivo, caminho, 150, 79);
                    PortalImagem objImagem = new PortalImagem();
                    objImagem.Imagem = infoArquivo.Name;
                    objImagem.Diretorio = arquivo;
                    objImagem.Portal = new Model.Entidade.Portal().Selecionar(Id);
                    objImagem.Confirmar();
                    this.DesenharImagem();
                
                }
            }
            catch (Exception ex)
            {
                this.DesenharImagem();
                Mensagem1.Aviso("Erro ao fazer o upload da imagem. Motivo: " + ex.Message);
                
            }
            
          
        }
        protected void rdlOpcao_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Opcao();
        }
        private void Opcao()
        {
            Editor1.Visible = rdlOpcao.SelectedValue != "3";
            trData.Visible = rdlOpcao.SelectedValue == "1";
            if (rdlOpcao.SelectedValue == "3")
                lblTitulo.Text = "Link";
            else
                lblTitulo.Text = "Título";
        }

        #endregion

        #region Metodos
        #region Base Members

        public void Selecionar()
        {
            try
            {
                Id = int.Parse(Request["codigo"]);
                Model.Entidade.Portal objPortal = new Model.Entidade.Portal().Selecionar(Id);
                txtTitulo.Text = objPortal.Titulo;
                Editor1.Content = objPortal.Descricao;
                txtData.Text = objPortal.Data.ToString("dd/MM/yyyy HH:mm");
                if (objPortal.Tipo == "1")
                    rdlOpcao.Items[0].Selected = true;
                else if (objPortal.Tipo == "2")
                    rdlOpcao.Items[1].Selected = true;
                else if (objPortal.Tipo == "3")
                    rdlOpcao.Items[2].Selected = true;
                this.Opcao();
                this.DesenharImagem();
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "visible", "desabilitarCampos('" + objPortal.Tipo.ToString() + "')", true);


            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.ToString());
            }
        }

        public bool Salvar()
        {
            bool result = false;
            try
            {
                if (this.ValidarCamposObrigatorios())
                {
                    Model.Entidade.Portal objPortal = new Model.Entidade.Portal();
                    objPortal.Titulo = txtTitulo.Text;
                    if (txtData.Text.Replace("_", "").Replace("/", "").Replace(":", "").Trim() == "")
                        objPortal.Data = DateTime.Now;
                    else
                        objPortal.Data = Convert.ToDateTime(txtData.Text);
                    objPortal.Descricao = Editor1.Content;
                    objPortal.Tipo = rdlOpcao.SelectedValue;
                    result = objPortal.Confirmar();
                    Id = objPortal.Codigo;
                    
                    
                }
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (FormatException)
            {
                Mensagem1.Aviso("Data Inválida.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool Alterar()
        {
            bool result = false;
            try
            {
                if (this.ValidarCamposObrigatorios())
                {
                    Model.Entidade.Portal objPortal = new Model.Entidade.Portal().Selecionar(Id);
                    objPortal.Titulo = txtTitulo.Text;
                    objPortal.Descricao = Editor1.Content;
                    objPortal.Tipo = rdlOpcao.SelectedValue;
                    if (trData.Visible)
                    {
                        if (txtData.Text.Replace("_", "").Replace("/", "").Replace(":", "").Trim() == "")
                            objPortal.Data = DateTime.Now;
                        else
                            objPortal.Data = Convert.ToDateTime(txtData.Text);
                    }
                    result = objPortal.Confirmar();
                    this.DesenharImagem();
                }
            }
            catch (Model.Base.GepexException.EBancoDados ex)
            {
                Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                Mensagem1.Aviso(ex.Message);
            }
            catch (FormatException)
            {
                Mensagem1.Aviso("Data Inválida.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool ValidarCamposObrigatorios()
        {
            bool retorno = true;
            if (Editor1.Visible && Editor1.Content.Trim() == "")
            {
                Mensagem1.Aviso("O campo descrição deve ser preenchido.");
                retorno = false;
            }
            if (trData.Visible && txtData.Text.Replace("_", "").Replace("/", "").Replace(":", "").Trim() == "")
            {
                Mensagem1.Aviso("A Data do evento deve ser informada.");
                retorno = false;
            }
            if (!Editor1.Visible)
            {
                Editor1.Content = string.Empty;
            }
            if (!trData.Visible)
            {
                txtData.Text = string.Empty;
            }
            return retorno;
        }

        public void Limpar()
        {
            throw new NotImplementedException();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }
        private void DesenharImagem(){
          pnlGaleria.Controls.Clear();
          if (Id > 0){
            Model.Entidade.Portal objPortal = new Model.Entidade.Portal().Selecionar(Id);
            if(objPortal != null && objPortal.ListaImagem != null && objPortal.ListaImagem.Count > 0){
              qtdeImagem = 0;
              for (int i = 0; i < objPortal.ListaImagem.Count; i++) {
                  ImageButton img = new ImageButton();
                  img.Width = 120;
                  img.ImageUrl = ConfigurationManager.AppSettings["caminhoFTPTemp"].ToString() + objPortal.ListaImagem[i].Diretorio;
                  qtdeImagem++;
                  img.ID = "IMAGE" + qtdeImagem.ToString();
                  img.ToolTip = objPortal.ListaImagem[i].Imagem;
                  img.Attributes.Add("onMouseOver", "this.style.cursor='hand';");
                  img.CausesValidation = false;
                  img.OnClientClick = "return OnClick();";
                  img.BorderWidth = 2;
                  img.Click += new ImageClickEventHandler(Image_Click);
                  pnlGaleria.Controls.Add(img);
              }
            
            }
          }

        }

        private int qtdeImagem
        {

            get
            {
                return ViewState["qtde"] == null ? 0 : (int)ViewState["qtde"];
            }
            set
            {
                ViewState["qtde"] = value;
            }

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
            this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgSalvarOnClick += new botao.EventHandler(BarraBotao_Click);
            this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
        }
        private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Novo":
                    Response.Redirect("../Portal/CadastrarGerenciador.aspx");
                    break;
                case "Pesquisar":
                    Response.Redirect("../Portal/ConsultarGerenciador.aspx");
                    break;
                case "Limpar":
                    this.Limpar();
                    break;
                case "Voltar":
                    Response.Redirect("../Portal/ConsultarGerenciador.aspx");
                    break;
                case "Salvar":
                    if (Id > 0)
                    {
                        if (this.Alterar())
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["03_Alteracao"].ToString());
                    }
                    else
                    {
                        if (this.Salvar())
                        {
                            Mensagem1.Aviso(ConfigurationManager.AppSettings["01_Inclusao"].ToString());
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        protected void Image_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string arquivoTMP = (sender as ImageButton).ImageUrl.ToString();
                FileInfo infoArquivoTMP = new FileInfo(arquivoTMP);
                string arquivo = ConfigurationManager.AppSettings["caminhoFTP"].ToString() + infoArquivoTMP.Name; //"e:/home/escola29ma/web/upload/Portal/" + infoArquivoTMP.Name;
                FileInfo infoArquivo = new FileInfo(arquivo);

                if (infoArquivo.Exists)
                    infoArquivo.Delete();
                if (infoArquivoTMP.Exists)
                    infoArquivoTMP.Delete();

                Model.Entidade.Portal objPortal = new Model.Entidade.Portal().Selecionar(Id);
                if (objPortal != null && objPortal.ListaImagem != null && objPortal.ListaImagem.Count > 0)
                {

                    bool existe = false;
                    int imagemBanco = 0;
                    for (int i = 0; i < objPortal.ListaImagem.Count; i++)
                    {
                        if (infoArquivo.Name == objPortal.ListaImagem[i].Diretorio)
                        {
                            existe = true;
                            imagemBanco = objPortal.ListaImagem[i].Codigo;
                            objPortal.ListaImagem.Remove(objPortal.ListaImagem[i]);
                            break;
                        }
                    }
                    if (existe && imagemBanco > 0)
                    {

                        try
                        {
                            PortalImagem portal = new PortalImagem().Selecionar(imagemBanco);
                            portal.Excluir(portal);
                        }
                        catch (Model.Base.GepexException.EBancoDados ex)
                        {
                            Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
                        }
                        catch (Model.Base.GepexException.ERegraNegocio ex)
                        {
                            Mensagem1.Aviso(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                }
                DesenharImagem();
            }
            catch (Exception ex)
            {
                Mensagem1.Aviso(ex.Message.ToString());
            }
        }

        

       


    }

}
