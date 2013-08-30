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
    public class Atendimento: Comum<Atendimento>
    {
       #region Atributos da classe
        
        private Compromisso compromisso;
        private Profissao profissao;
        private Aluno aluno;
        private Docente docente;
        private string descricao;        
        private DateTime dataHorarioInicial;
        private DateTime dataHorarioFinal;
        private DateTime dataAlteracao;
        private string tipoAtendimento;
        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public virtual string TipoAtendimento
        {
            get { return tipoAtendimento; }
            set { tipoAtendimento = value; }
        }

        public virtual DateTime DataAlteracao
        {
            get { return dataAlteracao; }
            set { dataAlteracao = value; }
        }

        public virtual DateTime DataHorarioFinal
        {
            get { return dataHorarioFinal; }
            set { dataHorarioFinal = value; }
        }

        public virtual DateTime DataHorarioInicial
        {
            get { return dataHorarioInicial; }
            set { dataHorarioInicial = value; }
        }
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
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
        public virtual Compromisso Compromisso
        {
            get { return compromisso; }
            set { compromisso = value; }
        }
        public virtual Profissao Profissao
        {
            get { return profissao; }
            set { profissao = value; }
        }
       #endregion

        #region Construtores da classe

        public Atendimento() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
            string msgErro = string.Empty;
            if(this.DataHorarioInicial > DateTime.Now)
                msgErro = msgErro + "<b>Data Atendimento</b>: deve ser menor ou igual que a Data atual.<br />";    
            if (this.DataHorarioInicial >= this.dataHorarioFinal)
                msgErro = msgErro + "<b>Data/Hora Final</b>: deve ser maior que a Data/Hora Final.<br />";
            Parametro param = new Parametro().Selecionar(1);
            if (this.DataAlteracao < DateTime.Now.AddDays((int)(-1 * param.MaximoDiasAtendimento)))
                msgErro = msgErro + "Não é permitido alterar um atendimento após " + param.MaximoDiasAtendimento.ToString() + " dias a Data de sua última alteração.<br />";
            //garante um compromisso na especialidade para o aluno
            if (SobreposicaoEspecialidade(this.Aluno.Codigo, this.Profissao.Codigo, this.Data))
                msgErro = msgErro + "Não é permitido a inclusão de um novo atendimento para o aluno na mesma especialidade e data . <br />";
            //verifica a sobre posição do horário do aluno
            if (SobreposicaoHorarioAluno(this.Aluno.Codigo, this.dataHorarioInicial, this.DataHorarioFinal))
                msgErro = msgErro + "Inclusão não permitida. Sobreposição de horário do aluno. <br />";
            //verifica a sobre posição do horário do docente
            if (SobreposicaoHorarioDocente(this.Docente.Codigo, this.dataHorarioInicial, this.DataHorarioFinal))
                msgErro = msgErro + "Inclusão não permitida. Sobreposição de horário do docente.";
                        
            if (msgErro != "")
                throw new GepexException.ERegraNegocio("<b>Erro ao gravar o atedimento.</b> <br /> " + msgErro);
        }
        public override void AntesConfirmar()
        {
            base.AntesConfirmar();
            this.DataAlteracao = DateTime.Now;
            this.TipoAtendimento = this.Profissao.Tipo;
            if (this.Codigo == 0)
                this.Data = this.dataHorarioInicial.Date;
        }
        #endregion

        #region Métodos especificos da classe
        public IList<Atendimento> SelecionarPorCriterio()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Aluno != null)            
                criteria.Add(Expression.Eq("Aluno", this.Aluno));            
            if (this.Docente != null)            
                criteria.Add(Expression.Eq("Docente", this.Docente));
            if (this.Profissao != null)
                criteria.Add(Expression.Eq("Profissao", this.Profissao));   
            if(this.Data != null)
                criteria.Add(Expression.Eq("Data", this.Data));   

            return criteria.List<Atendimento>();
        }
        public IList<Atendimento> SelecionarPorAluno()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Aluno != null)
                criteria.Add(Expression.Eq("Aluno", this.Aluno));
            Parametro param = new Parametro().Selecionar(1);
            criteria.Add(Expression.Ge("Data",DateTime.Now.AddDays((int)(-1 * param.MaximoDiasAtendimento))));
          
            return criteria.List<Atendimento>();
        }
        public virtual Int32 NumeroAtendimentos()
        {
            var count = (Int32)Sessao.CreateCriteria(this.GetType()) 
                .Add(Expression.Eq("Aluno", this.Aluno))
                .Add(Expression.Eq("Docente", this.Docente))
                .SetProjection(Projections.Count("Codigo")).UniqueResult();
            return count;
        }

        public bool SobreposicaoEspecialidade(int codAluno, int codEspecialidade, DateTime data)
        {
            bool retorno = false;
            string sql = "FROM Atendimento as c WHERE c.Aluno = " + codAluno.ToString() +
                " and Profissao = " + codEspecialidade.ToString() + " and Data = '" + data.ToString("yyyy-MM-dd") + "'";
            IList<Atendimento> lista = Selecionar(sql);
            if (lista.Count > 0)
                retorno = true;
            return retorno;
        }
        public bool SobreposicaoHorarioAluno(int codAluno, DateTime dataInicial, DateTime dataFinal)
        {
            bool retorno = false;
            string sql = "FROM Atendimento as c WHERE c.Aluno = " + codAluno.ToString() +
                " and ( (c.DataHorarioInicial between '" + dataInicial.ToString("yyyy-MM-dd hh:mm") + "' and '" + dataFinal.ToString("yyyy-MM-dd hh:mm") + "')" +
                " or (c.DataHorarioInicial between '" + dataInicial.ToString("yyyy-MM-dd hh:mm") + "' and '" + dataFinal.ToString("yyyy-MM-dd hh:mm") + "'))";
            IList<Atendimento> lista = Selecionar(sql);
            if (lista.Count > 0)
                retorno = true;
            return retorno;
        }

        public bool SobreposicaoHorarioDocente(int codDocente, DateTime dataInicial, DateTime dataFinal)
        {
            bool retorno = false;
            string sql = "FROM Atendimento as c WHERE c.Docente = " + codDocente.ToString() +
                " and ( (c.DataHorarioInicial between '" + dataInicial.ToString("yyyy-MM-dd hh:mm") + "' and '" + dataFinal.ToString("yyyy-MM-dd hh:mm") + "')" +
                " or  (c.DataHorarioFinal between '" + dataInicial.ToString("yyyy-MM-dd hh:mm") + "' and '" + dataFinal.ToString("yyyy-MM-dd hh:mm") + "'))";
            IList<Atendimento> lista = Selecionar(sql);
            if (lista.Count > 0)
                retorno = true;
            return retorno;
        }

        #endregion
    }
}
