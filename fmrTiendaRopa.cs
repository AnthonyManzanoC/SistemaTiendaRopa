using SistemaTiendaRopa.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTiendaRopa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           fmrProductos fmrProductos = new fmrProductos();  
            fmrProductos . Show();  
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmrClientes fmrClientes = new fmrClientes();
            fmrClientes.Show();
        }
    }
}
