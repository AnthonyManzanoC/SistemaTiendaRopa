
namespace SistemaTiendaRopa.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.Data.SqlClient;
    using SistemaTiendaRopa.Config;


    class ProductosModels
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }

        private ConexionBDD conexionBDD = new ConexionBDD();

        SqlCommand cmd = new SqlCommand();

        public List<ProductosModels> Todos()
        {
            List<ProductosModels> listaProductos = new List<ProductosModels>();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Productos", conexionBDD.AbrirConexion());
            DataTable data = new DataTable();
            adapter.Fill(data);
            foreach (DataRow fila in data.Rows)
            {
                listaProductos.Add(new ProductosModels
                {
                    ProductoId = Convert.ToInt32(fila["ProductoId"]),
                    Nombre = fila["Nombre"].ToString(),
                    Talla = fila["Talla"].ToString(),
                    Color = fila["Color"].ToString(),
                    Precio = Convert.ToDecimal(fila["Precio"])
                });
            }
            conexionBDD.CerrarConexion();
            return listaProductos;
        }

       

        public string Insertar(ProductosModels producto)
        {
            try
            {
                cmd.Connection = conexionBDD.AbrirConexion();
                cmd.CommandText = $"insert into Productos (Nombre, Talla, Color, Precio) values('{producto.Nombre}', '{producto.Talla}', '{producto.Color}', {producto.Precio.ToString().Replace(',', '.')})";
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception e)
            {
                return "No Entra " + e.Message;
            }
            finally
            {
                conexionBDD.CerrarConexion();
            }
        }



      

        public string Editar(ProductosModels producto)
        {
            try
            {
                cmd.Connection = conexionBDD.AbrirConexion();
                cmd.CommandText = $"update Productos set Nombre='{producto.Nombre}', Talla='{producto.Talla}', Color='{producto.Color}', Precio={producto.Precio.ToString().Replace(',', '.')} where ProductoId={producto.ProductoId}";
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                conexionBDD.CerrarConexion();
            }
        }


        public string Eliminar(int productoId)
        {
            try
            {
                cmd.Connection = conexionBDD.AbrirConexion();
                cmd.CommandText = $"delete from Productos where ProductoId={productoId}";
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                conexionBDD.CerrarConexion();
            }
        }
    }

}
