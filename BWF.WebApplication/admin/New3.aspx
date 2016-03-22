<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New3.aspx.cs" Inherits="_New3" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Neue Umfrage</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container login-form">
                <h1>Umfrage erstellt</h1>
                Die Umfrage wurde erfolgreich erstellt
				<form method="get" action="Admin.aspx">
					<div class="form-group">
						<label for="log">Log</label><br />
						<textarea class="form-control" name="log" id="log" readonly="readonly" rows="20" cols="60"><%=Log%></textarea>
					</div>
				</form>
				<input type="button" value="Zur Übersicht" onclick="top.location.href='Admin.aspx'" class="btn btn-primary" /><br />
                <br />
                <%if (!string.IsNullOrEmpty(err)){ %>
                <div class="alert alert-danger" role="alert"><%=Server.HtmlEncode(err)%></div>
                <%}%>
			</div>
		</div>
	</body>
</html>
