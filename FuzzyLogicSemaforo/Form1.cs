namespace FuzzyLogicSemaforo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double flujo = (double)nudFlujo.Value;       // Vehículos/h
            double velocidad = (double)nudVelocidad.Value; // Km/h
            double hora = (double)nudHora.Value;

            double tiempoVerde = FuzzyController.CalcularTiempoVerde(flujo, velocidad, hora);
            lblResultado.Text = $"Tiempo semáforo en verde: {tiempoVerde:F1} segundos";

        }
    }
}
