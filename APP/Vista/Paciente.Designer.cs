namespace APP.Vista
{
    partial class Paciente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.bttCerrarSesion = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnCitas = new System.Windows.Forms.Button();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(171, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 62);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hospital del Día";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(157, 112);
            this.lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(145, 27);
            this.lbl.TabIndex = 6;
            this.lbl.Text = "Bienvenido:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(16, 246);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 62);
            this.button1.TabIndex = 11;
            this.button1.Text = "Reservar cita";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bttCerrarSesion
            // 
            this.bttCerrarSesion.BackColor = System.Drawing.Color.Red;
            this.bttCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttCerrarSesion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bttCerrarSesion.Location = new System.Drawing.Point(253, 347);
            this.bttCerrarSesion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bttCerrarSesion.Name = "bttCerrarSesion";
            this.bttCerrarSesion.Size = new System.Drawing.Size(149, 50);
            this.bttCerrarSesion.TabIndex = 14;
            this.bttCerrarSesion.Text = "Cerrar sesiòn";
            this.bttCerrarSesion.UseVisualStyleBackColor = false;
            this.bttCerrarSesion.Click += new System.EventHandler(this.bttCerrarSesion_Click_1);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(233, 180);
            this.lblMensaje.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(215, 29);
            this.lblMensaje.TabIndex = 15;
            this.lblMensaje.Text = "Eliga una opción:";
            // 
            // btnCitas
            // 
            this.btnCitas.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCitas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCitas.Location = new System.Drawing.Point(240, 246);
            this.btnCitas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCitas.Name = "btnCitas";
            this.btnCitas.Size = new System.Drawing.Size(163, 62);
            this.btnCitas.TabIndex = 16;
            this.btnCitas.Text = "Citas pendientes";
            this.btnCitas.UseVisualStyleBackColor = false;
            this.btnCitas.Click += new System.EventHandler(this.btnCitas_Click);
            // 
            // btnHistorial
            // 
            this.btnHistorial.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorial.Location = new System.Drawing.Point(431, 246);
            this.btnHistorial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(219, 62);
            this.btnHistorial.TabIndex = 17;
            this.btnHistorial.Text = "Ver mi historia clínica";
            this.btnHistorial.UseVisualStyleBackColor = false;
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorial_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(345, 112);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(103, 36);
            this.lblNombre.TabIndex = 18;
            this.lblNombre.Text = "-----------";
            // 
            // Paciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 469);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnHistorial);
            this.Controls.Add(this.btnCitas);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.bttCerrarSesion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Paciente";
            this.Text = "Paciente";
            this.Load += new System.EventHandler(this.Paciente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bttCerrarSesion;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnCitas;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Label lblNombre;
    }
}