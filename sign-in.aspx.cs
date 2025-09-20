using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using System.Data;


namespace Extranet
{
	public partial class sing_in : System.Web.UI.Page
	{
		public Models.Login login = new Models.Login();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				Session["UserName"] = "";
				Session["RUTCliente"] = "";
				Session["UsuarioID"] = "";
				Session["NombreUsuario"] = "";
				Session["Icono"] = "";
				Session["TipoUsuario"] = "";
			}
		}

		protected void btnIngresar_Click(object sender, EventArgs e)
		{
			if (txtUser.Text != "" && txtPassword.Text != "")
			{


				//	var url = ConfigurationManager.AppSettings["ApiLogin"].ToString() + "GetLogin";

				//	HttpContext.Current.Server.ScriptTimeout = 6200;

				//	var request = (HttpWebRequest)WebRequest.Create(url);
				//	request.ServicePoint.ConnectionLimit = 10;    // The default value of 2 within a ConnectionGroup caused me always a "Timeout exception" because a user's 1-3 concurrent WebRequests within a second.
				//	request.ServicePoint.MaxIdleTime = 5 * 10000;  // (5 sec) default was 100000 (100 sec).  Max idle time for a connection within a ConnectionGroup for reuse before closing
				//	string json = "{'UserName':'" + txtUser.Text.Replace(".", "") + "', 'Password':'" + txtPassword.Text + "'}";

				//	request.Method = "POST";
				//	request.ContentType = "application/json";
				//	request.Accept = "application/json";

				//	using (var streamWriter = new StreamWriter(request.GetRequestStream()))
				//	{
				//		streamWriter.Write(json);
				//		streamWriter.Flush();
				//		streamWriter.Close();
				//	}

				//	try
				//	{
				//		using (WebResponse response = request.GetResponse())
				//		{
				//			using (Stream strReader = response.GetResponseStream())
				//			{
				//				if (strReader == null) return;
				//				using (StreamReader objReader = new StreamReader(strReader))
				//				{
				//					string respuesta = objReader.ReadToEnd();
				//					// Do something with responseBody
				//					login = JsonConvert.DeserializeObject<Models.Login>(respuesta);
				//					if (login.UserID == 0)
				//					{
				//						ScriptManager.RegisterStartupScript(this, GetType(), "validacion", "function onLoad(){alert(\"Usuario o Clave Incorrectos\")}", true);
				//					}
				//					else
				//					{
				//						Session["Login"] = login;
				//						marcoLogin.Visible = false;
				//						marcoValidacion.Visible = true;


				//					}
				//				}
				//			}
				//		}
				//	}
				//	catch
				//	{
				//		// Handle error
				//	}

				// Login Default - Descomentar arriba y sacar esto
				Session["UserName"] = "10080671-1";
				Session["RUTCliente"] = "97036000-K";
				Session["UsuarioID"] = "12";
				Session["NombreUsuario"] = "SOLEDAD SUAREZ";
				Session["Icono"] = "./images/logoSantander.jpg";
				Session["TipoUsuario"] = "1";
				Session["MenuEspecial"] = 0;
				Session["ClienteID"] = "2";
				Session["NombreCliente"] = "BANCO SANTANDER";
				Session["NombrePerfil"] = "ADMINISTRADOR";
				Response.Redirect("./home.aspx");
				Response.End();

				// HASTA AQUI lOGIN

			}
		}


		protected void btnValidarAcceso_Click(object sender, EventArgs e)
		{

			var url = ConfigurationManager.AppSettings["ApiLogin"].ToString() + "ValidaCode";

			HttpContext.Current.Server.ScriptTimeout = 6200;

			var request = (HttpWebRequest)WebRequest.Create(url);
			request.ServicePoint.ConnectionLimit = 10;    // The default value of 2 within a ConnectionGroup caused me always a "Timeout exception" because a user's 1-3 concurrent WebRequests within a second.
			request.ServicePoint.MaxIdleTime = 5 * 10000;  // (5 sec) default was 100000 (100 sec).  Max idle time for a connection within a ConnectionGroup for reuse before closing

			request.Method = "POST";
			request.ContentType = "application/json";
			request.Accept = "application/json";


			login = (Models.Login)Session["Login"];
			login.LoginCode = txtAutorizacion.Text;

			string json = JsonConvert.SerializeObject(login);
			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				streamWriter.Write(json);
				streamWriter.Flush();
				streamWriter.Close();
			}

			try
			{
				using (WebResponse response = request.GetResponse())
				{
					using (Stream strReader = response.GetResponseStream())
					{
						if (strReader == null) return;
						using (StreamReader objReader = new StreamReader(strReader))
						{
							string respuesta = objReader.ReadToEnd();
							// Do something with responseBody
							ClientScriptManager cs = Page.ClientScript;
							if (respuesta == "NOK")
							{
								ScriptManager.RegisterStartupScript(this, GetType(), "validacion", "function onLoad(){alert(\"Código de Autorización Inválido\");}", true);
							}
							else
							{
								//login = JsonConvert.DeserializeObject<Models.Login>(respuesta);
								DataTable dtDatos = BusinessLayer.DBO.sp_LimboLogin(txtUser.Text.Replace(".", ""), txtPassword.Text);

								if (dtDatos.Rows.Count > 0)
								{
									Session["UserName"] = txtUser.Text.Replace(".", "");
									Session["RUTCliente"] = dtDatos.Rows[0]["RUTCliente"].ToString();  
									Session["UsuarioID"] = dtDatos.Rows[0]["UsuarioID"].ToString();
									Session["NombreUsuario"] = dtDatos.Rows[0]["Nombre"].ToString();
									Session["Icono"] = dtDatos.Rows[0]["Icono"].ToString();
									Session["TipoUsuario"] = dtDatos.Rows[0]["TipoUsuario"].ToString();
									Session["MenuEspecial"] = 0;
									Session["ClienteID"] = dtDatos.Rows[0]["ClienteID"].ToString();
									Session["NombreCliente"] = dtDatos.Rows[0]["Cliente"].ToString();
									Session["NombrePerfil"] = dtDatos.Rows[0]["NombrePerfil"].ToString();


								}
								Response.Redirect("./home.aspx");
								Response.End();
							}
						}
					}
				}
			}
			catch
			{
				ScriptManager.RegisterStartupScript(this, GetType(), "validacion", "function onLoad(){alert(\"Hubo un error en la validación\");}", true);
			}

		}

	}
}
namespace Models
{
	public class Login
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Nombre { get; set; }
		public string LoginCode { get; set; }
		public int ClienteID { get; set; }
		public string EMail { get; set; }
	}
}