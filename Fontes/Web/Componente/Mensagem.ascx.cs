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

namespace Web
{
    public partial class Mensagem : System.Web.UI.UserControl
    {
        private string titulo;

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void Aviso(string descricao)
        {
            lblTitulo.Text = "Informação";
            lblMsg.Text = descricao;
            ModalPopupExtender1.Show();
        }

    }
}