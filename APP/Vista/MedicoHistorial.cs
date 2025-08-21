using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APP.Controlador;
using APP.Modelo;
using System.Drawing.Drawing2D; // Necesario para LinearGradientBrush

namespace APP.Vista
{
    public partial class MedicoHistorial : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;
        private ControladorMedico _medicoController;

        public MedicoHistorial(string nombre, string apellido, int id_usuario, string rol)
        {
            InitializeComponent();
            Nombre = nombre;
            Apellido = apellido;
            idUsuario = id_usuario;
            Rol = rol;
            _medicoController = new ControladorMedico(new Conexion());
            CargarDatos();
        }

        private void CargarDatos()
        {
            // Cargar historial médico del paciente asociado a este médico (o de todos los pacientes, dependiendo de la lógica de tu controlador)
            DataTable historial = _medicoController.ObtenerHistorialMedico(idUsuario);
            if (historial == null || historial.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron registros en el historial médico.");
                Medico medicoForm = new Medico(Nombre, Apellido, idUsuario, Rol);
                medicoForm.Show();
                this.Hide();
            }
            dgvHistorial.DataSource = historial;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            // Regresar al formulario principal de Médico
            Medico medicoForm = new Medico(Nombre, Apellido, idUsuario, Rol);
            medicoForm.Show();
            this.Hide();
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            // Este evento está vacío, no se requiere acción específica aquí.
        }

        private void MedicoHistorial_Load(object sender, EventArgs e)
        {
            // Vincula el método de pintura para el degradado al evento Paint
            this.Paint += MedicoHistorial_Paint;
        }

        // Método para dibujar el degradado de fondo
        private void MedicoHistorial_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(180, 220, 255), // Color superior (un celeste claro)
                Color.White,                  // Color inferior (blanco)
                90F)) // 90F para un degradado de arriba hacia abajo
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}