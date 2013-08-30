using System;
using System.Collections;
using System.Collections.Generic;
using Model.Base;

namespace Model.Entidade
{
    [Serializable]
	public class Cidade : Comum<Cidade>
	{
		#region Atributos da classe
        private Estado estado;
        private string descricao;
	
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public virtual Estado Estado
		{
			get { return estado; }
			set {estado= value; }
		}
                
        #endregion

        #region Construtores da classe

        public Cidade() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe

        #endregion

        public override string ToString()
        {
            return this.descricao + " - " + this.estado.Sigla;
        }
	}
}
