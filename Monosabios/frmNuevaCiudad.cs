using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Monosabios
{
    public partial class frmNuevaCiudad : Form
    {
        List<string> nombreciudad= new List<string>();
        List<string> pais = new List<string>();
        List<string> region = new List<string>();
        public frmNuevaCiudad()
        {
            InitializeComponent();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {

            cmbCiudades.Items.Clear();
            nombreciudad.Clear();
            pais.Clear();
            region.Clear();
            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var client = new HttpClient();
            var request = new HttpRequestMessage

            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://wft-geo-db.p.rapidapi.com/v1/geo/cities?namePrefix=" + txtCiudad.Text),
                Headers =
    {
        { "X-RapidAPI-Key", "0cc49e505fmsh774725bc463dbd3p132b6djsnc77ddbd16043" },
        { "X-RapidAPI-Host", "wft-geo-db.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    dynamic resp = new JavaScriptSerializer().DeserializeObject(body);

                    var result = resp["data"];
                    foreach (var item in result)
                    {
                        cmbCiudades.DisplayMember = item["name"];
                        cmbCiudades.ValueMember = Convert.ToString( item["id"]);
                        cmbCiudades.Items.Add(item["name"]);
                        nombreciudad.Add(item["name"]);
                        pais.Add(item["country"]);
                        region.Add(item["region"]);
                    }
                    


                }
            }

         txtCiudad.Text = "";
        }

        private void cmbCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelNombreCiudad.Text = "Nombre Ciudad: " + nombreciudad[cmbCiudades.SelectedIndex];
            labelPais.Text = "Pais: " + pais[cmbCiudades.SelectedIndex];
            labelRegion.Text = "Region: " + region[cmbCiudades.SelectedIndex];
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //Almacenando nuevo producto
            string sql = "insert into ciudad (nombre_ciudad, pais,region) values ('"
                + nombreciudad[cmbCiudades.SelectedIndex] +
                "','"+pais[cmbCiudades.SelectedIndex] +"','" +
                region[cmbCiudades.SelectedIndex] + "')";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Ciudad Guardada correctamente");

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
