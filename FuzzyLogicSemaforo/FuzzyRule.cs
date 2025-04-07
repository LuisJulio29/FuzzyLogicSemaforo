using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicSemaforo
{
    public class FuzzyRule
    {
        // Lista de etiquetas de entrada que deben cumplirse
        // Por ejemplo: [ (Flujo, Alto), (Velocidad, Regular), (Hora, Tarde) ]
        public List<FuzzyLabel> Antecedents { get; set; }

        // Etiqueta de la variable de salida
        public FuzzyLabel Consequent { get; set; }

        // Operador: AND (mínimo), OR (máximo) — aquí asumimos AND
        public string Operator { get; set; } = "AND";

        public FuzzyRule(List<FuzzyLabel> antecedents, FuzzyLabel consequent, string op = "AND")
        {
            Antecedents = antecedents;
            Consequent = consequent;
            Operator = op;
        }

        // Calcula la activación de la regla para valores crisp
        public double GetActivation(Dictionary<string, double> crispInputs)
        {
            // crispInputs["Flujo"] = valorFlujo, crispInputs["Velocidad"] = valorVelocidad, etc.
            // Antecedents[0].GetMembership(crispInputs[variableName]) -> grado de pertenencia
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
                // Tomamos el máximo de todas las membresías
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
                // Por defecto, retornamos 0 si no reconocemos el operador
                activation = 0;
            }

            return activation;
        }
    }
}
