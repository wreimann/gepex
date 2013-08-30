using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;
using NHibernate;
using NHibernate.Criterion;

namespace Model.Entidade
{
    [Serializable]
    public class Disciplina: Comum<Disciplina>
    {
       #region Atributos da classe
        private string materia;
        private string descricao;
        private bool situacao;

        public virtual bool Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        public virtual string Materia
        {
            get { return materia; }
            set { materia = value; }
        }
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
 
       #endregion

        #region Construtores da classe

        public Disciplina() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public IList<Disciplina> SelecionarPorDescricao()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Materia != null)
                criteria.Add(Expression.Like("Materia", "%" + this.Materia + "%"));
            return criteria.List<Disciplina>();
        }
        public IList<Disciplina> SelecionarAtivos()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Situacao", true));
            return criteria.List<Disciplina>();
        }
        #endregion
    }
}
