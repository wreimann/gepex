using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;

namespace Model.Entidade
{
    
    [Serializable]
    public class CorRaca : Comum<CorRaca>
    {
        #region Atributos da classe

        private string descricao;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }


        #endregion

        #region Construtores da classe

        public CorRaca() { }

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
