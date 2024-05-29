
namespace sistema_asistencia
{
    partial class nomina
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
            this.gridNomina = new System.Windows.Forms.DataGridView();
            this.regresar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridNomina)).BeginInit();
            this.SuspendLayout();
            // 
            // gridNomina
            // 
            this.gridNomina.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNomina.Location = new System.Drawing.Point(175, 69);
            this.gridNomina.Name = "gridNomina";
            this.gridNomina.Size = new System.Drawing.Size(472, 290);
            this.gridNomina.TabIndex = 0;
            this.gridNomina.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // regresar
            // 
            this.regresar.Location = new System.Drawing.Point(298, 395);
            this.regresar.Name = "regresar";
            this.regresar.Size = new System.Drawing.Size(215, 37);
            this.regresar.TabIndex = 1;
            this.regresar.Text = "regresar";
            this.regresar.UseVisualStyleBackColor = true;
            this.regresar.Click += new System.EventHandler(this.regresar_Click);
            // 
            // nomina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.regresar);
            this.Controls.Add(this.gridNomina);
            this.Name = "nomina";
            this.Text = "nomina";
            this.Load += new System.EventHandler(this.nomina_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridNomina)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridNomina;
        private System.Windows.Forms.Button regresar;
    }
}