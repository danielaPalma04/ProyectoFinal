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
    public partial class Empleado : Form
    {
        private Conexion conexion;
        public Empleado()
        {
            InitializeComponent();

            conexion = new Conexion();
        }
        int horaEntrada = 0;
        int minutoEntrada = 0;
        int horaSalida = 0;

        //sobrecarga del contructor para obtener el id del usuario logeado
        public Empleado(int id)
        {
            InitializeComponent();
            conexion = new Conexion();
            user(id);
            llenar(id);

            DateTime fecha = DateTime.Now;

            horaEntrada = fecha.Hour;
            minutoEntrada = fecha.Minute;

            //valida hora de entrada
            if (horaEntrada >= 8 && minutoEntrada > 0)
            {
                lblestado.Text = "Horario con retraso";
            }
            else
            {
                lblestado.Text = "Horario correcto";
            }
        }
        // btn cerrar sesión
        private void button1_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            this.Hide();
            salida(GetId(usuario));
            nomina(GetId(usuario));
        }
        string usuario = "";

        //metodo que obtiene los datos de usuario por el id
        private void user(int id)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                string query = "SELECT nombres, apellido, usuario FROM usuarios WHERE id = '"+ id +"'; ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lblnombre.Text = (reader["nombres"].ToString());
                    lblapellido.Text = (reader["apellido"].ToString());
                    lblusuario.Text = (reader["usuario"].ToString());
                    usuario = lblusuario.Text;
                }
                reader.Close();
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

        private void Empleado_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //metodo para registrar la salida del ususaro.
        private void salida(int id)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();

                string query = "INSERT INTO registro_salida (salida, usuarios_id)" +
                    "values (CURRENT_TIMESTAMP,'" + id + "');";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar la salida");
            }
            finally
            {
                conn.Close();
            }
        }

        //obtiene el id
        private int GetId(string userName)
        {
            int id = 0;
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                string query = "SELECT id FROM usuarios WHERE usuario = @userName ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32((reader["id"].ToString()));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        //Metodo para hacer el calculo de la nomina del empleado.
        private void nomina(int id)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                DateTime fecha = DateTime.Now;

                horaSalida = fecha.Hour;

                int horasLaborales = horaSalida - horaEntrada;
                double sueldoHorasLaborales = horasLaborales * 50;
                int horasExtras = 0;
                double sueldoHorasExtratas = 0;

                if (horasLaborales > 8)
                {
                    horasExtras = horasLaborales - 8;
                    sueldoHorasExtratas = (horasExtras * 50) * 1.5;
                }
                double sueldoTotal = sueldoHorasLaborales + sueldoHorasExtratas;

                conn.Open();

                string query = "INSERT INTO nomina_empleado (horas_laborales, sueldo_horas_laborales, horas_extras, sueldo_horas_extras, sueldo_total, usuarios_id)" +
                    "values ('" + horasLaborales + "' , '" + sueldoHorasLaborales + "' , '" + horasExtras + "' , '" + sueldoHorasExtratas + "' , '" + sueldoTotal + "' , '" + id + "');";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

               
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

        private void gridEmpleado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //muestra las emtradas y salidas del empleado 
        private void llenar(int id)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT re.entrada, rs.salida FROM registro_entrada re INNER JOIN registro_salida rs ON re.usuarios_id = rs.usuarios_id WHERE re.usuarios_id = '"+id+"' AND rs.usuarios_id = '"+id+"'", conn);

                da.Fill(dt);

                this.gridEmpleado.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroor: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
