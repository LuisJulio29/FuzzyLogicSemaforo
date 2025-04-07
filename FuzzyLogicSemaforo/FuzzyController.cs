using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicSemaforo
{
    public class FuzzyController
    {
        // -------------------------------
        // Funciones de membresía para Flujo de vehículos (entrada)
        // Usamos función trapezoidal con:
        // - Bajo: intervalo [0, 200] (aquí se asume a=0, b=0, c=200, d=200)
        // - Medio: intervalo [350, 550]
        // - Alto: intervalo [700, 900]
        // (Puedes ajustar los puntos para generar solapamientos si lo requieres)
        public static double FlujoBajo(double flujo)
        {
            return FuzzyFunctions.Trapezoidal(flujo, 0, 0, 200, 200);
        }

        public static double FlujoMedio(double flujo)
        {
            return FuzzyFunctions.Trapezoidal(flujo, 350, 350, 550, 550);
        }

        public static double FlujoAlto(double flujo)
        {
            return FuzzyFunctions.Trapezoidal(flujo, 700, 700, 900, 900);
        }

        // -------------------------------
        // Funciones de membresía para Velocidad Vial (entrada)
        // Usamos función triangular:
        // - Trancon: valor representativo <5 (triangle con pico en 0, intervalo [0, 5])
        // - Lenta: pico en 10 (intervalo [0,10,20])
        // - Regular: pico en 30 (intervalo [10,30,40])
        // - Rápida: pico en 50 (intervalo [40,50,60])
        public static double VelocidadTrancon(double velocidad)
        {
            return FuzzyFunctions.Triangular(velocidad, 0, 0, 5);
        }

        public static double VelocidadLenta(double velocidad)
        {
            return FuzzyFunctions.Triangular(velocidad, 0, 10, 20);
        }

        public static double VelocidadRegular(double velocidad)
        {
            return FuzzyFunctions.Triangular(velocidad, 10, 30, 40);
        }

        public static double VelocidadRapida(double velocidad)
        {
            return FuzzyFunctions.Triangular(velocidad, 40, 50, 60);
        }

        // -------------------------------
        // Funciones de membresía para Hora (entrada)
        // Usamos función gaussiana:
        // - Mañana: horas < 10:00 AM (centro en 9, sigma 1)
        // - Tarde: de 11:00 AM a 5:00 PM (centro en 14, sigma 1.5)
        // - Noche: >6:00 PM (centro en 19, sigma 1)
        public static double HoraManana(double hora)
        {
            return FuzzyFunctions.Gaussian(hora, 9, 1);
        }

        public static double HoraTarde(double hora)
        {
            return FuzzyFunctions.Gaussian(hora, 14, 1.5);
        }

        public static double HoraNoche(double hora)
        {
            return FuzzyFunctions.Gaussian(hora, 19, 1);
        }

       
        private static List<(int flujo, int velocidad, int hora, double salida)> reglas = new List<(int, int, int, double)>
        {
            // Ejemplo de matriz de reglas (total de 17 reglas de acuerdo al documento)
            // Estas reglas son solo un ejemplo; puedes ajustarlas según la matriz real.
            (0, 0, 0, 37.5), // IF Flujo Bajo AND Velocidad Trancon AND Hora Mañana THEN Tiempo Bajo
            (0, 1, 0, 37.5), // IF Flujo Bajo AND Velocidad Lenta AND Hora Mañana THEN Tiempo Bajo
            (0, 0, 1, 37.5), // IF Flujo Bajo AND Velocidad Trancon AND Hora Tarde THEN Tiempo Bajo
            (0, 1, 1, 37.5), // IF Flujo Bajo AND Velocidad Lenta AND Hora Tarde THEN Tiempo Bajo
            (1, 1, 1, 57.5), // IF Flujo Medio AND Velocidad Lenta AND Hora Tarde THEN Tiempo Medio
            (1, 2, 1, 57.5), // IF Flujo Medio AND Velocidad Regular AND Hora Tarde THEN Tiempo Medio
            (1, 2, 0, 57.5), // IF Flujo Medio AND Velocidad Regular AND Hora Mañana THEN Tiempo Medio
            (1, 3, 1, 57.5), // IF Flujo Medio AND Velocidad Rápida AND Hora Tarde THEN Tiempo Medio
            (2, 2, 2, 80),   // IF Flujo Alto AND Velocidad Regular AND Hora Noche THEN Tiempo Alto
            (2, 3, 2, 80),   // IF Flujo Alto AND Velocidad Rápida AND Hora Noche THEN Tiempo Alto
            (2, 2, 1, 80),   // IF Flujo Alto AND Velocidad Regular AND Hora Tarde THEN Tiempo Alto
            (2, 3, 1, 80),   // IF Flujo Alto AND Velocidad Rápida AND Hora Tarde THEN Tiempo Alto
            (1, 0, 1, 57.5), // IF Flujo Medio AND Velocidad Trancon AND Hora Tarde THEN Tiempo Medio
            (0, 2, 0, 37.5), // IF Flujo Bajo AND Velocidad Regular AND Hora Mañana THEN Tiempo Bajo
            (2, 0, 2, 80),   // IF Flujo Alto AND Velocidad Trancon AND Hora Noche THEN Tiempo Alto
            (1, 2, 2, 57.5), // IF Flujo Medio AND Velocidad Regular AND Hora Noche THEN Tiempo Medio
            (0, 3, 0, 37.5)  // IF Flujo Bajo AND Velocidad Rápida AND Hora Mañana THEN Tiempo Bajo
        };

        // Método principal que realiza la inferencia y defuzzificación
        // Recibe los valores crisp de las entradas y retorna el tiempo en verde calculado
        public static double CalcularTiempoVerde(double flujo, double velocidad, double hora)
        {
            // Evaluar los grados de pertenencia de las entradas
            double[] muFlujo = new double[3]
            {
                FlujoBajo(flujo),
                FlujoMedio(flujo),
                FlujoAlto(flujo)
            };

            double[] muVelocidad = new double[4]
            {
                VelocidadTrancon(velocidad),
                VelocidadLenta(velocidad),
                VelocidadRegular(velocidad),
                VelocidadRapida(velocidad)
            };

            double[] muHora = new double[3]
            {
                HoraManana(hora),
                HoraTarde(hora),
                HoraNoche(hora)
            };

            double sumaActivacion = 0;
            double sumaPonderada = 0;

            // Para cada regla, se calcula la activación usando el operador AND (mínimo)
            foreach (var regla in reglas)
            {
                double activacionFlujo = muFlujo[regla.flujo];
                double activacionVelocidad = muVelocidad[regla.velocidad];
                double activacionHora = muHora[regla.hora];

                double activacionRegla = Math.Min(activacionFlujo, Math.Min(activacionVelocidad, activacionHora));

                sumaPonderada += activacionRegla * regla.salida;
                sumaActivacion += activacionRegla;
            }

            if (sumaActivacion == 0)
                return 0;

            // Defuzzificación: método del centroide (promedio ponderado)
            return sumaPonderada / sumaActivacion;
        }
    }
}
