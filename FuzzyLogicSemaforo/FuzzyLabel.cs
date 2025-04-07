using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicSemaforo
{
    public class FuzzyLabel
    {
        public string VariableName { get; set; }   // p.ej., "Flujo", "Velocidad", "Hora"
        public string LabelName { get; set; }      // p.ej., "Bajo", "Medio", "Alto", etc.

        // Función que calcula la membresía dado un valor x
        public Func<double, double> MembershipFunction { get; set; }

        public FuzzyLabel(string variable, string label, Func<double, double> membership)
        {
            VariableName = variable;
            LabelName = label;
            MembershipFunction = membership;
        }

        public double GetMembership(double x)
        {
            return MembershipFunction(x);
        }
    }
}
