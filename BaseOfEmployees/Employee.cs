using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseOfEmployees
{
    enum Vacancies //Описать перечисление должностей Vacancies {Manager, Boss, Clerk, Salesman, etc.}
    { Manager, Boss, Clerk, Salesman, Chief, Someone }
    struct Employee//Описать структуру «Employee» состоящую из:
    {
        /*string _name;//поля name строкового типа; 
        public string name { get { return _name; } set { _name = value; } }//свойство доступа к _name; */
        //internal string name;//поля name строкового типа;
        public string name;//поля name строкового типа; 
        public Vacancies vacancy;//поля vacancy типа Vacancies;
        public int salary;//поля salary целого типа;.       
        public DateTime hiredate; //использовать стандартный тип даты с учетом его особенностей)
    }
}