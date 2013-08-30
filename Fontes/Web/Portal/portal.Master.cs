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

namespace Web.Portal
{
    public partial class portal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = lkbEnviar.UniqueID;
            if (!IsPostBack)
            {
                this.Page.ClientScript.RegisterClientScriptInclude("Script_Jquery", ResolveUrl("../App_Themes/JQuery/JQuery/js/jquery_min.js"));
                this.Page.ClientScript.RegisterClientScriptInclude("Script_JqueryUI", ResolveUrl("../App_Themes/JQuery/JQuery/js/jquery-ui.min.js"));
                this.Page.ClientScript.RegisterClientScriptInclude("Script_JqueryMenu", ResolveUrl("../App_Themes/JQuery/JQuery/js/jquery.menu_ici.js"));
                this.Page.ClientScript.RegisterClientScriptInclude("Script_JqueryMask", ResolveUrl("../App_Themes/JQuery/JQuery/js/jquery.maskedinput.js"));
                this.Page.ClientScript.RegisterClientScriptInclude("Script_JqueryUICore", ResolveUrl("../App_Themes/JQuery/JQuery/js/jquery.ui.core.js"));
                this.Page.ClientScript.RegisterClientScriptInclude("Script_JqueryDate", ResolveUrl("../App_Themes/JQuery/JQuery/js/jquery.ui.datepicker.js"));
                this.Page.ClientScript.RegisterClientScriptInclude("Script_JqueryModal", ResolveUrl("../App_Themes/JQuery/JQuery/js/jquery.nyroModal-1.5.0.js"));
                this.Page.ClientScript.RegisterClientScriptInclude("Script_JqueryText", ResolveUrl("text/javascript"));
                this.SelecionarPropagandas();
            }
        }

        protected void lkbEnviar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario().Selecionar(txtLogin.Text, txtSenha.Text);
            if (usuario == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptName", "alert('Login ou senha inválido.');", true);
            }
            else
            {
                if (usuario.Situacao == "B") 
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptName", "alert('Usuário bloqueado. Entre em contato com a secretaria da escola.');", true);
                }
                else if (usuario.Situacao == "I")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptName", "alert('Usuáro sem acesso.');", true);
                }
                else
                {
                    Session.Add("UsuarioLogado", usuario);
                    Response.Redirect("Geral/index.aspx");
                }
            }
        }
        private void SelecionarPropagandas()
        {
            Model.Entidade.Portal objPortal = new Model.Entidade.Portal();
            objPortal.Tipo = "3";
            IList<Model.Entidade.Portal> lsPropagandas = objPortal.SelecionarporTipo(5);
            if (lsPropagandas != null) 
            {
                foreach (Model.Entidade.Portal ls in lsPropagandas) 
                {
                    foreach (PortalImagem lsImg in ls.ListaImagem)
                    {
                        string caminhoTemp = "../upload/portal/tmp/" + lsImg.Diretorio;
                        LiteralControl lc = new LiteralControl(
                            "<p><a href='http://" + ls.Titulo + "'><img src='" + caminhoTemp + "'id='centro'/></a></p>"
                            );

                        Panel1.Controls.Add(lc);
                    }

                }
            }
        }

    }
}
