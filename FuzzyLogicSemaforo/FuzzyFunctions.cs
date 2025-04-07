using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicSemaforo
{
    public class FuzzyFunctions
    {
        // Función trapezoidal
        public static double Trapezoidal(double value, double a, double b, double c, double d)
        {
            if (value <= a || value >= d)
                return 0;
            else if (value >= b && value <= c)
                return 1;
            else if (value > a && value < b)
                return (value - a) / (b - a);
            else // x > c && x < d
                return (d - value) / (d - c);
        }

        // Función triangular
        public static double Triangular(double value, double a, double b, double c)
        {
            if (value <= a || value >= c)
                return 0;
            else if (value == b)
                return 1;
            else if (value < b)
                return (value - a) / (b - a);
            else // x > b
                return (c - value) / (c - b);
        }

        // Función gaussiana
        public static double Gaussian(double value, double mean, double sigma)
        {
            return Math.Exp(-Math.Pow(value - mean, 2) / (2 * Math.Pow(sigma, 2)));
        }
    }
}
