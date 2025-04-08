using FuzzyLogicSemaforo.Fuzzificación;
using System;
using System.Collections.Generic;

namespace FuzzyLogicSemaforo.Logic
{
    public static class FuzzyAggregation
    {
        public static Dictionary<double, double> AggregateOutput(
            List<(FuzzyRule rule, double activation)> activeRules,
            double start, double end, double step)
        {
            var maxAgregado = new Dictionary<double, double>();
            for (double x = start; x <= end; x += step)
            {
                maxAgregado[x] = 0.0;
            }
            foreach (var (rule, activation) in activeRules)
            {
                FuzzyLabel salida = rule.Consequent;
                foreach (var kvp in maxAgregado)
                {
                    double xValue = kvp.Key;
                    double membershipSalida = salida.GetMembership(xValue);
                    double recortado = Math.Min(membershipSalida, activation);
                    if (recortado > maxAgregado[xValue])
                    {
                        maxAgregado[xValue] = recortado;
                    }
                }
            }
            return maxAgregado;
        }
    }
}
