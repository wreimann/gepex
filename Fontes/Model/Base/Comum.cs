/*
      Autor: Wellingthon Reimann         Data: 19/07/2010
   Objetivo: Implementação da classe de persistência de objetos.
Atualização:
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using MySql.Data.MySqlClient;
using NHibernate;
using System.Collections;

namespace Model.Base
{
    [Serializable]
    public abstract class Comum<T>: BaseEntidade where T: BaseEntidade
    {
        #region Controle de sessão e transação
        private ITransaction transacao;
        private ISession sessao;

        public virtual ISession Sessao
        {
            get { return this.sessao = SessionHelper.CurrentSession; }
        }

        public virtual void BeginTran()
        {
            this.transacao = Sessao.BeginTransaction();
        }

        public virtual void Commit()
        {
            this.transacao.Commit();
        }

        public virtual void Rollback()
        {
            if(this.transacao != null && this.transacao.IsActive)
                this.transacao.Rollback();
        }
        #endregion

        #region Métodos de persistência
        protected virtual bool Inserir(object objeto)
		{
			try
			{
                Sessao.Save(objeto);          
                return true;
			}
            catch (HibernateException h)
            {
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
                throw e;              
			}
		}

        protected virtual bool Atualizar(object objeto)
        {
            try
            {
                Sessao.Update(objeto);
                return true;
            }
            catch (HibernateException h)
            {
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
                throw e;
            }
        }

        public virtual bool Excluir(object objeto)
        {
            try
            {
                AntesExcluir();
                BeginTran();
                Sessao.Delete(objeto);
                Sessao.Flush();
                Commit();
                Sessao.Clear(); 
                return true;
            }
            catch (HibernateException h)
            {
                if (this.transacao.WasRolledBack)
                    this.Rollback();
                Type aux = typeof(MySqlException);
                if (h.InnerException != null && h.InnerException.GetType().Equals(aux))
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
                if (this.transacao.WasRolledBack)
                    this.Rollback();
                throw e;
            }
            finally
            {
            }
        }

        public virtual bool Excluir(int codigo)
        {
           return Excluir(Selecionar(codigo));
        }

        public virtual bool Confirmar()
        {
            try
            {
                AntesConfirmar();
                Validar();
                this.BeginTran();
                if (Codigo > 0)
                    Atualizar(this);
                else
                    Inserir(this);
                 this.Commit();
                 AposConfirmar();
                 Sessao.Clear(); 
                 return true;
                 
            }
            catch (HibernateException h)
            {
                if (this.transacao.WasRolledBack)
                    this.Rollback();
                Type aux = typeof(MySqlException);
                if (h.InnerException != null && h.InnerException.GetType().Equals(aux))
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
 
        public virtual void AntesConfirmar()
        {
     
        }
        public virtual void AposConfirmar()
        {

        }
        public virtual void AntesExcluir()
        {

        }
        /*   Autor: Wellingthon Reimann         Data: 01/08/2010
          Objetivo: Validar os fields da classe. */
        public virtual void Validar()
        {
            throw new Exception("Método não implementado.");
        }

        #endregion

        #region Métodos de seleção
        
        /*Autor: Wellingthon 
         Próposito: Lista todos os registros na tabela. */
        public virtual IList<T> Selecionar()
        {
            ICriteria criteria = Sessao.CreateCriteria(typeof(T));
            return criteria.List<T>();
        }
        
        /*Autor: Wellingthon 
         Próposito: Consulta HQL (HIbernate Query Language) */
        protected virtual IList<T> Selecionar(string sql)
        {
            return Sessao.CreateQuery(sql).List<T>();
        }
        /*Autor: Wellingthon 
         Próposito: Reterno um objeto de acordo com o PK da tabela. */
        public virtual T Selecionar(int codigo)
        {
            return Sessao.Get<T>(codigo);
        }
  
        public virtual DataSet ToDataSet(IList<T> lista)
        {   
            return DataSetUtil<T>.ConvertToDataSet(lista);
        }

        #endregion
    }
}
