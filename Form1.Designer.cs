namespace game_v2
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
            components = new System.ComponentModel.Container();
            tiempoJuego = new System.Windows.Forms.Timer(components);
            txtPuntaje = new Label();
            pbJugador = new PictureBox();
            tituloInicio = new Label();
            btnEmpezar = new Label();
            txtMensajes = new Label();
            ((System.ComponentModel.ISupportInitialize)pbJugador).BeginInit();
            SuspendLayout();
            // 
            // tiempoJuego
            // 
            tiempoJuego.Interval = 20;
            tiempoJuego.Tick += eventoPrincipalTiempoJuego;
            // 
            // txtPuntaje
            // 
            txtPuntaje.AutoSize = true;
            txtPuntaje.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            txtPuntaje.Location = new Point(12, 535);
            txtPuntaje.Name = "txtPuntaje";
            txtPuntaje.Size = new Size(79, 21);
            txtPuntaje.TabIndex = 1;
            txtPuntaje.Text = "Puntaje: 0";
            txtPuntaje.UseCompatibleTextRendering = true;
            txtPuntaje.Visible = false;
            // 
            // pbJugador
            // 
            pbJugador.Image = Properties.Resources.nave_espacial;
            pbJugador.Location = new Point(333, 499);
            pbJugador.Name = "pbJugador";
            pbJugador.Size = new Size(55, 50);
            pbJugador.SizeMode = PictureBoxSizeMode.StretchImage;
            pbJugador.TabIndex = 2;
            pbJugador.TabStop = false;
            pbJugador.Visible = false;
            // 
            // tituloInicio
            // 
            tituloInicio.AutoSize = true;
            tituloInicio.Font = new Font("Comic Sans MS", 48F, FontStyle.Bold, GraphicsUnit.Point);
            tituloInicio.Location = new Point(145, 191);
            tituloInicio.Name = "tituloInicio";
            tituloInicio.Size = new Size(490, 90);
            tituloInicio.TabIndex = 4;
            tituloInicio.Text = "Space Invader";
            // 
            // btnEmpezar
            // 
            btnEmpezar.AutoSize = true;
            btnEmpezar.BackColor = SystemColors.ActiveCaption;
            btnEmpezar.Cursor = Cursors.Hand;
            btnEmpezar.FlatStyle = FlatStyle.Popup;
            btnEmpezar.Font = new Font("Comic Sans MS", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnEmpezar.Location = new Point(275, 307);
            btnEmpezar.Name = "btnEmpezar";
            btnEmpezar.Size = new Size(174, 51);
            btnEmpezar.TabIndex = 5;
            btnEmpezar.Text = "Empezar";
            btnEmpezar.Click += presionaEmpezar;
            // 
            // txtMensajes
            // 
            txtMensajes.AutoSize = true;
            txtMensajes.Font = new Font("Comic Sans MS", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtMensajes.Location = new Point(12, 9);
            txtMensajes.Name = "txtMensajes";
            txtMensajes.Size = new Size(183, 27);
            txtMensajes.TabIndex = 6;
            txtMensajes.Text = "Mensaje de alerta";
            txtMensajes.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(734, 561);
            Controls.Add(txtMensajes);
            Controls.Add(btnEmpezar);
            Controls.Add(tituloInicio);
            Controls.Add(pbJugador);
            Controls.Add(txtPuntaje);
            Name = "Form1";
            Text = "Space Invader";
            KeyDown += teclaPresionada;
            KeyUp += teclaArriba;
            ((System.ComponentModel.ISupportInitialize)pbJugador).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer tiempoJuego;
        private Label txtPuntaje;
        private PictureBox pbJugador;
        private Label tituloInicio;
        private Label btnEmpezar;
        private Label txtMensajes;
    }
}