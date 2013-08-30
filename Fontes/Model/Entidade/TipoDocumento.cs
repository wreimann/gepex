using System;
using System.Collections.Generic;
using Model.Base;
using NHibernate;
using NHibernate.Criterion;

namespace Model.Entidade
{
    [Serializable]
    public class TipoDocumento: Comum<TipoDocumento>
    {
        #region Atributos da classe
        
        private string descricao;
        private string mascara;
        private bool situacao;

        public virtual bool Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public virtual string Mascara
        {
            get { return mascara; }
            set { mascara = value; }
        }
        
        #endregion

        #region Construtores da classe

        public TipoDocumento() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public virtual IList<TipoDocumento> SelecionarPorDescricao()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Descricao != null)
                criteria.Add(Expression.Like("Descricao", "%" + this.Descricao + "%"));
            return criteria.List<TipoDocumento>();
        }
        public virtual IList<TipoDocumento> SelecionarAtivos()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Situacao", true));
            return criteria.List<TipoDocumento>();
        }
        #endregion
    }
}
