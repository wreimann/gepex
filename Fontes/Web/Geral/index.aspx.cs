using System;
using Model.Entidade;
using System.Collections.Generic;

namespace GEPEX.Geral
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((principal)this.Master).AlteraTitulo("Home");            
        }

    }
}
