using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // Necesario para Color, Rectangle
using System.Drawing.Drawing2D; // Necesario para LinearGradientBrush, LinearGradientMode
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APP.Controlador; // Asegúrate de que esta directiva sea correcta
using APP.Vista; // Asegúrate de que esta directiva sea correcta

namespace APP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtContrasena.UseSystemPasswordChar = true;
            // Habilitamos el doble buffer para evitar el parpadeo visual
            this.DoubleBuffered = true;
        }

        // Este método se encargará de dibujar el degradado en el fondo del formulario
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); // Llama al método base para que se dibujen los controles estándares

            // Definimos el área del degradado (todo el formulario)
            Rectangle rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);

            // Creamos el pincel de degradado, de color celeste a blanco
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.LightSkyBlue,  // Color inicial (celeste claro)
                Color.White,   // Color final (blanco)
                LinearGradientMode.Vertical)) // Dirección del degradado: de arriba a abajo
            {
                // Rellenamos el área con el degradado
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Puedes dejar esto vacío o añadir lógica que se ejecute al cargar el formulario.
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text;
            string password = txtContrasena.Text;

            LoginPersona loginService = new LoginPersona();
            if (loginService.Login(username, password, out string nombre, out string apellido, out int id_usuario, out string rol))
            {
   
                Form formularioDestino = null;

                switch (rol.ToLower())
                {
                    case "paciente":
                        formularioDestino = new Paciente(nombre, apellido, id_usuario, rol);
                        break;
                    case "medico":
                        formularioDestino = new Medico(nombre, apellido, id_usuario, rol);
                        break;
                    case "auditor":
                        formularioDestino = new Auditor(nombre, apellido, id_usuario, rol);
                        break;
                    default:
                        MessageBox.Show("Rol desconocido", "Error");
                        return;
                }

                formularioDestino.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registro eRegistros = new Registro();
            eRegistros.Show();
            this.Hide();
        }
    }
}