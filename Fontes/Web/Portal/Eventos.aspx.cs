using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entidade;

namespace Web.Portal
{
    public partial class Eventos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Selecionar();                            
        }
        private void Selecionar()
        {
            Model.Entidade.Portal objPortal = new Model.Entidade.Portal();
            objPortal.Tipo = "1";
            IList<Model.Entidade.Portal> lsPortal;
            if (Request.QueryString["codigo"] != null)
            {                
                objPortal.Codigo = Convert.ToInt32(Request.QueryString["codigo"]);
                lsPortal = objPortal.SelecionarporCodigo();
            }
            else
            {                
                lsPortal = objPortal.SelecionarporTipo(0);               
            }
            if (lsPortal != null)
            {
                for (int i = 0; i < lsPortal.Count; i++)
                {

                    LiteralControl lc = new LiteralControl(@"<HR WIDTH=100%>
                    <table>
                    <tr>
                        <td>Evento:<b> " + lsPortal[i].Titulo + "</b></td>" +
                        "</tr>" +
                        "<tr><td>Data: " + lsPortal[i].Data.ToString("dd/MM/yyyy HH:mm") + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>" + lsPortal[i].Descricao + "</td>" +
                        "</tr>" +
                     "</table>"
                    );
                    Panel1.Controls.Add(lc);
                    Panel pn = new Panel();
                    foreach (PortalImagem ls in lsPortal[i].ListaImagem)
                    {
                        string caminho = "../upload/Portal/tmp/" + ls.Diretorio;
                        string caminhoTemp = "../upload/Portal/tmp/" + ls.Diretorio;
                        LiteralControl lcImagens = new LiteralControl(
                            "<a rel='gal" + i.ToString() + "' href='" + caminho + "' class='nyroModal' title='" + ls.Imagem + "' ><img src='" + caminhoTemp + "' Width=50px Height=50px/></a>"
                            );
                        pn.Controls.Add(lcImagens);
                        Panel1.Controls.Add(pn);
                    }

                }
            }
        }
    }
}
