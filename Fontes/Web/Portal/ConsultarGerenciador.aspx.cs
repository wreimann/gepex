using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.Web.UI.WebControls;
using GEPEX;
using Model.Base;
using Model.Entidade;
using Web.Util;
namespace Web.Portal {
  public partial class GerenciadorPortal : System.Web.UI.Page, Base {
    #region Eventos
    protected void Page_Load(object sender, EventArgs e) {
      ((principal)this.Master).AlteraTitulo("Consulta Conteúdo do Portal");
      botao1.Desabilitar(false, false, true, false, true, true, true, true, false);

    }
    protected void gdvPortal_PageIndexChanging(object sender, GridViewPageEventArgs e) {
      gdvPortal.PageIndex = e.NewPageIndex;
      this.Selecionar();
    }
    public SortDirection GridViewSortDirection {
      get {
        if (ViewState["sortDirection"] == null)
          ViewState["sortDirection"] = SortDirection.Ascending;
        return (SortDirection)ViewState["sortDirection"];
      }
      set {
        ViewState["sortDirection"] = value;
      }

    }
    protected void gdvPortal_Sorting(object sender, GridViewSortEventArgs e) {
      try {
        List<Model.Entidade.Portal> lista = (List<Model.Entidade.Portal>)ViewState["Grid"];
        if (lista != null) {

          string sortDireciton = Comum.ConvertSortDirectionToSql(((principal)this.Master).GridViewSortDirection);
          ((principal)this.Master).GridViewSortDirection = Comum.TrocarSortDirection(Comum.ConvertSqlDirectionToSort(sortDireciton));
          DataTable dt = new DataTable();
          dt.Columns.Add("codigo", System.Type.GetType("System.String"));
          dt.Columns.Add("data", System.Type.GetType("System.String"));
          dt.Columns.Add("tipoFormatado", System.Type.GetType("System.String"));
          dt.Columns.Add("titulo", System.Type.GetType("System.String"));
          dt.Columns.Add("descricao", System.Type.GetType("System.String"));
          foreach (Model.Entidade.Portal t in lista) {
            dt.Rows.Add(new String[] { t.Codigo.ToString(), t.Data.ToString("dd/MM/yyyy"), 
                                               t.TipoFormatado.ToString(), t.Titulo.ToString(), t.Descricao.ToString()});
          }
          DataView dataView = new DataView(dt);
          dataView.Sort = e.SortExpression + " " + sortDireciton;
          gdvPortal.DataSource = dataView;
          gdvPortal.DataBind();
        }
      }
      catch (Exception) {
        Mensagem1.Aviso(ConfigurationManager.AppSettings["07_Operacao"].ToString());
      }
    }
    protected void gdvPortal_RowDeleting(object sender, GridViewDeleteEventArgs e) {
      try {
        Model.Entidade.Portal objPortal = new Model.Entidade.Portal().Selecionar(Convert.ToInt32(gdvPortal.DataKeys[e.RowIndex].Values[0]));
        foreach (PortalImagem imagem in objPortal.ListaImagem) {
          string arquivo = "e:/home/escola29ma/web/upload/Portal/" + imagem.Diretorio;
          FileInfo infoArquivo = new FileInfo(arquivo);
          string arquivoTMP = "e:/home/escola29ma/web/upload/Portal/tmp/" + imagem.Diretorio;
          FileInfo infoArquivoTMP = new FileInfo(arquivoTMP);
          if (infoArquivo.Exists)
            infoArquivo.Delete();
          if (infoArquivoTMP.Exists)
            infoArquivoTMP.Delete();
        }

        if (objPortal.Excluir(objPortal.Codigo)) {
          Mensagem1.Aviso(ConfigurationManager.AppSettings["02_Exclusao"].ToString());
          this.Limpar();
        }
      }
      catch (Model.Base.GepexException.EBancoDados ex) {
        Mensagem1.Aviso(Comum.TraduzirMensagem(ex));
      }
      catch (Exception ex) {
        throw new Exception(ex.ToString());
      }
    }
    #endregion
    #region Metodos
    #region Base Members

    public void Selecionar() {
      Model.Entidade.Portal objPortal = new Model.Entidade.Portal();
      IList<Model.Entidade.Portal> lsPortal = null;
      objPortal.Tipo = rdlTipo.SelectedValue;
      if (txtTitulo.Text != string.Empty)
        objPortal.Titulo = txtTitulo.Text;
      if (txtData.Text != "__/__/____")
        objPortal.Data = Convert.ToDateTime(txtData.Text);
      lsPortal = objPortal.SelecionarPorCriterio();
      if (lsPortal.Count != 0) {
        ViewState["Grid"] = lsPortal;
        gdvPortal.DataSource = lsPortal;
        gdvPortal.DataBind();
      }
      else {
        this.Limpar();
        Mensagem1.Aviso("Nenhum contéudo foi localizado.");
      }
    }

    public bool Salvar() {
      throw new NotImplementedException();
    }

    public bool Alterar() {
      throw new NotImplementedException();
    }

    public bool ValidarCamposObrigatorios() {
      throw new NotImplementedException();
    }

    public void Limpar() {
      txtTitulo.Text = string.Empty;
      txtData.Text = string.Empty;
      rdlTipo.Items[0].Selected = true;
      rdlTipo.Items[1].Selected = false;
      rdlTipo.Items[2].Selected = false;
      gdvPortal.DataBind();
    }

    public void Excluir() {
      throw new NotImplementedException();
    }

    #endregion
    #endregion
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e) {
      InitializeComponent();
      base.OnInit(e);
    }
    private void InitializeComponent() {
      this.botao1.imgNovoOnClick += new botao.EventHandler(BarraBotao_Click);
      this.botao1.imgLimparOnClick += new botao.EventHandler(BarraBotao_Click);
      this.botao1.imgPesquisarOnClick += new botao.EventHandler(BarraBotao_Click);
      this.botao1.imgVoltarOnClick += new botao.EventHandler(BarraBotao_Click);
    }
    private void BarraBotao_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e) {
      switch (e.CommandName) {
        case "Novo":
          Response.Redirect("../Portal/CadastrarGerenciador.aspx");
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

    protected void gdvPortal_RowEditing(object sender, GridViewEditEventArgs e) {
      int codigo = Convert.ToInt32(gdvPortal.DataKeys[e.NewEditIndex].Values[0]);
      Response.Redirect("../Portal/CadastrarGerenciador.aspx?codigo=" + codigo);
    }

  }
}
