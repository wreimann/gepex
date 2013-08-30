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
    public class Agenda : Comum<Agenda>
    {
        #region Atributos da classe

        private Docente docente;
        private DateTime data;
        private IList<Compromisso> compromissos;

        public virtual IList<Compromisso> Compromissos
        {
            get { return compromissos; }
            set { compromissos = value; }
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


        #endregion

        #region Construtores da classe

        public Agenda() { }

        #endregion

        #region Regras de Negócio

        public override void Validar()
        {

        }

        #endregion

        #region Métodos especificos da classe
        public IList<Agenda> SelecionarPorCriterio()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Docente != null)
                criteria.Add(Expression.Eq("Docente", this.Docente));
            if (this.Data != null)
                criteria.Add(Expression.Eq("Data", this.Data));
           
            return criteria.List<Agenda>();
        }
        public Agenda SelecionarPorData()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Data", this.Data));
            criteria.Add(Expression.Eq("Docente", this.Docente));
            IList<Agenda> ls = criteria.List<Agenda>();
            if (ls.Count > 0)
                return ls[0];
            else
                return null;
        }
        public List<Agenda> SelecionarAgendas(Docente objDocente, int codigoAluno)
        {
            
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
                                        
            if (objDocente != null)
                criteria.Add(Expression.Eq("Docente", objDocente));
                


            IList<Agenda> lsAgenda = criteria.List<Agenda>();            
            List<Agenda> lsAg =  new List<Agenda>();;
            foreach (Agenda ls in lsAgenda)
            {
                Agenda agenda = new Agenda();
                agenda.Docente = ls.Docente;
                agenda.Data = ls.Data;
                agenda.Compromissos = new List<Compromisso>();

                IList<Compromisso> lsCompromisso = ls.Compromissos;
                if (lsCompromisso != null)
                {
                    foreach (Compromisso lsComp in lsCompromisso)
                    {
                        if (lsComp.Aluno.Codigo == codigoAluno && lsComp.Situacao.Equals("M"))
                        {
                            agenda.Compromissos.Add(lsComp);
                        }
                    }
                    lsAg.Add(agenda);
                }
                
            }
            return lsAg;
        }

        
        #endregion
    }
}
