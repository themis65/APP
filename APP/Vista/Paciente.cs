using APP.Controlador;
using APP.Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace APP.Vista
{
    public partial class Paciente : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;

        public Paciente(string nombre, string apellido, int id_usuario, string rol)
        {

            InitializeComponent();
            lblNombre.Text = nombre + " " + apellido;
            idUsuario = id_usuario;
            Rol = rol;
            Nombre = nombre;
            Apellido = apellido;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(180, 220, 255), // Color superior
                Color.White,                   // Color inferior
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

        

        private void Paciente_Load(object sender, EventArgs e)
        {
            this.Paint += Form1_Paint;

        }

        private void bttCerrarSesion_Click_1(object sender, EventArgs e)
        {
            Form1 formularioLogin = new Form1();
            formularioLogin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PacienteReservarCita pacienteReservarCita = new PacienteReservarCita(Nombre, Apellido, idUsuario, Rol);
            pacienteReservarCita.Show();
            this.Hide();
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            PacienteCitas pacienteCitas = new PacienteCitas(Nombre, Apellido, idUsuario, Rol);
            pacienteCitas.Show();
            this.Hide();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            PacienteHistorial pacienteHistorial = new PacienteHistorial(Nombre, Apellido, idUsuario, Rol);
            pacienteHistorial.Show();
            this.Hide();
        }
    }
}
