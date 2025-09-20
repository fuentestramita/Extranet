<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="consulta-comercial.aspx.cs" Inherits="Extranet.consulta_comercial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script>
		function mostrarFila(obj) {
			if (document.getElementById(obj).className == "cssHide") {
				document.getElementById(obj).className = "cssDisplayFlex";
			}
			else {
				document.getElementById(obj).className = "cssHide";
			}
		}

		function validaBuscar() {

			if ($("#txtPPU").val() == "" && $("#txtNroOperacion").val() == "" && $("#txtRutCliente").val() == "") {
				alert("Debe ingresar al menos un dato para buscar");
				return false;
			}
			return true;
		}


		function buscaPorRUT(RUT) {
			$("#txtPPU").val("");
			$("#txtNroOperacion").val("");
			$("#txtRutCliente").val(RUT);
			$("#btnBuscar").click();
		}

		function buscaPorOperacion(Operacion) {
			$("#txtPPU").val("");
			$("#txtNroOperacion").val(Operacion);
			$("#txtRutCliente").val("");
			$("#btnBuscar").click();
		}


	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<section class="card card-fluid">
		<div class="card-body">
			<nav class="navbar navbar-expand-lg navbar-dark bg-dark mb-3">
				<!-- toggle menu -->
				<button class="hamburger hamburger hamburger-squeeze js-hamburger d-lg-none" type="button" data-toggle="collapse" data-target="#navbarColor1" aria-controls="navbarColor1" aria-expanded="false" aria-label="Toggle navigation">
					<span class="hamburger-box">
						<span class="hamburger-inner"></span>
					</span>
				</button>
				<div class="collapse navbar-collapse" id="navbarColor1">
					<asp:TextBox ID="txtPPU" CssClass="form-control size-buscador" placeholder="P.P.U." autofocus="" Text="" runat="server" />
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:TextBox ID="txtNroOperacion" CssClass="form-control size-buscador" placeholder="Número de Operación" autofocus="" Text="" runat="server" />
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:TextBox ID="txtRutCliente" CssClass="form-control size-buscador" placeholder="RUT Cliente" autofocus="" Text="" runat="server" />
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:Button ID="btnBuscar" Text="Buscar" CssClass="btn btn-primary" OnClientClick="return validaBuscar();" OnClick="btnBuscar_Click" runat="server" />
				</div>
			</nav>
		</div>
	</section>

	<section class="card card-fluid">
		<div class="card-body">
			<div class="table-responsive">
				<asp:GridView ID="grvDatos" CssClass="cssGridView" GridLines="None" AutoGenerateColumns="false" PageIndex="0" PageSize="120" AllowPaging="true" ShowFooter="false" ShowHeader="false" runat="server" RowHeaderColumn="0" OnPageIndexChanging="grvDatos_PageIndexChanging" OnRowDataBound="grvDatos_RowDataBound" OnDataBinding="grvDatos_DataBinding" >
					<PagerStyle CssClass="cssPaginamiento" BorderStyle="None" />
					<RowStyle BorderStyle="None" />
					<AlternatingRowStyle BorderStyle="None" />
					<Columns>
						<asp:TemplateField>
							<ItemTemplate>

								<div class="card">
									<div class="card-header text-center">
										<asp:Label ID="Label1" Text='<%# Bind("TipoSolicitud") %>' runat="server"></asp:Label>
									</div>
									<div class="card-body">
										<table border="0" cellspacing="0px" cellpadding="3px" width="100%">
											<tr>
												<td colspan="6"></td>
											</tr>
											<tr>
												<td style="padding: 5px;">ESTADO</td>
												<td style="padding: 5px;">:</td>
												<td style="padding: 5px;">
													<asp:Label ID="Label2" Text='<%# Bind("Estado") %>' runat="server"></asp:Label>
												</td>
												<td style="padding-left: 15px; width: 180px">PPU</td>
												<td style="padding: 5px; width: 10px">:</td>
												<td style="padding: 5px; width: 330px">
													<asp:Label ID="PPU" Text='<%# Bind("PPU") %>' runat="server"></asp:Label>
												</td>
												<td style="padding-left: 15px; width: 120px"></td>
												<td style="padding: 5px; width: 10px"></td>
												<td style="padding: 5px;width: 330px"></td>
											</tr>
											<tr>
												<td style="padding: 5px; width: 150px">FECHA RECEPCIÓN</td>
												<td style="padding: 5px; width: 10px">:</td>
												<td style="padding: 5px; width: 100px">
													<asp:Label ID="Label11" Text='<%# Eval("TipoSolicitud").ToString().ToUpper()!="DELIVERY"?Eval("FechaRecepcionBanco"):Eval("FechaIngresoRNVM") %>' runat="server"></asp:Label>
												</td>
												<td style="padding-left: 15px;">NRO.OPERACIÓN</td>
												<td style="padding: 5px; width: 10px">:</td>
												<td style="padding: 5px;">
													<a href="#" class="cssLabel" onclick="buscaPorOperacion('<%# Eval("NroOperacion") %>');return false;">
														<asp:Label ID="Label12" Text='<%# Bind("NroOperacion") %>' runat="server"></asp:Label>
													</a>
												</td>
											</tr>
											<tr>
												<td style="padding: 5px;">
													<asp:Label ID="Label3" Text='FECHA INGRESO' runat="server" CssClass='<%# Eval("TipoSolicitud").ToString() != "DELIVERY" ? "cssDisplayFlex" : "cssHide" %>'></asp:Label></td>
												<td style="padding: 5px; width: 10px">
													<asp:Label ID="Label5" Text=':' runat="server" CssClass='<%# Eval("TipoSolicitud").ToString() != "DELIVERY" ? "cssDisplayFlex" : "cssHide" %>'></asp:Label></td>
												<td style="padding: 5px;">
													<asp:Label ID="Label13" Text='<%# Eval("FechaIngresoRNVM")' runat="server" CssClass='<%# Eval("TipoSolicitud").ToString() == "DELIVERY" ? "cssDisplayFlex" : "cssHide" %>' runat="server"></asp:Label>
												</td>
												<td style="padding-left: 15px;">RUT CLIENTE</td>
												<td style="padding: 5px;">:</td>
												<td style="padding: 5px;">
													<a href="#" class="cssLabel" onclick="buscaPorRUT('<%# Eval("RutCliente") %>');return false;">
														<asp:Label ID="Label14" Text='<%# Eval("RutCliente") %>' runat="server"></asp:Label>
													</a>
												</td>
											</tr>
											<tr>
												<td style="padding: 5px;"><%# Eval("TipoSolicitudID").ToString() != "M" ? "FECHA ENTREGA" : "FECHA ANOTACIÓN" %></td>
												<td style="padding: 5px; width: 10px">:</td>
												<td style="padding: 5px;">
													<asp:Label ID="Label15" Text='<%# Bind("FechaEntrega") %>' runat="server"></asp:Label>
												</td>
												<td style="padding-left: 15px;">NOMBRE/RAZÓN SOCIAL</td>
												<td style="padding: 5px; width: 10px">:</td>
												<td style="padding: 5px;">
													<asp:Label ID="Label16" Text='<%# Bind("RazonSocialCliente") %>' runat="server"></asp:Label>
												</td>
											</tr>
											<tr class='<%# ((Eval("TipoSolicitudID").ToString() == "P" || Eval("TipoSolicitudID").ToString() == "T") && (Eval("NroValija").ToString()!="" && Eval("NroValija").ToString()!="0")) ? "" : "cssHide" %>'>
												<td style="padding: 5px;">NRO.VALIJA
												</td>
												<td style="padding: 5px;">:</td>
												<td style="padding: 5px;">
													<asp:Label ID="Label7" Text='<%# Bind("NroValija") %>' runat="server"></asp:Label>
												</td>
												<td style="padding-left: 15px;">SUCURSAL</td>
												<td style="padding: 5px;">:</td>
												<td style="padding: 5px;">
													<asp:Label ID="Label9" Text='<%# Eval("Sucursal") %>' runat="server"></asp:Label>
												</td>
												<td style="padding-left: 15px;">EJCUTIVO</td>
												<td style="padding: 5px;">:</td>
												<td style="padding: 5px;">
													<asp:Label ID="Label10" Text='<%# Eval("Ejecutivo") %>' runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td style="padding: 5px;" colspan="9">
													<div onclick="<%# "mostrarFila('Informacion" + Container.DataItemIndex + "'); return false;" %>" class="cssDisplayFlex">
														<i class="fas fa-info-circle Azul" title="Observaciones"></i>&nbsp;&nbsp;

														<div id="Informacion<%# Container.DataItemIndex %>" class="cssHide">
															<asp:Label ID="Label4" Text='<%# Bind("Comentario") %>' CssClass="text-uppercase" runat="server"></asp:Label>&nbsp;&nbsp;
															<a href="#" onclick="<%# "mostrarFila('Informacion" + Container.DataItemIndex + "); return false;" %>"><i class="far fa-times-circle Rojo"></i></i></a>
														</div>
													</div>
												</td>
											</tr>


											<tr>
												<td style="text-align: center; padding: 5px;" class="Size-24" colspan="9">
													<%# GetEstadoIcono(Eval("Estado")) %>
													<%#GetPDF(Eval("PPU"), null, Eval("TipoSolicitudID"),  Eval("TipoSolicitud"), null, null)%>
													<%#GetPDF(null, Eval("NroOperacion"), Eval("TipoSolicitudID"),  Eval("TipoSolicitud"), null, null)%>
													<%#GetPDF(null, null, Eval("TipoSolicitudID"), Eval("TipoSolicitud"), Eval("CodigoDespachoCorreo"), null)%>
													<%#GetPDF(null, null, Eval("TipoSolicitudID"), Eval("TipoSolicitud"), null, Eval("CodigoDespachoCorreoMaestroDocumentos"))%>

												</td>
											</tr>
										</table>
									</div>
									<br />
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>
		</div>
	</section>
</asp:Content>
