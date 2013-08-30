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
    public class AgendaAluno: Comum<AgendaAluno>
    {
       #region Atributos da classe
        
        private Aluno aluno;
        private Docente docente;
        private DateTime data;
        private string recado;

        public virtual string Recado
        {
            get { return recado; }
            set { recado = value; }
        }

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public virtual Docente Docente
        {
            get { return docente; }
            set { docente = value; }
        }

        public virtual Aluno Aluno
        {
            get { return aluno; }
            set { aluno = value; }
        }
 
       #endregion

        #region Construtores da classe

        public AgendaAluno() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public IList<AgendaAluno> SelecionarPorCriterio()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Aluno != null)
            {
                criteria.Add(Expression.Like("Aluno", this.Aluno));                
            }
            if (this.Data != null)
            {
                criteria.Add(Expression.Eq("Data", this.Data));
            }
            return criteria.List<AgendaAluno>();
        }
        public IList<AgendaAluno> SelecionarPorAluno()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Aluno != null)
            {
                criteria.Add(Expression.Eq("Aluno", this.Aluno));
            }
            
            return criteria.List<AgendaAluno>();
        }
        #endregion
    }
}
