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

namespace APP.Vista // Asegúrate de que este sea el namespace correcto para tu formulario Registro
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
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

        private void txtCI_TextChanged(object sender, EventArgs e)
        {
            // Deja tu lógica actual si tienes algo aquí
        }

        private void Registro_Load(object sender, EventArgs e)
        {
            // Deja tu lógica actual si tienes algo aquí
        }

        private void bttCerrarSesion_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
