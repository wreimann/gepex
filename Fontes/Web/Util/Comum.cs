using System;
using System.Web.UI.WebControls;
using Web.Util;
using System.Collections;


namespace Web.Util
{
    public static class Comum
    {
        #region Metodos de ordenação da GridView        
        public static string ConvertSortDirectionToSql(SortDirection sortDireciton)
        {
            string m_SortDirection = String.Empty;
            switch (sortDireciton)
            {
                case SortDirection.Ascending:
                    m_SortDirection = Constante.Ascending;
                    break;

                case SortDirection.Descending:
                    m_SortDirection = Constante.Descending;
                    break;
            }
            return m_SortDirection;
        }
        public static SortDirection ConvertSqlDirectionToSort(string sortDireciton)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            switch (sortDireciton)
            {
                case Constante.Ascending:
                sortDirection = SortDirection.Ascending;
                break;

                case Constante.Descending:
                sortDirection = SortDirection.Descending;
                break;
            }
            return sortDirection;
        }
        public static SortDirection TrocarSortDirection(SortDirection sortDireciton)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            switch (sortDireciton)
            {
                case SortDirection.Ascending:
                    sortDirection = SortDirection.Descending;
                    break;

                case SortDirection.Descending:
                    sortDirection = SortDirection.Ascending;
                    break;
            }
            return sortDirection;
        }
        #endregion   

        #region Tratar mensagem de exceções do banco de dados
        public static string TraduzirMensagem(Model.Base.GepexException.EBancoDados e)
        {

            string msg = "<b>Erro ao gravar o registro.</b> Mensagem original:  <br>";
            switch (e.Numero)
            {
                case 1011:
                    msg = msg + "Erro ao deletar o registro no banco de dados.";
                    break;
                case 1012:
                    msg = msg + "Não é possível ler o registro na tabela do sistema.";
                    break;
                case 1022:
                    msg = msg + "Chave duplicada na tabela.";
                    break;
                case 1047:
                    msg = msg + "Comando SQL inválido procure o suporte técnico.";
                    break;
                case 1048:
                    msg = msg + "Campo(s) obrigátório(s) não preenchido.";
                    break;
                case 1054:
                    msg = msg + "Coluna desconhecida na tabela. Entre em contato com o suporte técnico."; 
                    break;
                case 1062:
                    msg = msg + "Regristro duplicado. Inclusão não permitida.";
                    break;
                case 1065:
                    msg = msg + "Consulta vazia.";
                    break;
                case 1074:
                    msg = msg + "Tamanho do campo maior que o permitido.";
                    break;
                case 1088:
                    msg = msg + "ID duplicado.";
                    break;
                case 1114:
                    msg = msg + "Tabela está sem espaço.";
                    break;
                case 1451:
                    msg = msg + "Registro associado a outros cadastros. Exclusão não permitida.";
                    break;
                default:
                    msg = msg + e.Message;
                    break;
            }
            return msg;
        }
        #endregion

        #region Calcular idade

        public static int CalculaIdade(DateTime DataNascimento)
        {
            if (DataNascimento.Date > DateTime.Now.Date)
                throw new Exception("Data de nascimento deve ser menor que Data atual.");
            if (DataNascimento.Year < 1900)
                throw new Exception("Data inválida.");

            int anos = DateTime.Now.Year - DataNascimento.Year;
            if (DateTime.Now.Month < DataNascimento.Month || (DateTime.Now.Month == DataNascimento.Month && DateTime.Now.Day < DataNascimento.Day))
                anos--;
            return anos;
        }
        #endregion

        #region Calcular IMC

        public static decimal CalculaIMC(decimal? Altura, decimal? Peso)
        {
            if (Altura != null && Peso != null && Altura > 0)
                return Convert.ToDecimal(Peso) / (Convert.ToDecimal(Altura) * Convert.ToDecimal(Altura));
            else
                return 0;
            
             
        }
        #endregion

        #region Mascara de Telefone 

        public static string RetiraMascaraTelefone(string telefone)
        {
           return telefone.Replace("(", "").Replace(")", "").Replace("_", "").Replace("-","").Replace(" ","");
        }
        public static string InsereMascaraTelefone(decimal? telefone)
        {
            string fone = Convert.ToString(telefone);
            if (fone != "" && telefone != null && telefone > 0)
                fone = "(" + fone.Substring(0, 2) + ") " + fone.Substring(2, 4) + "-" + fone.Substring(6, 4);
            else
                fone = "";
            return fone;
        }
        #endregion

        #region Mascara de CNAE
        //9999-9/99
        public static string RetiraMascaraCNAE(string cnae)
        {
            return cnae.Replace("_", "").Replace("-", "").Replace("_", "").Replace("/", "").Replace(" ", "");
        }
        public static string InsereMascaraCNAE(string cnae)
        {
            string novo = Convert.ToString(cnae);
            if (novo != "" && cnae != null)
                novo = "(" + novo.Substring(0, 4) + "-" + novo.Substring(4, 1) + "/" + novo.Substring(5, 2);
            else
                novo = "";
            return novo;
        }
        #endregion
        #region Mascara CEP

        public static string RetiraMascaraCEP(string cep)
        {
            return cep.Replace("_", "").Replace("-", "").Replace(" ", "");
        }
        public static string InsereMascaraCEP(int cep)
        {
            string aux = Convert.ToString(cep);
            aux = aux.Substring(0, 4) + "-" + aux.Substring(4, 4);
            return aux;
        }
        #endregion

		#region Mascara Geral

		public static string RetiraMascara(string valor)
		{
			return valor.Replace("_", "").Replace("-", "").Replace(" ", "").Replace(".","").Replace("/","");
		}
		public static string ColocarMascara(string valor, string mascara)
		{
	        string novoValor = string.Empty;
            int posicao = 0;

            for (int i = 0; mascara.Length > i; i++)
            {
                if (mascara[i] == '9' || mascara[i] == 'C')
                {
                    if (valor.Length > posicao)
                    {
                        novoValor = novoValor + valor[posicao];
                        posicao++;
                    }
                    else
                        break;
                }
                else
                {
                    if (valor.Length > posicao)
                        novoValor = novoValor + mascara[i];
                    else
                        break;
                }
            }
            return novoValor;
        }

        #endregion

        #region  Perido 
        public static string FormatarPeriodo(string periodo)
        {
            string aux = string.Empty;
            switch (periodo) { 
                case("M"):
                    aux = "Manhã";
                    break;
                case ("T"):
                    aux = "Tarde";
                    break;
                case ("I"):
                    aux = "Integral";
                    break;
            }
            return aux;
        }        

        #endregion
    }
}
