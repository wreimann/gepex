using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Base
{
    [Serializable]
    public abstract class BaseEntidade
    {
        #region Fields em comun em todas as classes
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        #endregion
    }
}
