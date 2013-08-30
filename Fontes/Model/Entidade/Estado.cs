using System;
using System.Collections;
using System.Collections.Generic;
using Model.Base;

namespace Model.Entidade
{
    [Serializable]
    public class Estado : Comum<Estado>
	{
	    #region Atributos da classe
        
		private string descricao;
		private string sigla;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public virtual string Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }
        
        #endregion

        #region Construtores da classe

        public Estado() { }

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
