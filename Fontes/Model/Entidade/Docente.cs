using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace Model.Entidade
{
    [Serializable]
    public class Docente: Comum<Docente>
    {
       #region Atributos da classe

        private Pessoa pessoa;
        private Profissao profissao;
        private Escolaridade formacao;
        private string curso;
        private bool situacao;
        private string observacao;

        public string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        public virtual bool Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        public virtual string Curso
        {
            get { return curso; }
            set { curso = value; }
        }

        public virtual Escolaridade Formacao
        {
            get { return formacao; }
            set { formacao = value; }
        }

        public virtual Profissao Profissao
        {
            get { return profissao; }
            set { profissao = value; }
        }

        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }
 
       #endregion

        #region Construtores da classe

        public Docente() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
            string msgErro = string.Empty;
            if (this.Pessoa.DataNascimento.Date > DateTime.Now.Date)
                msgErro = msgErro + "<b>Data de Nascimento</b>: deve menor que a data atual.";
            if (msgErro != "")
                throw new GepexException.ERegraNegocio("<b>Erro ao gravar o docente.</b><br /> " + msgErro);
        
        }

        #endregion

        #region Métodos especificos da classe

        public virtual string[] SelecionarAutoComplete(string prefixText, int count, string tipo)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Pessoa", "pes").
                                        CreateAlias("Profissao", "prof");
            criteria.Add(Expression.Like("pes.Nome", prefixText, MatchMode.Anywhere).IgnoreCase());
            if(tipo != null)
                criteria.Add(Expression.Eq("prof.Tipo",tipo).IgnoreCase());
            criteria.SetMaxResults(count);
            
            IList<Docente> lista = criteria.List<Docente>();
            ArrayList resultado = new ArrayList();
            foreach (Docente docente in lista)
            {
                resultado.Add(docente.Pessoa.Nome);
            }
            return (string[])resultado.ToArray(typeof(string));
        }
        public virtual IList<Docente> SelecionarPorNome(string nome)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Pessoa", "pes");
            criteria.Add(Expression.Like("pes.Nome", nome, MatchMode.Anywhere).IgnoreCase());
            return criteria.List<Docente>();
           
        }

        public virtual Docente SelecionarPorPessoa(Pessoa pessoa)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Pessoa", pessoa));
            IList<Docente> lista = criteria.List<Docente>();
            if (lista.Count > 0)
                return lista[0];
            else
                return null;
        }
        public virtual IList<Docente> SelecionarClinicos()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Profissao", "prof").
                                        CreateAlias("Pessoa", "pes");
            criteria.Add(Expression.Eq("prof.Tipo", "C").IgnoreCase());
            criteria.AddOrder(Order.Asc("pes.Nome"));
            return criteria.List<Docente>();
        }
        #endregion
    }
}
