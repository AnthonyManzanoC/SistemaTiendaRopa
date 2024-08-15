

namespace SistemaTiendaRopa.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using SistemaTiendaRopa.Controllers;
    using SistemaTiendaRopa.Models;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;



    public partial class fmrClientes : Form
    {
        ClientesController clientesController = new ClientesController();
        public string ClienteId = null;
        public fmrClientes()
        {
            InitializeComponent();
        }

        private void fmrClientes_Load(object sender, EventArgs e)
        {
            CargarLista();
        }
        private void CargarLista()
        {
            //lstProductos.Items.Clear();
            lstClientes.DataSource = clientesController.Todos();
            lstClientes.DisplayMember = "Nombre";
            lstClientes.ValueMember = "ClienteId";
        }

        private void lstClientes_DoubleClick(object sender, EventArgs e)
        {
            //ClienteId = lstClientes.SelectedValue.ToString();
            ClientesModels cliente = (ClientesModels)lstClientes.SelectedItem;
            if (cliente!=null)
            { 
           


            txt_Nombre.Text = cliente.Nombre;
            txt_Apellido.Text = cliente.Apellido;
            txt_Email.Text = cliente.Email;
            txt_Telefono.Text = cliente.Telefono;
        
            }
        }

        private void btn_Grabar_Click(object sender, EventArgs e)
        {
            string respuesta = "";
            ClientesModels cliente = new ClientesModels
            {
                ClienteId = string.IsNullOrEmpty(ClienteId) ? 0 : Convert.ToInt32(ClienteId),
                Nombre = txt_Nombre.Text,
                Apellido = txt_Apellido.Text,
                Email = txt_Email.Text,
                Telefono = txt_Telefono.Text
            };

            if (cliente.ClienteId > 0)
            {
                respuesta = clientesController.Editar(cliente);
            }
            else
            {
                respuesta = clientesController.Insertar(cliente);
            }

            if (respuesta == "ok")
            {
                ClienteId = null;
                txt_Nombre.Enabled = false;
                txt_Apellido.Enabled = false;
                txt_Email.Enabled = false;
                txt_Telefono.Enabled = false;
                txt_Nombre.Text = "";
                txt_Apellido.Text = "";
                txt_Email.Text = "";
                txt_Telefono.Text = "";

                CargarLista();
                MessageBox.Show("Se guardó con éxito");
            }
            else
            {
                MessageBox.Show("Error al guardar: " + respuesta);
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (lstClientes.SelectedItem != null)
            {
                ClienteId = lstClientes.SelectedValue.ToString();
                ClientesModels cliente = clientesController.Todos().Find(c => c.ClienteId == Convert.ToInt32(ClienteId));
                txt_Nombre.Enabled = true;
                txt_Apellido.Enabled = true;
                txt_Email.Enabled = true;
                txt_Telefono.Enabled = true;
            }
            else
            {
                MessageBox.Show("Seleccione un cliente de la lista");
            }
        }

          
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (lstClientes.SelectedItem != null)
            {
                int ClienteId = Convert.ToInt32(lstClientes.SelectedValue);
                string mensaje = clientesController.Eliminar(ClienteId);
                MessageBox.Show("El Cliente se eliminó con exito");
                LimpiarCampos();
                CargarLista(); 
            }
            else
            {
                MessageBox.Show("Seleccione un cliente de la lista");
            }
        }



        private void LimpiarCampos()
        {
            txt_Nombre.Clear();
            txt_Apellido.Clear();
            txt_Email.Clear();
            txt_Telefono.Clear();
            ClienteId = null;
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
