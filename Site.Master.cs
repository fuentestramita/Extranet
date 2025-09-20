using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Extranet
{
	public partial class Site : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["NombreCliente"] == null)
			{
				Response.Redirect("sign-in.aspx");
				Response.End();
			}
			if (!Page.IsPostBack)
			{
				lblNombreCliente.Text = (string)Session["NombreCliente"];
				lblNombreUsuario.Text = (string)Session["NombreUsuario"];
				lblNombrePerfil.Text = (string)Session["NombrePerfil"];
				string nombrePagina = System.IO.Path.GetFileName(Request.Path);

				// Aqui cambio el menu Activo segun el nombre de pagina
				if (nombrePagina.ToUpper().IndexOf("HOME.ASPX")>=0)
					liMenuHome.Attributes["class"] += " has-active";

				if (nombrePagina.ToUpper().IndexOf("ACTUALIZA-BD.ASPX") >= 0)
					liMenuActualizaBD.Attributes["class"] += " has-active";

				if (nombrePagina.ToUpper().IndexOf("CONSULTA-COMERCIAL.ASPX") >= 0)
					liMenuConsultaComercial.Attributes["class"] += " has-active";

			}
		}
	}
}