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
    public class Permissao: Comum<Permissao>
    {
        #region Atributos da classe

        private Formulario formulario;
        private Perfil perfil;
        private bool acesso;
        private bool inclui;
        private bool exclui;
        private bool altera;
        
        public Formulario Formulario
        {
            get { return formulario; }
            set { formulario = value; }
        }
        public virtual Perfil Perfil
        {
            get { return perfil; }
            set { perfil = value; }
        }
        public virtual bool Acesso
        {
            get { return acesso; }
            set { acesso = value; }
        }
        public virtual bool Inclui
        {
            get { return inclui; }
            set { inclui = value; }
        }
        public virtual bool Exclui
        {
            get { return exclui; }
            set { exclui = value; }
        }
        public virtual bool Altera
        {
            get { return altera; }
            set { altera = value; }
        }
       
        #endregion

        #region Construtores da classe

        public Permissao() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public virtual IList<Permissao> SelecionarPorPerfil(Perfil perfil)
        {
            ICriteria criteria = Sessao.CreateCriteria(this.GetType());
            criteria.Add(Expression.Eq("Perfil", perfil));
            return criteria.List<Permissao>();
        }
        #endregion

    }
}
