using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monosabios
{
    public partial class frmEditarUsuario : Form
    {
        public string id_usuario;
        public string nombre_usuario;
        public string password;

        public frmEditarUsuario()
        {
            InitializeComponent();
        }

        private void frmEditarUsuario_Load(object sender, EventArgs e)
        {
            txtNombreUsuario.Text = nombre_usuario;
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Si hay nueva contraseña entonces enviarla a la base
            string sql="";
            if (txtPasswordUsuario.Text != "")
            {
                sql = "update usuarios set nombre_usuario= '"
                    + txtNombreUsuario.Text +
                    "', password=MD5('"+txtPasswordUsuario.Text +
                     "') WHERE id_usuario= '" + id_usuario + "'";
            }

            else
            {
                sql = "update usuarios set nombre_usuario= '"
                + txtNombreUsuario.Text +
                 "'WHERE id_usuario= '" + id_usuario + "'";
            }
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Usuario Actualizado");
                this.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);


            }
            finally
            {
                conexionBD.Close();

            }
        }
    }
}
