using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Web.Util
{
    public interface Base
    {   
        void Selecionar();
        bool Salvar();
        bool Alterar();
        bool ValidarCamposObrigatorios();
        void Limpar();
        void Excluir();
    }
}
