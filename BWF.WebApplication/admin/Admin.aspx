<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="_Admin" %>

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
                <form method="get" action="Account.aspx" class="text-right">
			        <input type="submit" class="btn btn-default btn-primary" value="Passwort ändern" />
			        <input type="submit" name="logoff" class="btn btn-danger" value="Abmelden" />
			    </form>
			    <table>
			        <tr>
			            <th>Umfrage</th>
			            <th>Datum</th>
			            <th>Teilnehmer</th>
			            <th>Beantwortet</th>
			            <th>Erfolgreich</th>
			            <th>Optionen</th>
			        </tr>
                    <%if (Liste.Count == 0)
                      {
                          %>
                          <tr>
                            <td colspan="6">
                                <i>Keine Umfrage vorhanden</i>
                            </td>
                          </tr>
                          <%
                      }%>
                    <%foreach (Base.Anlass A in Liste){%>
			        <tr>
			            <td><a href="Details.aspx?ID=<%=A.AnlassID.ToString()%>"><%=Server.HtmlEncode(A.Name)%></a></td>
			            <td><%=A.Datum.ToString("dd.MM.yyyy HH:mm")%></td>
			            <td><%=A.Soldaten.Length%></td>
			            <td><%=A.Beantwortet%></td>
			            <td><%=A.Erfolg%></td>
			            <td>
			                <a href="Details.aspx?ID=<%=A.AnlassID.ToString()%>" class="glyphicon glyphicon-search"></a>
			                <span>&nbsp;&nbsp;</span>
			                <a href="Export.aspx?ID=<%=A.AnlassID.ToString()%>" class="glyphicon glyphicon-save"></a>
			                <span>&nbsp;&nbsp;</span>
			                <a href="Delete.aspx?ID=<%=A.AnlassID.ToString()%>" class="glyphicon glyphicon-trash"></a>
			            </td>
			        </tr>
                    <%}%>
			    </table>
                <!--
                QR Code Test:<br />
                <img src="http://chart.apis.google.com/chart?cht=qr&chs=100x100&chl=<%=UmfrageUrl()%>&chld=H|0" alt="QR-Code" />
                -->
                
			    <br /><br />
			    <form method="get" action="New.aspx">
			        <input type="submit" class="btn btn-default btn-primary" value="Neue Umfrage" />
			    </form>
			</div>
		</div>
	</body>
</html>
