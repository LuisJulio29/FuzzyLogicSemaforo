using FuzzyLogicSemaforo.Fuzzificación;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicSemaforo.Logic
{
    public static class FuzzyInference
    {
        // Este método devuelve las "reglas activas" junto con el grado de activación
        public static List<(FuzzyRule rule, double activation)> GetActiveRules(
            List<FuzzyRule> allRules,
            Dictionary<string, double> crispInputs)
        {
            var activeRules = new List<(FuzzyRule, double)>();

            foreach (var rule in allRules)
            {
                double activation = rule.GetActivation(crispInputs);
                if (activation > 0)
                {
                    activeRules.Add((rule, activation));
                }
            }

            return activeRules;
        }
    }
}
