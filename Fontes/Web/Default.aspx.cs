using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Web.Portal
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            this.Selecionar();
        }
        private void Selecionar()
        {
            Model.Entidade.Portal objPortal = new Model.Entidade.Portal();

            //Seleciona os 5 primeiros eventos.
            objPortal.Tipo = "1";
            IList<Model.Entidade.Portal> lsEventos = objPortal.SelecionarporTipo(5);
            for (int i = 0; i < lsEventos.Count; i++)
            {
                LiteralControl lc = new LiteralControl(@"<table>
                    <tr>
                        <td WIDTH=31%>" + lsEventos[i].Data.ToString("dd/MM/yyyy HH:mm") + "</td>" +
                        "<td><a href=Portal/Eventos.aspx?codigo=" + lsEventos[i].Codigo + "><p>" + lsEventos[i].Titulo.Trim() + " ...</p></a></td>" +
                    "</tr>" +
                "</table>" +
                "<HR WIDTH=100%>");

                Panel1.Controls.Add(lc);
            }
            
            //seleciona as 5 primeiras noticias
            objPortal.Tipo = "2";
            IList<Model.Entidade.Portal> lsNoticias = objPortal.SelecionarporTipo(5);
            for (int i = 0; i < lsNoticias.Count; i++)
            {
                LiteralControl lc = new LiteralControl(@"<table>
                    <tr>
                        <td WIDTH=31%>" + lsNoticias[i].Data.ToString("dd/MM/yyyy HH:mm") + "</td>" +
                        "<td><a href=Portal/Noticias.aspx?codigo=" + lsNoticias[i].Codigo + "><p>" + lsNoticias[i].Titulo.Trim() + " ...</p></a></td>" +
                    "</tr>" +
                "</table>" +
                "<HR WIDTH=100%>");

                Panel2.Controls.Add(lc);
            }
        }
        
    }
}
