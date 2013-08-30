using System;
using System.Collections;
using System.Collections.Generic;
using Model.Base;
using NHibernate;
using NHibernate.Criterion;

namespace Model.Entidade
{
    [Serializable]
    public class PessoaDocumento : Comum<PessoaDocumento>
	{
		
		#region Atributos da classe

        private Pessoa pessoa;
        private TipoDocumento tipoDocumento;
        private string orgaoEmissor;
        private string numero;
        private string uf;
        private DateTime? dataEmissao;
        private string infAdicional;

        public virtual string InfAdicional
        {
            get { return infAdicional; }
            set { infAdicional = value; }
        }

        public virtual DateTime? DataEmissao
        {
            get { return dataEmissao; }
            set { dataEmissao = value; }
        }

        public virtual string UF
        {
            get { return uf; }
            set { uf = value; }
        }
        
        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }
        public virtual TipoDocumento TipoDocumento
        {
            get { return tipoDocumento; }
            set { tipoDocumento = value; }
        }
        public virtual string OrgaoEmissor
        {
            get { return orgaoEmissor; }
            set { orgaoEmissor = value; }
        }
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
              
        #endregion

        #region Construtores da classe

        public PessoaDocumento() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public virtual PessoaDocumento SelecionarPorDocumento(int pessoa, int documento)
        {
            PessoaDocumento doc = new PessoaDocumento();
            ICriteria criteria = Sessao.CreateCriteria(this.GetType()).
                                        CreateAlias("Pessoa", "pes").
                                        CreateAlias("TipoDocumento", "doc");    
            criteria.Add(Expression.Eq("pes.Codigo", pessoa));
            criteria.Add(Expression.Eq("doc.Codigo", documento));
            IList<PessoaDocumento> lista = criteria.List<PessoaDocumento>();
            if (lista.Count > 0)
                return doc = lista[0];
            else
                return null;
        }
        #endregion
	}
}
