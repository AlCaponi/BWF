<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="_Delete" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Administration</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.min.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container">
                <h1>Umfrage löschen</h1>
                Sind Sie sicher, dass sie die folgende Umfrage löschen wollen?<br />
                <br />

                <br />

                Dies hat zur folge:
                <ul>
                    <li>Die Umfrage wird entfernt</li>
                    <li>Alle gespeicherten Antworten werden entfernt</li>
                    <li>Alle mit der Umfrage verbundenen Soldaten werden entfernt</li>
                    <li>Die Liste mit den Antworten kann nicht mehr länger heruntergeladen werden</li>
                </ul>
			    <form method="post" action="Delete.aspx">
                    <input type="hidden" name="ID" value="<%=Request["ID"]%>" />
                    <input type="hidden" name="confirm" value="1" />
			        <input type="submit" class="btn btn-default btn-danger" value="Umfrage löschen" />
			    </form>
                <br />
                <br />
                <br />
                <br />
                <br />
			    <form method="get" action="Admin.aspx">
			        <input type="submit" class="btn btn-default btn-primary" value="Zurück" />
			    </form>
			</div>
		</div>
	</body>
</html>
