using ControlDifusoSemaforo;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace FuzzyLogicSemaforo
{
    public partial class ChartRuleForm : Form
    {
        private FuzzyRule _rule;
        private double _activation;
        private Dictionary<string, double> _crispInputs;
        public ChartRuleForm(FuzzyRule rule, double activation, Dictionary<string, double> crispInputs)
        {
            InitializeComponent();
            _rule = rule;
            _activation = activation;
            _crispInputs = crispInputs;
            ConfigureChartAreas();
            PlotRule();
        }
        private void ConfigureChartAreas()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            // Área de Flujo
            var areaFlujo = new ChartArea("FlujoArea");
            areaFlujo.AxisX.Title = "Flujo (veh/h)";
            areaFlujo.AxisY.Title = "Grado de membresía";
            areaFlujo.AxisX.Minimum = 0;
            areaFlujo.AxisX.Maximum = 900;
            areaFlujo.AxisY.Minimum = 0;
            areaFlujo.AxisY.Maximum = 1;
            // Ajusta la posición y el tamaño relativo en la vista
            // para que tengas 4 áreas en 2x2 (o 4 en vertical/horizontal)
            areaFlujo.Position = new ElementPosition(0, 0, 50, 50);
            chart1.ChartAreas.Add(areaFlujo);

            // Área de Velocidad
            var areaVelocidad = new ChartArea("VelocidadArea");
            areaVelocidad.AxisX.Title = "Velocidad (km/h)";
            areaVelocidad.AxisY.Title = "Grado de membresía";
            areaVelocidad.AxisX.Minimum = 0;
            areaVelocidad.AxisX.Maximum = 60;
            areaVelocidad.AxisY.Minimum = 0;
            areaVelocidad.AxisY.Maximum = 1;
            areaVelocidad.Position = new ElementPosition(50, 0, 50, 50);
            chart1.ChartAreas.Add(areaVelocidad);

            // Área de Hora
            var areaHora = new ChartArea("HoraArea");
            areaHora.AxisX.Title = "Hora del día";
            areaHora.AxisY.Title = "Grado de membresía";
            areaHora.AxisX.Minimum = 0;
            areaHora.AxisX.Maximum = 24;
            areaHora.AxisY.Minimum = 0;
            areaHora.AxisY.Maximum = 1;
            areaHora.Position = new ElementPosition(0, 50, 50, 50);
            chart1.ChartAreas.Add(areaHora);

            // Área de Salida (Tiempo)
            var areaSalida = new ChartArea("SalidaArea");
            areaSalida.AxisX.Title = "Tiempo Semáforo (s)";
            areaSalida.AxisY.Title = "Grado de membresía";
            areaSalida.AxisX.Minimum = 30;
            areaSalida.AxisX.Maximum = 90;
            areaSalida.AxisY.Minimum = 0;
            areaSalida.AxisY.Maximum = 1;
            areaSalida.Position = new ElementPosition(50, 50, 50, 50);
            chart1.ChartAreas.Add(areaSalida);
        }

        private void PlotRule()
        {
            lblReglas.Text = $"Regla: {string.Join($" {_rule.Operator} ",_rule.Antecedents.Select(a => $"{a.VariableName}={a.LabelName}"))} " +
                $"=> {_rule.Consequent.VariableName}={_rule.Consequent.LabelName}";


            // Graficamos los antecedentes
            foreach (var antecedent in _rule.Antecedents)
            {
                PlotAntecedent(antecedent);
            }

            // Graficamos la salida recortada
            PlotSalidaRecortada(_rule.Consequent);

            // También podrías, si deseas, agregar un Title en la parte superior del Chart
            // chart1.Titles.Add("Regla Activa");
        }

        private void PlotAntecedent(FuzzyLabel antecedent)
        {
            // Determinamos a cuál ChartArea dibujar según el nombre de la variable
            string? areaName = GetChartAreaForVariable(antecedent.VariableName);
            if (string.IsNullOrEmpty(areaName)) return;

            // Creamos la serie para la función de membresía
            var serie = new Series($"{antecedent.VariableName}-{antecedent.LabelName}")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                ChartArea = areaName
            };

            // Tomamos el dominio correspondiente
            var domain = DomainSettings.Domains[antecedent.VariableName];
            for (double x = domain.Min; x <= domain.Max; x += domain.Step)
            {
                double y = antecedent.GetMembership(x);
                serie.Points.AddXY(x, y);
            }
            chart1.Series.Add(serie);

            // Graficamos el valor crisp en rojo
            if (_crispInputs.TryGetValue(antecedent.VariableName, out double crispVal))
            {
                double crispY = antecedent.GetMembership(crispVal);
                var crispSerie = new Series($"{antecedent.VariableName}-Crisp")
                {
                    ChartType = SeriesChartType.Point,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 8,
                    Color = System.Drawing.Color.Red,
                    ChartArea = areaName
                };
                crispSerie.Points.AddXY(crispVal, crispY);
                chart1.Series.Add(crispSerie);
            }
        }

        private void PlotSalidaRecortada(FuzzyLabel consequent)
        {
            // Usamos siempre el área "SalidaArea"
            string areaName = "SalidaArea";
            var domain = DomainSettings.Domains["Tiempo"];

            var serieSalida = new Series($"Salida - {consequent.LabelName}")
            {
                ChartType = SeriesChartType.Line,
                BorderDashStyle = ChartDashStyle.Dash,
                BorderWidth = 2,
                Color = System.Drawing.Color.Blue,
                ChartArea = areaName
            };

            for (double x = domain.Min; x <= domain.Max; x += domain.Step)
            {
                double membership = consequent.GetMembership(x);
                double recortado = Math.Min(membership, _activation);
                serieSalida.Points.AddXY(x, recortado);
            }
            chart1.Series.Add(serieSalida);
        }

        // Método para mapear la variable al nombre del ChartArea
        private string? GetChartAreaForVariable(string variableName)
        {
            switch (variableName)
            {
                case "Flujo": return "FlujoArea";
                case "Velocidad": return "VelocidadArea";
                case "Hora": return "HoraArea";
                case "Tiempo": return "SalidaArea"; // no lo usamos en antecedente, pero ahí está
                default: return null;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
