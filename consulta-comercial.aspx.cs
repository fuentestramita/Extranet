using Extranet.admin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Extranet
{
	public partial class consulta_comercial : System.Web.UI.Page
	{
		private int intRow = 0;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				//muestraColumnas();
			}

		}

		protected void muestraColumnas()
		{

			//grvDatos.Columns.Clear();

			//grvDatos.Columns.Add(new BoundField { HeaderText = "#", DataField = "IntROW", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "ID", DataField = "MaestroID", Visible = false });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "PPU", DataField = "PPU", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "Nrooperacion", DataField = "Nrooperacion", Visible = true });
				//grvDatos.Columns.Add(new BoundField { HeaderText = "NroSolicitudRNVM", DataField = "NroSolicitudRNVM", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "TipoSolicitudID", DataField = "TipoSolicitudID", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "TipoSolicitud", DataField = "TipoSolicitud", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "Estado", DataField = "Estado", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "FechaRecepcionBanco", DataField = "FechaRecepcionBanco", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "FechaIngresoRNVM", DataField = "FechaIngresoRNVM", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "FechaEntrega", DataField = "FechaEntrega", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "Comentario", DataField = "Comentario", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "RutCliente", DataField = "RutCliente", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "RazonSocialCliente", DataField = "RazonSocialCliente", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "CodigoDespachoCorreoMaestroDocumentos", DataField = "CodigoDespachoCorreoMaestroDocumentos", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "MaestroDocumentoID", DataField = "MaestroDocumentoID", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "FechaEntrega", DataField = "FechaEntrega", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "NroValija", DataField = "NroValija", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "Sucursal", DataField = "Sucursal", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "Ejecutivo", DataField = "Ejecutivo", Visible = true });
			//grvDatos.Columns.Add(new BoundField { HeaderText = "EntregaValija", DataField = "EntregaValija", Visible = true });

		}

		protected void grvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			grvDatos.DataSource = Session["DatosGrilla"] as DataTable;
			grvDatos.PageIndex = e.NewPageIndex;
			grvDatos.DataBind();
			grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
			//muestraColumnas();
		}
		
		protected void grvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
				for (int cellIndex = 0; cellIndex < ((GridView)sender).Columns.Count; cellIndex++)
				{
					DataControlField column = ((GridView)sender).Columns[cellIndex];
					if (column.HeaderText.ToUpper() == "CHECKBOX")
					{
						CheckBox chkUpdate = new CheckBox();
						chkUpdate.ID = "ChkUpdateTodos";
						chkUpdate.Attributes.Add("onClick", "ToggleTodosUpdate(this);");
						e.Row.Cells[cellIndex].Controls.Add(chkUpdate);
						break;
					}
				}
			}
			
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			DataTable dtDatos = BusinessLayer.DBO.spListaConultaComercial((string)Session["RutCliente"], txtPPU.Text, txtNroOperacion.Text, txtRutCliente.Text);
			grvDatos.DataSource = dtDatos;
			grvDatos.DataBind();
			//grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
			//		muestraColumnas();
		}

		public string GetEstadoIcono(object estado)
		{
			if (estado == null) return string.Empty;

			string estadoStr = estado.ToString().ToUpper();

			if (estadoStr == "ACEPTADO" || estadoStr == "1" || estadoStr == "ENTREGADO")
			{
				return "<i class='far fa-check-circle Verde' title='ACEPTADO'></i>";
			}
			else if (estadoStr == "RECHAZADO")
			{
				return "<i class='fas fa-times Rojo' title='RECHAZADO'></i>";
			}
			else
			{
				return "<i class='far fa-circle Verde' title='INGRESADA/EN PROCESO'></i>";
			}
		}
		public string GetPDF(object PPU, object Operacion, object TipoSolicitudID, object TipoSolicitud, object CodigoDespachoCorreo, object CodigoDespachoCorreoMaestroDocumentos)
		{

			string apiUrl = ConfigurationManager.AppSettings["ApiFile"].ToString() + "getExisteArchivo"; // Reemplaza con la URL real de tu API
			string jsonPDFs = "{";

			if ((TipoSolicitudID.ToString() == "C" || TipoSolicitud.ToString() == "DELIVERY") && CodigoDespachoCorreo != null)
			{
				jsonPDFs += "\"NombreCliente\": \"" + Session["NombreCliente"] + "\",";
				jsonPDFs += "\"PPU\": null,";
				jsonPDFs += "\"TipoSolicitudID\": \"" + TipoSolicitudID.ToString() + "\",";
				jsonPDFs += "\"CodigoDespachoCorreo\": \"" + CodigoDespachoCorreo.ToString() + "\",";
			}
			else if ((TipoSolicitudID.ToString() == "C" || TipoSolicitud.ToString() == "DELIVERY") && CodigoDespachoCorreoMaestroDocumentos != null)
			{
				jsonPDFs += "\"NombreCliente\": \"" + Session["NombreCliente"] + "\",";
				jsonPDFs += "\"PPU\": null,";
				jsonPDFs += "\"Operacion\":  null,";
				jsonPDFs += "\"TipoSolicitudID\": \"" + TipoSolicitudID.ToString() + "\",";
				jsonPDFs += "\"CodigoDespachoCorreoMaestroDocumentos\": \"" + CodigoDespachoCorreoMaestroDocumentos.ToString() + "\"";
			}
			else if (Operacion != null)
			{
				jsonPDFs += "\"NombreCliente\": \"" + Session["NombreCliente"] + "\",";
				jsonPDFs += "\"Operacion\": \"" + Operacion.ToString() + "\",";
				jsonPDFs += "\"TipoSolicitudID\": \"" + TipoSolicitudID.ToString() + "\",";
			}
			else if (PPU != null && TipoSolicitudID != null)
			{
				jsonPDFs += "\"NombreCliente\": \"" + Session["NombreCliente"] + "\",";
				jsonPDFs += "\"PPU\": \"" + PPU.ToString() + "\",";
				jsonPDFs += "\"TipoSolicitudID\": \"" + TipoSolicitudID.ToString() + "\",";
			}

			jsonPDFs += "}";
			ConsultaArchivoRequest Archivo = new ConsultaArchivoRequest();
			if (!string.IsNullOrWhiteSpace(jsonPDFs))
			{
				try
				{
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
					request.Method = "POST";
					request.ContentType = "application/json";
					request.Accept = "application/json";

					byte[] byteData = Encoding.UTF8.GetBytes(jsonPDFs);
					request.ContentLength = byteData.Length;

					using (Stream dataStream = request.GetRequestStream())
					{
						dataStream.Write(byteData, 0, byteData.Length);
					}

					using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))

					{
						string result = reader.ReadToEnd();

						// Parsear la respuesta como JSON

						Archivo = JsonConvert.DeserializeObject<ConsultaArchivoRequest>(result);

					}
				}
				catch (WebException ex)
				{
					// Manejo de error
					using (var errorResponse = (HttpWebResponse)ex.Response)
					using (var reader = new StreamReader(errorResponse.GetResponseStream()))
					{
						string errorText = reader.ReadToEnd();
						// Log o mostrar error
					}
				}
			}
			string Retorno = "";
			if (Archivo.Existe)
			{
				string Eco = Utilities.cipher.EncryptString(jsonPDFs);

				byte[] bytes = Encoding.UTF8.GetBytes(Eco);

				// Paso 2: Codificar los bytes a Base64
				string base64String = Convert.ToBase64String(bytes);

				Retorno = "<a  href=\"#\" onCLick=\"window.open('\\getFile.aspx?eco=" + base64String + "','_pdf','toolbar=no, status=yes, location=no, resizable=yes,  menubar=no,');return false;\" target=\"_new\" runat=\"server\"><i class=\"far fa-file-pdf rojo\"></i></a></a>";

			}


			return Retorno;
		}

		public string getIntRow()
		{
			
			return intRow.ToString();
		}

		protected void grvDatos_DataBinding(object sender, EventArgs e)
		{
				intRow++;


		}
	}


	public class ConsultaArchivoRequest
	{
		public string NombreCliente { get; set; }
		public string PPU { get; set; }
		public string Operacion { get; set; }
		public string TipoSolicitudID { get; set; }
		public string TipoSolicitud { get; set; }
		public string CodigoDespachoCorreo { get; set; }
		public string CodigoDespachoCorreoMaestroDocumentos { get; set; }
		public string ContenidoBase64 { get; set; }
		public bool Existe { get; set; }
	}
}