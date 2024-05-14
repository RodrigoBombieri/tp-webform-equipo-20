using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_WebForm_Equipo_20
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = "-1";
            Articulo articulo = new Articulo();
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            if (!IsPostBack)
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                try
                {
                    ddlMarca.DataSource = marcaNegocio.listar();
                    ddlMarca.DataBind();
                    ddlCategoria.DataSource = categoriaNegocio.listar();
                    ddlCategoria.DataBind();

                    if (Request.QueryString["id"] != null)
                    {
                        id = Request.QueryString["id"].ToString();
                        articulo = articuloNegocio.buscar(id);
                        if (articulo.ID != -1)
                        {
                            txtId.Text = articulo.ID.ToString();
                            txtNombre.Text = articulo.Nombre.ToString();
                            txtDescripcion.Text = articulo.Descripcion.ToString();
                            txtCodigo.Text = articulo.Codigo.ToString();
                            ddlMarca.SelectedIndex = articulo.Marca.ID;
                            ddlCategoria.SelectedIndex = articulo.Categoria.ID;
                            txtPrecio.Text = articulo.Precio.ToString();
                        }
                        else
                        {
                            //articulo no encontrado;

                        }
                    }
                    else
                    {
                        //articulo no encontrado;
                    }                    
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    //MessageBox.Show("Error al cargar ventana.");
                }
                
            }
            
            //ddlMarca.Items.Add("Marca1");
            //ddlMarca.Items.Add("Marca2");
        }
    }
}