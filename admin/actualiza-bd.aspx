<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="actualiza-bd.aspx.cs" Inherits="Extranet.admin.actualiza_bd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="wrapper">
  <!-- .empty-state -->
  <section id="notfound-state" class="empty-state"  style="padding-top:105px;">
    <!-- .empty-state-container -->
    <div class="empty-state-container cajablanca">
      <h3 class="state-header"> Proceso de actualización de Datos </h3>
      <p class="state-description lead text-muted"> Ejecute este proceso para realizar la carga de los datos actualizados el día de ayer</p>
				<div class="marco">
					<div class="form-group">
						<asp:Button ID="btnActualizaDatos" Text="Actualizar Datos" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnActualizaDatos_Click" runat="server" />
					</div>
				</div>
      </div>
    <!-- /.empty-state-container -->
  </section>
  <!-- /.empty-state -->
</div>

</asp:Content>
