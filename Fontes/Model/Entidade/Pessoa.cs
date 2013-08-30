using System;
using System.Collections;
using System.Collections.Generic;
using Model.Base;
using NHibernate;
using System.Text;
using NHibernate.Criterion;

namespace Model.Entidade
{
    [Serializable]
    public class Pessoa: Comum<Pessoa>
	{
	    #region Atributos da classe
  
        private Endereco endereco;
        private string nome;
        private string tipo;
        private string nacionalidade;
        private string naturalidade;
        private string sexo;
        private DateTime dataNascimento;
        private string estadoCivil;
        private int predical;
        private string complemento;
        private decimal? telefone;
        private string foto;
        private string email;
        private IList<PessoaDocumento> pessoaDocumento;
        private decimal? celular;
        private int? identificacaoCopel;
        private CorRaca corRaca;

        public virtual CorRaca CorRaca
        {
            get { return corRaca; }
            set { corRaca = value; }
        }

        public virtual int? IdentificacaoCopel
        {
            get { return identificacaoCopel; }
            set { identificacaoCopel = value; }
        }


        public decimal? Celular
        {
            get { return celular; }
            set { celular = value; }
        }


        public virtual Endereco Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public virtual string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public virtual string Nacionalidade
        {
            get { return nacionalidade; }
            set { nacionalidade = value; }
        }
        public virtual string Naturalidade
        {
            get { return naturalidade; }
            set { naturalidade = value; }
        }
        public virtual string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        public virtual DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }
        public virtual string EstadoCivil
        {
            get { return estadoCivil; }
            set { estadoCivil = value; }
        }
        public virtual int Predical
        {
            get { return predical; }
            set { predical = value; }
        }
        public virtual string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }
        public virtual decimal? Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }
        public virtual string Foto
        {
            get { return foto; }
            set { foto = value; }
        }
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }
        public virtual IList<PessoaDocumento> PessoaDocumento
        {
            get { return pessoaDocumento; }
            set { pessoaDocumento = value; }
        }
        
        #endregion

        #region Construtores da classe

        public Pessoa() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
            

        }

        #endregion

        #region Métodos especificos da classe


        public virtual string[] SelecionarAutoComplete(string prefixText, int count)
        {
            ICriteria criteria = Sessao.CreateCriteria(typeof(Pessoa));
            criteria.Add(Expression.Like("Nome", prefixText, MatchMode.Anywhere).IgnoreCase());
            criteria.SetMaxResults(count);
            IList<Pessoa> lista = criteria.List<Pessoa>();
            ArrayList resultado = new ArrayList();
            foreach (Pessoa pessoa in lista) {
                resultado.Add(pessoa.Nome);
            }
            return (string[])resultado.ToArray(typeof(string));
        }
        public virtual Pessoa SelecionarPorNome(string nome)
        {
            ICriteria criteria = Sessao.CreateCriteria(typeof(Pessoa));
            criteria.Add(Expression.Eq("Nome", nome).IgnoreCase());
            IList<Pessoa> lista = criteria.List<Pessoa>();
            if (lista.Count != 0)
                return lista[0];
            else
                return null;
        }
        public virtual Pessoa SelecionarPorNome(string nome, string tipo)
        {
            ICriteria criteria = Sessao.CreateCriteria(typeof(Pessoa));
            criteria.Add(Expression.Eq("Nome", nome).IgnoreCase());
            criteria.Add(Expression.Eq("Tipo", tipo).IgnoreCase());
            IList<Pessoa> lista = criteria.List<Pessoa>();
            if (lista.Count != 0)
                return lista[0];
            else
                return null;
        }
        #endregion

	}
}
