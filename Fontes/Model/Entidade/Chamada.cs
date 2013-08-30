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
    public class Chamada: Comum<Chamada>
    {
       #region Atributos da classe
        private Aluno aluno;
        private Turma turma;
        private DateTime data;
        private bool presenca;

        public virtual bool Presenca
        {
            get { return presenca; }
            set { presenca = value; }
        }
        public virtual Aluno Aluno
        {
            get { return aluno; }
            set { aluno = value; }
        }
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        public virtual Turma Turma
        {
            get { return turma; }
            set { turma = value; }
        } 
       #endregion

        #region Construtores da classe

        public Chamada() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public virtual IList<Chamada> SelecionarPorTurmaData(Turma turma, DateTime data)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Turma", turma));
            criteria.Add(Expression.Eq("Data", data));
            return criteria.List<Chamada>();      
        }
        #endregion
    }
}
