using ControlDifusoSemaforo;
using FuzzyLogicSemaforo.Fuzzificación;
using FuzzyLogicSemaforo.Logic;
using System.Collections.Generic;

namespace FuzzyLogicSemaforo
{
    public partial class Form1 : Form
    {
        private List<FuzzyLabel> labelsFlujo = new List<FuzzyLabel>();
        private List<FuzzyLabel> labelsVelocidad = new List<FuzzyLabel>();
        private List<FuzzyLabel> labelsHora = new List<FuzzyLabel>();
        private List<FuzzyLabel> labelsSalida = new List<FuzzyLabel>();
        private List<FuzzyRule> reglasDifusas = new List<FuzzyRule>();
        public Form1()
        {
            InitializeComponent();
            InicializarFuzzyLabels();
            InicializarReglas();
        }
        private void InicializarFuzzyLabels()
        {
            labelsFlujo = new List<FuzzyLabel>
                {
                    new FuzzyLabel("Flujo", "Bajo", x => FuzzyFunctions.Hombro(x, 300, 400)),
                    new FuzzyLabel("Flujo", "Medio", x => FuzzyFunctions.Trapezoidal(x, 350, 450, 550, 650)),
                    new FuzzyLabel("Flujo", "Alto", x => FuzzyFunctions.Saturacion(x, 600, 700))
                };
            labelsVelocidad = new List<FuzzyLabel>
                {
                    new FuzzyLabel("Velocidad", "Trancon", x => FuzzyFunctions.Hombro(x, 5, 8)),
                    new FuzzyLabel("Velocidad", "Lenta", x => FuzzyFunctions.Triangular(x, 5, 15, 25)),
                    new FuzzyLabel("Velocidad", "Regular", x => FuzzyFunctions.Triangular(x, 20, 30, 40)),
                    new FuzzyLabel("Velocidad", "Rapida", x => FuzzyFunctions.Triangular(x, 35, 45, 55)),
                    new FuzzyLabel("Velocidad", "MuyRapida", x => FuzzyFunctions.Saturacion(x, 50, 60))
                };
            labelsHora = new List<FuzzyLabel>
                {
                    new FuzzyLabel("Hora", "Manana", x => FuzzyFunctions.Hombro(x,11,12)),
                    new FuzzyLabel("Hora", "Tarde", x => FuzzyFunctions.Gaussian(x, 14, 1.5)),
                    new FuzzyLabel("Hora", "Noche", x => FuzzyFunctions.Saturacion(x,17,18))
                };
            labelsSalida = new List<FuzzyLabel>
                {
                    // Bajo: [30..45]
                    new FuzzyLabel("Tiempo", "Corto", x => FuzzyFunctions.Hombro(x,40,45)),
                    new FuzzyLabel("Tiempo", "MedioCorto", x => FuzzyFunctions.Trapezoidal(x, 40, 45, 50, 55)),
                    new FuzzyLabel("Tiempo", "Medio", x => FuzzyFunctions.Trapezoidal(x, 50, 55, 65, 70)),
                    new FuzzyLabel("Tiempo", "MedioLargo", x => FuzzyFunctions.Trapezoidal(x, 65, 70, 75, 80)),
                    new FuzzyLabel("Tiempo", "Largo", x => FuzzyFunctions.Saturacion(x,75,80))
                };
        }
        private void InicializarReglas()
        {
            reglasDifusas = new List<FuzzyRule>();
            var flujoBajo = labelsFlujo.Find(l => l.LabelName == "Bajo");
            var flujoMedio = labelsFlujo.Find(l => l.LabelName == "Medio");
            var flujoAlto = labelsFlujo.Find(l => l.LabelName == "Alto");
            var velTrancon = labelsVelocidad.Find(l => l.LabelName == "Trancon");
            var velLenta = labelsVelocidad.Find(l => l.LabelName == "Lenta");
            var velRegular = labelsVelocidad.Find(l => l.LabelName == "Regular");
            var velRapida = labelsVelocidad.Find(l => l.LabelName == "Rapida");
            var velMuyRapida = labelsVelocidad.Find(l => l.LabelName == "MuyRapida");
            var horaManana = labelsHora.Find(l => l.LabelName == "Manana");
            var horaTarde = labelsHora.Find(l => l.LabelName == "Tarde");
            var horaNoche = labelsHora.Find(l => l.LabelName == "Noche");
            var tiempoCorto = labelsSalida.Find(l => l.LabelName == "Corto");
            var tiempoMedioCorto = labelsSalida.Find(l => l.LabelName == "MedioCorto");
            var tiempoMedio = labelsSalida.Find(l => l.LabelName == "Medio");
            var tiempoMedioLargo = labelsSalida.Find(l => l.LabelName == "MedioLargo");
            var tiempoLargo = labelsSalida.Find(l => l.LabelName == "Largo");
            var reglas = new List<FuzzyRule>
            {
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velTrancon }, tiempoLargo, "OR"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRegular }, tiempoMedio, "OR"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velMuyRapida }, tiempoCorto, "OR"),

                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velTrancon }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRegular,horaManana }, tiempoMedioCorto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRegular,horaNoche }, tiempoMedioCorto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velMuyRapida }, tiempoCorto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRapida }, tiempoCorto, "AND"),

                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velTrancon }, tiempoMedioLargo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velLenta }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRegular, horaManana }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRegular, horaNoche }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRapida, horaTarde }, tiempoMedioCorto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRapida }, tiempoCorto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velMuyRapida }, tiempoCorto, "AND"),

                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velLenta }, tiempoMedioLargo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRegular, horaTarde }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velTrancon, horaTarde }, tiempoLargo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRegular, horaManana }, tiempoMedioLargo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRegular, horaNoche }, tiempoMedioLargo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRapida }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velMuyRapida }, tiempoMedioCorto, "AND"),
            };
            reglasDifusas.AddRange(reglas);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double flujo = (double)nudFlujo.Value;       // Vehículos/h
            double velocidad = (double)nudVelocidad.Value; // Km/h
            double hora = (double)nudHora.Value;
            var crispInputs = new Dictionary<string, double>
                {
                    { "Flujo", flujo },
                    { "Velocidad", velocidad },
                    { "Hora", hora }
                };
            var activeRules = FuzzyInference.GetActiveRules(reglasDifusas, crispInputs);

            double tiempoVerde = FuzzyEngine.CalcularTiempoVerde(reglasDifusas, flujo, velocidad, hora);
            lblResultado.Text = $"Tiempo semáforo en verde: {tiempoVerde:F4} segundos";

            foreach (var (rule, activation) in activeRules)
            {
                // Mostrar solo aquellas reglas cuya activación sea mayor a un umbral, si se desea
                if (activation > 0)
                {
                    ChartRuleForm chartForm = new ChartRuleForm(rule, activation, crispInputs);
                    chartForm.Show();
                }
            }
        }

        private void nudFlujo_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
