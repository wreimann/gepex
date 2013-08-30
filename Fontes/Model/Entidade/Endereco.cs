using System;
using System.Collections;
using System.Collections.Generic;
using Model.Base;
using NHibernate;
using NHibernate.Criterion;

namespace Model.Entidade
{
    [Serializable]
    public class Endereco : Comum<Endereco>
	{
        #region Atributos da classe
	    private Cidade cidade;
		private string logradouro;
		private int cep;
		private string bairro;
        
		
        public virtual Cidade Cidade
		{
			get { return cidade; }
			set {cidade= value; }
		}
		public virtual string Logradouro
		{
			get { return logradouro; }
			set {logradouro= value; }
		}
		public virtual int Cep
		{
			get { return cep; }
			set {cep= value; }
		}
		public virtual string Bairro
		{
			get { return bairro; }
			set {bairro= value; }
		}
       
                
        #endregion

        #region Construtores da classe

        public Endereco() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe

        public virtual Endereco SelecionarCep(int cep){
            
            Endereco endereco = new Endereco();
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Cep", cep));
            IList<Endereco> lista = criteria.List<Endereco>();
            if (lista.Count > 0)
                return endereco = lista[0];
            else
                return null;
        }
        public virtual IList<Endereco> SelecionarPorCriterios(string logradouro, string bairro, string cidade, string uf)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Cidade", "cid").
                                        CreateAlias("cid.Estado", "est");
            if (logradouro!= "")
                criteria.Add(Expression.Like("Logradouro", logradouro, MatchMode.Anywhere).IgnoreCase());
            if (bairro != "")
                criteria.Add(Expression.Like("Bairro", bairro, MatchMode.Anywhere).IgnoreCase());
            if (cidade != "")
                criteria.Add(Expression.Like("cid.Descricao", cidade, MatchMode.Anywhere).IgnoreCase());
            if (uf != "")
                criteria.Add(Expression.Eq("est.Sigla", uf));
            criteria.SetMaxResults(10);
            return criteria.List<Endereco>(); ;
        }

        #endregion
		
	}
}
