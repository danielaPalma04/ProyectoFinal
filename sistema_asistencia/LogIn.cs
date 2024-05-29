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
    public partial class LogIn : Form
    { 
        //crea el objeto de la clase conexión para la bd
        private Conexion conexion;
        public LogIn()
        {
            InitializeComponent();
            conexion = new Conexion();
            //hace la conexion hacia la bd
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                MessageBox.Show("Conexion exitosa.");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = txtUsuario.Text.Trim().ToString();
            string password = txtContraseña.Text.Trim().ToString();

            //valida el tipo de rol

            if (validateLogin(userName, password))
            {
                if(GetRol(userName, password).Equals("admin"))
                {
                    Admin admin = new Admin();
                    admin.Show();
                    this.Hide();
                }
                else
                {
                    Empleado empleado = new Empleado(GetId(userName, password));
                    entrada(GetId(userName, password));
                    empleado.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Usuario incorrecto ");
            }
        }

        //metodo para encontrar al usuario y si es exixtente
        private bool validateLogin(string userName, string password)
        {
            bool isValid = false;
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @userName AND pass = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                isValid = result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isValid;
        }

        //metodo para obtener el rol del usuario
        private string GetRol(string userName, string password)
        {
            string rol = "";
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                string query = "SELECT rol FROM usuarios WHERE usuario = @userName AND pass = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    rol = (reader["rol"].ToString());
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
            return rol;
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        //metodo para obtener el id del usuario.
        private int GetId(string userName, string password)
        {
            int id = 0;
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                string query = "SELECT id FROM usuarios WHERE usuario = @userName AND pass = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
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

        //registra la hora de entrada
        private void entrada(int id)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();

                string query = "INSERT INTO registro_entrada (entrada, usuarios_id)" +
                    "values (CURRENT_TIMESTAMP,'" + id + "');";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                
                while (reader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear el usuario");
            }
            finally
            {
                conn.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
