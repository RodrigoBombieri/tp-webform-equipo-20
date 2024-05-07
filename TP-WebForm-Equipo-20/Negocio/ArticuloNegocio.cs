using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();

            try
            {
                datos.setearConsulta("select A.ID, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.ID as IDMarca, M.Descripcion as Marca, C.ID as IDCategoria, C.Descripcion as Categoria from ARTICULOS as A inner join MARCAS as M on A.IDMarca = M.ID inner join CATEGORIAS as C on A.IDCategoria = C.ID");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    if (!(datos.Lector["ID"] is DBNull))
                        aux.ID = (int)datos.Lector["ID"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];


                    /*aux.UrlImagen = new Imagen();
                    aux.UrlImagen.ID = (int)datos.Lector["ID"];
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];*/

                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.ID = (int)datos.Lector["IDMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.ID = (int)datos.Lector["IDCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
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


        public List<Articulo> listarConImagenes()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();
            List<Imagen> imagenes = new List<Imagen>();
            ImagenNegocio negocioImagen = new ImagenNegocio();
            try
            {
                datos.setearConsulta("select A.ID, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.ID as IDMarca, M.Descripcion as Marca, C.ID as IDCategoria, C.Descripcion as Categoria from ARTICULOS as A inner join MARCAS as M on A.IDMarca = M.ID inner join CATEGORIAS as C on A.IDCategoria = C.ID");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    if (!(datos.Lector["ID"] is DBNull))
                        aux.ID = (int)datos.Lector["ID"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];                 
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.ID = (int)datos.Lector["IDMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.ID = (int)datos.Lector["IDCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];                 

                    aux.Imagenes = negocioImagen.listar(aux.ID);

                    lista.Add(aux);
                }

                return lista;
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

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            int id = -1;
            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)values( @Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.ID);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.ID);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.ejecutarAccion();              
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
        public int buscarUltimo()
        {
            AccesoDatos datos = new AccesoDatos();
            int id=-1;
            try
            {

                datos.setearConsulta("SELECT TOP 1 * FROM ARTICULOS ORDER BY ID DESC");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    //Articulo aux = new Articulo();
                    id = (int)datos.Lector["ID"];
                }
                return id;
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
       
        public void modificar(Articulo aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @nombre, Descripcion = @descripcion, Precio=@Precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria Where Id = @id");
                datos.setearParametro("@Codigo", aux.Codigo);
                datos.setearParametro("@nombre", aux.Nombre);
                datos.setearParametro("@descripcion", aux.Descripcion);
               // datos.setearParametro("@UrlImagen", aux.UrlImagen);
                datos.setearParametro("@Precio", aux.Precio);
                datos.setearParametro("@IdMarca", aux.Marca.ID);
                datos.setearParametro("@IdCategoria", aux.Categoria.ID);
                datos.setearParametro("@id", aux.ID);

                datos.ejecutarAccion();
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


        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();


            try
            {
                string consulta = "select A.ID, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.ID as IDMarca, M.Descripcion as Marca, C.ID as IDCategoria, C.Descripcion as Categoria, I.ImagenUrl from ARTICULOS as A inner join MARCAS as M on A.IDMarca = M.ID inner join CATEGORIAS as C on A.IDCategoria = C.ID inner join Imagenes i on a.IdCategoria = c.Id and a.IdMarca = m.Id and a.Id = i.IdArticulo and ";

                if (campo == "Precio")

                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "A.Precio < " + filtro;
                            break;
                        default:
                            consulta += "A.Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")

                {
                    switch (criterio)
                    {
                        case "Comienza con":

                            consulta += "A.Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "A.Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Nombre like '%" + filtro + "%'";

                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":

                            consulta += "A.Descripcion like '" + filtro + "%'";

                            break;
                        case "Termina con":
                            consulta += "A.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    if (!(datos.Lector["ID"] is DBNull))
                        aux.ID = (int)datos.Lector["ID"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Imagenes = new List<Imagen>();
                    Imagen nuevaImagen = new Imagen();
                    nuevaImagen.ID = (int)datos.Lector["ID"];
                    nuevaImagen.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Imagenes.Add(nuevaImagen);

                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.ID = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.ID = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
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
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
