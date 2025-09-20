<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sign-in.aspx.cs" Inherits="Extranet.sing_in" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<!-- Required meta tags -->
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
	<!-- End Required meta tags -->
	<!-- Begin SEO tag -->
	<title>EXTRANET TRAMITA.CL</title>
	<meta property="og:title" content="Sign In">
	<meta name="author" content="Beni Arisandi">
	<meta property="og:locale" content="en_US">
	<!-- End SEO tag -->
	<!-- Favicons -->
	<link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/apple-touch-icon.png">
	<link rel="shortcut icon" href="assets/favicon.ico">
	<!-- BEGIN BASE STYLES -->
	<link rel="stylesheet" href="assets/vendor/bootstrap/css/bootstrap.min.css">
	<link rel="stylesheet" href="assets/vendor/font-awesome/css/fontawesome-all.min.css">
	<!-- END BASE STYLES -->
	<!-- BEGIN THEME STYLES -->
	<link rel="stylesheet" href="assets/stylesheets/main.min.css">
	<link rel="stylesheet" href="assets/stylesheets/custom.css">
	<!-- END THEME STYLES -->
</head>
<body id="page-top" onload="onLoad();">
	<!-- .auth -->
	<main class="auth">
		<header id="auth-header" class="auth-header" style="background-image: url(assets/images/illustration/img-1.png);">
			<h1>TRAMITA - SCT
          
        </h1>
			<p>
				&nbsp;
       
			</p>
		</header>
		<!-- form -->
		<form id="form1" class="auth-form" runat="server">

			<div id="marcoLogin" class="marco" runat="server">
				<!-- .form-group -->
				<div class="form-group">
					<div class="form-label-group">
						<asp:TextBox ID="txtUser" CssClass="form-control" placeholder="Username" required="" autofocus="" Text="10080671-1" runat="server" />
						<label for="inputUser">RUT</label>
					</div>
				</div>
				<!-- /.form-group -->
				<!-- .form-group -->
				<div class="form-group">
					<div class="form-label-group">
						<asp:TextBox ID="txtPassword" class="form-control" placeholder="Password" Text="BSAN##$$bsansole24" required="" runat="server" />
						<label for="inputPassword">Clave</label>
					</div>
				</div>
				<!-- /.form-group -->
				<!-- .form-group -->
				<div class="form-group">
					<asp:Button ID="btnIngresar" class="btn btn-lg btn-primary btn-block" Text="Ingresar" runat="server" OnClick="btnIngresar_Click" />
				</div>
				<!-- /.form-group -->
				<!-- .form-group -->
				<!-- /.form-group -->
				<!-- recovery links -->
				<div class="text-center pt-3">
					<a href="auth-recovery-username.html" class="link">Olvidó su Clave?</a>
				</div>
				<!-- /recovery links -->
			</div>



			<div id="marcoValidacion" class="marco" visible="false" runat="server">
				<!-- .form-group -->
				<div class="form-group">
					<div class="form-label-group">
						<asp:TextBox ID="txtAutorizacion" CssClass="form-control" placeholder="Código Autorización" required="" autofocus="" Text="10080671-1" runat="server" />
						<label for="inputUser">Código Autorización</label>
					</div>
				</div>
				<!-- /.form-group -->
				<!-- .form-group -->
				<div class="form-group">
					<asp:Button ID="btnValidarAcceso" Text="Validar Acceso" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnValidarAcceso_Click" runat="server" />
				</div>
				<!-- /.form-group -->
				<!-- .form-group -->
				<!-- /.form-group -->
				<!-- recovery links -->
				<div class="text-center pt-3">
					<a href="auth-recovery-username.html" class="link">Olvidó su Clave?</a>
				</div>
				<!-- /recovery links -->
			</div>




			<!-- End Login -->
		</form>
		<!-- /.auth-form -->
		<!-- copyright -->
		<footer class="auth-footer">
			© 2025 Todos los derechos reservados
     
		</footer>
	</main>
	<!-- /.auth -->
	<!-- BEGIN PLUGINS JS -->
	<script src="assets/vendor/particles.js/particles.min.js"></script>
	<script src="assets/vendor/jquery/jquery.min.js"></script>
	<script src="assets/vendor/bootstrap/js/bootstrap.min.js"></script>
	<!-- END BASE JS -->
	<script>
		/* particlesJS.load(@dom-id, @path-json, @callback (optional)); */
		particlesJS.load('auth-header', 'assets/javascript/particles.json');
    </script>
	<!-- END PLUGINS JS -->

	<script>
		window.dataLayer = window.dataLayer || [];

		function Login() {
			var pattern = new RegExp(/(?=^.{8,}$)(?=.*[A-Z])(?=.*\d)[^0-9](?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Za-z]).*.*[a-z].*$/);

			if (pattern.test($("#txtClave").val())) {
				str = $("#txtUsuario").val();
				str = str.replace(/\./g, '');
				$("#txtUsuario").val(str);
				document.forms["login_form"].action = "home.aspx";
				document.forms["login_form"].submit();
			}
			else {
				alert('Clave Incorrecta')
			}
		}


	</script>
</body>
</html>
