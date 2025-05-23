﻿using FuzzyLogicSemaforo.Desfuzzificacion;
using FuzzyLogicSemaforo.Fuzzificación;
using FuzzyLogicSemaforo.Logic;
using System;
using System.Collections.Generic;

namespace ControlDifusoSemaforo
{
    public static class FuzzyEngine
    {
        public static double CalcularTiempoVerde(
            List<FuzzyRule> reglasDifusas,
            double flujo,
            double velocidad,
            double hora)
        {
            // 1) Fuzzificar entradas
            var crispInputs = new Dictionary<string, double>
            {
                { "Flujo", flujo },
                { "Velocidad", velocidad },
                { "Hora", hora }
            };
            // 2) Identificar reglas activas
            var activeRules = FuzzyInference.GetActiveRules(reglasDifusas, crispInputs);
            // 3) Recorte y Agregación sobre el dominio de la salida 
            var aggregated = FuzzyAggregation.AggregateOutput(activeRules, 30, 90, 1.0);
            // 4) Defuzzificar (Centroide)
            double resultado = FuzzyDefuzzification.Centroid(aggregated);
            return resultado;
        }
    }
}
