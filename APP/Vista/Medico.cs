using APP.Vista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D; // Necesario para LinearGradientBrush
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APP.Controlador;

namespace APP
{
    public partial class Medico : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;

        public Medico(string nombre, string apellido, int id_usuario, string rol)
        {
            InitializeComponent();
            lblNombre.Text = nombre + " " + apellido;
            idUsuario = id_usuario;
            Rol = rol;
            Nombre = nombre;
            Apellido = apellido;
        }

        private void Medico_Load(object sender, EventArgs e)
        {
            // Vincula el método de pintura para el degradado al evento Paint
            this.Paint += Medico_Paint;
        }

        // Método para dibujar el degradado de fondo
        private void Medico_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(180, 220, 255), // Color superior (Azul claro)
                Color.White,                  // Color inferior (Blanco)
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void bttCerrarSesion_Click(object sender, EventArgs e)
        {
            Form formularioLogin = new Form1();
            formularioLogin.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            LoginPersona loginService = new LoginPersona();
            if(loginService.Logout(idUsuario))
            {
                MessageBox.Show("Sesión cerrada correctamente.");
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error al cerrar sesión.");
            }
            //Form1 form1 = new Form1();
            //form1.Show();
            //this.Hide();
        }

        private void btnAgenda_Click(object sender, EventArgs e)
        {
            MedicoAgenda medicoAgendaForm = new MedicoAgenda(Nombre, Apellido, idUsuario, Rol);
            medicoAgendaForm.Show();
            this.Hide();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            MedicoHistorial medicoHistorialForm = new MedicoHistorial(Nombre, Apellido, idUsuario, Rol);
            medicoHistorialForm.Show();
            this.Hide();
        }

        private void btnAgregarPaciente_Click(object sender, EventArgs e)
        {
            MedicoAgregarPaciente medicoAgregarPacienteForm = new MedicoAgregarPaciente(Nombre, Apellido, idUsuario, Rol);
            medicoAgregarPacienteForm.Show();
            this.Hide();
        }
    }
}