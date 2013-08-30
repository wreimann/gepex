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
    public class Aluno: Comum<Aluno>
    {
       #region Atributos da classe
        
        private Pessoa pessoa;
        private decimal matricula;
        private bool sites;
        private bool medicar;
        private bool bolsaFamilia;      
        private string pai;
        private string mae;
        private decimal emergencia;
        private string contato;
        private string convenioMedico;
        private string carteirinhaConvenio;
        private decimal? telefoneConvenio;
        private decimal? altura;
        private decimal? peso;
        private string tipoSanguineo;
        private string fatorRH;
        private string alergias;
        private string observacao;
        private string medicamentos;
        private string situacao;
        private string outrosTransportes;
        private string outrosBeneficios;


        public virtual bool BolsaFamilia
        {
            get { return bolsaFamilia; }
            set { bolsaFamilia = value; }
        }
        public virtual string OutrosTransportes
        {
            get { return outrosTransportes; }
            set { outrosTransportes = value; }
        }

        public virtual string OutrosBeneficios
        {
            get { return outrosBeneficios; }
            set { outrosBeneficios = value; }
        }

        public virtual decimal? TelefoneConvenio
        {
            get { return telefoneConvenio; }
            set { telefoneConvenio = value; }
        }
        public virtual string CarteirinhaConvenio
        {
            get { return carteirinhaConvenio; }
            set { carteirinhaConvenio = value; }
        }
        public virtual string Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        public virtual string Medicamentos
        {
            get { return medicamentos; }
            set { medicamentos = value; }
        }
        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }
        public virtual string Alergias
        {
            get { return alergias; }
            set { alergias = value; }
        }

        public virtual string FatorRH
        {
            get { return fatorRH; }
            set { fatorRH = value; }
        }

        public virtual string TipoSanguineo
        {
            get { return tipoSanguineo; }
            set { tipoSanguineo = value; }
        }


        public virtual decimal? Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        public virtual decimal? Altura
        {
            get { return altura; }
            set { altura = value; }
        }


        public virtual string ConvenioMedico
        {
            get { return convenioMedico; }
            set { convenioMedico = value; }
        }
         

        public virtual string Contato
        {
            get { return contato; }
            set { contato = value; }
        }


        public virtual decimal Emergencia
        {
            get { return emergencia; }
            set { emergencia = value; }
        }

        public virtual string Mae
        {
            get { return mae; }
            set { mae = value; }
        }


        public virtual string Pai
        {
            get { return pai; }
            set { pai = value; }
        }


        public virtual bool Medicar
        {
            get { return medicar; }
            set { medicar = value; }
        }

        
        public virtual bool Sites
        {
            get { return sites; }
            set { sites = value; }
        }

        public virtual decimal Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }
        
        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
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
                        aux = "Matrícula em Andamento";
                        break;
                    case ("M"):
                        aux = "Matriculado";
                        break;
                    case ("L"):
                        aux = "Lista de Espera";
                        break;
                }
                return aux;
            }
        }
       #endregion

        #region Construtores da classe

        public Aluno() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
            string msgErro = string.Empty;
            if (this.Pessoa.DataNascimento.Date > DateTime.Now.Date)
                msgErro = msgErro + "<b>Data de Nascimento</b>: deve menor que a data atual.";
            if (msgErro != "")
                throw new GepexException.ERegraNegocio("<b>Erro ao gravar o aluno.</b><br /> " + msgErro);
        }

        #endregion

        #region Métodos especificos da classe
        public virtual string[] SelecionarAutoComplete(string prefixText, int count)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Pessoa", "pes");
            criteria.Add(Expression.Like("pes.Nome", prefixText, MatchMode.Anywhere).IgnoreCase());
            criteria.SetMaxResults(count);
            criteria.AddOrder(Order.Asc("pes.Nome"));
            IList<Aluno> lista = criteria.List<Aluno>();
            ArrayList resultado = new ArrayList();
            foreach (Aluno aluno in lista)
            {
                resultado.Add(aluno.Pessoa.Nome);
            }
            
            return (string[])resultado.ToArray(typeof(string));
        }

        public virtual Aluno SelecionarPorPessoa(Pessoa pessoa)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Pessoa", pessoa));
            IList<Aluno> lista = criteria.List<Aluno>();
            if (lista.Count > 0)
                return lista[0];
            else
                return null;
        }
        public virtual IList<Aluno> SelecionarPorNomeMatricula(string nome, string matricula)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Pessoa", "pes");
            if(nome != string.Empty)
                criteria.Add(Expression.Like("pes.Nome", nome, MatchMode.Anywhere).IgnoreCase());
            if (matricula != string.Empty)
                criteria.Add(Expression.Eq("Matricula", Convert.ToDecimal(matricula)));
            return criteria.List<Aluno>();

        }
        
        public virtual bool FinalizarAnoLetivo()
        {
            bool retorno = false;
            try
            {
                ICriteria criteria = Sessao.CreateCriteria(typeof(Aluno));
                criteria.Add(Expression.Not(Expression.Eq("Situacao", "I")));
                IList<Aluno> lista = criteria.List<Aluno>();
                foreach (Aluno t in lista)
                {
                    t.situacao = "I";
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
        public virtual IList<Aluno> SelecionarMatriculados()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Pessoa", "pes");
            criteria.Add(Expression.Eq("Situacao","M" ));
            criteria.AddOrder(Order.Asc("pes.Nome"));
            return criteria.List<Aluno>();

        }

       #endregion

        
    }
}
