using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

namespace game_v2
{
    public partial class Form1 : Form
    {

        // Movimiento del jugador
        bool movIzquierda, movDerecha;
        // Velocidad del jugador
        int jugadorVelocidad = 12;
        // Velocidad de los enemigos
        int enemigosVelocidad = 5;
        // Puntaje del jugador
        int puntajeContador = 0;
        // cuando generar las balas enemigas
        int tiempoBalasEnemigas = 300;
        // cantidad enemigos
        int cantidadEnemigos = 10;

        // cuadro de imagenes de los invasores, es un array, para que sean varias
        PictureBox[] invasoresArray;

        bool disparo;
        bool juegoPerdido;
        bool empezoJuego;

        public Form1()
        {
            InitializeComponent();
        }

        // Nueva clase Jugador
        public class Jugador
        {
            // Propiedades del jugador
            public string Nombre { get; set; }
            public int Puntos { get; set; }
            public int Velocidad { get; set; }
            public int Vidas { get; set; }

            // Constructor
            public Jugador(string nombre, int puntos, int velocidad, int vidas)
            {
                Nombre = nombre;
                Puntos = puntos;
                Velocidad = velocidad;
                Vidas = vidas;
            }

        }

        private void teclaPresionada(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                movIzquierda = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                movDerecha = true;
            }
        }

        private void teclaArriba(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && empezoJuego)
            {
                movIzquierda = false;
            }
            if (e.KeyCode == Keys.Right && empezoJuego)
            {
                movDerecha = false;
            }
            if (e.KeyCode == Keys.Space && !disparo && empezoJuego)
            {
                disparo = true;
                crearDisparo("jugadorDisparo");
            }
            if (e.KeyCode == Keys.Enter && juegoPerdido && empezoJuego)
            {
                quitarTodo();
                crearJuego();
            }
        }

        private void presionaEmpezar(object sender, EventArgs e)
        {
            txtMensajes.Visible = false;
            empezoJuego = true;
            crearJuego();
        }

        private void crearInvasores()
        {
            invasoresArray = new PictureBox[cantidadEnemigos];

            int left = 0;

            for (int i = 0; i < invasoresArray.Length; i++)
            {

                invasoresArray[i] = new PictureBox();
                invasoresArray[i].Size = new Size(60, 50);
                invasoresArray[i].Image = Properties.Resources.alien; // cambiar imagen del enemigo
                invasoresArray[i].Top = 0;
                invasoresArray[i].Tag = "invasor";
                invasoresArray[i].Left = left;
                invasoresArray[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(invasoresArray[i]);
                left = left - 80;

            }

        }

        // Funcion para definir o comenzar el juego
        private void crearJuego()
        {
            // Pongo los elementos visibles
            if (empezoJuego)
            {
                // Ponemos los personajes visibles
                pbJugador.Visible = true;
                txtPuntaje.Visible = true;
                // Borramos el inicio del juego
                btnEmpezar.Visible = false;
                tituloInicio.Visible = false;

                txtPuntaje.Text = "Puntaje: 0";
                puntajeContador = 0;
                juegoPerdido = false;

                tiempoBalasEnemigas = 300;
                enemigosVelocidad = 5;
                disparo = false;

                crearInvasores();
                tiempoJuego.Start();
            }

        }

        private void quitarTodo()
        {

            foreach (PictureBox i in invasoresArray)
            {
                this.Controls.Remove(i);
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "jugadorDisparo" || (string)x.Tag == "enemigoDisparo")
                    {
                        this.Controls.Remove(x);
                    }
                }
            }

        }

        private void crearDisparo(string tipoDisparo)
        {
            PictureBox bala = new PictureBox();
            bala.Image = Properties.Resources.bala_1;
            bala.Size = new Size(24, 24);
            bala.Tag = tipoDisparo;
            // Estara en la mitad de la imagen del jugador
            // Como si recreara un rayo o algo así
            bala.Left = pbJugador.Left + pbJugador.Width / 2;

            if ((string)bala.Tag == "jugadorDisparo")
            {
                bala.Top = pbJugador.Top - 20;
            }
            else if ((string)bala.Tag == "enemigoDisparo")
            {
                bala.Top = -100;
            }

            this.Controls.Add(bala);

            // Poner la bala, por encima de todo
            bala.BringToFront();

        }

        private void eventoPrincipalTiempoJuego(object sender, EventArgs e)
        {

            txtPuntaje.Text = $"Puntaje: {puntajeContador}";

            if (movIzquierda && empezoJuego)
            {
                pbJugador.Left -= jugadorVelocidad;
            }

            if (movDerecha && empezoJuego)
            {
                pbJugador.Left += jugadorVelocidad;
            }

            tiempoBalasEnemigas -= 10;

            if (tiempoBalasEnemigas < 1)
            {
                tiempoBalasEnemigas = 300;
                crearDisparo("enemigoDisparo");
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "invasor")
                {

                    x.Left += enemigosVelocidad;

                    if (x.Left > 730)
                    {
                        x.Top += 65;
                        x.Left = -80;
                    }

                    if (x.Bounds.IntersectsWith(pbJugador.Bounds))
                    {
                        perdioElJuego("Has sido invadido por los invasores, ¡ahora estás triste!");
                    }

                    foreach (Control y in this.Controls)
                    {

                        if (y is PictureBox && (string)y.Tag == "jugadorDisparo")
                        {
                            if (y.Bounds.IntersectsWith(x.Bounds))
                            {
                                this.Controls.Remove(x);
                                this.Controls.Remove(y);
                                puntajeContador += 1;
                                disparo = false;
                            }
                        }

                    }

                }

                if (x is PictureBox && (string)x.Tag == "jugadorDisparo")
                {
                    x.Top -= 20;

                    if (x.Top < 15)
                    {
                        this.Controls.Remove(x);
                        disparo = false;
                    }

                }

                // Si falla el tiro por aca cambiamos
                if (x is PictureBox && (string)x.Tag == "enemigoDisparo")
                {
                    x.Top += 20;

                    if (x.Top > 620)
                    {
                        this.Controls.Remove(x);
                    }

                    if (x.Bounds.IntersectsWith(pbJugador.Bounds))
                    {
                        this.Controls.Remove(x);
                        perdioElJuego("Te han matado. Ahora estarás triste para siempre.");
                    }

                }

            }

            // Validamos si vamos a ganar
            if (puntajeContador > 8)
            {
                enemigosVelocidad = 12;
            }

            if (puntajeContador == invasoresArray.Length)
            {
                perdioElJuego("Woohoo Felicidades, ¡Mantenlo a salvo!");
            }
        }

        private void perdioElJuego(string message)
        {
            juegoPerdido = true;
            empezoJuego = false;
            txtPuntaje.Visible = false;
            tiempoJuego.Stop();
            pbJugador.Visible = false;
            quitarTodo();
            txtMensajes.Visible = true;
            txtMensajes.Text = $"Puntaje: {puntajeContador} {message}";
            txtMensajes.BringToFront(); // poner encima de todo
            btnEmpezar.Visible = true;
            tituloInicio.Visible = true;
        }

    }
}