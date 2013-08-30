using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;
using NHibernate.Criterion;
using NHibernate;
using MySql.Data.MySqlClient;

namespace Model.Entidade
{
    [Serializable]
    public class Compromisso: Comum<Compromisso>
    {
       #region Atributos da classe

        private Agenda agenda;
        private Aluno aluno;
        private DateTime data;      
        private DateTime horarioInicial;
        private DateTime horarioFinal;
        private string situacao;
        private string motivo;
        private Profissao profissao;

        public virtual Profissao Profissao
        {
            get { return profissao; }
            set { profissao = value; }
        }


        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        public virtual string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }
        public virtual string Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }
        public virtual Agenda Agenda
        {
            get { return agenda; }
            set { agenda = value; }
        }
        public virtual Aluno Aluno
        {
            get { return aluno; }
            set { aluno = value; }
        }
        public virtual DateTime HorarioInicial
        {
            get { return horarioInicial; }
            set { horarioInicial = value; }
        }
        public virtual DateTime HorarioFinal
        {
            get { return horarioFinal; }
            set { horarioFinal = value; }
        }
       #endregion

        #region Construtores da classe

        public Compromisso() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
            string msgErro = string.Empty;
            //nao permite alterar o registro
            if (this.Codigo > 0)
                msgErro = msgErro + "Não é permitido alterar um compromisso. <br />";
            //garante um compromisso na especialidade para o aluno
            if (SobreposicaoEspecialidade(this.Aluno.Codigo, this.Profissao.Codigo, this.Data))
                msgErro = msgErro + "Não é permitido a inclusão de um novo compromisso para o aluno na mesma especialidade e data. <br />";
            //verifica a sobre posição do horário do aluno
            if (SobreposicaoHorarioAluno(this.Aluno.Codigo, this.horarioInicial, this.HorarioFinal))
                msgErro = msgErro + "Inclusão não permitida. Sobreposição de horário da agenda do aluno. <br />";
            //verifica a sobre posição do horário do docente
            if (SobreposicaoHorarioDocente(this.Agenda.Docente.Codigo, this.horarioInicial, this.HorarioFinal))
                msgErro = msgErro + "Inclusão não permitida. Sobreposição de horário da agenda do docente.";
       
            if (msgErro != "")
                throw new GepexException.ERegraNegocio("<b>Erro ao gravar o compromisso.</b><br /> " + msgErro);

        }

        public override void AntesConfirmar()
        {
            base.AntesConfirmar();
            if (this.Codigo == 0)
                this.Data = this.horarioInicial.Date;
            this.Profissao = this.Agenda.Docente.Profissao;
            
        }
               

        #endregion

        #region Métodos especificos da classe

        public bool SobreposicaoEspecialidade(int codAluno, int codEspecialidade, DateTime data)
        {
            bool retorno = false;
            string sql = "FROM Compromisso as c WHERE c.Aluno = " + codAluno.ToString() +
                " and Profissao = " + codEspecialidade.ToString() + " and Data = '" + data.ToString("yyyy-MM-dd") + "'";
            IList<Compromisso> lista = Selecionar(sql);
            if (lista.Count > 0)
                retorno = true;
            return retorno;
        }
        public bool SobreposicaoHorarioAluno(int codAluno, DateTime dataInicial, DateTime dataFinal)
        {
            bool retorno = false;
            string sql = "FROM Compromisso as c WHERE c.Aluno = " + codAluno.ToString() +
                " and ( (c.HorarioInicial between '" + dataInicial.ToString("yyyy-MM-dd hh:mm") + "' and '" +dataFinal.ToString("yyyy-MM-dd hh:mm") + "')" +
                " or (c.HorarioFinal between '" + dataInicial.ToString("yyyy-MM-dd hh:mm") + "' and '" + dataFinal.ToString("yyyy-MM-dd hh:mm") + "'))";            
            IList<Compromisso> lista = Selecionar(sql);
            if(lista.Count > 0)
                retorno = true;
            return retorno;
        }

        public bool SobreposicaoHorarioDocente(int codDocente, DateTime dataInicial, DateTime dataFinal)
        {
            bool retorno = false;
            string sql = "FROM Compromisso as c WHERE c.Agenda.Docente = " + codDocente.ToString() +
                " and ( (c.HorarioInicial between '" + dataInicial.ToString("yyyy-MM-dd hh:mm") + "' and '" + dataFinal.ToString("yyyy-MM-dd hh:mm") + "')" +
                " or  (c.HorarioFinal between '" + dataInicial.ToString("yyyy-MM-dd hh:mm") + "' and '" + dataFinal.ToString("yyyy-MM-dd hh:mm") + "'))";
            IList<Compromisso> lista = Selecionar(sql);
            if (lista.Count > 0)
                retorno = true;
            return retorno;
        }

        public IList<Compromisso> SelecionarPorCriterio()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Aluno != null)
            {
                criteria.Add(Expression.Eq("Aluno", this.Aluno));
            }
            if(this.Data != null)
                criteria.Add(Expression.Eq("Data", this.Data));
            criteria.AddOrder(Order.Asc("HorarioInicial"));

            return criteria.List<Compromisso>();
        }
        public IList<Compromisso> SelecionarPorEspecialidade()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Profissao != null)
            {
                criteria.Add(Expression.Eq("Profissao", this.Profissao));
            }
            return criteria.List<Compromisso>();
        }
        public bool TrocarHorario(Compromisso pOrigem, Compromisso pDestino, string pMotivo) 
        {
            try
            {
                
                this.BeginTran();
                
                // delete o registro na tabela para nao dar problemas com os Index na tabela de compromisso
                Sessao.Delete(pOrigem);
                Sessao.Flush();
                Sessao.Delete(pDestino);
                Sessao.Flush();

                Compromisso CompDestino = new Compromisso();
                Compromisso CompOrigem = new Compromisso();

                //copia do destino para a origem
                CompOrigem.Aluno = pOrigem.Aluno;
                CompOrigem.Profissao = pDestino.Profissao;
                CompOrigem.Data = pDestino.Data;
                CompOrigem.Agenda = pDestino.Agenda;
                CompOrigem.HorarioInicial = pDestino.HorarioInicial;
                CompOrigem.HorarioFinal = pDestino.HorarioFinal;
                CompOrigem.Situacao = pDestino.Situacao;
                CompOrigem.Motivo = pMotivo;

                //copia da origem para destino
                CompDestino.Aluno = pDestino.Aluno;
                CompDestino.Profissao = pOrigem.Profissao;
                CompDestino.Data = pOrigem.Data;
                CompDestino.Agenda = pOrigem.Agenda;
                CompDestino.HorarioInicial = pOrigem.HorarioInicial;
                CompDestino.HorarioFinal = pOrigem.HorarioFinal;
                CompDestino.Situacao = pOrigem.Situacao;
                CompDestino.Motivo = pMotivo;
                //insere
                CompDestino.Validar();
                Sessao.Save(CompDestino);
                Sessao.Flush();
                CompOrigem.Validar();
                Sessao.Save(CompOrigem);
                Sessao.Flush();
                
                this.Commit();
                return true;
            }
            catch (HibernateException h)
            {
                this.Rollback();
                Type aux = typeof(MySqlException);
                if (h.InnerException.GetType().Equals(aux))
                {
                    GepexException.EBancoDados gex = new GepexException.EBancoDados();
                    gex.Numero = ((MySqlException)h.InnerException).Number;
                    throw gex;
                }
                else
                    throw h;
            }
            catch (Model.Base.GepexException.ERegraNegocio ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                this.Rollback();
                throw e;
            }
        }
        public bool Excluir(Compromisso horario)
        {
            try
            {
                AntesExcluir();
                BeginTran();
                Sessao.Delete(horario);
                Sessao.Flush();

                //Deleta Agenda caso a mesma não tenha compromissos
                Agenda agenda = new Agenda().Selecionar(horario.Agenda.Codigo);
                if (agenda.Compromissos != null && agenda.Compromissos.Count == 0)
                {
                    Sessao.Delete(agenda);
                    Sessao.Flush();
                }
                               
                Commit();
                return true;
            }
            catch (HibernateException h)
            {
                Rollback();
                Type aux = typeof(MySqlException);
                if (h.InnerException.GetType().Equals(aux))
                {
                    GepexException.EBancoDados gex = new GepexException.EBancoDados();
                    gex.Numero = ((MySqlException)h.InnerException).Number;
                    throw gex;
                }
                else
                    throw h;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }
        #endregion
    }
}
