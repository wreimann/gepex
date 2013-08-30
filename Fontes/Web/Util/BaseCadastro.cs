using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Util
{
    public class BaseCadastro : System.Web.UI.Page
    {
        protected int Id
        {
            get { return ViewState["id"] == null ? -1 : (int)ViewState["id"]; }
            set { ViewState["id"] = value; }
        }
    }

}
