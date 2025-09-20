using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Extranet.admin
{
	public partial class actualiza_bd : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnActualizaDatos_Click(object sender, EventArgs e)
		{

			DataTable dtDatos = BusinessLayer.DBO.SEL_MAXDIGITACION_CLOUD();
			string FechaDigitado = dtDatos.Rows[0]["FechaDigitado"].ToString();

			string apiUrl = "http://www.tramita.cl/fileapi/api/archivo/getDataMaestro"; // Reemplaza con la URL real de tu API
			string jsonFechas = "{"+String.Format("'FechaDigitacion':'{0}'", FechaDigitado)+"}";
			

			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
				request.Method = "POST";
				request.ContentType = "application/json";
				request.Accept = "application/json";

				byte[] byteData = Encoding.UTF8.GetBytes(jsonFechas);
				request.ContentLength = byteData.Length;

				using (Stream dataStream = request.GetRequestStream())
				{
					dataStream.Write(byteData, 0, byteData.Length);
				}

				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))

				{
					string result = reader.ReadToEnd();


					dtDatos=BusinessLayer.DBO.INS_MAESTRO_CLOUD_BULK(result);

					//dtDatos = ConvertJsonToDataTable(result);

					//BusinessLayer.DBO.BulkInsertDataTable(dtDatos, "Maestro");

					ScriptManager.RegisterStartupScript(this, GetType(), "validacion", "function onLoad(){alert(\"Carga Finalizada\");}", true);

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


		public DataTable ConvertJsonToDataTable(string jsonString)
		{
			DataTable dt = new DataTable();
			List<Maestro> maestros;
			try
			{
				if (jsonString.Trim().StartsWith("["))
				{
					// El JSON es una matriz, deserializa a una lista.
					maestros = JsonConvert.DeserializeObject<List<Maestro>>(jsonString);
					// ...
				}
				else if (jsonString.Trim().StartsWith("{"))
				{
					// El JSON es un objeto individual, deserializa a un solo objeto.
					Maestro maestroIndividual = JsonConvert.DeserializeObject<Maestro>(jsonString);
					maestros = new List<Maestro> { maestroIndividual };
					// ...
				}
				else
				{
					// Formato JSON desconocido.
					throw new Exception("Formato JSON no válido.");
				}


				dt = maestros.ToDataTable();
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"Error deserializing JSON: {ex.Message}");
			}

			return dt;
		}
	}

	public static class ListExtensions
	{
		public static DataTable ToDataTable<T>(this List<T> items)
		{
			DataTable dt = new DataTable(typeof(T).Name);

			// Obtiene las propiedades del tipo T para usarlas como columnas
			PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (var prop in props)
			{
				dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
			}

			// Llena el DataTable con los datos de cada objeto en la lista
			foreach (T item in items)
			{
				var values = new object[props.Length];
				for (int i = 0; i < props.Length; i++)
				{
					values[i] = props[i].GetValue(item, null);
				}
				dt.Rows.Add(values);
			}

			return dt;
		}
	}

	public class Maestro
	{
		public decimal MaestroID { get; set; }
		public decimal ClienteID { get; set; }
		public string PPU { get; set; }
		public string PPUDV { get; set; }
		public string NroOperacion { get; set; }
		public string TipoSolicitudID { get; set; }
		public decimal NroSolicitudRNVM { get; set; }
		public DateTime? FechaIngresoRNVM { get; set; }
		public decimal EstadoRNVMID { get; set; }
		public decimal NroFactura { get; set; }
		public DateTime? FechaRecepcionBanco { get; set; }
		public decimal ObservacionID { get; set; }
		public bool TieneInscripcionOriginal { get; set; }
		public bool TieneFacturaOriginal { get; set; }
		public bool EstaEntregado { get; set; }
		public bool HayqueEntregar { get; set; }
		public short NroPlacas { get; set; }
		public string LimitacionID { get; set; }
		public decimal TipoVehiculoID { get; set; }
		public decimal MarcaID { get; set; }
		public decimal ModeloID { get; set; }
		public short AnoFabricacion { get; set; }
		public string Color { get; set; }
		public string NroMotor { get; set; }
		public string NroChasis { get; set; }
		public short NroPuertas { get; set; }
		public short NroAsientos { get; set; }
		public decimal Carga { get; set; }
		public decimal UnidadCargaID { get; set; }
		public string NroSerieVIN { get; set; }
		public decimal CombustibleID { get; set; }
		public decimal PesoBruto { get; set; }
		public string UnidadPeso { get; set; }
		public string Traccion { get; set; }
		public string PotenciaMotor { get; set; }
		public decimal UnidadPotenciaID { get; set; }
		public string TipoTraccion { get; set; }
		public decimal CarroceriaID { get; set; }
		public string OtraCarroceria { get; set; }
		public string NroEjesDisponibles { get; set; }
		public decimal TipoDocumentoID { get; set; }
		public decimal NroDocumento { get; set; }
		public string NaturalezaAdquisicion { get; set; }
		public string LugarDocumento { get; set; }
		public DateTime? FechaDocumento { get; set; }
		public string EmisorDocumento { get; set; }
		public string RutEmisorDocumento { get; set; }
		public string Acreedor { get; set; }
		public double PrecioVenta { get; set; }
		public double DerechoMunicipal { get; set; }
		public decimal NotarioID { get; set; }
		public string RutPropietarioActual { get; set; }
		public string NombreRazonSocialPropietarioActual { get; set; }
		public string DireccionPropietarioActual { get; set; }
		public string NumeroDireccionPropietarioActual { get; set; }
		public string ComplementoDireccionPropietarioActual { get; set; }
		public string ComunaPropietarioActual { get; set; }
		public string CiudadPropietarioActual { get; set; }
		public string RutAdquirente { get; set; }
		public string NombreRazonSocialAdquirente { get; set; }
		public string DireccionAdquirente { get; set; }
		public string NumeroDireccionAdquirente { get; set; }
		public string ComplementoDireccionAdquirente { get; set; }
		public string ComunaAdquirente { get; set; }
		public string CiudadAdquirente { get; set; }
		public string ClaveCorreo { get; set; }
		public string URLPdf { get; set; }
		public bool IndicarAlerta { get; set; }
		public DateTime? FechaAlerta { get; set; }
		public DateTime? FechaDigitado { get; set; }
		public DateTime? FechaSolicitudRNVM { get; set; }
		public bool TieneCopias { get; set; }
		public bool TieneNroPlaca { get; set; }
		public string Oficina { get; set; }
		public string NroVIN { get; set; }
		public string NombresAdquirente { get; set; }
		public bool TienePDF { get; set; }
		public bool TieneListadoPI { get; set; }
		public string Comentario { get; set; }
		public bool TieneListadoTR { get; set; }
		public string Material { get; set; }
		public string RutMeroTenedor { get; set; }
		public string NombreRazonSocialMeroTenedor { get; set; }
		public string DireccionMeroTenedor { get; set; }
		public string NumeroDireccionMeroTenedor { get; set; }
		public string ComplementoMeroTenedor { get; set; }
		public string ComunaMeroTenedor { get; set; }
		public string CiudadMeroTenedor { get; set; }
		public string Causa { get; set; }
		public DateTime? FechaAnotacion { get; set; }
		public decimal NroRechazo { get; set; }
		public DateTime? FechaRechazo { get; set; }
		public string OficinaRechazo { get; set; }
		public string CausaRechazo { get; set; }
		public bool SolicitudBanco { get; set; }
		public string DocumentoSolicitados { get; set; }
		public DateTime? FechaSolicitudBanco { get; set; }
		public bool TieneListadoR { get; set; }
		public decimal TituloMeraTenenciaID { get; set; }
		public decimal CalidadMeraTenenciaID { get; set; }
		public string RutSolicitante { get; set; }
		public decimal NaturalezaDocumentoID { get; set; }
		public string Tribunal { get; set; }
		public short AnoCausa { get; set; }
		public decimal Folio { get; set; }
		public string TelefonoSolicitante { get; set; }
		public string Observaciones { get; set; }
		public double AvaluoFiscal { get; set; }
		public decimal UnidadPesoID { get; set; }
		public int CorrelativoEntrega { get; set; }
		public decimal AutorizanteDocumentoID { get; set; }
		public bool EnviadoRegistro { get; set; }
		public bool ParaEnvio { get; set; }
		public bool Enviado { get; set; }
		public bool TieneListadoRI { get; set; }
		public decimal NroContrato { get; set; }
		public string Origen { get; set; }
		public decimal LugarDocumentoID { get; set; }
		public string UrlPDFFolio { get; set; }
		public int AnoCorrelativo { get; set; }
		public decimal CorrelativoOld { get; set; }
		public bool EnviarRegistro { get; set; }
		public bool TieneListadoAlz { get; set; }
		public bool AnotacionMeraTenencia { get; set; }
		public bool AnotacionProhibiciones { get; set; }
		public bool DeclaracionJurada { get; set; }
		public bool MandatoCiaSeguro { get; set; }
		public bool CopiaContratoLeasing { get; set; }
		public bool EntregaAlzamiento { get; set; }
		public DateTime? FechaCargaLimbo { get; set; }
		public bool ReenviarLimbo { get; set; }
		public decimal ObservacionLimboID { get; set; }
		public DateTime? FechaCargaBanco { get; set; }
		public string ObservacionesLimbo { get; set; }
		public DateTime? FechaAlerta2 { get; set; }
		public string RutCliente { get; set; }
		public string RazonSocialCliente { get; set; }
		public double ValorInscripcion { get; set; }
		public double ValorTramita { get; set; }
		public string RazonSocialProveedor { get; set; }
		public bool Limitaciones { get; set; }
		public bool ZonaFranca { get; set; }
		public bool MeraTenencia { get; set; }
		public string PreTransRUTProActual { get; set; }
		public short CodigoMoneda { get; set; }
		public string ValorCuota { get; set; }
		public string Iva { get; set; }
		public string EstadoOperacion { get; set; }
		public string DescripcionBien { get; set; }
		public string DVPPU { get; set; }
		public bool RechazoPrimera { get; set; }
		public string DomicilioCliente { get; set; }
		public string NumDomicilioCliente { get; set; }
		public string ComplementoCliente { get; set; }
		public string ComunaCliente { get; set; }
		public string CiudadCliente { get; set; }
		public bool EscrituraAlzamiento { get; set; }
		public bool Factura { get; set; }
		public bool ActaEntreagaVoluntaria { get; set; }
		public bool OrdenJudicial { get; set; }
		public double ValorNotaria { get; set; }
		public double ValorTransferencia { get; set; }
		public double ValorCAV { get; set; }
		public double ValorAlzamientoTransferencia { get; set; }
		public double ValorAnotacion { get; set; }
		public double ValorDespachoCorreo { get; set; }
		public DateTime? FechaEntrega { get; set; }
		public double ValorAlzamiento { get; set; }
		public DateTime? VctoCtoL { get; set; }
		public string CodigoCliente { get; set; }
		public string Ejecutivo { get; set; }
		public string Sucursal { get; set; }
		public bool TieneTAG { get; set; }
		public string ObservacionEntrega { get; set; }
		public string CertificadoHomoligacion { get; set; }
		public short CorrelativoCarga { get; set; }
		public decimal ImpuestoTGR { get; set; }
		public DateTime? FechaIngresoTAG { get; set; }
		public decimal ValorTAG { get; set; }
		public decimal ValorConsultaPDF { get; set; }
		public decimal ValorReingreso { get; set; }
		public DateTime? FechaSolicitudMT { get; set; }
		public decimal NroSolicitudMT { get; set; }
		public string OficinaSolicitudMT { get; set; }
		public string CodigoDespachoCorreo { get; set; }
		public bool EstadoEntrega { get; set; }
		public short ObservacionCorreoID { get; set; }
		public bool ImprimirParaDespachoCorreo { get; set; }
		public string Contacto { get; set; }
		public string TelefonoContacto { get; set; }
		public bool chkFotocopiaLegalizada { get; set; }
		public bool chkFotocopiaLegalizadaDI { get; set; }
		public bool chk1U { get; set; }
		public bool chk2U { get; set; }
		public bool chk3U { get; set; }
		public bool chk4U { get; set; }
		public bool chkSolicitudPrimera { get; set; }
		public bool chkCertificadoLeasing { get; set; }
		public bool chkCertificadoCombustibles { get; set; }
		public bool chkContratoTelevia { get; set; }
		public bool chkConvenioPAC { get; set; }
		public bool chkDispositivoTelevia { get; set; }
		public bool chkContratoLeasing { get; set; }
		public bool chkPadron { get; set; }
		public bool SolicitudTransferencia { get; set; }
		public bool SolicitudAlzamiento { get; set; }
		public bool FacturaTransferencia { get; set; }
		public bool CertificadoAnotacionesVigentes { get; set; }
		public bool PagoF23TGR { get; set; }
		public bool BoletaNotaria { get; set; }
		public bool CesionDerechos { get; set; }
		public string ObservacionDespachoCorreo { get; set; }
		public DateTime? FechaEntregaCorreo { get; set; }
		public DateTime? FechaEntSolicitudMT { get; set; }
		public decimal NroEntSolicitudMT { get; set; }
		public string OficinaEntSolicitudMT { get; set; }
		public string EmailContacto { get; set; }
		public bool PendienteContrato { get; set; }
		public bool PendienteAnotacionMeratenencia { get; set; }
		public bool DespachoExterno { get; set; }
		public string NombreQuienRecibe { get; set; }
		public bool FotocopiaLegalizadaNotaCredito { get; set; }
		public bool FotocopiaLegalizadaCert5594 { get; set; }
		public int AnoProceso { get; set; }
		public short EstadoEntregaCorreo { get; set; }
		public bool SolicitaDespacho { get; set; }
		public bool FotocopiaRUTBanco { get; set; }
		public string ContactoTransferencia { get; set; }
		public string EMailContactoTransferencia { get; set; }
		public string TelefonoContactoTransferencia { get; set; }
		public bool chkDetalleValores { get; set; }
		public bool chkCompraventa { get; set; }
		public bool chkOrdenJudicial { get; set; }
		public DateTime? FechaRecepcionPadron { get; set; }
		public bool chkRut { get; set; }
		public bool chkEC { get; set; }
		public double NetoFactura { get; set; }
		public double IVAFactura { get; set; }
		public string CodigoCIT { get; set; }
		public bool InformativoSeguro { get; set; }
		public bool F88 { get; set; }
		public decimal ValorF88 { get; set; }
		public bool ImprimirParaDespachoPadron { get; set; }
		public bool SolicitaDespachoPadron { get; set; }
		public string CodigoDespachoPadron { get; set; }
		public bool EstadoEntregaPadron { get; set; }
		public int ObservacionDespachoPadronID { get; set; }
		public DateTime? FechaEntregaPadron { get; set; }
		public DateTime? FechaRecepcionPadronP { get; set; }
		public string ObservacionDespachoPadron { get; set; }
		public bool chkPlacas { get; set; }
		public decimal NroBienes { get; set; }
		public DateTime? FechaPadron { get; set; }
		public int AnoFiltro { get; set; }
		public bool chkCI { get; set; }
		public string RutRepresentanteLegal { get; set; }
		public string NombreRepresentanteLegal { get; set; }
		public bool Multas { get; set; }
		public decimal NroValija { get; set; }
		public bool EntregaValija { get; set; }
		public DateTime? FechaEntregaValija { get; set; }
		public DateTime? FechaAceptado { get; set; }
		public string SLA { get; set; }
	}


}

