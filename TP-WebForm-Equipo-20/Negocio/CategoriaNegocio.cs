using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio; // agrego la capa dominio

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar() // cargo la lista con las categorias de la base de datos
        {
            AccesoDatos datos = new AccesoDatos();
            List<Categoria> lista = new List<Categoria>();

            try
            {
                datos.setearConsulta("select ID, Descripcion from CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read()) // mientras haya registros en la BD cargo aux y agrego a la lista, muevo el puntero
                {
                    Categoria aux = new Categoria(); //aux para ir guardando los registros
                    aux.ID = (int)datos.Lector["ID"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux); // agrego el registro aux a la lista
                }

                return lista; // cuando no quedan mas registros, el puntero se posiciona al final y sale del while
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion(); // cierro la conexion a la BD
            }
        }


        public void eliminar(int id)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE ID = @ID"); // le pasamos la query DELETE
                datos.setearParametro("@ID", id); // seteamos el parametro 
                datos.ejecutarAccion(); // finalmente ejecutamos la accion y va a eliminar el registro que tenga el id que le pasamos
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
