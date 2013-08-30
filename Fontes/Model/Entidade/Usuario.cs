using System;
using System.Collections;
using System.Collections.Generic;
using Model.Base;
using NHibernate;
using NHibernate.Criterion;

namespace Model.Entidade
{
    [Serializable]
    public class Usuario : Comum<Usuario>
	{
        #region Atributos da classe

        private Pessoa pessoa;
        private Perfil perfil;
        private string login;
        private string situacao;
        private string senha;
        private DateTime dataAlteracao;
        private string motivo;
      
        public virtual string Senha
        {
            get { return senha; }
            set { senha = value; }
        }
        
        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }
        public virtual Perfil Perfil
        {
            get { return perfil; }
            set { perfil = value; }
        }
        public virtual string Login
        {
            get { return login; }
            set { login = value; }
        }
        public virtual string Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }
       
        public virtual DateTime DataAlteracao
        {
            get { return dataAlteracao; }
            set { dataAlteracao = value; }
        }
        public virtual string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }
        public virtual string SituacaoFormatada
        {
            get
            {
                string aux = string.Empty;
                switch (this.situacao)
                {
                    case ("I"):
                        aux = "Inativo";
                        break;
                    case ("A"):
                        aux = "Ativo";
                        break;
                    case ("B"):
                        aux = "Bloqueado";
                        break;
                }
                return aux;
            }
        }
        
        #endregion

        #region Construtores da classe

        public Usuario() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
           
        
        }
        public override void AntesConfirmar()
        {
            base.AntesConfirmar();
            this.DataAlteracao = DateTime.Now;
        }

        #endregion

        #region Métodos especificos da classe
        
        public virtual Usuario SelecionarPorLogin(string login)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Login", login).IgnoreCase());
            IList<Usuario> lista = criteria.List<Usuario>();
            if (lista.Count != 0)
                return lista[0];
            else
                return null;
        }
        public virtual Usuario Selecionar(string login, string senha)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Login", login).IgnoreCase());
            string senhaCrip = CryptographyHelper.ToBase64(senha);
            criteria.Add(Expression.Eq("Senha", senhaCrip ));
            IList<Usuario> lista = criteria.List<Usuario>();
            if (lista.Count != 0)
                return lista[0];
            else
                return null;
        }
        public virtual IList<Usuario> SelecionarPorCriterio(string nome, string login, Perfil perfil)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Pessoa", "pes");
            if (nome != "")
                criteria.Add(Expression.Like("pes.Nome", nome, MatchMode.Anywhere).IgnoreCase());
            if (login != "")
                criteria.Add(Expression.Eq("Login", login).IgnoreCase());
            if (perfil != null)
                criteria.Add(Expression.Eq("Perfil", perfil));
            return criteria.List<Usuario>();
            
        }
        #endregion
		
	}
}
