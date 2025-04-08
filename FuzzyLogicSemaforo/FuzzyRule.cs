using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicSemaforo
{
    public class FuzzyRule
    {
        public List<FuzzyLabel> Antecedents { get; set; }
        public FuzzyLabel Consequent { get; set; }
        public string Operator { get; set; } = "AND";
        public FuzzyRule(List<FuzzyLabel> antecedents, FuzzyLabel consequent, string op = "AND")
        {
            Antecedents = antecedents;
            Consequent = consequent;
            Operator = op;
        }
        public double GetActivation(Dictionary<string, double> crispInputs)
        {
            double activation;
            if (Operator == "AND")
            {
                // Tomamos el mínimo de todas las membresías
                activation = double.MaxValue;
                foreach (var antecedent in Antecedents)
                {
                    double valor = antecedent.GetMembership(crispInputs[antecedent.VariableName]);
                    if (valor < activation)
                        activation = valor;
                }
            }
            else if (Operator == "OR")
            {
                activation = 0.0;
                foreach (var antecedent in Antecedents)
                {
                    double valor = antecedent.GetMembership(crispInputs[antecedent.VariableName]);
                    if (valor > activation)
                        activation = valor;
                }
            }
            else
            { 
                activation = 0;
            }
            return activation;
        }
    }
}
