using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Base;

namespace Model.Entidade
{
    [Serializable]
    public class Parametro: Comum<Parametro>
    {
       #region Atributos da classe

        private Endereco endereco;
        private string instituicao;
        private int predical;
        private string complemento;
        private decimal telefone;   
        private int? maximoDiasAtendimento;
        private string cnae;
        private string cnpj;
        private string email;
        private string termoMatricula;

        public virtual string TermoMatricula
        {
            get { return termoMatricula; }
            set { termoMatricula = value; }
        }

        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }


        public virtual int Predical
        {
            get { return predical; }
            set { predical = value; }
        }
        public virtual string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }
        public virtual decimal Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }
        public virtual Endereco Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }
        public virtual string Instituicao
        {
            get { return instituicao; }
            set { instituicao = value; }
        }
        public virtual int? MaximoDiasAtendimento
        {
            get { return maximoDiasAtendimento; }
            set { maximoDiasAtendimento = value; }
        }
        public virtual string Cnae
        {
            get { return cnae; }
            set { cnae = value; }
        }
        public virtual string Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

       #endregion

        #region Construtores da classe

        public Parametro() { }

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
