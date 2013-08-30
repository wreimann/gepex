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
    public class ConteudoPedagogico: Comum<ConteudoPedagogico>
    {
       #region Atributos da classe
        private int numeroAulas;
        private string objetivoEspecifico;
        private string conteudo;
        private string metodo;
        private DateTime dataInicial;
        private DateTime dataFinal;
        private PlanejamentoPedagogico planejamento;

        public virtual PlanejamentoPedagogico Planejamento
        {
            get { return planejamento; }
            set { planejamento = value; }
        }

        public virtual DateTime DataFinal
        {
            get { return dataFinal; }
            set { dataFinal = value; }
        }

        public virtual DateTime DataInicial
        {
            get { return dataInicial; }
            set { dataInicial = value; }
        }

        public virtual string Metodo
        {
            get { return metodo; }
            set { metodo = value; }
        }
        public virtual string Conteudo
        {
            get { return conteudo; }
            set { conteudo = value; }
        }

        public virtual string ObjetivoEspecifico
        {
            get { return objetivoEspecifico; }
            set { objetivoEspecifico = value; }
        }

        public virtual int NumeroAulas
        {
            get { return numeroAulas; }
            set { numeroAulas = value; }
        } 
       #endregion

        #region Construtores da classe

        public ConteudoPedagogico() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public virtual IList<ConteudoPedagogico> SelecionarPorCriterio()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Planejamento != null)
                criteria.Add(Expression.Eq("Planejamento", this.Planejamento));

            return criteria.List<ConteudoPedagogico>();
        }
        #endregion
    }
}
