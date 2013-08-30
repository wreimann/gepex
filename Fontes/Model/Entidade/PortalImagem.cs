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
    public class PortalImagem: Comum<PortalImagem>
    {
        #region Atributos da classe
        
        private string imagem;
        private Portal portal;
        private string diretorio;

        public virtual string Diretorio
        {
            get { return diretorio; }
            set { diretorio = value; }
        }

        public virtual string Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }
        public virtual Portal Portal
        {
            get { return portal; }
            set { portal = value; }
        }
        #endregion

        #region Construtores da classe

        public PortalImagem() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public IList<PortalImagem> SelecionarPorPortal()
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            if (this.Portal != null)
            {
                criteria.Add(Expression.Like("Portal", this.Portal));
                criteria.Add(Expression.Eq("Portal", this.Portal));
            }
            return criteria.List<PortalImagem>();
        }
        #endregion
    }
}
