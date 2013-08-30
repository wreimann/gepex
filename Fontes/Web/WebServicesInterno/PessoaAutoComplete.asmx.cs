using System.Web.Services;
using Model.Entidade;

namespace Web.Ajax
{
    [WebService(Namespace = "http://tempuri.org/")]
    
    [System.Web.Script.Services.ScriptService]
    public class PessoaAutoComplete : System.Web.Services.WebService
    {
        [WebMethod][System.Web.Script.Services.ScriptMethod]
        public string[] CompletarList(string prefixText, int count)
        {
           Pessoa pessoa = new Pessoa();
           return pessoa.SelecionarAutoComplete(prefixText, count);
        
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] ListaAluno(string prefixText, int count)
        {
            Aluno aluno = new Aluno();
            return aluno.SelecionarAutoComplete(prefixText, count);

        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] ListaDocente(string prefixText, int count)
        {
            Docente docente = new Docente();
            return docente.SelecionarAutoComplete(prefixText, count, null);

        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] ListaPedagogico(string prefixText, int count)
        {
            Docente docente = new Docente();
            return docente.SelecionarAutoComplete(prefixText, count, "P");

        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] ListaClinico(string prefixText, int count)
        {
            Docente docente = new Docente();
            return docente.SelecionarAutoComplete(prefixText, count, "C");

        }
    }
}
