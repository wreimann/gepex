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
    public class PlanejamentoPedagogico: Comum<PlanejamentoPedagogico>
    {
        #region Atributos da classe
        private Disciplina disciplina;
        private Turma turma;
        private DateTime dataCadastro;
        private int cargaHoraria;
        private string ementa;
        private string competenciaHabilidades;
        private string objetivoGeral;
        private DateTime dataInicial;
        private DateTime dataFinal;
        private IList<ConteudoPedagogico> listaConteudo;

        public virtual IList<ConteudoPedagogico> ListaConteudo
        {
            get { return listaConteudo; }
            set { listaConteudo = value; }
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


        public virtual string ObjetivoGeral
        {
            get { return objetivoGeral; }
            set { objetivoGeral = value; }
        }

        public virtual string CompetenciaHabilidades
        {
            get { return competenciaHabilidades; }
            set { competenciaHabilidades = value; }
        }

        public virtual string Ementa
        {
            get { return ementa; }
            set { ementa = value; }
        }

        public virtual int CargaHoraria
        {
            get { return cargaHoraria; }
            set { cargaHoraria = value; }
        }

        public virtual DateTime DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }

        public virtual Turma Turma
        {
            get { return turma; }
            set { turma = value; }
        }

        public virtual Disciplina Disciplina
        {
            get { return disciplina; }
            set { disciplina = value; }
        }
        
       #endregion

        #region Construtores da classe

        public PlanejamentoPedagogico() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public virtual IList<PlanejamentoPedagogico> SelecionarPorCriterios(Disciplina disciplina, IList<Turma> turma)
        {
            if (disciplina == null && turma.Count == 0)
            {
                return null;
            }
            else
            {
                ICriteria criteria = Sessao.CreateCriteria(this.GetType());
                if (disciplina != null)
                    criteria.Add(Expression.Eq("Disciplina", disciplina));
                if (turma.Count > 0)
                    criteria.Add(Expression. In("Turma", turma.ToArray()));
                return criteria.List<PlanejamentoPedagogico>();
            }
        }
        #endregion
    }
}
