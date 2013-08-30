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
    public class Matricula: Comum<Matricula>
    {
       #region Atributos da classe
        
        private Aluno aluno;
        private Turma turma;
        private DateTime data;
        private int anoLetivo;

        public virtual int AnoLetivo
        {
            get { return anoLetivo; }
            set { anoLetivo = value; }
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

        public Matricula() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
            string msgErro = string.Empty;
            IList<Matricula> matriculados = new Matricula().SelecionarPorTurma(this.Turma);
            if (this.Aluno.Situacao == "I")
                msgErro = msgErro + "Aluno deve preencher o requerimento de matrícula.<br>";
            if (this.Aluno.Situacao == "M")
                msgErro = msgErro + "Aluno já está matriculado.<br>";
            if (this.Turma.NumeroMaximoAlunos == matriculados.Count)
                msgErro = msgErro + "Número máximo de alunos atingido para a turma selecionada.<br>";
            if (msgErro != "")
                throw new GepexException.ERegraNegocio("<b>Erro ao gerar a matrícula.</b><br> " + msgErro);
        }

        #endregion

        #region Métodos especificos da classe
        public override void AntesConfirmar()
        {
            base.AntesConfirmar();
            this.AnoLetivo = this.Turma.AnoLetivo;
        }
        public override void AposConfirmar()
        {
            base.AposConfirmar();
            Aluno al = new Aluno().Selecionar(this.Aluno.Codigo);
            if (al.Situacao == "A" || al.Situacao == "L")
                al.Situacao = "M";
            else if (al.Situacao == "M")
                al.Situacao = "A";
            al.Confirmar();
        }

        public override void AntesExcluir()
        {
            base.AntesExcluir();
            Aluno al = new Aluno().Selecionar(this.Aluno.Codigo);
            if (al.Situacao == "M")
            {
               al.Situacao = "A";
               al.Confirmar();
            }
             
        }     
        public virtual IList<Matricula> SelecionarPorTurma(Turma turma)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Turma", turma));
            return criteria.List<Matricula>();
        }
        public virtual Matricula SelecionarPorCriterio()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Aluno != null)
                criteria.Add(Expression.Eq("Aluno", this.Aluno));
            if (this.AnoLetivo == 0)
                criteria.Add(Expression.Eq("AnoLetivo", DateTime.Now.Year));
            else
                criteria.Add(Expression.Eq("AnoLetivo", this.AnoLetivo));
            IList<Matricula> ls = criteria.List<Matricula>();
            if (ls.Count != 0)
                return ls[0];
            else
                return null;
        }
        #endregion
    }
}
