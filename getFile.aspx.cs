using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Extranet
{
	public partial class getFile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			byte[] bytes = Convert.FromBase64String(Request["eco"].ToString());

			// Paso 2: Convertir los bytes a una cadena usando UTF-8
			string parametros = Encoding.UTF8.GetString(bytes);

			string jsonPDFs = Utilities.cipher.DecryptString(parametros);
			ConsultaArchivoRequest Archivo = new ConsultaArchivoRequest();
			if (!string.IsNullOrWhiteSpace(jsonPDFs))
			{
				try
				{
					string apiUrl = ConfigurationManager.AppSettings["ApiFile"].ToString() + "getConsultaComercial"; // Reemplaza con la URL real de tu API
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
			if (Archivo.Existe)
			{
				byte[] bytesPDF = Convert.FromBase64String(Archivo.ContenidoBase64);
				Response.ContentType = "application/pdf";

				Response.BinaryWrite(bytesPDF);
			//Response.AddHeader("Content-Disposition", "attachment;filename=MiArchivo.pdf");
				Response.End();
			}
		}
	}
}