using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_WebForm_Equipo_20
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Literal carritoCantidad = Master.FindControl("carritoCantidad") as Literal;
                int cantidadInicial = Convert.ToInt32(Session["CantidadCarrito"] ?? "0");
                carritoCantidad.Text = cantidadInicial.ToString();
            }
        }
    }
}