<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="_Register" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Registrieren</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container login-form">
                <div class="img-responsive">
                    <img src="/images/Logo_ZSOEMME.png" alt="Logo" class="img-responsive" />
                </div>
				<form method="post" action="Register.aspx">
					<div class="form-group">
						<label for="uname">Benutzername</label>
						<input type="text" class="form-control" id="uname" name="uname" placeholder="Benutzername" />
					</div>
					<div class="form-group">
						<label for="upass">Passwort</label>
						<input type="password" class="form-control" id="upass" name="upass" placeholder="Passwort" />
					</div>
					<div class="form-group">
						<label for="master">Master Passwort</label>
						<input type="password" class="form-control" id="master" name="master" placeholder="Master Passwort" />
					</div>
					<input type="submit" value="Registrieren" class="btn btn-primary" />
				</form>
                <br />
                <%if (!string.IsNullOrEmpty(err))
                  { %>
                    <div class="alert alert-danger" role="alert"><%=err%></div>
                <%}%>
			</div>
		</div>
	</body>
</html>
