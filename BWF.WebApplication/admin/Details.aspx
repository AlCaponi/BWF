<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="_Details" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Details</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.min.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container">
			    <h1><%=Server.HtmlEncode(A.Name)%></h1>
			    <table>
			        <tr>
			            <th>Teilnehmer</th>
			            <th>Beantwortet</th>
			            <th>Problematisch</th>
			        </tr>
                    <%
                        foreach (Base.Soldat S in A.Soldaten)
                        {
                        %>
			        <tr>
			            <td><a href="User.aspx?ID=<%=S.ID%>"><%=Server.HtmlEncode(S.Vorname)%> <%=Server.HtmlEncode(S.Nachname)%></a></td>
			            <td class="<%=S.Beantwortet ? "OK" : "fail"%>"><%=S.Beantwortet ? "Ja" : "Nein"%></td>
			            <td class="<%=S.Beantwortet && !S.Problematic ? "OK" : "fail"%>"><%=S.Beantwortet && !S.Problematic ? "Nein" : "Ja"%></td>
			        </tr>
                    <%}%>
			    </table>
			    <br /><br />
			    <form method="get" action="Admin.aspx">
			        <input type="submit" class="btn btn-default" value="&lt;&lt; Zurück" />
			    </form>
			</div>
		</div>
	</body>
</html>
