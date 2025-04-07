﻿using System.Collections.Generic;

namespace ControlDifusoSemaforo
{
    public static class FuzzyDefuzzification
    {
        public static double Centroid(Dictionary<double, double> aggregated)
        {
            double numerator = 0.0;
            double denominator = 0.0;

            // aggregated[x] = valor de membresía en x
            foreach (var kvp in aggregated)
            {
                double x = kvp.Key;
                double mu = kvp.Value;

                numerator += x * mu;
                denominator += mu;
            }

            if (denominator == 0)
                return 0;

            return numerator / denominator;
        }
    }
}
