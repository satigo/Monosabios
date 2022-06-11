using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monosabios
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Cargando categorias y productos
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            string sql = "select * from categoria_producto";
            string sql2 = "select * from ciudad";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlCommand comando2 = new MySqlCommand(sql2, conexionBD);
                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                MySqlDataAdapter da2 = new MySqlDataAdapter(comando2);
                da.Fill(dt);
                da2.Fill(dt2);
                categoryBox.DisplayMember = "descripcion";
                categoryBox.ValueMember = "category_id";
                categoryBox.DataSource = dt;
                cmbCiudad.DisplayMember = "nombre_ciudad";
                cmbCiudad.ValueMember = "id_ciudad";
                cmbCiudad.DataSource = dt2;
                dataGridView1.DataSource = llenarDataGrid();
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

        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            frmNuevaCategoria frmnewCategory = new frmNuevaCategoria();
            frmnewCategory.FormClosing += FrmnewCategory_FormClosing;
            frmnewCategory.Show();

        }

        private DataTable llenarDataGrid()
        {
            DataTable dt = new DataTable();
            string sql = "select * from producto";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje: " + ex.Message);

            }
            finally
            {
                conexionBD.Close();
            }
            return dt;

        }
        private void FrmnewCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            categoryBox.DataSource = null;
            categoryBox.Items.Clear();
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

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            //Almacenando nuevo producto
            string sql = "insert into producto (category_id, precio,  descripcion) values ('"
                + categoryBox.SelectedValue +
                "','" + Double.Parse(textPrecio.Text) + "','" +
                textDescripcion.Text + "')";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Producto Guardado");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);


            }
            finally
            {
                conexionBD.Close();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = llenarDataGrid();
            }
            //Limpiando textBox
            textDescripcion.Clear();
            textPrecio.Clear();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmUpdateProducto frmactualizar = new frmUpdateProducto();
            frmactualizar.FormClosing += Frmactualizar_FormClosing;

            frmactualizar.id_producto = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            frmactualizar.id_categoria = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            frmactualizar.precio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            frmactualizar.descripcion = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
            frmactualizar.Show();
        }

        private void Frmactualizar_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = llenarDataGrid();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            dataGridView2.DataSource = llenarDataGridUsers();
            dataGridView3.DataSource = llenarDataGridClientes();
        }

        private DataTable llenarDataGridUsers()
        {
            DataTable dt = new DataTable();
            string sql = "select * from usuarios";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje: " + ex.Message);

            }
            finally
            {
                conexionBD.Close();
            }
            return dt;

        }

        private DataTable llenarDataGridClientes()
        {
            DataTable dt = new DataTable();
            string sql = "select * from cliente";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje: " + ex.Message);

            }
            finally
            {
                conexionBD.Close();
            }
            return dt;

        }

        private void brnAgregarUsuario_Click(object sender, EventArgs e)
        {
            //Almacenando nuevo producto
            string sql = "insert into usuarios (nombre_usuario, password) values ('"
                + txtUserName.Text +
                "',MD5('" + txtPassword.Text + "'))";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Usuario Guardado");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);


            }
            finally
            {
                conexionBD.Close();
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = llenarDataGridUsers();
            }
            //Limpiando textBox
            txtUserName.Clear();
            txtPassword.Clear();
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            frmEditarUsuario frmactualizar = new frmEditarUsuario();
            frmactualizar.FormClosing += FrmactualizarUsuario_FormClosing;

            frmactualizar.id_usuario = dataGridView2.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            frmactualizar.nombre_usuario = dataGridView2.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            frmactualizar.password = dataGridView2.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

            frmactualizar.Show();
        }

        private void FrmactualizarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = llenarDataGridUsers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmNuevaCiudad frmNewCity = new frmNuevaCiudad();
            frmNewCity.FormClosing += frmNewCity_FormClosing;
            frmNewCity.Show();
        }

        private void frmNewCity_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmbCiudad.DataSource = null;
            DataTable dt = new DataTable();
            string sql = "select * from ciudad";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                cmbCiudad.DisplayMember = "nombre_ciudad";
                cmbCiudad.ValueMember = "id_ciudad";
                cmbCiudad.DataSource = dt;

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


        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            //Almacenando nuevo cliente
            string sql = "insert into cliente (nit_cliente, nombre_completo, Direccion, telefono,fecha_nacimiento,id_ciudad) values ('"
                + txtNit.Text +
                "','" + txtNombre.Text +
                "','" + txtDireccion.Text +
                "','" + txtTelefono.Text +
                "','" + dtFechaNacimiento.Value.ToString("yyyy-MM-dd") +
                "','" + cmbCiudad.SelectedValue +
                "')";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Cliente Guardado");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);


            }
            finally
            {
                conexionBD.Close();

            }
            //Limpiando textBox

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3)
            {
                //cargando datos de clientes y Productos
                cmbClientes.DataSource = null;
                cmbProductos.DataSource = null;
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                string sql = "select * from cliente";
                string sql2 = "select * from producto";


                MySqlConnection conexionBD = Conexion.conexion();
                conexionBD.Open();
                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                    MySqlCommand comando2 = new MySqlCommand(sql2, conexionBD);

                    MySqlDataAdapter da = new MySqlDataAdapter(comando);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(comando2);
                    da.Fill(dt);
                    da2.Fill(dt2);
                    cmbClientes.DisplayMember = "nombre_completo";
                    cmbClientes.ValueMember = "id_cliente";
                    cmbClientes.DataSource = dt;

                    cmbProductos.DisplayMember = "descripcion";
                    cmbProductos.ValueMember = "idproducto";
                    cmbProductos.DataSource = dt2;

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
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            DataGridViewRow file = new DataGridViewRow();
            float subtotal = float.Parse(labelPrecioProducto.Text) * float.Parse(txtCantidad.Text);
            
            file.CreateCells(datagridPedidos);
            file.Cells[0].Value = txtCantidad.Text;
            file.Cells[1].Value = cmbProductos.SelectedValue;
            file.Cells[2].Value = subtotal.ToString();
            datagridPedidos.Rows.Add(file);
            obtenerTotal();

        }

        public void obtenerTotal()
        {
            float total = 0;
            float contador = datagridPedidos.RowCount-1;

            for(int i=0; i<contador;i++)
            {
                total += float.Parse(datagridPedidos.Rows[i].Cells[2].Value.ToString());
            }
            labelTotal.Text="Total: " + total.ToString(); 
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {



            string sql = "select precio from producto where idproducto=" + cmbProductos.SelectedValue;

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlDataReader reader = comando.ExecuteReader();
                reader.Read();
                labelPrecioProducto.Text = reader["precio"].ToString();




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

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            int codigo_cliente;            
            int id_pedido;
            DateTime fecha = DateTime.Now;
            string estado = "EN PROCESO";

            float total = 0;
            float contador = datagridPedidos.RowCount - 1;

            for (int i = 0; i < contador; i++)
            {
                total += float.Parse(datagridPedidos.Rows[i].Cells[2].Value.ToString());
            }
            

            //Obteniendo datos a enviar a BD
            codigo_cliente = int.Parse( cmbClientes.SelectedValue.ToString());
            //Enviando a base;

            //Almacenando nuevo pedido
            string sql = "insert into pedidos(id_cliente, fecha, estado, monto_total) values ('"
                + codigo_cliente +
                "','" + fecha.ToString("yyyy-MM-dd hh:mm:ss") +
                "','" + estado+
                "','" + total +
                "')";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {

                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();

                string query = "select id_pedido from pedidos order by id_pedido DESC LIMIT 1";
                MySqlCommand comando2 = new MySqlCommand(query, conexionBD);
                MySqlDataReader reader = comando2.ExecuteReader();
                reader.Read();
                id_pedido = int.Parse( reader["id_pedido"].ToString());
                //LLAMANDO A FUNCION que envie los detalles de pedido;
                ingresarDetallePedido(id_pedido);
                MessageBox.Show("Pedido Guardado Exitosamente");

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

        public void ingresarDetallePedido(int id_pedido)
        {
            int contador = datagridPedidos.RowCount -1;
            
            for (int i = 0; i < contador; i++)
            {
                //Almacenando detalle de pedido
                int precio = int.Parse( datagridPedidos.Rows[i].Cells[2].Value.ToString());
                int cantidad = int.Parse( datagridPedidos.Rows[i].Cells[0].Value.ToString());
                int id_producto = int.Parse( datagridPedidos.Rows[i].Cells[1].Value.ToString());
                string sql = "insert into detalle_pedido (id_producto, id_pedido, cantidad, precio) values ('"
                    + id_producto +"', '"+
                    +id_pedido + "', '" +
                    +cantidad + "', '" +
                    +precio+
                    "')";
                MySqlConnection conexionBD = Conexion.conexion();
                conexionBD.Open();
                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Detalle Guardado");

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);


                }
                finally
                { conexionBD.Close(); }
            }

        }
    }
}
