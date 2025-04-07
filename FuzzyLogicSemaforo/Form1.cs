using ControlDifusoSemaforo;
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
            // Ejemplo: Flujo Bajo (Trapezoidal 0,0,200,200), Medio (350,350,550,550), Alto (700,700,900,900)
            labelsFlujo = new List<FuzzyLabel>
                {
                    new FuzzyLabel("Flujo", "Bajo", x => FuzzyFunctions.Hombro(x, 0, 300)),
                    new FuzzyLabel("Flujo", "Medio", x => FuzzyFunctions.Trapezoidal(x, 350, 350, 550, 550)),
                    new FuzzyLabel("Flujo", "Alto", x => FuzzyFunctions.Saturacion(x, 700, 900))
                };

            // Velocidad: Trancon, Lenta, Regular, Rápida (funciones triangulares)
            labelsVelocidad = new List<FuzzyLabel>
                {
                    new FuzzyLabel("Velocidad", "Trancon", x => FuzzyFunctions.Hombro(x, 0, 5)),
                    new FuzzyLabel("Velocidad", "Lenta", x => FuzzyFunctions.Triangular(x, 0, 10, 20)),
                    new FuzzyLabel("Velocidad", "Regular", x => FuzzyFunctions.Triangular(x, 10, 30, 40)),
                    new FuzzyLabel("Velocidad", "Rapida", x => FuzzyFunctions.Saturacion(x, 40, 60))
                };

            // Hora: Mañana (Gauss), Tarde (Gauss), Noche (Gauss)
            labelsHora = new List<FuzzyLabel>
                {
                    new FuzzyLabel("Hora", "Manana", x => FuzzyFunctions.Gaussian(x, 9, 1)),
                    new FuzzyLabel("Hora", "Tarde", x => FuzzyFunctions.Gaussian(x, 14, 1.5)),
                    new FuzzyLabel("Hora", "Noche", x => FuzzyFunctions.Gaussian(x, 19, 1))
                };

            // Salida: Tiempo semáforo (Bajo, Medio, Alto) – usaremos trapezoidal
            labelsSalida = new List<FuzzyLabel>
                {
                    // Bajo: [30..45]
                    new FuzzyLabel("Tiempo", "Bajo", x => FuzzyFunctions.Trapezoidal(x, 30, 30, 45, 45)),
                    // Medio: [50..65]
                    new FuzzyLabel("Tiempo", "Medio", x => FuzzyFunctions.Trapezoidal(x, 50, 50, 65, 65)),
                    // Alto: [70..90]
                    new FuzzyLabel("Tiempo", "Alto", x => FuzzyFunctions.Trapezoidal(x, 70, 70, 90, 90))
                };
        }
        private void InicializarReglas()
        {
            reglasDifusas = new List<FuzzyRule>();

            // Ejemplo de creación de reglas:
            // IF Flujo=Bajo AND Velocidad=Trancon AND Hora=Manana THEN Tiempo=Bajo
            var flujoBajo = labelsFlujo.Find(l => l.LabelName == "Bajo");
            var flujoMedio = labelsFlujo.Find(l => l.LabelName == "Medio");
            var flujoAlto = labelsFlujo.Find(l => l.LabelName == "Alto");
            var velTrancon = labelsVelocidad.Find(l => l.LabelName == "Trancon");
            var velLenta = labelsVelocidad.Find(l => l.LabelName == "Lenta");
            var velRegular = labelsVelocidad.Find(l => l.LabelName == "Regular");
            var velRapida = labelsVelocidad.Find(l => l.LabelName == "Rapida");
            var horaManana = labelsHora.Find(l => l.LabelName == "Manana");
            var horaTarde = labelsHora.Find(l => l.LabelName == "Tarde");
            var horaNoche = labelsHora.Find(l => l.LabelName == "Noche");
            var tiempoBajo = labelsSalida.Find(l => l.LabelName == "Bajo");
            var tiempoMedio = labelsSalida.Find(l => l.LabelName == "Medio");
            var tiempoAlto = labelsSalida.Find(l => l.LabelName == "Alto");

            var reglas = new List<FuzzyRule>
            {
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velTrancon, horaManana }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velLenta, horaManana }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRegular, horaManana }, tiempoBajo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRapida, horaManana }, tiempoBajo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velTrancon, horaTarde }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velLenta, horaTarde }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRegular, horaTarde }, tiempoBajo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRapida, horaTarde }, tiempoBajo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velTrancon, horaNoche }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velLenta, horaNoche }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRegular, horaNoche }, tiempoBajo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoBajo, velRapida, horaNoche }, tiempoBajo, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velTrancon, horaManana }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velLenta, horaManana }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRegular, horaManana }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRapida, horaManana }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velTrancon, horaTarde }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velLenta, horaTarde }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRegular, horaTarde }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRapida, horaTarde }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velTrancon, horaNoche }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velLenta, horaNoche }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRegular, horaNoche }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoMedio, velRapida, horaNoche }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velTrancon, horaManana }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velLenta, horaManana }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRegular, horaManana }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRapida, horaManana }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velTrancon, horaTarde }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velLenta, horaTarde }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRegular, horaTarde }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRapida, horaTarde }, tiempoMedio, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velTrancon, horaNoche }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velLenta, horaNoche }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRegular, horaNoche }, tiempoAlto, "AND"),
                new FuzzyRule(new List<FuzzyLabel> { flujoAlto, velRapida, horaNoche }, tiempoAlto, "AND")
            };

            reglasDifusas.AddRange(reglas);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double flujo = (double)nudFlujo.Value;       // Vehículos/h
            double velocidad = (double)nudVelocidad.Value; // Km/h
            double hora = (double)nudHora.Value;

            double tiempoVerde = FuzzyEngine.CalcularTiempoVerde(reglasDifusas, flujo, velocidad, hora);
            lblResultado.Text = $"Tiempo semáforo en verde: {tiempoVerde:F2} segundos";
        }
    }
}
