using System.Collections.Generic;

namespace FuzzyLogicSemaforo.Fuzzificación
{
    public static class DomainSettings
    {
        public static Dictionary<string, (double Min, double Max, double Step)> Domains = new Dictionary<string, (double, double, double)>
        {
            { "Flujo", (0, 900, 10) },
            { "Velocidad", (0, 100, 1) },
            { "Hora", (0, 24, 0.5) },
            { "Tiempo", (0, 90, 1) }
        };
    }
}
