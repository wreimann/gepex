using System;
using System.Collections.Generic;
using Model.Base;
using NHibernate;
using NHibernate.Criterion;

namespace Model.Entidade
{
    [Serializable]
    public class Profissao: Comum<Profissao>
    {
        #region Atributos da classe
        
        private string descricao;
        private bool situacao;
        private string tipo;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public virtual bool Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }
        public virtual string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        #endregion

        #region Construtores da classe

        public Profissao() {}

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe

        public virtual IList<Profissao> SelecionarPorDescricao()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Descricao != null)
                criteria.Add(Expression.Like("Descricao", "%" + this.Descricao + "%").IgnoreCase());
            return criteria.List<Profissao>();
        }
        public virtual IList<Profissao> SelecionarAtivos()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Situacao", true));
            return criteria.List<Profissao>();
        }
        public virtual IList<Profissao> SelecionarAtivosClinico()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Situacao", true));
            criteria.Add(Expression.Eq("Tipo", "C"));
            return criteria.List<Profissao>();
        }
        public virtual Profissao SelecionarPorEspecialidade(string descricao)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Descricao", descricao).IgnoreCase());
            IList<Profissao> lista = criteria.List<Profissao>();
            if (lista.Count == 0 || lista.Count > 1)
                return null;
            else
                return lista[0];
        }
        #endregion
    }
}
