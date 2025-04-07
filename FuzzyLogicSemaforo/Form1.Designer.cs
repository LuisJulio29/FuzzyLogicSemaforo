namespace FuzzyLogicSemaforo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            nudFlujo = new NumericUpDown();
            nudVelocidad = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnCalcular = new Button();
            lblResultado = new Label();
            nudHora = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nudFlujo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVelocidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHora).BeginInit();
            SuspendLayout();
            // 
            // nudFlujo
            // 
            nudFlujo.AccessibleName = "nudFlujo";
            nudFlujo.Location = new Point(357, 168);
            nudFlujo.Maximum = new decimal(new int[] { 900, 0, 0, 0 });
            nudFlujo.Name = "nudFlujo";
            nudFlujo.Size = new Size(250, 27);
            nudFlujo.TabIndex = 0;
            // 
            // nudVelocidad
            // 
            nudVelocidad.Location = new Point(357, 258);
            nudVelocidad.Name = "nudVelocidad";
            nudVelocidad.Size = new Size(250, 27);
            nudVelocidad.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(357, 145);
            label1.Name = "label1";
            label1.Size = new Size(105, 20);
            label1.TabIndex = 2;
            label1.Text = "Flujo Vehicular";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(357, 235);
            label2.Name = "label2";
            label2.Size = new Size(139, 20);
            label2.TabIndex = 3;
            label2.Text = "Velocidad Vehicular";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(357, 335);
            label3.Name = "label3";
            label3.Size = new Size(42, 20);
            label3.TabIndex = 5;
            label3.Text = "Hora";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(740, 250);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(171, 50);
            btnCalcular.TabIndex = 6;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += button1_Click;
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.Location = new Point(782, 319);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(75, 20);
            lblResultado.TabIndex = 7;
            lblResultado.Text = "Resultado";
            // 
            // nudHora
            // 
            nudHora.Location = new Point(357, 355);
            nudHora.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            nudHora.Name = "nudHora";
            nudHora.Size = new Size(250, 27);
            nudHora.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1283, 707);
            Controls.Add(nudHora);
            Controls.Add(lblResultado);
            Controls.Add(btnCalcular);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(nudVelocidad);
            Controls.Add(nudFlujo);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)nudFlujo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVelocidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHora).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown nudFlujo;
        private NumericUpDown nudVelocidad;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnCalcular;
        private Label lblResultado;
        private NumericUpDown nudHora;
    }
}
