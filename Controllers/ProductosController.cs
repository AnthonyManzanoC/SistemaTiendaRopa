

namespace SistemaTiendaRopa.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SistemaTiendaRopa.Models;
    class ProductosController
    {
        private ProductosModels productosModels = new ProductosModels();

        public List<ProductosModels> Todos()
        {
            return productosModels.Todos();
        }

        public string Insertar(ProductosModels producto)
        {
            return productosModels.Insertar(producto);
        }

        public string Editar(ProductosModels producto)
        {
            return productosModels.Editar(producto);
        }

        public string Eliminar(int productoId)
        {
            return productosModels.Eliminar(productoId);
        }
    }
}
