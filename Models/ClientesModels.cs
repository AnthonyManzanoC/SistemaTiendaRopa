
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

    class ClientesModels
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        private ConexionBDD conexionBDD = new ConexionBDD();
        SqlCommand cmd = new SqlCommand();

        public List<ClientesModels> Todos()
        {
            return Todos(ClienteId);
        }

        public List<ClientesModels> Todos(int clienteId)
        {
            List<ClientesModels> listaClientes = new List<ClientesModels>();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Clientes", conexionBDD.AbrirConexion());
            DataTable data = new DataTable();
            adapter.Fill(data);
            foreach (DataRow fila in data.Rows)
            {
                listaClientes.Add(new ClientesModels
                {
                    ClienteId = Convert.ToInt32(fila["ClienteId"]),
                    Nombre = fila["Nombre"].ToString(),
                    Apellido = fila["Apellido"].ToString(),
                    Email = fila["Email"].ToString(),
                    Telefono = fila["Telefono"].ToString()
                });
            }
            conexionBDD.CerrarConexion();
            return listaClientes;
        }

        public string Insertar(ClientesModels cliente)
        {
            try
            {
                cmd.Connection = conexionBDD.AbrirConexion();
                cmd.CommandText = $"insert into Clientes (Nombre, Apellido, Email, Telefono) values ('{cliente.Nombre}', '{cliente.Apellido}', '{cliente.Email}', '{cliente.Telefono}')";
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

        public string Editar(ClientesModels cliente)
        {
            try
            {
                cmd.Connection = conexionBDD.AbrirConexion();
                cmd.CommandText = $"update Clientes set Nombre='{cliente.Nombre}', Apellido='{cliente.Apellido}', Email='{cliente.Email}', Telefono='{cliente.Telefono}' where ClienteId={cliente.ClienteId}";
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

        public string Eliminar(int clienteId)
        {
            try
            {
                cmd.Connection = conexionBDD.AbrirConexion();
                cmd.CommandText = $"delete from Clientes where ClienteId={clienteId}";
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
