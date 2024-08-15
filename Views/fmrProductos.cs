

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


    public partial class fmrProductos : Form
    {
        ProductosController productosController = new ProductosController();
        public string ProductoId = null;
        public fmrProductos()
        {
            InitializeComponent();
        }

        private void lstProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        public void cargaLista()
        {
            //lstProductos.Items.Clear();
            lstProductos.DataSource = productosController.Todos();
            lstProductos.DisplayMember = "Nombre";
            lstProductos.ValueMember = "ProductoId";
        }

        private void lstProductos_DoubleClick(object sender, EventArgs e)
        {
           if (lstProductos.SelectedItem != null)
            {
               
                var productoSeleccionado = lstProductos.SelectedItem as ProductosModels;
                if (productoSeleccionado != null)
                {
                   
                    txt_Nombre.Text = productoSeleccionado.Nombre;
                    txt_Talla.Text = productoSeleccionado.Talla;
                    txt_Color.Text = productoSeleccionado.Color;
                    txt_Precio.Text = productoSeleccionado.Precio.ToString();
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del producto. Por favor, intente nuevamente.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista.");
            }
        }
    

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (lstProductos.SelectedItem != null)
            {
                ProductoId = lstProductos.SelectedValue.ToString();
                ProductosModels producto = productosController.Todos().Find(p => p.ProductoId == Convert.ToInt32(ProductoId));
                txt_Nombre.Enabled = true;
                txt_Talla.Enabled = true;
                txt_Color.Enabled = true;
                txt_Precio.Enabled = true;
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista");
            }
        }

       
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (lstProductos.SelectedItem != null)
            {
                ProductoId = lstProductos.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(ProductoId))
                {
                    string mensaje = productosController.Eliminar(Convert.ToInt32(ProductoId));
                    MessageBox.Show("El Producto se eliminó con exito");
                    limpiaCampos();
                    cargaLista(); 
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID del producto. Por favor, intente nuevamente.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista.");
            }
        }


        private void limpiaCampos()
        {
            txt_Nombre.Clear();
            txt_Talla.Clear();
            txt_Color.Clear();
            txt_Precio.Clear();
            ProductoId = null;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            limpiaCampos();
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmrProductos_Load(object sender, EventArgs e)
        {
            cargaLista();
        }

        private void btn_Grabar_Click(object sender, EventArgs e)
        {
            string respuesta = "";
            ProductosModels producto = new ProductosModels
            {
                ProductoId = string.IsNullOrEmpty(ProductoId) ? 0 : Convert.ToInt32(ProductoId),
                Nombre = txt_Nombre.Text,
                Talla = txt_Talla.Text,
                Color = txt_Color.Text,
                Precio = decimal.Parse(txt_Precio.Text)
            };
            if (producto.ProductoId > 0)
            {
                respuesta = productosController.Editar(producto);
            }
            else
            {
                respuesta = productosController.Insertar(producto);
            }

            if (respuesta == "ok")
            {
                ProductoId = null;
                txt_Nombre.Enabled = false;
                txt_Talla.Enabled = false;
                txt_Color.Enabled = false;
                txt_Precio.Enabled = false;
                txt_Nombre.Text = "";
                txt_Talla.Text = "";
                txt_Color.Text = "";
                txt_Precio.Text = "";

                cargaLista();
                MessageBox.Show("Se guardó con éxito");
            }
            else
            {
                MessageBox.Show("Error al guardar: " + respuesta);
            }
        }
    
    }
}
        
  

