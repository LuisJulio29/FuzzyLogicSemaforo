using FuzzyLogicSemaforo;
using System;
using System.Collections.Generic;

namespace ControlDifusoSemaforo
{
    public static class FuzzyAggregation
    {
        // Agrega las salidas recortadas (por ejemplo, dominio 30..90 para tiempo semáforo)
        public static Dictionary<double, double> AggregateOutput(
            List<(FuzzyRule rule, double activation)> activeRules,
            double start, double end, double step)
        {
            // maxAgregado[x] guardará el valor de membresía en el punto x
            var maxAgregado = new Dictionary<double, double>();

            // Inicializamos en 0 la membresía para cada punto del dominio
            for (double x = start; x <= end; x += step)
            {
                maxAgregado[x] = 0.0;
            }

            // Para cada regla activa, recortamos el conjunto de salida
            foreach (var (rule, activation) in activeRules)
            {
                FuzzyLabel salida = rule.Consequent; // Etiqueta de la variable de salida

                // Para cada x en el dominio de la salida
                foreach (var kvp in maxAgregado)
                {
                    double xValue = kvp.Key;
                    double membershipSalida = salida.GetMembership(xValue);

                    // Recorte con el grado de activación
                    double recortado = Math.Min(membershipSalida, activation);

                    // Agregación (operación máximo)
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
