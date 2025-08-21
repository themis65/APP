namespace APP.Vista
{
    partial class MedicoDiagnostico
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
            this.lblHistoriaClinica = new System.Windows.Forms.Label();
            this.lblCedulaPaciente = new System.Windows.Forms.Label();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.lbldiagnostico = new System.Windows.Forms.Label();
            this.txtDiagnostico = new System.Windows.Forms.TextBox();
            this.lblServicio = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(195, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 62);
            this.label1.TabIndex = 19;
            this.label1.Text = "Hospital del Día";
            // 
            // lblHistoriaClinica
            // 
            this.lblHistoriaClinica.AutoSize = true;
            this.lblHistoriaClinica.BackColor = System.Drawing.Color.Transparent;
            this.lblHistoriaClinica.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoriaClinica.Location = new System.Drawing.Point(93, 100);
            this.lblHistoriaClinica.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoriaClinica.Name = "lblHistoriaClinica";
            this.lblHistoriaClinica.Size = new System.Drawing.Size(131, 36);
            this.lblHistoriaClinica.TabIndex = 20;
            this.lblHistoriaClinica.Text = "Nro Historia: ";
            // 
            // lblCedulaPaciente
            // 
            this.lblCedulaPaciente.AutoSize = true;
            this.lblCedulaPaciente.BackColor = System.Drawing.Color.Transparent;
            this.lblCedulaPaciente.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCedulaPaciente.Location = new System.Drawing.Point(430, 100);
            this.lblCedulaPaciente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCedulaPaciente.Name = "lblCedulaPaciente";
            this.lblCedulaPaciente.Size = new System.Drawing.Size(45, 36);
            this.lblCedulaPaciente.TabIndex = 21;
            this.lblCedulaPaciente.Text = "C.I :";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.BackColor = System.Drawing.Color.Transparent;
            this.lblPaciente.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaciente.Location = new System.Drawing.Point(93, 146);
            this.lblPaciente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(97, 36);
            this.lblPaciente.TabIndex = 22;
            this.lblPaciente.Text = "Paciente: ";
            // 
            // lbldiagnostico
            // 
            this.lbldiagnostico.AutoSize = true;
            this.lbldiagnostico.BackColor = System.Drawing.Color.Transparent;
            this.lbldiagnostico.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldiagnostico.Location = new System.Drawing.Point(93, 203);
            this.lbldiagnostico.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbldiagnostico.Name = "lbldiagnostico";
            this.lbldiagnostico.Size = new System.Drawing.Size(124, 36);
            this.lbldiagnostico.TabIndex = 23;
            this.lbldiagnostico.Text = "Diagnostico:";
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.Location = new System.Drawing.Point(99, 242);
            this.txtDiagnostico.Multiline = true;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.Size = new System.Drawing.Size(557, 127);
            this.txtDiagnostico.TabIndex = 24;
            // 
            // lblServicio
            // 
            this.lblServicio.AutoSize = true;
            this.lblServicio.BackColor = System.Drawing.Color.Transparent;
            this.lblServicio.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicio.Location = new System.Drawing.Point(430, 146);
            this.lblServicio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(93, 36);
            this.lblServicio.TabIndex = 25;
            this.lblServicio.Text = "Servicio: ";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(293, 376);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(149, 42);
            this.btnGuardar.TabIndex = 26;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(41, 376);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(149, 42);
            this.btnVolver.TabIndex = 27;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // MedicoDiagnostico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblServicio);
            this.Controls.Add(this.txtDiagnostico);
            this.Controls.Add(this.lbldiagnostico);
            this.Controls.Add(this.lblPaciente);
            this.Controls.Add(this.lblCedulaPaciente);
            this.Controls.Add(this.lblHistoriaClinica);
            this.Controls.Add(this.label1);
            this.Name = "MedicoDiagnostico";
            this.Text = "MedicoDiagnostico";
            this.Load += new System.EventHandler(this.MedicoDiagnostico_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHistoriaClinica;
        private System.Windows.Forms.Label lblCedulaPaciente;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label lbldiagnostico;
        private System.Windows.Forms.TextBox txtDiagnostico;
        private System.Windows.Forms.Label lblServicio;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVolver;
    }
}