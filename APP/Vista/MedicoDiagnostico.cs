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
    public partial class MedicoDiagnostico : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;
        private String[] historiaClinica;
        private int idCita;
        private ControladorPersona _pacienteController;
        private ControladorMedico _medicoController;
        public MedicoDiagnostico(int id_Cita, string nombre, string apellido, int id_usuario, string rol)
        {
            InitializeComponent();
            idCita = id_Cita;
            Nombre = nombre;
            Apellido = apellido;
            idUsuario = id_usuario;
            Rol = rol;
            this.Load += MedicoDiagnostico_Load;


        }

        private void MedicoDiagnostico_Load(object sender, EventArgs e)
        {
            _pacienteController = new ControladorPersona(new Conexion());
            _medicoController = new ControladorMedico(new Conexion());
            historiaClinica = _medicoController.ObtenerHistoriaClinica(idCita);
            if (historiaClinica == null || historiaClinica.Length < 4)
            {
                MessageBox.Show("No se pudo cargar la información de la historia clínica.");
                MedicoAgenda medico = new MedicoAgenda(Nombre, Apellido, idUsuario, Rol);
                medico.Show();
                this.Close();
                this.Hide();  
                return;
            }
            lblHistoriaClinica.Text = "Nro Historia: " + historiaClinica[0];
            lblCedulaPaciente.Text = "C.I: " + historiaClinica[1];
            lblPaciente.Text = "Paciente: " + historiaClinica[2];
            lblServicio.Text = "Servicio: " + historiaClinica[3];

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string diagnostico = txtDiagnostico.Text;
            if (string.IsNullOrWhiteSpace(diagnostico))
            {
                MessageBox.Show("El diagnóstico no puede estar vacío.");
                return;
            }

            string patronCaracteresInvalidos = @"[;'\-]{2,}";
            if (Regex.IsMatch(diagnostico, patronCaracteresInvalidos))
            {
                MessageBox.Show("El diagnóstico contiene caracteres no permitidos.");
                return;
            }
            diagnostico = diagnostico.ToUpper();
            
            int idDiagnostico = _medicoController.InsertarDiagnostico(idCita, diagnostico, idUsuario);
            if (idDiagnostico > 0)
            {
                if (_medicoController.ActualizarCita(idCita, idDiagnostico, idUsuario))
                {
                    bool actualizarHistoria = _medicoController.ActualizarHistorial(idCita, idDiagnostico, idUsuario);
                    if (actualizarHistoria)
                    {
                        MessageBox.Show("Diagnóstico guardado correctamente.");
                        MedicoHistorial medicoHistorial = new MedicoHistorial(Nombre, Apellido, idUsuario, Rol);
                        medicoHistorial.Show();
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Error al guardar el diagnóstico. Por favor, inténtelo de nuevo.");
            }


        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Medico medico = new Medico(Nombre, Apellido, idUsuario, Rol);
            medico.Show();
            this.Hide();
        }
    }
}
