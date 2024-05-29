using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sistema_asistencia
{
    public partial class nomina : Form
    {

        private Conexion conexion;
        public nomina()
        {
            InitializeComponent();
            conexion = new Conexion();
        }
        public nomina(int id)
        {
            InitializeComponent();
            conexion = new Conexion();
            viewNomina(id);
        }

        private void nomina_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        // muestra la nomia del empleado seleccionado por el administrador.
        private void viewNomina(int id)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT SUM(horas_laborales) as TotalHorasLaborales, SUM(sueldo_horas_laborales) as SueldoHorasLaborales, SUM(horas_extras) as HorasExtras, SUM(sueldo_horas_extras) as SueldoHorasExtras, SUM(sueldo_Total) as Sueldo  FROM nomina_empleado WHERE usuarios_id = '"+id+"'", conn);

                da.Fill(dt);

                this.gridNomina.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        //btn regresar al admin
        private void regresar_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }
    }
}
