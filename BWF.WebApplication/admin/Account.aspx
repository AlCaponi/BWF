<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="admin_Account" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Account</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.min.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container">
                <%if(!string.IsNullOrEmpty(Err)){%>
                <div class="alert alert-danger" role="alert">
                        <%=Server.HtmlEncode(Err)%>
                    </div>
                <%}%>
                <%if (Ok) {%>
                <div class="alert alert-success" role="alert">
                        Das Passwort wurde geändert
                    </div>
                <%}%>
                <form method="post" action="Account.aspx">
			        <table>
			            <tr>
			                <td style="width:200px;">Passwort</td>
                            <td><input class="form-control" type="password" name="pwd1" placeholder="Passwort (6 Zeichen)" /></td>
			            </tr>
			            <tr>
			                <td>Passwort bestätigen</td>
                            <td><input class="form-control" type="password" name="pwd2" placeholder="Passwort (6 Zeichen)" /></td>
			            </tr>
			        </table>
			        <input type="submit" class="btn btn-default btn-primary" value="Speichern" />
                    <input type="button" onclick="top.location.href='Admin.aspx';" class="btn btn-default" value="Zurück" />
			    </form>
			</div>
		</div>
	</body>
</html>
