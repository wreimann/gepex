using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Base
{
    [Serializable]
    public static class GepexException
    {
        public class EBancoDados : Exception {
            private int numero;

            public int Numero
            {
                get { return numero; }
                set { numero = value; }
            }

        }

        public class ERegraNegocio : Exception {
           
            public ERegraNegocio(string mensagem): base(mensagem) {
                //Construtor que chama o construtor da classe genérica
            }
           
        }
    }
}
