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
    public class GradeHorario: Comum<GradeHorario>
    {
        #region Atributos da classe
        
        private Disciplina disciplina;
        private Turma turma;
        private Docente docente;
        private int diaSemana;
        private int horario;

        public virtual int Horario
        {
            get { return horario; }
            set { horario = value; }
        }

        public virtual int DiaSemana
        {
            get { return diaSemana; }
            set { diaSemana = value; }
        }

        public virtual Docente Docente
        {
            get { return docente; }
            set { docente = value; }
        }


        public virtual Turma Turma
        {
            get { return turma; }
            set { turma = value; }
        }

        public virtual Disciplina Disciplina
        {
            get { return disciplina; }
            set { disciplina = value; }
        }
 
        public virtual string DiaSemanaFormatado
        {
            get 
            { 
                string aux = string.Empty;
                switch(this.DiaSemana){
                    case(2):{ 
                        aux = "Segunda-feira"; 
                        break;
                    }
                    case(3):{ 
                        aux = "Terça-feira"; 
                        break;
                    }
                    case(4):{ 
                        aux = "Quarta-feira"; 
                        break;
                    }
                    case(5):{ 
                        aux = "Quinta-feira"; 
                        break;
                    }
                    case(6):{ 
                        aux = "Sexta-feira"; 
                        break;
                    }
                    case(7):{ 
                        aux = "Sábado"; 
                        break;
                    }
                }
                return aux; 
            }

        }
        public virtual string HorarioFormatado
        {
            get
            {
                string aux = string.Empty;
                switch (this.Horario)
                {
                    case (1):
                        {
                            aux = "1º Horário";
                            break;
                        }
                    case (2):
                        {
                            aux = "2º Horário";
                            break;
                        }
                    case (3):
                        {
                            aux = "3º Horário";
                            break;
                        }
                    case (4):
                        {
                            aux = "4º Horário";
                            break;
                        }
                    case (5):
                        {
                            aux = "5º Horário";
                            break;
                        }
                    case (6):
                        {
                            aux = "6º Horário";
                            break;
                        }
                }
                return aux;
            }

        }
       #endregion

        #region Construtores da classe

        public GradeHorario() { }

        #endregion

        #region Regras de Negócio
        
        public override void Validar()
        {
        
        }

        #endregion

        #region Métodos especificos da classe
        public virtual IList<GradeHorario> SelecionarPorCriterios(Docente professor, IList<Turma> turma)
        {
            if (professor == null && turma.Count == 0) {
                return null;
            }else
            {
                ICriteria criteria = Sessao.CreateCriteria(this.GetType());
                if (professor != null)
                    criteria.Add(Expression.Eq("Docente", professor));
                if (turma.Count> 0)
                    criteria.Add(Expression.In("Turma", turma.ToArray()));
                criteria.AddOrder(Order.Asc("Turma"));
                criteria.AddOrder(Order.Asc("DiaSemana"));
                criteria.AddOrder(Order.Asc("Horario"));
                return criteria.List<GradeHorario>(); 
            }
        }
        public virtual IList<GradeHorario> SelecionarPorTurma()
        {
                ICriteria criteria = Sessao.CreateCriteria(this.GetType());
                if (this.Turma != null)
                    criteria.Add(Expression.Eq("Turma", this.Turma));
                criteria.AddOrder(Order.Asc("Turma"));
                criteria.AddOrder(Order.Asc("DiaSemana"));
                criteria.AddOrder(Order.Asc("Horario"));
           
                return criteria.List<GradeHorario>();
            }
        }
        #endregion
    
}
