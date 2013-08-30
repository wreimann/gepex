using System;
using System.Collections;
using System.Collections.Generic;
using Model.Base;

namespace Model.Entidade
{
    [Serializable]
    public class Perfil : Comum<Perfil>
	{
      
        #region Atributos da classe
        
        private string descricao;
        private IList<Permissao> permissao;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public virtual IList<Permissao> Permissao
        {
            get { return permissao; }
            set { permissao = value; }
        }
        #endregion

        #region Construtores da classe

        public Perfil() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe

        #endregion
	}
}
