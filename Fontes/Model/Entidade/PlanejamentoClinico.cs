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
    public class PlanejamentoClinico: Comum<PlanejamentoClinico>
    {
       #region Atributos da classe
        private Profissao profissao;
        private Aluno aluno;
        private DateTime dataCadastro;
        private string competenciaHabilidades;
        private string objetivoGeral;
        private DateTime dataInicial;
        private DateTime dataFinal;
        private int numeroAtendimento;

        public virtual int NumeroAtendimento
        {
            get { return numeroAtendimento; }
            set { numeroAtendimento = value; }
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

        public virtual DateTime DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }
        
        public virtual Aluno Aluno
        {
            get { return aluno; }
            set { aluno = value; }
        }

        public virtual Profissao Profissao
        {
            get { return profissao; }
            set { profissao = value; }
        }
       
        
       #endregion

        #region Construtores da classe

        public PlanejamentoClinico() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
           
            string msgErro = string.Empty;
            if (this.dataInicial > this.DataFinal)
                msgErro = msgErro + "A data incial deve ser menor que a data Final.";
            if (msgErro != "")
                throw new GepexException.ERegraNegocio("<b>Erro ao gravar o planejamento clínico.</b><br /> " + msgErro);
        }

        #endregion

        #region Métodos especificos da classe
        public IList<PlanejamentoClinico> SelecionarPorCriterio()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType(),"pla");
            if (this.Aluno != null)
            {
                criteria.Add(Expression.Like("pla.Aluno", this.Aluno));
                criteria.Add(Expression.Like("pla.Profissao", this.Profissao));     
            }
            DateTime dataAux = new DateTime();
            if (this.DataInicial != dataAux) 
            {
                criteria.Add(Expression.Sql("'" + this.dataInicial.ToString("yyyy-MM-dd hh:mm") + "' between PLC_DATA_INICIAL and PLC_DATA_FINAL"));
            }
            return criteria.List<PlanejamentoClinico>();
        }
        public IList<PlanejamentoClinico> SelecionarPorEspecialidade()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Profissao != null)
            {
                criteria.Add(Expression.Eq("Profissao", this.Profissao));                
            }
            DateTime dataAux = new DateTime();
            if (this.DataInicial != dataAux)
            {
                criteria.Add(Expression.Sql("'" + this.dataInicial.ToString("yyyy-MM-dd hh:mm") + "' between PLC_DATA_INICIAL and PLC_DATA_FINAL"));
            }
            return criteria.List<PlanejamentoClinico>();
        }
        public virtual IList<PlanejamentoClinico> SelecionarPorCriterios(Profissao especialidade, Aluno aluno, int anoLetivo)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
                if (especialidade != null)
                    criteria.Add(Expression.Eq("Profissao", especialidade));
                if (aluno != null)
                    criteria.Add(Expression.Eq("Aluno", aluno));
                if (anoLetivo > 0) 
                {
                    DateTime dataIni = Convert.ToDateTime("01/01/" + anoLetivo.ToString());
                    DateTime dataFin = Convert.ToDateTime("01/01/" + Convert.ToString(anoLetivo + 1));
                    criteria.Add(Expression.Ge("DataInicial", dataIni));
                    criteria.Add(Expression.Lt("DataInicial", dataFin));
                
                }
                
                return criteria.List<PlanejamentoClinico>();
            
        }
        #endregion
    }
}
