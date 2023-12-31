using System.Numerics;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace game_v2
{
    public partial class Form1 : Form
    {

        // Movimiento del jugador
        bool movIzquierda, movDerecha;
        // Velocidad del jugador
        // int jugadorVelocidad = 12;
        // Velocidad de los enemigos
        int enemigosVelocidad = 5;
        // Puntaje del jugador
        // int puntajeContador = 0;
        // cuando generar las balas enemigas
        int tiempoBalasEnemigas = 300;
        // cantidad enemigos
        int cantidadEnemigos = 15;

        // cuadro de imagenes de los invasores, es un array, para que sean varias
        PictureBox[] invasoresArray;

        bool disparo;
        bool juegoPerdido;
        bool empezoJuego;

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

        //variable de tipo de jugador
        Jugador pooJugador;

        public Form1()
        {
            InitializeComponent();
            // Jugador
            this.pooJugador = new Jugador("Jugador1", 0, 12, 3);
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
                pooJugador.Puntos = 0;
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
            // Como si recreara un rayo o algo as�
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

            txtPuntaje.Text = $"Puntaje: {pooJugador.Puntos}";

            if (movIzquierda && empezoJuego)
            {
                pbJugador.Left -= pooJugador.Velocidad;
            }

            if (movDerecha && empezoJuego)
            {
                pbJugador.Left += pooJugador.Velocidad;
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
                        perdioElJuego($"{pooJugador.Nombre}, has sido invadido por los invasores, �ahora est�s triste!", false);
                    }

                    foreach (Control y in this.Controls)
                    {

                        if (y is PictureBox && (string)y.Tag == "jugadorDisparo")
                        {
                            if (y.Bounds.IntersectsWith(x.Bounds))
                            {
                                this.Controls.Remove(x);
                                this.Controls.Remove(y);
                                pooJugador.Puntos += 1;
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
                        perdioElJuego($"Te han matado {pooJugador.Nombre}. Ahora estar�s triste para siempre.", false);
                    }

                }

            }

            // Validamos si vamos a ganar
            if (pooJugador.Puntos > 8)
            {
                enemigosVelocidad = 12;
            }

            if (pooJugador.Puntos == invasoresArray.Length)
            {
                perdioElJuego($"Woohoo Felicidades {pooJugador.Nombre}, �Mantenlo a salvo!", true);
            }
        }

        private void perdioElJuego(string message, bool limpio)
        {

            pooJugador.Vidas -= 1; // Quitarle una vida al jugador
            // Si el mk tiene vidas, y no a limpiado
            if (empezoJuego && pooJugador.Vidas > 0 && !limpio)
            {
                txtPuntaje.Visible = false; // Ocultar puntaje
                // Mostrar mensaje
                txtMensajes.Visible = true;
                txtMensajes.Text = $"Puntaje: {pooJugador.Puntos} \n {message}\nTe quedan {pooJugador.Vidas} vidas, presiona empezar para volver a intentarlo";
                txtMensajes.BringToFront(); // poner encima de todo
                // Volver a ver el inicio
                btnEmpezar.Visible = true;
                tituloInicio.Visible = true;
                btnEmpezar.Text = "Continuar";
                quitarTodo(); // Limpiamos todo el form
                tiempoJuego.Stop(); // Parar el juego
            }
            // Aca, si ya limpio todo
            else if (limpio)
            {
                reiniciarJuego($"{message}");
            }

            // Por aca, si ya se le acabaron
            else if (empezoJuego && pooJugador.Vidas == 0)
            {
                reiniciarJuego($"{pooJugador.Nombre},\ntu vida a llegado a su fin.");
            }

        }

        private void reiniciarJuego(string message)
        {
            pooJugador = new Jugador("Jugador1", 0, 12, 3); // Reiniciamos jugador
            juegoPerdido = true; // Decimos que ya perdio
            empezoJuego = false; // pa que comience de cero
            txtPuntaje.Visible = false;
            tiempoJuego.Stop();
            pbJugador.Visible = false; // No ver el jugador
            quitarTodo(); // Limpiamos todo el form
            txtMensajes.Visible = true;
            txtMensajes.Text = $"Puntaje: {pooJugador.Puntos}\n{message}";
            txtMensajes.BringToFront(); // poner encima de todo
            btnEmpezar.Visible = true;
            tituloInicio.Visible = true;

        }

    }
}