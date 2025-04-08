using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicSemaforo
{
    public class FuzzyFunctions
    {
        // Funcion Hombro
        public static double Hombro(double value, double a, double b)
        {
            if (value <= a)
                return 1.0;
            else if (value <= b && value >= a)
                return (value - b) / (b - a);
            else
                return 0.0;
        }
        // Función saturación
        public static double Saturacion(double value, double a, double b)
        {
            if (value <= a)
                return 0.0;
            else if (value <= b && value >= a)
                return (value - a) / (b - a);
            else
                return 1.0;
        }
        // Función trapezoidal
        public static double Trapezoidal(double value, double a, double b, double y, double s)
        {
            if (value <= a || value >= s)
                return 0;
            else if (value >= a && value <= b)
                return (value - a) / (b - a);
            else if (value >= b && value <= y)
                return 1.0;
            else if(value >= y && value <= s)
                return (s - value) / (s - y);
            else
                return 0;
        }

        // Función triangular
        public static double Triangular(double value, double a, double b, double y)
        {
            if (value <= a || value >= y)
                return 0;
            else if (value >= a && value <= b)
                return (value - a) / (b - a);
            else if (value >= b && value <= y)
                return (y - value) / (y - b);
            else
                return 0;
        }
        // Función gaussiana
        public static double Gaussian(double value, double mean, double sigma)
        {
            return Math.Exp(-Math.Pow(value - mean, 2) / (2 * Math.Pow(sigma, 2)));
        }
    }
}
