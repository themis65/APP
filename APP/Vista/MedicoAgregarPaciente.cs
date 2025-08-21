using APP.Controlador;
using APP.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP.Vista
{
    public partial class MedicoAgregarPaciente : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;
        private ControladorMedico _medicoController;
        public MedicoAgregarPaciente(string nombre, string apellido, int id_usuario, string rol)
        {
            InitializeComponent();
            Nombre = nombre;
            Apellido = apellido;
            idUsuario = id_usuario;
            Rol = rol;
            _medicoController = new ControladorMedico(new Conexion());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Medico medicoForm = new Medico(Nombre, Apellido, idUsuario, Rol);
            medicoForm.Show();
            this.Hide();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$");
            Regex injection = new Regex(@"[;'\-]{2,}");
            string nombre1 = txtNombre1.Text.Trim();
            if (string.IsNullOrEmpty(nombre1) || !regex.IsMatch(nombre1) || injection.IsMatch(nombre1))
            {
                MessageBox.Show("El nombre tiene insconsistencias");
                return;
            }
            string nombre2 = txtNombre2.Text.Trim();
            if (string.IsNullOrEmpty(nombre2) || !regex.IsMatch(nombre2) || injection.IsMatch(nombre2))
            {
                MessageBox.Show("El nombre tiene insconsistencias");
                return;
            }
            string apellido1 = txtApellido1.Text.Trim();
            if (string.IsNullOrEmpty(apellido1) || !regex.IsMatch(apellido1) || injection.IsMatch(apellido1))
            {
                MessageBox.Show("El apellido tiene insconsistencias");
                return;
            }
            string apellido2 = txtApellido2.Text.Trim();
            if (string.IsNullOrEmpty(apellido2) || !regex.IsMatch(apellido2) || injection.IsMatch(apellido2))
            {
                MessageBox.Show("El apellido tiene insconsistencias");
                return;
            }
            string cedula = txtCedula.Text.Trim();
            if (string.IsNullOrEmpty(cedula) || !cedula.All(char.IsDigit) || cedula.Length != 10)
            {
                MessageBox.Show("La cédula tiene inconsistencias.");
                return;
            }
            string telefono = txtCelular.Text.Trim();
            if (string.IsNullOrEmpty(telefono) || !telefono.All(char.IsDigit) || telefono.Length != 10)
            {
                MessageBox.Show("El teléfono tiene inconsistencias.");
                return;
            }
            DateTime fechaNacimiento = dtpFnacimiento.Value.Date;
            DateTime hoy = DateTime.Today;
            if (fechaNacimiento > hoy)
            {
                MessageBox.Show("La fecha tiene inconsistencias");
                return;
            }
            string antecedentes = txtAntecedentes.Text.Trim();

            if (string.IsNullOrEmpty(antecedentes) || !regex.IsMatch(antecedentes) || injection.IsMatch(antecedentes))
            {
                MessageBox.Show("Los antecedentes no pueden estar vacíos.");
                return;
            }
            string[] credenciales = _medicoController.insertarUsuarioPaciente(nombre1.ToLower(), apellido1.ToLower(), cedula);
            if (credenciales == null || credenciales.Length < 2)
            {
                MessageBox.Show("Error al insertar el usuario. Por favor, inténtelo de nuevo.");
                return;
            }
            string nombres = nombre1 + " " + nombre2;
            string apellidos = apellido1 + " " + apellido2;
            int idUsuario = Convert.ToInt32(credenciales[0]);
            if (_medicoController.insertarPaciente(cedula, idUsuario, nombres.ToUpper(), apellidos.ToUpper(), fechaNacimiento, telefono, antecedentes.ToUpper()))
            {
                MessageBox.Show("Usuario agregado correctamente.\nCredenciales:\nUsuario: " + credenciales[1] + "\nContraseña: Cedula");
                Medico medicoForm = new Medico(Nombre, Apellido, idUsuario, Rol);
                medicoForm.Show();
                this.Hide();
                return;
            }
        }

        private void MedicoAgregarPaciente_Load(object sender, EventArgs e)
        {

        }
    }
}
