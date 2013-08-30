using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;
using NHibernate.Criterion;
using NHibernate;

namespace Model.Entidade
{
    [Serializable]
    public class Portal: Comum<Portal>
    {
        #region Atributos da classe
        
        private string titulo;
        private string descricao;
        private DateTime data;
        private string tipo;
        private string link;      
        private IList<PortalImagem> listaImagem;

        public virtual string Link
        {
            get { return link; }
            set { link = value; }
        }
        public virtual string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        public virtual string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public virtual IList<PortalImagem> ListaImagem
        {
            get { return listaImagem; }
            set { listaImagem = value; }
        }
        public virtual string TipoFormatado
        {
            get
            {
                string aux = string.Empty;
                switch (this.Tipo)
                {
                    case ("1"):
                        aux = "Eventos";
                        break;
                    case ("2"):
                        aux = "Notícias";
                        break;
                    case ("3"):
                        aux = "Colaboradores";
                        break;
                   
                }
                return aux;
            }
        }
        #endregion

        #region Construtores da classe

        public Portal() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe      
        
        public IList<Portal> SelecionarPorCriterio()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Titulo != null)
                criteria.Add(Expression.Like("Titulo", this.Titulo, MatchMode.Anywhere).IgnoreCase());
            if (this.Data != DateTime.MinValue)
            {
                DateTime dataInicial = this.Data.Date.AddDays(-1);
                DateTime dataFinal = this.Data.Date.AddDays(1);
                criteria.Add(Expression.Gt("Data", dataInicial));
                criteria.Add(Expression.Lt("Data", dataFinal));
            } 
            criteria.Add(Expression.Eq("Tipo", this.Tipo).IgnoreCase());    
            return criteria.List<Portal>();
        }
        public IList<Portal> SelecionarporTitulo()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Titulo != null)
            {
                criteria.Add(Expression.Like("Titulo", "%" + this.Titulo + "%"));
                criteria.Add(Expression.Eq("Tipo", this.Tipo));
            }
            return criteria.List<Portal>();
        }
        public IList<Portal> SelecionarporData()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Titulo != null)
            {
                criteria.Add(Expression.Like("Data", "%" + this.Data + "%"));
                criteria.Add(Expression.Eq("Tipo", this.Tipo));
            }
            return criteria.List<Portal>();
        }
        public IList<Portal> SelecionarporTituloData()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Titulo != null)
            {
                criteria.Add(Expression.Like("Titulo", "%" + this.Titulo + "%"));
                criteria.Add(Expression.Eq("Data", this.Data));
                criteria.Add(Expression.Eq("Tipo", this.Tipo));
            }
            return criteria.List<Portal>();
        }
        public IList<Portal> SelecionarporTipo(int limite)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if(limite > 0)
                criteria.SetMaxResults(limite);
            if (this.Tipo != null)
            {
                criteria.Add(Expression.Eq("Tipo", this.Tipo));
            }
            criteria.AddOrder(Order.Desc("Data"));
            return criteria.List<Portal>();
        }
        public IList<Portal> SelecionarporCodigo()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Codigo > 0)
            {
                criteria.Add(Expression.Eq("Codigo", this.Codigo));
            }
            return criteria.List<Portal>();
        }
        
        #endregion
    }
}
