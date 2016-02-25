<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>ZSO Emme - Umfrage neue Soldaten</title>
		<script type="text/javascript" src="js/jquery.js"></script>
		<script type="text/javascript" src="js/bootstrap.min.js"></script>
		<link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">

			<div class="container login-form">
                <div class="img-responsive">
                        <img src="images/Logo_ZSOEMME.png" alt="Logo" class="img-responsive" />
                </div>
				<form method="post" action="/" runat="server">
					<div class="form-group col-md-12">
						<label for="svnr">Sozialversicherungsnummer</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="svnr" name="svnr" placeholder="Sozialversicherungsnummer" />
					</div>
					<label class="gray">* Bitte die SV Nummer aus dem Begleitbrief eingeben.</label>
                    <br />
                    <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_OnClick"/>
				    <asp:Label runat="server" ID="lblErrorMessage" CssClass="label label-danger" Visible="false" />
                </form>
			</div>
		</div>
	</body>
</html>
