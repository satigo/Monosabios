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
    public partial class frmUpdateProducto : Form
    {
        public string id_producto;
        public string id_categoria;
        public string precio;
        public string descripcion;

        public frmUpdateProducto()
        {
            InitializeComponent();
        }

        private void frmUpdateProducto_Load(object sender, EventArgs e)
        {
            label1.Text = "Codigo Producto: " + id_producto;
            txtPrecio.Text = precio;
            txtDescripcion.Text = descripcion;
            //Cargando categorias
            //Cargando categorias y productos
            DataTable dt = new DataTable();
            string sql = "select * from categoria_producto";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                categoryBox.DisplayMember = "descripcion";
                categoryBox.ValueMember = "category_id";
                categoryBox.DataSource = dt;
                categoryBox.SelectedValue = id_categoria;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje: " + ex.Message);

            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Almacenando nuevo producto
            string sql = "update producto set category_id= '"
                + categoryBox.SelectedValue +
                "', precio='" + Double.Parse(txtPrecio.Text) + "', descripcion='" +
                txtDescripcion.Text + "'WHERE idproducto= '"+id_producto+"'";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Producto Actualizado");
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
