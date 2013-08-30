using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace Model.Entidade
{
    [Serializable]
    public class Turma: Comum<Turma>
    {    
       #region Atributos da classe
        
        private string serie;
        private string serieTurma;
        private string ensino;
        private int? sala;
        private int numeroMaximoAlunos;
        private string situacao;
        private string observacao;
        private string periodo;
        private int anoMinimo;
        private int? anoMaximo;
        private int anoLetivo;
        private IList<GradeHorario> gradeHorario;
        private IList<Matricula> alunosMatriculados;

        public virtual string PeriodoFormatado {
            get
            {   string aux = string.Empty;
                switch (this.periodo)
                {
                    case ("M"):
                        aux = "Manhã";
                        break;
                    case ("T"):
                        aux = "Tarde";
                        break;
                    case ("I"):
                        aux = "Integral";
                        break;
                }
                return aux;
            }
        }
        public virtual string EnsinoFormatado
        {
            get
            {
                string aux = string.Empty;
                switch (this.Ensino)
                {
                    case ("F"):
                        aux = "Fundamental";
                        break;
                    case ("P"):
                        aux = "Profissionalizante";
                        break;
                   
                }
                return aux;
            }
        }
        public virtual IList<Matricula> AlunosMatriculados
        {
            get { return alunosMatriculados; }
            set { alunosMatriculados = value; }
        }

        public virtual int AnoLetivo
        {
            get { return anoLetivo; }
            set { anoLetivo = value; }
        }
        public virtual string SerieTurma
        {
            get { return serieTurma; }
            set { serieTurma = value; }
        }
        public IList<GradeHorario> GradeHorario
        {
            get { return gradeHorario; }
            set { gradeHorario = value; }
        }

        public virtual int? AnoMaximo
        {
            get { return anoMaximo; }
            set { anoMaximo = value; }
        }

        public virtual int AnoMinimo
        {
            get { return anoMinimo; }
            set { anoMinimo = value; }
        }


        public virtual string Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }

        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }


        public virtual string Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        public virtual int NumeroMaximoAlunos
        {
            get { return numeroMaximoAlunos; }
            set { numeroMaximoAlunos = value; }
        }

        public virtual int? Sala
        {
            get { return sala; }
            set { sala = value; }
        }

        public virtual string Ensino
        {
            get { return ensino; }
            set { ensino = value; }
        }

        public virtual string Serie
        {
            get { return serie; }
            set { serie = value; }
        }
        
       #endregion

        #region Construtores da classe

        public Turma() { }

        #endregion

        #region Regras de Negócio

        public override string ToString()
        {
            return this.Serie + " - " + this.SerieTurma + " (" + this.PeriodoFormatado + ") - Ensino: " + EnsinoFormatado;
        }
        
        public override void Validar()
        {
            int anoLetivoAtual = DateTime.Now.Year;
            string msgErro = string.Empty;
            if (this.AnoLetivo > 2050 || this.AnoLetivo < 2007)
                msgErro = msgErro + "<b>Ano Letivo</b>: deve estar entre o intervalo de 2007 e 2050.<br />";
            if (this.AnoMinimo < 1900)
                msgErro = msgErro + "<b>Ano Nasc. Mínimo</b>: inválido! <br />";
            if(this.AnoMinimo > anoLetivoAtual)
                msgErro = msgErro + "<b>Ano Nasc. Mínimo</b>: não pode ser maior que o Ano Atual! <br />";
            if (this.AnoMaximo > anoLetivoAtual)
                msgErro = msgErro + "<b>Ano Nasc. Máximo</b>: não pode ser maior que o Ano Atual! <br />";
            if (this.AnoMaximo != 0 && this.AnoMinimo > this.AnoMaximo)
                msgErro = msgErro + "<b>Ano Nasc. Mínimo</b>: deve ser menor que Ano Nasc. Máximo! ";
            if (this.NumeroMaximoAlunos <= 0 )
                msgErro = msgErro + "<b>Nº. máximo de Alunos</b>: deve ser maior que 0! ";
            if (msgErro != "")
                throw new GepexException.ERegraNegocio("<b>Erro ao gravar a turma.</b><br /> " + msgErro);
        }

        #endregion

        #region Métodos especificos da classe
        
        public virtual IList<Turma> SelecionarPorCriterio(){
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Serie != null)
                criteria.Add(Expression.Eq("Serie", this.Serie));
            if (this.SerieTurma != null)
                criteria.Add(Expression.Eq("SerieTurma", this.SerieTurma));
            if (this.Sala != null)
                criteria.Add(Expression.Eq("Sala", this.Sala));
            if (this.Ensino != null)
                criteria.Add(Expression.Eq("Ensino", this.Ensino));
            if (this.Periodo != null)
                criteria.Add(Expression.Eq("Periodo", this.Periodo));
            if (this.AnoLetivo != 0)
                criteria.Add(Expression.Eq("AnoLetivo", this.AnoLetivo));   
                      
            return criteria.List<Turma>();
        }
        public virtual Turma SelecionarPorSerieTurma()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.SerieTurma != null)
                criteria.Add(Expression.Eq("SerieTurma", this.SerieTurma));

            IList<Turma> ls = criteria.List<Turma>();
            if (ls.Count > 0)
                return ls[0];
            else
                return null;
        }
        public virtual IList<Turma> SelecionarPorAnoLetivoAtual(){
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            int anoLetivoAtual = DateTime.Now.Year;
            criteria.Add(Expression.Eq("AnoLetivo", anoLetivoAtual));
            return criteria.List<Turma>();
        }
        public virtual IList<Turma> SelecionarPorAnoLetivo(int ano)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("AnoLetivo", ano));
            return criteria.List<Turma>();
        }
        public virtual IList<Turma> SelecionarPorMatricula(int anoLetivo, string periodo, int anoNasc)
        {
            string sql = "FROM Turma t WHERE t.AnoLetivo = " + anoLetivo.ToString() +
                         " and " + anoNasc.ToString() + " between t.AnoMinimo and IFNULL(t.AnoMaximo, 9999)";
            if (periodo.Trim() != "")
                sql = sql + " and t.Periodo = '" + periodo + "'";
            return Selecionar(sql);
        }
        
        public virtual IList<Turma> ObterAnoLetivo()
        {
            ICriteria criteria = Sessao.CreateCriteria(typeof(Turma));
            criteria.SetProjection(Projections.Distinct(Projections.ProjectionList().Add(
                Projections.Alias(Projections.Property("AnoLetivo"), "AnoLetivo")))); 
            criteria.AddOrder(Order.Desc("AnoLetivo"));
            criteria.SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(Turma)));
            IList<Turma> lista = criteria.List<Turma>();             
            return lista;
        }
        public virtual bool FinalizarAnoLetivo(int anoLetivo)
        {
            bool retorno = false;
            try
            {
                ICriteria criteria = Sessao.CreateCriteria(typeof(Turma));
                criteria.Add(Expression.Eq("AnoLetivo", anoLetivo));
                IList<Turma> lista = criteria.List<Turma>();
                foreach (Turma t in lista)
                {
                    t.situacao = "F";
                    t.Confirmar();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            retorno = true;
            return retorno;
        }

        #endregion
    }
}
