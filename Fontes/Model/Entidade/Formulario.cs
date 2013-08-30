using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;

namespace Model.Entidade
{
    [Serializable]
    public class Formulario: Comum<Formulario>
    {
        #region Atributos da classe
        
        private string descricao;
        private bool situacao;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public virtual bool Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }
               

        #endregion

        #region Construtores da classe

        public Formulario() { }

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
